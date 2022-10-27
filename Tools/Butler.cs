//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Players;
using Skua.Core.Options;
using System.IO;

public class Follower
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public bool DontPreconfigure = true;
    public string OptionsStorage = "Butler";
    public List<IOption> Options = new()
    {
        new Option<string>("playerName", "Player Name", "Insert the name of the player to follow", ""),
        CoreBots.Instance.SkipOptions,
        new Option<bool>("lockedMaps", "Locked Zone Handling", "When the followed account goes in to a locked map, this function allows the Butler to follow that account.", true),
        new Option<ClassType>("classType", "Class Type", "This uses the farm or solo class set in [Options] > [CoreBots]", ClassType.Farm),
        new Option<bool>("copyWalk", "Copy Walk", "Set to true if you want to move to the same position of the player you follow.", false),
        new Option<int>("roomNumber", "Room Number", "Insert the room number which will be used when looking through Locked Zones.", 999999),
        new Option<bool>("rejectDrops", "Reject Drops", "Do you wish for the Butler to reject all drops? If false, your drop screen will fill up.", true),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        Butler(
            Bot.Config.Get<string>("playerName"),
            Bot.Config.Get<bool>("lockedMaps"),
            Bot.Config.Get<ClassType>("classType"),
            Bot.Config.Get<bool>("copyWalk"),
            Bot.Config.Get<int>("roomNumber"),
            Bot.Config.Get<bool>("rejectDrops")
        );

        Core.SetOptions(false);
    }

    public void Butler(string playerName, bool LockedMaps = true, ClassType classType = ClassType.Farm, bool CopyWalk = false, int roomNr = 1, bool rejectDrops = true)
    {
        // Double checking the playername and assigning it so all functions can read it
        if (playerName == "Insert Name" || String.IsNullOrEmpty(playerName))
            Core.Logger("No name was inserted, stopping the bot.", messageBox: true, stopBot: true);
        playerName = playerName.Trim().ToLower();
        this.playerName = playerName;

        // Assigning params to private objects.
        doLockedMaps = LockedMaps;
        doCopyWalk = CopyWalk;

        // Creating directory and file to communicate with the followed player.
        if (!Directory.Exists("options/Butler"))
            Directory.CreateDirectory("options/Butler");
        File.Create($"options/Butler/{Bot.Player.Username.ToLower()}~!{playerName}.txt");
        // Deleting old files
        if (Directory.Exists("options/FollowerJoe"))
            Directory.Delete("options/FollowerJoe", true);

        // Setting room number
        if (roomNr != 999999 && roomNr >= 1000)
        {
            Core.PrivateRooms = true;
            Core.PrivateRoomNumber = roomNr;
        }

        // Enabling listeners
        Bot.Events.MapChanged += MapNumberParses;
        Bot.Events.ScriptStopping += ScriptStopping;
        if (CopyWalk)
            Bot.Events.ExtensionPacketReceived += CopyWalkListener;

        // Equipping class
        Core.EquipClass(classType);

        // Toggling drops
        if (!rejectDrops)
            Bot.Drops.Stop();

        while (!Bot.ShouldExit)
        {
            // Try to go to the followed player
            if (!tryGoto(playerName))
            {
                // Do these things if that fails
                Core.Join("whitemap");
                Core.Logger($"Could not find {playerName}. Check if \"{playerName}\" is in the same server with you.", "tryGoto");
                Core.Logger($"The bot will now hibernate and try to /goto to {playerName} every 60 seconds.", "tryGoto");

                int min = 1;
                while (!Bot.ShouldExit)
                {
                    for (int t = 0; t < 60; t++)
                    {
                        Bot.Sleep(1000);
                        if (Bot.ShouldExit)
                            break;
                    }
                    if (tryGoto(playerName))
                    {
                        Core.Logger(playerName + " found!");
                        return;
                    }
                    min++;

                    if (min % 5 == 0)
                        Core.Logger($"The bot is has been hibernating for {min} minutes");
                }
            }

            // Attack any monster that is alive.
            if (!Bot.Combat.StopAttacking && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                Bot.Combat.Attack("*");

            Bot.Sleep(Core.ActionDelay);
        }
    }
    private string playerName = null;
    private bool doLockedMaps = true;
    private bool doCopyWalk = false;

    private bool tryGoto(string userName)
    {
        if (Bot.Map.PlayerExists(userName) && Bot.Map.TryGetPlayer(userName, out PlayerInfo playerObject) && playerObject.Cell == Bot.Player.Cell)
            return true;

        if (doLockedMaps)
            Bot.Events.ExtensionPacketReceived += LockedZoneListener;

        for (int i = 0; i < 3; i++)
        {
            if (!Bot.Map.PlayerExists(userName))
                Core.JumpWait();

            Bot.Player.Goto(userName);
            Bot.Sleep(1000);

            if (LockedZoneWarning)
                break;

            if (Bot.Map.PlayerExists(userName))
            {
                if (Bot.Map.TryGetPlayer(userName, out playerObject) && playerObject.Cell == Bot.Player.Cell)
                    Bot.Player.SetSpawnPoint();
                return true;
            }
        }

        if (doLockedMaps && LockedZoneWarning && !insideLockedMaps)
        {
            LockedZoneWarning = false;
            LockedMaps();
            Bot.Events.ExtensionPacketReceived -= LockedZoneListener;
            return true;
        }

        LockedZoneWarning = false;
        Bot.Events.ExtensionPacketReceived -= LockedZoneListener;
        return false;
    }
    private bool LockedZoneWarning = false;
    private bool insideLockedMaps = false;

    private void LockedZoneListener(dynamic packet)
    {
        string type = packet["params"].type;
        dynamic data = packet["params"].dataObj;

        if (type is not null and "str")
        {
            string cmd = data[0];
            switch (cmd)
            {
                case "warning":
                    string LockerZonePacket = Convert.ToString(packet);
                    if (LockerZonePacket.Contains("a Locked zone."))
                        LockedZoneWarning = true;
                    break;
            }
        }
    }

    private void LockedMaps()
    {
        if (File.Exists($"options/FollowerJoe/{playerName}.txt"))
        {
            string targetMap = File.ReadAllLines($"options/FollowerJoe/{playerName}.txt").FirstOrDefault();
            if (targetMap != null)
            {
                Core.Join(targetMap);
                if (Bot.Map.PlayerExists(playerName))
                    return;
            }
        }

        string[] NonMemMaps =
        {
            "tercessuinotlim",
            "doomvaultb",
            "doomvault",
            "shadowrealmpast",
            "shadowrealm",
            "battlegrounda",
            "battlegroundb",
            "battlegroundc",
            "battlegroundd",
            "battlegrounde",
            "battlegroundf",
            "confrontation",
            "darkoviaforest",
            "doomwood",
            "hollowdeep",
            "hyperium",
            "willowcreek"
        };
        string[] MemMaps =
        {
            "shadowlordpast",
            "binky",
            "superlowe"
        };

        int maptry = 1;
        int mapCount = Core.IsMember ? (NonMemMaps.Count() + MemMaps.Count()) : NonMemMaps.Count();
        Bot.Quests.UpdateQuest(3799);

        foreach (string map in NonMemMaps)
        {
            Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {playerName} in /{map}", "LockedZoneHandler");
            Core.Join(map);

            if (!Bot.Map.PlayerExists(playerName))
                continue;

            tryGoto(playerName);
            Core.Logger($"[{((maptry - 1).ToString().Length == 1 ? "0" : "")}{maptry - 1}/{mapCount}] Found {playerName} in /{map}", "LockedZoneHandler");

            switch (map.ToLower())
            {
                case "doomvault":
                    _killTheUltra("r26");
                    break;

                case "doomvaultb":
                    _killTheUltra("r5");
                    break;
            }
            Bot.Combat.Attack("*");
            return;
        }

        if (Core.IsMember)
        {
            foreach (string map in MemMaps)
            {
                Core.Logger($"[{(maptry.ToString().Length == 1 ? "0" : "")}{maptry++}/{mapCount}] Searching for {playerName} in /{map}", "LockedZoneHandler");
                Core.Join(map);

                if (!Bot.Map.PlayerExists(playerName))
                    continue;

                tryGoto(playerName);
                Core.Logger($"[{((maptry - 1).ToString().Length == 1 ? "0" : "")}{maptry - 1}/{mapCount}] Found {playerName} in /{map}", "LockedZoneHandler");

                switch (map.ToLower())
                {
                    case "binky":
                        _killTheUltra("binky");
                        break;
                }
                Bot.Combat.Attack("*");
                return;
            }
        }

        insideLockedMaps = true;
        if (tryGoto(playerName))
        {
            insideLockedMaps = false;
            return;
        }
        insideLockedMaps = false;

        Core.Join("whitemap");
        Core.Logger($"Could not find {playerName} in any of the maps within the LockedZoneHandler.", "LockedZoneHandler");
        Core.Logger($"The bot will now hibernate and try to /goto to {playerName} every 60 seconds", "LockedZoneHandler");

        int min = 1;
        while (!Bot.ShouldExit)
        {
            for (int t = 0; t < 60; t++)
            {
                Bot.Sleep(1000);
                if (Bot.ShouldExit)
                    break;
            }
            if (tryGoto(playerName))
            {
                Core.Logger(playerName + " found!");
                return;
            }
            min++;

            if (min % 5 == 0)
                Core.Logger($"The bot is has been hibernating for {min} minutes");
        }
        return;

        void _killTheUltra(string cell)
        {
            if (Bot.Player.Cell == cell && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
            {
                Monster Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                if (Target == null)
                {
                    Core.Logger("No monsters found", "KillUltra");
                    return;
                }
                Bot.Combat.Attack(Target.Name);
            }
        }
    }

    private async void MapNumberParses(string map)
    {
        if (String.IsNullOrEmpty(Bot.Map.FullName))
        {
            for (int a = 0; a < 10; a++)
            {
                if (!String.IsNullOrEmpty(Bot.Map.FullName))
                    break;
                await Task.Delay(Core.ActionDelay);
                if (a == 9)
                    return;
            }
        }
        if (!Int32.TryParse(Bot.Map.FullName.Split('-').Last(), out int mapNr) || map == prevRoom || !Bot.Map.PlayerExists(playerName))
            return;

        if (allocRoomNr == mapNr)
        {
            if (Core.PrivateRoomNumber != mapNr)
            {
                Core.Logger("Static room number detected. PrivateRoomNumber is now " + mapNr);
                Core.PrivateRoomNumber = mapNr;
            }
            Core.PrivateRooms = mapNr >= 1000;
            Bot.Events.MapChanged -= MapNumberParses;
            return;
        }

        prevRoom = map;
        allocRoomNr = mapNr;
    }
    private int allocRoomNr = 0;
    private string prevRoom = null;

    private void CopyWalkListener(dynamic packet)
    {
        string type = packet["params"].type;
        dynamic data = packet["params"].dataObj;
        if (type is not null and "str")
        {
            string cmd = data[0];
            switch (cmd)
            {
                //movement in the same cell || From server: %xt%uotls%-1%{playerName}%sp:8,tx:181,ty:358,strFrame:Bigger%
                //movement to another cell || From server: %xt%uotls%-1%{playerName}%mvts:-1,px:500,py:375,strPad:Left,bResting:false,mvtd:0,tx:0,ty:0,strFrame:Bigger%
                case "uotls":
                    string WalkPacket = Convert.ToString(packet);
                    if (!WalkPacket.Contains(playerName))
                        break;

                    string playerNameCell = "";
                    string playerNamePad = "";
                    int playerNameSpeed = 0;

                    foreach (string str in WalkPacket.Split(','))
                    {
                        string spl = "";
                        if (str.Contains(':'))
                            spl = str.Split(':')[1];
                        switch (str.Split(':')[0])
                        {
                            case "strFrame":
                                playerNameCell = spl;
                                break;
                            case "strPad":
                                playerNamePad = spl;
                                break;
                            case "sp":
                                playerNameSpeed = int.Parse(spl);
                                break;

                            case "tx":
                                playerNameX = int.Parse(spl);
                                break;
                            case "ty":
                                playerNameY = int.Parse(spl);
                                break;
                        }
                    }

                    // if (playerNameCell != "" && playerNamePad != "")
                    //     Core.Jump(playerNameCell, playerNamePad);

                    if (playerNameX != 0 && playerNameY != 0)
                        Bot.Flash.Call("walkTo", playerNameX, playerNameY, playerNameSpeed);
                    break;
            }
        }
    }
    private int playerNameX = 0;
    private int playerNameY = 0;

    private bool ScriptStopping(Exception e)
    {
        Bot.Events.MapChanged -= MapNumberParses;
        Bot.Events.ExtensionPacketReceived -= LockedZoneListener;
        Bot.Events.ExtensionPacketReceived -= CopyWalkListener;
        if (File.Exists($"options/Butler/{Bot.Player.Username.ToLower()}~!{playerName}.txt"))
            File.Create($"options/Butler/{Bot.Player.Username.ToLower()}~!{playerName}.txt");
        return true;
    }
}

//
//                                  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░                    
//                              ▓▓▓▓████████████████▓▓▓▓▒▒              
//                         ▓▓▓▓████░░░░░░░░░░░░░░░░██████▓▓            
//                      ▓▓████░░░░░░░░░░░░░░░░░░░░░░░░░░████          
//                   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██        
//                ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██      
//              ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██      
//             ▓▓██░░░░░░▓▓██░░  ░░░░░░░░░░░░░░░░░░░░▓▓██░░  ░░██    
//           ▓▓██░░░░░░░░██████░░░░░░░░░░░░░░░░░░░░░░██████░░░░░░██  
//          ▓▓██░░░░░░░░██████▓▓░░░░░░██░░░░██░░░░░░██████▓▓░░░░██  
//         ▓▓██▒▒░░░░░░░░▓▓████▓▓░░░░░░████████░░░░░░▓▓████▓▓░░░░░░██
//       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░██░░░░██░░░░░░░░░░░░░░░░░░░░██
//      ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//     ░░▓▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//      ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//    ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
//   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//    ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░father░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░i hunger░░░░░░░░░░░░░░░░░░░░░░░░░░██    
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//  ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//    ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//    ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//    ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//   ░░▓▓▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
//    ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██░░  
//     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
//      ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██      
//    ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██        
//      ▓▓████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██          
//        ▓▓▓▓████████░░░░░░░░░░░░░░░░░░░░░░░░████████░░          
//        ░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░    