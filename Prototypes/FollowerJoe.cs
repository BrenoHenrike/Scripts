//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Players;
using Skua.Core.Options;

// Bot by: 🥔 Tato 🥔

public class FollowerJoe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "Follower Joe";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("playerName", "Player Name", "Insert the name of the player to follow", "Insert Name"),
        new Option<bool>("skipSetup", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<bool>("LockedMaps", "Try Locked maps?", "If Following an acc thats doing scripts and can potentialy goto a locked map, swap this to true.", false),
        new Option<bool>("Solo?", "Use Solo Class?", "Set to true for Solo Class, False for Farm Class", false),
        new Option<string>("RoomNumber", "Room Number", "Insert the Room# of the Possible Locked Zone", "Room#"),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FollowJoe(Bot.Config.Get<bool>("LockedMaps"));

        Core.SetOptions(false);
    }

    public void FollowJoe(bool LockedMaps)
    {
        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        if (Bot.Config.Get<bool>("Solo?"))
            Core.EquipClass(ClassType.Solo);
        else Core.EquipClass(ClassType.Farm);

        playerName = Bot.Config.Get<string>("playerName");

        while (!Bot.ShouldExit)
        {
            Bot.Player.Goto((playerName));
            Bot.Sleep(2500);

            if (!Bot.Map.PlayerExists(playerName) && LockedMaps)
            {
                Core.Logger($"{playerName} not found in {Bot.Map.Name}, initiating LockedZoneHandler");
                Bot.Events.CellChanged -= Jumper;
                LockedMap();
            }

            if (Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                Bot.Combat.Attack("*");
            Bot.Sleep(Core.ActionDelay);
            Bot.Wait.ForCombatExit();
        }
        Bot.Events.PlayerAFK -= LockedMap;
        Bot.Events.CellChanged -= Jumper;
        Core.SetOptions(false);
    }
    private string playerName = null;

    private void Jumper(string map = null, string cell = null, string pad = null)
    {
        if (!Bot.Map.PlayerExists(playerName))
        {
            Core.Logger($"Teleporting to {playerName}");
            Core.JumpWait();
            Bot.Sleep(Core.ActionDelay);
            Bot.Player.Goto(playerName);
            Bot.Sleep(Core.ActionDelay);
        }

        if (Bot.Map.GetPlayer(playerName) != null && Bot.Map.GetPlayer(playerName).Cell != Bot.Player.Cell)
        {
            Core.Logger($"Cant Find {playerName}, Jumping");
            Core.JumpWait();
            Bot.Sleep(Core.ActionDelay);
            Bot.Map.Jump(Bot.Map.GetPlayer(playerName).Cell, Bot.Map.GetPlayer(playerName).Pad);
            Bot.Sleep(Core.ActionDelay);
        }
        Bot.Wait.ForCellChange(Bot.Map.GetPlayer(playerName).Cell);
        Bot.Events.CellChanged -= Jumper;
    }

    private void LockedMap()
    {
        string[] NonMemMaps =
        {
            "tercessuinotlim",
            "doomvault",
            "doomvaultb",
            "shadowrealmpast",
            "shadowrealm",
            "battlegrounda",
            "battlegroundb",
            "battlegroundc",
            "battlegroundd",
            "battlegrounde",
            "battlegroundf",
            "confrontation",
            "chaoslord",
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
        Core.Join("whitemap");
        Bot.Quests.UpdateQuest(3881);

        int mapCount = Core.IsMember ? (NonMemMaps.Count() + MemMaps.Count()) : NonMemMaps.Count();

        foreach (string Map in NonMemMaps)
        {
            Core.Logger($"Searching for {playerName} in /{Map} ({maptry}/{mapCount++})", "LockedZoneHandler");
            Core.Join(Map);

            //if (!Bot.Map.PlayerExists(playerName))
            //    continue;

            switch (Map.ToLower())
            {
                case "chaoslord":
                    Bot.Wait.ForCellChange(Bot.Player.Cell);
                    Bot.Sleep(2500);
                    if (Bot.Map.Name == "confrontaion")
                    {
                        Bot.Wait.ForCellChange(Bot.Player.Cell);
                        Bot.Sleep(2500);
                        if (Bot.Map.Name == "Shadowattack")
                        {
                            Bot.Wait.ForCellChange(Bot.Player.Cell);
                            Bot.Sleep(2500);
                        }
                        Core.Join("chaosLord");
                    }
                    break;

                case "shadowattack":
                    Core.Jump("Boss", "Left");
                    Bot.Options.AttackWithoutTarget = true;
                    Bot.Combat.Attack("Death");
                    Bot.Options.AttackWithoutTarget = false;
                    break;

                case "mobius":
                    Bot.Map.Reload();
                    Bot.Combat.Attack("*");
                    break;

                case "ledgermayne":
                    _killTheUltra("Boss");
                    break;

                case "doomvault":
                    _killTheUltra("r26");
                    break;

                case "doomvaultb":
                    _killTheUltra("r5");
                    break;
            }

            if (Bot.Map.PlayerExists(playerName))
            {
                Core.Logger($"{playerName} found in /{Map}, Jumper Re-Initialized");
                Bot.Events.CellChanged += Jumper;
                return;
            }
        }

        if (Core.IsMember)
        {
            foreach (string Map in MemMaps)
            {
                Core.Logger($"Searching for {playerName} in /{Map} ({maptry}/{mapCount++})", "LockedZoneHandler");
                Core.Join(Map);

                if (!Bot.Map.PlayerExists(playerName))
                    continue;

                switch (Map.ToLower())
                {
                    case "binky":
                        _killTheUltra("binky");
                        break;
                }
            }
        }
        Bot.Events.CellChanged += Jumper;

        void _killTheUltra(string cell)
        {
            if (Bot.Map.TryGetPlayer(playerName, out PlayerInfo player) && player.Cell == cell && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
            {
                Monster Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                if (Target == null)
                {
                    Core.Logger("No monsters found");
                    return;
                }
                Adv.KillUltra(Bot.Map.Name, player.Cell, player.Pad, Target.Name, log: true, forAuto: true);
            }
            else Bot.Combat.Attack("*");
        }
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