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

        LR();

        Core.SetOptions(false);
    }

    public void LR()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Bot.Events.PlayerAFK += PlayerAFK;

        Legion.JoinLegion();
        Legion.LegionRound4Medal();
        Seraph.SeraphicWar_Questline();
        CruxShip.StoryLine();
        DarkCasterCheck();
        /*
        ********************************************************************************
        ********************************PREFARM ZONE************************************
        ********************************************************************************
        */
        /*Step 1: Evil Rank 10*/
        ArmyEvilGoodRepMax();
        Army.waitForParty("whitemap");
        /*Step 2: Hooded Legion Cowl funds and some change for enhancement costs*/
        ArmyGoldFarm(5500000);
        Army.waitForParty("whitemap");
        /*Step 3: 3000 Dage Favor*/
        ArmyDageFavor();
        Army.waitForParty("whitemap");
        /*Step 4: 10 Emblem of Dage*/
        ArmyEmblemOfDage(10);
        Army.waitForParty("whitemap");
        /*Step 5: 300 Diamond Token of Dage*/
        ArmyDiamondTokenOfDage();
        Army.waitForParty("whitemap");
        /*Step 6: 600 Dark Token*/
        ArmyDarkTokenOfDage();
        Army.waitForParty("whitemap");
        /*
        ********************************************************************************
        **********************************FINISH****************************************
        ********************************************************************************
        */
        /*Step 7: LF1*/
        ArmyLF1();
        /*Step 9: LF2, thx tato :TatoGasm:*/
        ArmyFL2();
        /*Step 10: LF3 and Finish*/
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
            //Adv.BestGear(RacialGearBoost.Undead);
            ArmyHunt("judgement", new[] { "Ultra Aeacus" }, "Aeacus Empowered", ClassType.Solo, false, 50);
            ArmyHunt("revenant", new[] { "Forgotten Soul" }, "Tethered Soul", ClassType.Farm, false, 300);
            ArmyHunt("shadowrealmpast", new[] { "Pure Shadowscythe, Shadow Guardian, Shadow Warrior" }, "Darkened Essence", ClassType.Farm, false, 500);
            ArmyHunt("necrodungeon", new[] { "5 Headed Dracolich" }, "Dracolich Contract", ClassType.Farm, false, 1000);

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
        //Adv.BestGear(RacialGearBoost.Undead);
        Core.FarmingLogger("Conquest Wreath", quant);

        while (!Bot.ShouldExit && !Core.CheckInventory("Conquest Wreath", quant))
        {
            ArmyHunt("doomvault", new[] { "Grim Soldier" }, "Grim Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("mummies", new[] { "Mummy" }, "Ancient Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("wrath", new[] { "Undead Pirate", "Mutineer", "Dark Fire", "Fishbones" }, "Pirate Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("doomwar", new[] { "Zombie", "Zombie Knight" }, "Battleon Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("overworld", new[] { "Undead Minion", "Undead Mage", "Undead Bruiser" }, "Mirror Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("deathpits", new[] { "Ghastly Darkblood", "Rotting Darkblood", "Sundered Darkblood" }, "Darkblood Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("maxius", new[] { "Ghoul Minion", "Vampire Minion" }, "Vampire Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("curseshore", new[] { "Escaped Ghostly Zardman", "Escaped Wendighost", "Escaped Dai Tenghost" }, "Spirit Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("dragonbone", new[] { "Bone Dragonling", "Dark Fire", }, "Dragon Cohort Conquered", ClassType.Farm, false, 500);

            ArmyHunt("doomwood", new[] { "Doomwood Soldier", "Doomwood Bonemuncher", "Doomwood Ectomancer", "Undead Paladin", "Doomwood Treeant" }, "Doomwood Cohort Conquered", ClassType.Farm, false, 500);

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
            Army.waitForParty("whitemap");
            ArmyLTs(4000);
            Army.waitForParty("whitemap");
            Bot.Wait.ForPickup("Exalted Crown");
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyEvilGoodRepMax(int rank = 10)
    {
        ArmyEvilGoodRank4();
        ArmyEvilGoodRankMax();
    }

    public void ArmyEvilGoodRank4()
    {
        if (Farm.FactionRank("Good") >= 4 && Farm.FactionRank("Evil") >= 4)
            return;

        Farm.ToggleBoost(BoostType.Reputation);
        Core.RegisterQuests(364, 369); //Youthanize 364, That Hero Who Chases Slimes 369
        while (!Bot.ShouldExit && (Farm.FactionRank("Good") < 4 && Farm.FactionRank("Evil") < 4))
            ArmyHunt("swordhavenbridge", new[] { "Slime" }, "Slime in a Jar", ClassType.Farm, true, 6);
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Reputation, false);
    }

    public void ArmyEvilGoodRankMax()
    {
        if (Farm.FactionRank("Good") >= 10 && Farm.FactionRank("Evil") >= 10)
            return;

        Farm.ToggleBoost(BoostType.Reputation);
        Core.RegisterQuests(367, 372);
        while (!Bot.ShouldExit && (Farm.FactionRank("Good") < 10 && Farm.FactionRank("Evil") < 10))
            ArmyHunt("castleundead", new[] { "Skeletal Viking", "Skeletal Warrior" }, "Replacement Tibia", ClassType.Farm, true, 6);
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Reputation, false);
    }

    public void ArmyGoldFarm(int quant = 100000000)
    {
        if (Bot.Player.Gold >= quant)
            return;

        Farm.ToggleBoost(BoostType.Gold);
        Core.RegisterQuests(8578, 8579, 8580, 8581); //Legion Badges, Mega Legion Badges, Doomed Legion Warriors, Undead Legion Dread        
        while (!Bot.ShouldExit && Bot.Player.Gold < quant)
            ArmyHunt("darkwarnation", new[] { "High Legion Inquisitor", "Legion Doomknight", "Legion Dread Knight", "Legion Dreadmarch", "Legion Fiend Rider" }, "Legion Badges", ClassType.Farm, true, 999);
        // ArmyHunt("battlegrounde", new[] { "Living Ice", "Ice Lord", "Ice Demon", "Glacial Horror", "Icy Dragon", "Permafrost Pummeler", "Icy Banshee", "Frozen Deserter" }, "Battleground E Opponent Defeated", ClassType.Farm, true, 10);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
        Core.TrashCan("Nation Defender Medal");
    }

    public void ArmyDageFavor(int quant = 3000)
    {
        if (Core.CheckInventory("Dage's Favor", quant))
            return;
        while (!Bot.ShouldExit && !Core.CheckInventory("Dage's Favor", quant))
            ArmyHunt("evilwarnul", new[] { "Skeletal Warrior", "Skull Warrior" }, "Dage's Favor", ClassType.Farm, false, quant);
    }

    public void ArmyEmblemOfDage(int quant = 500)
    {
        if (Core.CheckInventory("Emblem of Dage", quant))
            return;

        Core.AddDrop("Emblem of Dage");
        Core.FarmingLogger("Emblem of Dage", quant);
        Core.EquipClass(ClassType.Farm);
        //Adv.BestGear(GenericGearBoost.gold);

        Core.RegisterQuests(4742);
        while (!Bot.ShouldExit && !Core.CheckInventory("Emblem of Dage", quant))
        {
            ArmyHunt("shadowblast", new[] { "Shadowrise Guard", "Doombringer", "DoomKnight Prime", "Draconic DoomKnight" }, "Legion Seal", ClassType.Farm, isTemp: false, 25);
            ArmyHunt("shadowblast", new[] { "Minotaurofwar", "Carnage", "CaesarisTheDark" }, "Gem of Mastery", ClassType.Farm);
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
            ArmyHunt("tercessuinotlim", new[] { "Dark Makai" }, "Defeated Makai", ClassType.Farm, false, 25);
            Army.waitForParty("bloodtitan");

            //Adv.BestGear(RacialGearBoost.Chaos);
            ArmyHunt("aqlesson", new[] { "Carnax" }, "Carnax Eye", ClassType.Solo, true, 1);
            Army.waitForParty("deepchaos");

            ArmyHunt("deepchaos", new[] { "Kathool" }, "Kathool Tentacle", ClassType.Solo, true, 1);
            Army.waitForParty("dflesson");

            ArmyHunt("dflesson", new[] { "Fluffy the Dracolich" }, "Fluffy's Bones", ClassType.Solo, true, 1);
            Army.waitForParty("lair");

            //Adv.BestGear(RacialGearBoost.Dragonkin);
            ArmyHunt("lair", new[] { "Red Dragon" }, "Red Dragon's Fang", ClassType.Solo, true);
            Army.waitForParty("bloodtitan");

            //Adv.BestGear(RacialGearBoost.Human);
            ArmyHunt("bloodtitan", new[] { "Blood Titan" }, "Blood Titan's Blade", ClassType.Solo, true, 1);
            Army.waitForParty("tercessuinotlim");
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyDarkTokenOfDage(int quant = 600)
    {
        if (Core.CheckInventory("Dark Token", quant))
            return;

        Core.FarmingLogger("Dark Token", quant);
        Core.AddDrop("Dark Token");
        //Adv.BestGear(RacialGearBoost.Human);
        Core.RegisterQuests(6248, 6249, 6251);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Token", quant))
            ArmyHunt("seraphicwardage", new[] { "Seraphic Commander, Seraphic Soldier" }, "Seraphic Commanders Slain", ClassType.Farm, true, 6);
        Core.CancelRegisteredQuests();
    }

    public void ArmyLTs(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant))
            return;

        //Adv.BestGear(RacialGearBoost.Human);
        Core.RegisterQuests(4849);
        while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            ArmyHunt("dreadrock", new[] { "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary" }, "Legion Token", ClassType.Farm, false, quant);
        Core.CancelRegisteredQuests();
    }

    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop(item);

        Core.EquipClass(classType);
        if (map == "revenant")
        {
            map = Array.IndexOf(Army.Players(), Core.Username()) > 2 ? "revenant" : "revenant-" + (Army.getRoomNr() + 1);
            Army.waitForParty(map, item, 3);
        }

        Core.FarmingLogger(item, quant);

        if (Bot.Map.Name == "tercessuinotlim")
        {
            Army.DivideOnCells("m1", "m2");
            Army.AggroMonMIDs(2, 3, 4, 5);
            Army.AggroMonStart("tercessuinotlim");
        }
        else Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }

    void DarkCasterCheck()
    {
        bool hasDarkCaster = false;
        if (Core.CheckInventory(new[] { "Love Caster", "Legion Revenant" }, any: true))
            hasDarkCaster = true;
        else
        {
            List<InventoryItem> InventoryData = Bot.Inventory.Items;
            foreach (InventoryItem Item in InventoryData)
            {
                if (Item.Name.Contains("Dark Caster") && Item.Category == ItemCategory.Class)
                {
                    hasDarkCaster = true;
                    break;
                }
            }
            if (!hasDarkCaster)
            {
                List<InventoryItem> BankData = Bot.Bank.Items;
                foreach (InventoryItem Item in BankData)
                {
                    if (Item.Name.Contains("Dark Caster") && Item.Category == ItemCategory.Class)
                    {
                        hasDarkCaster = true;
                        Core.Unbank(Item.Name);
                        break;
                    }
                }
            }
        }
        if (!hasDarkCaster)
        {
            ArmyLTs(2000);
            ILDC.GetILDC(false);
        }
    }

    void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }




}
