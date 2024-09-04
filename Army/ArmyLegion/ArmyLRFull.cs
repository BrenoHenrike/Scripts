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
        sArmy.player5,
        sArmy.player6,
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

        // LR();
        Core.Logger("This script, and any army script that requires more then 1 map is entirely broken, and there is no plan to fix them, for army stuff please use GrimLi");

        Core.SetOptions(false);
    }

    public void LR()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Bot.Events.PlayerAFK += PlayerAFK;

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
        if (Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

        Core.AddDrop("Legion Token");
        Core.AddDrop(LRMaterials);
        Core.AddDrop(LF1);

        Core.FarmingLogger("Revenant's Spellscroll", quant);
        Core.RegisterQuests(6897);
        while (!Bot.ShouldExit && !Core.CheckInventory("Revenant's Spellscroll", quant))
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
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyFL2(int quant = 6)
    {
        if (Core.CheckInventory("Conquest Wreath", quant))
            return;

        Core.AddDrop(LF2);

        Core.RegisterQuests(6898);
        Core.FarmingLogger("Conquest Wreath", quant);

        while (!Bot.ShouldExit && !Core.CheckInventory("Conquest Wreath", quant))
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
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyLF3(int quant = 10)
    {
        if (Core.CheckInventory("Exalted Crown", quant))
            return;

        Core.FarmingLogger("Exalted Crown", quant);
        Core.RegisterQuests(6899);
        Core.AddDrop(LF3);
        while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Crown", quant))
        {
            Adv.BuyItem("underworld", 216, "Hooded Legion Cowl");
            ArmyDarkTokenOfDage(100);
            ArmyLTs(4000);
            Bot.Wait.ForPickup("Exalted Crown");
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
        if (Farm.FactionRank("Good") >= 4 && Farm.FactionRank("Evil") >= 4)
            return;

        Farm.ToggleBoost(BoostType.Reputation);
        // //Army.waitForParty("swordhavenbridge");
        while (!Bot.ShouldExit && (Farm.FactionRank("Good") < 4 && Farm.FactionRank("Evil") < 4))
            ArmyHunt("swordhavenbridge", "Slime in a Jar", ClassType.Farm, 6, true);
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Reputation, false);
    }

    public void ArmyEvilGoodRankMax()
    {
        if (Farm.FactionRank("Good") >= 10 && Farm.FactionRank("Evil") >= 10)
            return;

        Farm.ToggleBoost(BoostType.Reputation);
        // //Army.waitForParty("castleundead");
        while (!Bot.ShouldExit && (Farm.FactionRank("Good") < 10 && Farm.FactionRank("Evil") < 10))
            ArmyHunt("castleundead", "Replacement Tibia", ClassType.Farm, 6, true);
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Reputation, false);
    }

    public void ArmyGoldFarm(int quant = 100000000)
    {
        if (Bot.Player.Gold >= quant)
            return;

        Farm.ToggleBoost(BoostType.Gold);
        Core.RegisterQuests(8578, 8579, 8580, 8581); //Legion Badges, Mega Legion Badges, Doomed Legion Warriors, Undead Legion Dread        
        if (!Bot.ShouldExit && Bot.Player.Gold < quant)
        {
            Army.AggroMonMIDs(3, 4, 5, 7, 8, 9);
            Army.AggroMonStart("darkwarnation");
            Army.DivideOnCells("r2", "r3", "r4");

            while (!Bot.ShouldExit && Bot.Player.Gold < quant)
                Bot.Combat.Attack("*");
        }
        Army.AggroMonStop(true);
        Core.JumpWait();
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
        Core.TrashCan("Nation Defender Medal");
    }

    public void ArmyDageFavor(int quant = 3000)
    {
        if (Core.CheckInventory("Dage's Favor", quant))
            return;
        // //Army.waitForParty("evilwarnul");
        while (!Bot.ShouldExit && !Core.CheckInventory("Dage's Favor", quant))
            ArmyHunt("evilwarnul", "Dage's Favor", ClassType.Farm, quant);
    }

    public void ArmyEmblemOfDage(int quant = 500)
    {
        if (Core.CheckInventory("Emblem of Dage", quant))
            return;

        Core.AddDrop("Emblem of Dage");
        Core.FarmingLogger("Emblem of Dage", quant);
        Core.EquipClass(ClassType.Farm);

        // //Army.waitForParty("shadowblast");
        while (!Bot.ShouldExit && !Core.CheckInventory("Emblem of Dage", quant))
        {
            ArmyHunt("shadowblast", "Legion Seal", ClassType.Farm, 25);
            ArmyHunt("shadowblast", "Gem of Mastery", ClassType.Farm);
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyDiamondTokenOfDage(int quant = 300)
    {
        if (Core.CheckInventory("Diamond Token of Dage", quant))
            return;

        ArmyLTs(50);

        Core.FarmingLogger("Diamond Token of Dage", quant);
        Core.AddDrop("Diamond Token of Dage", "Legion Token");

        Core.RegisterQuests(4743);
        while (!Bot.ShouldExit && !Core.CheckInventory("Diamond Token of Dage", quant))
        {
            ArmyHunt("tercessuinotlim", "Defeated Makai", ClassType.Farm, 25);
            ArmyHunt("aqlesson", "Carnax Eye", ClassType.Solo);
            ArmyHunt("deepchaos", "Kathool Tentacle", ClassType.Solo);
            ArmyHunt("dflesson", "Fluffy's Bones", ClassType.Solo);
            ArmyHunt("lair", "Red Dragon's Fang", ClassType.Solo);
            ArmyHunt("bloodtitan", "Blood Titan's Blade", ClassType.Solo);
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyDarkTokenOfDage(int quant = 600)
    {
        if (Core.CheckInventory("Dark Token", quant))
            return;

        Core.FarmingLogger("Dark Token", quant);
        Core.AddDrop("Dark Token");
        // //Army.waitForParty("seraphicwardage");
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Token", quant))
            ArmyHunt("seraphicwardage", "Seraphic Commanders Slain", ClassType.Farm, 6);
        Core.CancelRegisteredQuests();
    }

    public void ArmyLTs(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant))
            return;

        Core.FarmingLogger("Legion Token", quant);
        // //Army.waitForParty("dreadrock");
        while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            ArmyHunt("dreadrock", "Legion Token", ClassType.Farm, quant);
        Core.CancelRegisteredQuests();
    }

    void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Core.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }

    void ArmyHunt(string map, string? item, ClassType classType, int quant = 1, bool isTemp = false)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (item != null && isTemp == false)
            Core.AddDrop(item);

        Core.EquipClass(classType);
        if (Core.CheckInventory(item))
            //Army.waitForParty("whitemap", item);
        HandleMap(map, item, quant);

        

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }


    void HandleMap(string map, string? item, int quant)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        switch (map)
        {
            case "evilwarnul":
                Army.AggroMonMIDs(1, 3, 20, 21, 22, 24, 25);
                Army.AggroMonStart(map);
                Army.DivideOnCells("r2", "r9", "r10");
                break;

            case "revenant":
                map = "revenant" + (Array.IndexOf(Army.Players(), Core.Username()) > 2 ? (Army.getRoomNr() + 1).ToString() : "");
                Army.AggroMonMIDs(1, 2, 3, 4);
                Army.AggroMonStart(map);
                Army.DivideOnCells("r2");
                break;

            case "curseshore":
                Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
                Army.AggroMonStart(map);
                Army.DivideOnCells("Enter", "r2");
                break;

            case "dragonbone":
                Army.AggroMonMIDs(4, 67, 9);
                Army.AggroMonStart(map);
                Army.DivideOnCells("r2", "r3");
                break;

            case "doomwood":
                Army.AggroMonMIDs(3, 4, 5, 8, 9, 10, 11, 12);
                Army.AggroMonStart(map);
                Army.DivideOnCells("r3", "r5", "r6");
                break;

            case "swordhavenbridge":
                Core.RegisterQuests(364, 369); //Youthanize 364, That Hero Who Chases Slimes 369
                Army.AggroMonMIDs(1, 2, 3, 4, 5);
                Army.AggroMonStart(map);
                Army.DivideOnCells("Bridge", "End");
                break;

            case "castleundead":
                Core.RegisterQuests(367, 372);
                Army.AggroMonMIDs(1, 2, 3, 4, 5, 7, 10, 11, 12, 13);
                Army.AggroMonStart(map);
                Army.DivideOnCells("Enter", "Bleft", "Bright", "Tleft", "Hall");
                break;

            case "shadowblast":
                Core.RegisterQuests(4742);
                if (item == "Legion Seal")
                {
                    Army.AggroMonMIDs(25, 27, 29, 31);
                    Army.AggroMonStart(map);
                    Army.DivideOnCells("r12", "r13");
                }
                else if (item == "Gem of Mastery")
                {
                    Army.AggroMonMIDs(41, 43, 46, 48);
                    Army.AggroMonStart(map);
                    Army.DivideOnCells("r16", "r17");
                }
                break;

            case "tercessuinotlim":
                Army.AggroMonMIDs(1, 3, 4, 5);
                Army.AggroMonStart(map);
                Army.DivideOnCells("Enter", "m1", "m2");
                break;

            case "aqlesson":
                Army.AggroMonMIDs(17);
                Army.AggroMonStart(map);
                Army.DivideOnCells("Frame9");
                break;

            case "deepchaos":
                Army.AggroMonMIDs(9);
                Army.AggroMonStart(map);
                Army.DivideOnCells("Frame4");
                break;

            case "dflesson":
                Army.AggroMonMIDs(29);
                Army.AggroMonStart(map);
                Army.DivideOnCells("r12");
                break;

            case "lair":
                Army.AggroMonMIDs(14);
                Army.AggroMonStart("lair");
                Army.DivideOnCells("End");
                break;

            case "bloodtitan":
                Army.AggroMonMIDs(1);
                Army.AggroMonStart(map);
                Army.DivideOnCells("Enter");
                break;

            case "seraphicwardage":
                Core.RegisterQuests(6248, 6249, 6251);
                Army.AggroMonMIDs(7, 8, 9, 10, 11, 12);
                Army.AggroMonStart(map);
                Army.DivideOnCells("r3", "r4");
                break;

            case "dreadrock":
                Core.RegisterQuests(4849);
                Army.AggroMonMIDs(12, 14, 15, 22, 23, 24, 25);
                Army.AggroMonStart(map);
                Army.DivideOnCells("r3", "r8a");
                break;

            default:
                // Handle other maps or cases here if needed
                break;
        }
    }





}
