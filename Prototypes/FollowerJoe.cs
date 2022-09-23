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

        FollowJoe(bot, Bot.Config.Get<bool>("LockedMaps"));

        Bot.Events.PlayerAFK -= LockedMap;
        Bot.Events.CellChanged -= Jumper;
        Core.SetOptions(false);
    }

    public void FollowJoe(IScriptInterface bot, bool LockedMaps)
    {
        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        if (Bot.Config.Get<bool>("Solo?"))
            Core.EquipClass(ClassType.Solo);
        else Core.EquipClass(ClassType.Farm);

        while (!Bot.ShouldExit)
        {
            Bot.Player.Goto((Bot.Config.Get<string>("playerName")));
            Bot.Sleep(2500);

            if (!Bot.Map.PlayerExists((Bot.Config.Get<string>("playerName"))))
            {
                Core.Logger($" {Bot.Config.Get<string>("playerName")} Not found in {Bot.Map.Name}, LockedZoneHandler Started");
                Bot.Events.CellChanged -= Jumper;
                if (LockedMaps)
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

    public void Jumper(string map = null, string cell = null, string pad = null)
    {
        if (!Bot.Map.PlayerExists((Bot.Config.Get<string>("playerName"))))
        {
            Core.Logger($"Teleporting to {Bot.Config.Get<string>("playerName")}");
            Core.JumpWait();
            Bot.Sleep(Core.ActionDelay);
            Bot.Player.Goto((Bot.Config.Get<string>("playerName")));
            Bot.Sleep(Core.ActionDelay);
        }

        if (Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))) != null && Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell != Bot.Player.Cell)
        {
            Core.Logger($"Cant Find {Bot.Config.Get<string>("playerName")}, Jumping");
            Core.JumpWait();
            Bot.Sleep(Core.ActionDelay);
            Bot.Map.Jump(Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad);
            Bot.Sleep(Core.ActionDelay);
        }
        Bot.Wait.ForCellChange(Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell);
        Bot.Events.CellChanged -= Jumper;
    }

    public void LockedMap()
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
        string playerName = Bot.Config.Get<string>("playerName");

        int maptry = 1;
        Core.Join("whitemap");

        foreach (string Map in NonMemMaps)
        {
            Bot.Quests.UpdateQuest(3881);
            Core.Logger($"Searching for {playerName} in {Map}", "LockedZoneHandler");
            Core.Join(Map);

            switch (Map.ToLower())
            {
                case "ledgermayne":
                    if (Bot.Map.TryGetPlayer(playerName, out PlayerInfo player1) && player1.Cell == "Boss" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                    {
                        Monster Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                        if (Target == null)
                        {
                            Core.Logger("No monsters found");
                            return;
                        }
                        Core.SetOptions(disableClassSwap: true);
                        Core.Logger("Target: " + Target.Name);

                        Adv.KillUltra("ledgermayne", Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad, Target.Name, log: true, forAuto: true);
                    }
                    else Bot.Combat.Attack("*");
                    break;

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
                    Bot.Kill.Monster("Death");
                    Bot.Options.AttackWithoutTarget = false;
                    break;

                case "mobius":
                    Bot.Map.Reload();
                    Bot.Kill.Monster("*");
                    break;

                case "doomvault":
                    if (Bot.Map.TryGetPlayer(playerName, out PlayerInfo player2) && player2.Cell == "r26" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                    {
                        Monster Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                        if (Target == null)
                        {
                            Core.Logger("No monsters found");
                            return;
                        }
                        Adv.KillUltra("doomvault", Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad, Target.Name, log: true, forAuto: true);
                    }
                    else Bot.Combat.Attack("*");
                    break;

                case "doomvaultb":
                    if (Bot.Map.TryGetPlayer(playerName, out PlayerInfo player3) && player3.Cell == "r5" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                    {
                        Monster Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                        if (Target == null)
                        {
                            Core.Logger("No monsters found");
                            return;
                        }
                        Adv.KillUltra("doomvaultb", Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad, Target.Name, log: true, forAuto: true);
                    }
                    else Bot.Combat.Attack("*");
                    break;
            }

            if (Bot.Map.PlayerExists(playerName))
            {
                Core.Logger($"{playerName} found in Mmp: {Map}, Jumper Re-Initialized");
                Bot.Events.CellChanged += Jumper;
                return;
            }
        }

        foreach (string Map in MemMaps)
        {
            if (!Core.IsMember)
                break;

            Core.Logger($"Try {maptry++}, Joining Map: {Map}", "LockedZoneHandler");
            Core.Join(Map);

            while (Bot.Map.TryGetPlayer((Bot.Config.Get<string>("playerName")), out PlayerInfo player))
            {
                if (player.Cell.ToLower() == "binky" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                {
                    Monster Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                    if (Target == null)
                    {
                        Core.Logger("No monsters found");
                        return;
                    }
                    Adv.KillUltra(Bot.Map.Name, Target.Cell, "Left", Target.Name, log: false, forAuto: true);
                }
                else Bot.Combat.Attack("*");
            }
            Bot.Events.CellChanged += Jumper;
            break;

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