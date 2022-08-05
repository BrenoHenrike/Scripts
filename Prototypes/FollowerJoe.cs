//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Farm/AutoAttackKillUltra.cs
using RBot;
using RBot.Monsters;
using RBot.Options;

// Bot by: 🥔 Tato 🥔

public class FollowerJoe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();
    public AAKillUltra AAKA => new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "Follower Joe";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("playerName", "Player Name", "Insert the name of the player to follow", "Insert Name"),
        new Option<bool>("skipSetup", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<string>("RoomNumber", "Room Number", "Insert the Room# of the Possible Locked Zone", "Room#"),
    };


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        while (!Bot.ShouldExit())
        {
            Bot.Player.Goto((Bot.Config.Get<string>("playerName")));
            Bot.Sleep(2500);

            if (!Bot.Map.PlayerExists((Bot.Config.Get<string>("playerName"))))
            {
                Core.Logger($" {Bot.Config.Get<string>("playerName")} Not found in {Bot.Map.Name}, LockedZoneHandler Started");
                Bot.Events.CellChanged -= Jumper;
                LockedMap();
            }

            if (Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                Bot.Player.Attack("*");
            Bot.Sleep(Core.ActionDelay);
            Bot.Wait.ForCombatExit();
        }
        // Bot.Events.PlayerAFK -= LockedMap;
        Bot.Events.CellChanged -= Jumper;
        Core.SetOptions(false);
    }

    public void Jumper(ScriptInterface bot, string? map = null, string? cell = null, string? pad = null)
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
            Bot.Player.Jump(Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad);
            Bot.Sleep(Core.ActionDelay);
        }
        bot.Wait.ForCellChange(Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell);
        Bot.Events.CellChanged -= Jumper;
    }

    public void LockedMap()
    {
        string[] NonMemMaps =
        {
        "tercessuinotlim",
        "doomvault",
        "doomvaultb",
        "ledgermayne",
        "lair",
        "shadowrealmpast",
        "shadowrealm",
        "battlegrounda",
        "battlegroundb",
        "battlegroundc",
        "battlegroundd",
        "battlegrounde",
        "battlegroundf",
        "chaoslord",
        "darkoviaforest",
        "doomwoodforest",
        "hollowdeep",
        "hyperiumstarship",
        "moonyard",
        "moonyardb",
        "superlowe",
        "willowcreek",
        "zephyrus"
        };

        string[] MemMaps =
        {
        "shadowlordpast",
            "Binky"
        };

        int maptry = 1;
        Core.Join("whitemap");

        foreach (string Map in NonMemMaps)
        {
            switch (Map.ToLower())
            {
                default:
                    Bot.Quests.UpdateQuest(3881);
                    Bot.Quests.UpdateQuest(3008);
                    Bot.Quests.UpdateQuest(3004);
                    Core.Logger($"LockedZoneHandler Try {maptry++}, Joining Map: {Map}");
                    Core.JumpWait();
                    Bot.Sleep(Core.ActionDelay);
                    Core.Join(Map);
                    Bot.Wait.ForMapLoad(Map);

                    if (Bot.Map.PlayerExists((Bot.Config.Get<string>("playerName"))))
                    {
                        Core.Logger($"{Bot.Config.Get<string>("playerName")} found in Map: {Map}, Jumper Re-Initialized");
                        Bot.Events.CellChanged += Jumper;
                        return;
                    }

                    if (Bot.Map.Name == "ledgermayne")
                    {
                        if (Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell == "Boss" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                        {
                            Monster? Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                            if (Target == null)
                            {
                                Core.Logger("No monsters found");
                                return;
                            }
                            Core.SetOptions(disableClassSwap: true);
                            Core.Logger("Target: " + Target.Name);

                            Adv.KillUltra("ledgermayne", Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad, Target.Name, log: true, forAuto: true);
                        }
                        else Bot.Player.Attack("*");
                    }

                    if (Bot.Map.Name == "chaoslord")
                    {
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
                    }


                    if (Bot.Map.Name == "shadowattack" && Bot.Player.Cell == "cell")
                    {
                        Core.Jump("Boss", "Left");
                        Bot.Options.AttackWithoutTarget = true;
                        Bot.Player.Kill("Death");
                        Bot.Options.AttackWithoutTarget = false;
                    }


                    if (Bot.Map.Name == "Mobius" && Bot.Player.Cell == "cell")
                    {
                        Bot.Map.Reload();
                        Bot.Player.Kill("*");
                    }


                    if (Bot.Map.Name == "DoomVault")
                    {
                        if (Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell == "r26" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                        {
                            Monster? Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                            if (Target == null)
                            {
                                Core.Logger("No monsters found");
                                return;
                            }
                            Core.SetOptions(disableClassSwap: true);
                            Core.Logger("Target: " + Target.Name);

                            Adv.KillUltra("doomvault", Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad, Target.Name, log: true, forAuto: true);
                        }
                        else Bot.Player.Attack("*");
                    }


                    if (Bot.Map.Name == "Doomvaultb")
                    {
                        if (Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell == "r5" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                        {

                            Monster? Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                            if (Target == null)
                            {
                                Core.Logger("No monsters found");
                                return;
                            }
                            Core.SetOptions(disableClassSwap: true);
                            Core.Logger("Target: " + Target.Name);

                            Adv.KillUltra("doomvaultb", Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell, Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Pad, Target.Name, log: true, forAuto: true);
                        }
                        else Bot.Player.Attack("*");
                    }
                    break;
            }
        }
        foreach (string Map in MemMaps)
        {
            if (!Core.IsMember)
                return;
            switch (Map.ToLower())
            {
                case "Binky":
                    Core.Logger($"LockedZoneHandler Try {maptry++}, Joining Map: {Map}");
                    Core.JumpWait();
                    Bot.Sleep(Core.ActionDelay);
                    Core.Join(Map);
                    Bot.Wait.ForMapLoad(Map);

                    while (Bot.Map.PlayerExists((Bot.Config.Get<string>("playerName"))))
                    {
                        if (Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell == "Binky" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                        {
                            Monster? Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
                            if (Target == null)
                            {
                                Core.Logger("No monsters found");
                                return;
                            }
                            Core.SetOptions(disableClassSwap: true);
                            Core.Logger("Target: " + Target.Name);

                            Adv.KillUltra(Bot.Map.Name, Target.Cell, "Left", Target.Name, log: false, forAuto: true);
                        }
                        Bot.Player.Attack("*");
                    }
                    Bot.Events.CellChanged += Jumper;
                    break;
            }
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