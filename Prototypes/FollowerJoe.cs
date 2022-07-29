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
            // Bot.Events.PlayerAFK += LockedMap;
            Bot.Events.CellChanged += Jumper;
            Bot.Player.Goto((Bot.Config.Get<string>("playerName")));
            Bot.Sleep(2500);
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

    public void LockedMap(ScriptInterface bot)
    {
        //still not working sorry -- 🥔
        string[] Maps =
        {
        "banzai",
        "battlegrounda",
        "battlegroundb",
        "battlegroundc",
        "battlegroundd",
        "battlegrounde",
        "battlegroundf",
        "binky",
        "chaoslord",
        "darkoviaforest",
        "doomvault",
        "doomvaultb",
        "doomwoodforest",
        "hollowdeep",
        "hyperiumstarship",
        "ledgermayne",
        "moonyard",
        "moonyardb",
        "shadowlordpast",
        "shadowrealm",
        "shadowrealmpast",
        "superlowe",
        "tercessuinotlim",
        "lair",
        "willowcreek",
        "zephyrus"
        };

        while (!Bot.ShouldExit() && !Bot.Map.PlayerExists(Bot.Config.Get<string>("playerName")))
        {
            foreach (string Map in Maps)
            {
                switch (Map.ToLower())
                {
                    case "shadowattack":
                        Core.Join(Map);
                        Bot.Sleep(1500);
                        if (!Bot.Map.PlayerExists(Bot.Config.Get<string>("playerName")))
                            break;
                        Core.Jump("Boss", "Left");
                        Bot.Options.AttackWithoutTarget = true;
                        Bot.Player.Kill("Death");
                        Bot.Options.AttackWithoutTarget = false;
                        break;

                    case "Mobius":
                        Core.Join(Map);
                        Bot.Sleep(1500);
                        if (!Bot.Map.PlayerExists(Bot.Config.Get<string>("playerName")))
                            break;
                        Bot.Map.Reload();
                        Bot.Player.Kill("*");
                        break;

                    case "DoomVault":
                        {
                            Core.Join(Map);
                            Bot.Sleep(1500);
                            if (!Bot.Map.PlayerExists(Bot.Config.Get<string>("playerName")))
                                break;
                            if (Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell == "r26" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                            {
                                Bot.Quests.UpdateQuest(3004);
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
                            else Bot.Player.Attack("*");
                            break;
                        }

                    case "Doomvaultb":
                        {
                            Core.Join(Map);
                            Bot.Sleep(1500);
                            if (!Bot.Map.PlayerExists(Bot.Config.Get<string>("playerName")))
                                break;
                            if (Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell == "r5" && Bot.Monsters.CurrentMonsters.Count(m => m.Alive) > 0)
                            {
                                Bot.Quests.UpdateQuest(3008);

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
                            else Bot.Player.Attack("*");
                            break;
                        }
                }
            }
            Bot.Events.PlayerAFK -= LockedMap;
        }
    }

    public enum LockedZones
    {
        banzai,
        battlegrounda,
        battlegroundb,
        battlegroundc,
        battlegroundd,
        battlegrounde,
        battlegroundf,
        binky,
        chaoslord,
        darkoviaforest,
        doomvault,
        doomvaultb,
        doomwoodforest,
        hollowdeep,
        hyperiumstarship,
        ledgermayne,
        moonyard,
        moonyardb,
        shadowlordpast,
        shadowrealm,
        shadowrealmpast,
        superlowe,
        tercessuinotlim,
        lair,
        willowcreek,
        zephyrus
    }

    public enum IssueMaps
    {
        Mobius,
        Shadowattack
        //add maps here if they softlock.
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