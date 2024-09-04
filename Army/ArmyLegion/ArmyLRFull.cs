/*
name: Legion Revenant (Army)
description: Uses an army to to do the entirely of the legion revenant grind together
tags: legion, reventant, class, army, fealty
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Story/CruxShip.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Items;
using Skua.Core.Options;
using System.Linq;

public class ArmyLR
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreArmyLite Army = new();
    private CoreLegion Legion = new();
    private CoreLR CoreLR = new();
    private InfiniteLegionDC ILDC = new();
    private SeraphicWar_Story Seraph = new();
    private CruxShip CruxShip = new();
    private DarkWarLegionandNation DWLN = new();

    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLR";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public string[] LRMaterials =
    {
        "Exalted Crown",
        "Revenant's Spellscroll",
        "Conquest Wreath",
        "Legion Revenant"
    };

    public string[] LF1 =
    {
        "Aeacus Empowered",
        "Tethered Soul",
        "Darkened Essence",
        "Dracolich Contract"
    };

    public string[] LF2 =
    {
        "Grim Cohort Conquered",
        "Ancient Cohort Conquered",
        "Pirate Cohort Conquered",
        "Battleon Cohort Conquered",
        "Mirror Cohort Conquered",
        "Darkblood Cohort Conquered",
        "Vampire Cohort Conquered",
        "Spirit Cohort Conquered",
        "Dragon Cohort Conquered",
        "Doomwood Cohort Conquered",
    };

    public string[] LF3 =
    {
        "Hooded Legion Cowl",
        "Legion Token",
        "Dage's Favor",
        "Emblem of Dage",
        "Diamond Token of Dage",
        "Dark Token"
    };

    public string[] legionMedals =
    {
        "Legion Round 1 Medal",
        "Legion Round 2 Medal",
        "Legion Round 3 Medal",
        "Legion Round 4 Medal"
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(LRMaterials.Concat(LF1).Concat(LF2).Concat(LF3).Concat(legionMedals));
        Core.SetOptions();

        LR();

        Core.SetOptions(false);
    }

    public void LR()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Bot.Events.PlayerAFK += PlayerAFK;

        Bot.Options.SetFPS = 30;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.Logger("Step 1: Joining Legion");
        Legion.JoinLegion();
        Adv.BuyItem("underworld", 216, "Undead Champion");

        Core.Logger("Legion Round 4 Medal");
        Legion.LegionRound4Medal();

        Core.Logger("Seraphic War Questline");
        Seraph.SeraphicWar_Questline();

        Core.Logger("Crux Ship Storyline");
        CruxShip.StoryLine();

        Core.Logger("Dark War Nation/Legion Story");
        DWLN.DoBoth();

        Army.initArmy();
        Army.setLogName(OptionsStorage);

        Army.ClearLogFile();

        /* PREFARM ZONE */

        /* Step 1: Evil Rank 10 */
        Core.Logger("Step 1: Evil Rank 10");
        ArmyEvilGoodRepMax();

        /* Step 2: Hooded Legion Cowl funds and some change for enhancement costs */
        Core.Logger("Step 2: Hooded Legion Cowl funds and some change for enhancement costs");
        ArmyGoldFarm(5500000);

        /* Step 3: 3000 Dage Favor */
        Core.Logger("Step 3: 3000 Dage Favor");
        ArmyDageFavor();

        /* Step 4: 10 Emblem of Dage */
        Core.Logger("Step 4: 10 Emblem of Dage");
        ArmyEmblemOfDage(10);

        /* Step 5: 300 Diamond Token of Dage */
        Core.Logger("Step 5: 300 Diamond Token of Dage");
        ArmyDiamondTokenOfDage();

        /* Step 6: 600 Dark Token */
        Core.Logger("Step 6: 600 Dark Token");
        ArmyDarkTokenOfDage();
        /* FINISH */

        /* Step 7: LF1 */
        Core.Logger("Step 7: LF1");
        ArmyLF1();

        /* Step 9: LF2, thx tato :TatoGasm: */
        Core.Logger("Step 9: LF2");
        ArmyFL2();

        /* Step 10: LF3 and Finish */
        Core.Logger("Step 10: LF3 and Finish");
        ArmyLF3();

        Adv.RankUpClass("Legion Revenant");
        Bot.Events.PlayerAFK -= PlayerAFK;
    }


    public void ArmyLF1(int quant = 20)
    {
        
        // Dictionary<string, int> reqQuant = new Dictionary<string, int>();
        // reqQuant.Add("Revenant's Spellscroll", quant);
        // reqQuant.Add("Aeacus Empowered", 50);
        // reqQuant.Add("Tethered Soul", 300);
        // reqQuant.Add("Darkened Essence", 500);
        // reqQuant.Add("Dracolich Contract", 1000);

        // Core.Logger("checking ArmyLF1 req item");

        // foreach (var kvp in reqQuant)
        // {
        //     Core.AddDrop(kvp.Key);
        //     Army.registerMessage(kvp.Key, false);

        //     if (Core.CheckInventory(kvp.Key, kvp.Value))
        //     {
        //         Army.sendDone(20);
        //     }
        // }
        if(checkIsDone("Revenant's Speelscroll", quant)) return;

        Core.Join("whitemap");
        Army.waitForPartyCell("Enter", "Spawn");
        Army.waitForSignal("armyLF1ready", delPrevMsg: true);

        // Army.registerMessage("Revenant's Spellscroll", false);
        // if (Army.isDone(20)) return;

        Core.AddDrop("Legion Token");
        Core.AddDrop(LRMaterials);
        Core.AddDrop(LF1);

        Core.FarmingLogger("Revenant's Spellscroll", quant);
        Core.RegisterQuests(6897);
        while (!Bot.ShouldExit)
        {
            ArmyHunt("judgement", "Aeacus Empowered", ClassType.Solo, 50, false);
            // //Army.waitForParty("revenant");
            ArmyHunt("revenant", "Tethered Soul", ClassType.Farm, 300);
            // //Army.waitForParty("shadowrealmpast");
            ArmyHunt("shadowrealmpast", "Darkened Essence", ClassType.Farm, 500);
            // //Army.waitForParty("necrodungeon");
            ArmyHunt("necrodungeon", "Dracolich Contract", ClassType.Farm, 1000);
            // //Army.waitForParty("judgement");

            Bot.Wait.ForPickup("Revenant's Spellscroll");
            if(checkIsDone("Revenant's Speelscroll", quant)) break;
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyFL2(int quant = 6)
    {
        // Dictionary<string, int> reqQuant = new Dictionary<string, int>();
        // reqQuant.Add("Conquest Wreath", quant);
        // reqQuant.Add("Grim Cohort Conquered", 500);
        // reqQuant.Add("Ancient Cohort Conquered", 500);
        // reqQuant.Add("Pirate Cohort Conquered", 500);
        // reqQuant.Add("Battleon Cohort Conquered", 500);
        // reqQuant.Add("Mirror Cohort Conquered", 500);
        // reqQuant.Add("Darkblood Cohort Conquered", 500);
        // reqQuant.Add("Vampire Cohort Conquered", 500);
        // reqQuant.Add("Spirit Cohort Conquered", 500);
        // reqQuant.Add("Dragon Cohort Conquered", 500);
        // reqQuant.Add("Doomwood Cohort Conquered", 500);

        // Core.Logger("checking ArmyLF2 req item");

        // foreach (var kvp in reqQuant)
        // {
        //     Core.AddDrop(kvp.Key);
        //     Army.registerMessage(kvp.Key, false);

        //     if (Core.CheckInventory(kvp.Key, kvp.Value))
        //     {
        //         Army.sendDone(20);
        //     }
        // }

        if(checkIsDone("Conquest Wreath", quant)) return;

        Core.Join("whitemap");
        Army.waitForPartyCell("Enter", "Spawn");
        Army.waitForSignal("armyLF2ready", delPrevMsg: true);

        // Army.registerMessage("Conquest Wreath", false);
        // if (Army.isDone(20)) return;

        Core.AddDrop(LF2);

        Core.RegisterQuests(6898);
        Core.FarmingLogger("Conquest Wreath", quant);

        while (!Bot.ShouldExit)
        {
            ArmyHunt("doomvault", "Grim Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("mummies");
            ArmyHunt("mummies", "Ancient Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("wrath");
            ArmyHunt("wrath", "Pirate Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("doomwar");
            ArmyHunt("doomwar", "Battleon Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("overworld");
            ArmyHunt("overworld", "Mirror Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("deathpits");
            ArmyHunt("deathpits", "Darkblood Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("maxius");
            ArmyHunt("maxius", "Vampire Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("curseshore");
            ArmyHunt("curseshore", "Spirit Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("dragonbone");
            ArmyHunt("dragonbone", "Dragon Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("doomwood");
            ArmyHunt("doomwood", "Doomwood Cohort Conquered", ClassType.Farm, 500);
            // //Army.waitForParty("doomvault");

            Bot.Wait.ForPickup("Conquest Wreath");
            if(checkIsDone("Conquest Wreath", quant)) break;
            
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyLF3(int quant = 10)
    {
        // Dictionary<string, int> reqQuant = new Dictionary<string, int>();
        // reqQuant.Add("Exalted Crown", quant);

        // Core.Logger("checking ArmyLF3 req item");

        // foreach (var kvp in reqQuant)
        // {
        //     Core.AddDrop(kvp.Key);
        //     Army.registerMessage(kvp.Key, false);

        //     if (Core.CheckInventory(kvp.Key, kvp.Value))
        //     {
        //         Army.sendDone(20);
        //     }
        // }

        if(checkIsDone("Exalted Crown", quant)) return;

        Core.Join("whitemap");
        Army.waitForPartyCell("Enter", "Spawn");
        Army.waitForSignal("armyLF3ready", delPrevMsg: true);

        // Army.registerMessage("Exalted Crown", false);
        // if (Army.isDone(20)) return;

        Core.FarmingLogger("Exalted Crown", quant);
        Core.RegisterQuests(6899);
        Core.AddDrop(LF3);
        while (!Bot.ShouldExit)
        {
            Adv.BuyItem("underworld", 216, "Hooded Legion Cowl");
            ArmyDarkTokenOfDage(100);
            ArmyLTs(4000);
            Bot.Wait.ForPickup("Exalted Crown");
            if(checkIsDone("Exalted Crown", quant)) break;
            // Army.registerMessage("Exalted Crown", false);
            // if (Core.CheckInventory("Exalted Crown", quant)) Army.sendDone(20);
            // if (Army.isDone(20)) break;
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyEvilGoodRepMax()
    {
        ArmyEvilGoodRank4();
        ArmyEvilGoodRankMax();
    }

    public void ArmyEvilGoodRank4()
    {
        // Army.registerMessage("ArmyEvilGoodRank4", false);
        // if (Army.isDone(20)) return;

        if(repGoodEvil4()) return;

        Farm.ToggleBoost(BoostType.Reputation);

        Core.Join("swordhavenbridge");
        Army.waitForPartyCell("Enter", "Spawn");

        Army.DivideOnCellsPriority(new[] {"Bridge", "End"}, priorityCell: "", log: true);
        Core.RegisterQuests(364, 369); //Youthanize 364, That Hero Who Chases Slimes 369
        Army.AggroMonMIDs(1, 2, 3, 4, 5);
        Army.AggroMonStart();

        bool needSendDone = true;
        int countCheck = 0;
        while (!Bot.ShouldExit)
        {
            if ((Farm.FactionRank("Good") >= 4 && Farm.FactionRank("Evil") >= 4) && needSendDone)
            {
                if (Army.sendDone())
                    needSendDone = false;
            }
            if (!needSendDone && Army.isDone() && countCheck == 10)
            {
                break;
            }
            countCheck++;
            if (countCheck > 10)
                countCheck = 0;

            if (Army.IsMonsterAlive("*"))
            {
				Bot.Combat.Attack("*");
            }

            Bot.Sleep(100);
        }
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
    }

    public void ArmyEvilGoodRankMax()
    {
        // Army.registerMessage("ArmyEvilGoodRankMax", false);
        // if (Army.isDone(20)) return;
        if(repGoodEvilMax()) return;

        Farm.ToggleBoost(BoostType.Reputation);

        Core.Join("castleundead");
        Army.waitForPartyCell("Enter", "Spawn");

        Core.RegisterQuests(367, 372);
        Army.DivideOnCellsPriority(new[] {"Enter", "Bleft", "Bright", "Tleft"}, priorityCell: "", log: true);
        Army.AggroMonMIDs(1, 2, 3, 7, 10, 11, 12, 13);
        Army.AggroMonStart();

        bool needSendDone = true;
        int countCheck = 0;
        while (!Bot.ShouldExit)
        {
            if ((Farm.FactionRank("Good") >= 10 && Farm.FactionRank("Evil") >= 10) && needSendDone)
            {
                if (Army.sendDone())
                    needSendDone = false;
            }
            if (!needSendDone && Army.isDone() && countCheck == 10)
            {
                break;
            }
            countCheck++;
            if (countCheck > 10)
                countCheck = 0;

            if (Army.IsMonsterAlive("*"))
            {
				Bot.Combat.Attack("*");
            }

            Bot.Sleep(100);
        }

        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
    }

    public void ArmyGoldFarm(int quant = 100000000)
    {
        // Army.registerMessage("ArmyGoldFarm", false);
        // if (Army.isDone(20)) return;

        if(checkGold(quant)) return;

        Farm.ToggleBoost(BoostType.Gold);

        Core.Join("darkwarnation");
        Army.waitForPartyCell("Enter", "Spawn");

        Core.RegisterQuests(8578, 8579, 8580, 8581); //Legion Badges, Mega Legion Badges, Doomed Legion Warriors, Undead Legion Dread       

        Army.DivideOnCellsPriority(new[] {"r2", "r3", "r4"}, priorityCell: "", log: true);
        Army.AggroMonMIDs(3, 4, 5, 7, 8, 9);
        Army.AggroMonStart();

        Army.StartFarmGold(quant);

        Army.AggroMonStop(true);
        Core.JumpWait();
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
        Core.TrashCan("Nation Defender Medal");
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
    }

    public void ArmyDageFavor(int quant = 3000)
    {
        ArmyHunt("evilwarnul", "Dage's Favor", ClassType.Farm, quant);
    }

    public void ArmyEmblemOfDage(int quant = 500)
    {
        if(checkIsDone("Emblem of Dage", quant)) return;

        Core.AddDrop("Emblem of Dage");
        Core.FarmingLogger("Emblem of Dage", quant);
        Core.EquipClass(ClassType.Farm);

        // //Army.waitForParty("shadowblast");
        while (!Bot.ShouldExit)
        {
            ArmyHunt("shadowblast", "Legion Seal", ClassType.Farm, 25);
            ArmyHunt("shadowblast", "Gem of Mastery", ClassType.Farm);
            if(checkIsDone("Emblem of Dage", quant)) break;
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyDiamondTokenOfDage(int quant = 300)
    {
        // if (Core.CheckInventory("Diamond Token of Dage", quant))
        //     return;

        if(checkIsDone("Diamond Token of Dage", quant)) return;

        ArmyLTs(50);

        Core.FarmingLogger("Diamond Token of Dage", quant);
        Core.AddDrop("Diamond Token of Dage", "Legion Token");

        Core.RegisterQuests(4743);
        while (!Bot.ShouldExit)
        {
            ArmyHunt("tercessuinotlim", "Defeated Makai", ClassType.Farm, 25);
            ArmyHunt("aqlesson", "Carnax Eye", ClassType.Solo);
            ArmyHunt("deepchaos", "Kathool Tentacle", ClassType.Solo);
            ArmyHunt("dflesson", "Fluffy's Bones", ClassType.Solo);
            ArmyHunt("lair", "Red Dragon's Fang", ClassType.Solo);
            ArmyHunt("bloodtitan", "Blood Titan's Blade", ClassType.Solo);
            if(checkIsDone("Diamond Token of Dage", quant)) break;
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyDarkTokenOfDage(int quant = 600)
    {
        // if (Core.CheckInventory("Dark Token", quant))
        //     return;
        if(checkIsDone("Dark Token", quant)) return;

        Core.FarmingLogger("Dark Token", quant);
        Core.AddDrop("Dark Token");
        // //Army.waitForParty("seraphicwardage");
        while (!Bot.ShouldExit){
            ArmyHunt("seraphicwardage", "Seraphic Commanders Slain", ClassType.Farm, 6);
            if(checkIsDone("Diamond Token of Dage", quant)) break;
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyLTs(int quant = 25000)
    {
        // if (Core.CheckInventory("Legion Token", quant))
        //     return;
        if(checkIsDone("Legion Token", quant)) return;
        Core.FarmingLogger("Legion Token", quant);
        // //Army.waitForParty("dreadrock");
        while (!Bot.ShouldExit){
            ArmyHunt("dreadrock", "Legion Token", ClassType.Farm, quant);
            if(checkIsDone("Legion Token", quant)) return;
        }
        Core.CancelRegisteredQuests();
    }

    void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Core.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }

    void ArmyHunt(string map, string item, ClassType classType, int quant = 1, bool isTemp = false)
    {
        if(checkIsDone(item, quant)) return;

        Army.registerMessage(item);
        if (Army.isDone(20)) return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (item != null && isTemp == false)
            Core.AddDrop(item);

        Core.EquipClass(classType);
        Core.Join(map);
        Army.waitForPartyCell("Enter", "Spawn");
        
        Core.FarmingLogger(item, quant);
        Core.Logger($"army: starting {quant} {item}");
        
        HandleMap(map, item, quant);

        Army.StartFarm(item, quant);

        Army.AggroMonStop(true);
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
        Core.ToBank(item);
        Core.Logger($"everyone have finished {quant} {item}");
    }


    void HandleMap(string map, string? item, int quant)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        switch (map)
        {
            case "evilwarnul":
                Army.AggroMonMIDs(1, 3, 20, 21, 22, 24, 25);
                Army.DivideOnCellsPriority(new[] {"r2", "r3", "r9", "r10"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "revenant":
                map = "revenant-" + (Array.IndexOf(Army.Players(), Core.Username()) > 1 ? (Army.getRoomNr() + 1).ToString() : "");
                Army.AggroMonMIDs(1, 2, 3, 4);
                Army.DivideOnCellsPriority(new[] {"r2"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "curseshore":
                Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
                Army.DivideOnCellsPriority(new[] {"Enter", "r2"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "dragonbone":
                Army.AggroMonMIDs(4, 6, 7, 9);
                Army.DivideOnCellsPriority(new[] {"r2", "r3"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "doomwood":
                Army.AggroMonMIDs(3, 4, 5, 8, 9, 10, 11, 12);
                Army.DivideOnCellsPriority(new[] {"r3", "r5", "r6"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "swordhavenbridge":
                Core.RegisterQuests(364, 369); //Youthanize 364, That Hero Who Chases Slimes 369
                Army.AggroMonMIDs(1, 2, 3, 4, 5);
                Army.DivideOnCellsPriority(new[] {"Bridge", "End"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "castleundead":
                Core.RegisterQuests(367, 372);
                Army.AggroMonMIDs(1, 2, 3, 4, 5, 7, 10, 11, 12, 13);
                Army.DivideOnCellsPriority(new[] {"Enter", "Bleft", "Bright", "Tleft", "Hall"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "shadowblast":
                Core.RegisterQuests(4742);
                if (item == "Legion Seal")
                {
                    Army.AggroMonMIDs(25, 27, 29, 31);
                    Army.DivideOnCellsPriority(new[] {"r12", "r13"}, priorityCell: "", log: true);
                    Army.AggroMonStart();
                }
                else if (item == "Gem of Mastery")
                {
                    Army.AggroMonMIDs(41, 43, 46, 48);
                    Army.DivideOnCellsPriority(new[] {"r16", "r17"}, priorityCell: "", log: true);
                    Army.AggroMonStart();
                }
                break;

            case "tercessuinotlim":
                Army.AggroMonMIDs(1, 3, 4, 5);
                Army.DivideOnCellsPriority(new[] {"Enter", "m1", "m2"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "aqlesson":
                Army.AggroMonMIDs(17);
                Army.DivideOnCellsPriority(new[] {"Frame9"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "deepchaos":
                Army.AggroMonMIDs(9);
                Army.DivideOnCellsPriority(new[] {"Frame4"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "dflesson":
                Army.AggroMonMIDs(29);
                Army.DivideOnCellsPriority(new[] {"r12"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "lair":
                Army.AggroMonMIDs(14);
                Army.DivideOnCellsPriority(new[] {"End"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "bloodtitan":
                Army.AggroMonMIDs(1);
                Army.DivideOnCellsPriority(new[] {"Enter"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "seraphicwardage":
                Core.RegisterQuests(6248, 6249, 6251);
                Army.AggroMonMIDs(7, 8, 9, 10, 11, 12);
                Army.DivideOnCellsPriority(new[] {"r3", "r4"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;

            case "dreadrock":
                Core.RegisterQuests(4849);
                Army.AggroMonMIDs(12, 14, 15, 22, 23, 24, 25);
                Army.DivideOnCellsPriority(new[] {"r3", "r8a"}, priorityCell: "", log: true);
                Army.AggroMonStart();
                break;


            default:
                // Handle other maps or cases here if needed
                break;
        }
    }

    private bool checkIsDone(string item, int quant){
        Army.waitForSignal($"checking{item}{quant}", delPrevMsg: true);
        Army.registerMessage(item);
        if (Core.CheckInventory(item, quant)) Army.sendDone(20);
        if (Army.isDone(20)) return true;
        return false;
    }

    private bool repGoodEvil4(){
        Army.waitForSignal($"checkinggoodevil4",delPrevMsg:  true);
        Army.registerMessage("rep4");
        if (Farm.FactionRank("Good") >= 4 && Farm.FactionRank("Evil") >= 4) Army.sendDone(20);
        if (Army.isDone(20)) return true;
        return false;
    }

    private bool repGoodEvilMax(){
        Army.waitForSignal($"checkinggoodevilmax", delPrevMsg: true);
        Army.registerMessage("repmax");
        if (Farm.FactionRank("Good") >= 10 && Farm.FactionRank("Evil") >= 10) Army.sendDone(20);
        if (Army.isDone(20)) return true;
        return false;
    }
    
    private bool checkGold(int quant){
        Army.waitForSignal($"gold{quant}",delPrevMsg:  true);
        Army.registerMessage("gold");
        if (Bot.Player.Gold >= quant) Army.sendDone(20);
        if (Army.isDone(20)) return true;
        return false;
    }
}
