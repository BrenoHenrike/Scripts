//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Army/Various/ArmyLegionFealty2.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyLR
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    private CoreArmyLite Army = new();
    public CoreLegion Legion = new CoreLegion();
    public CoreLR CoreLR = new CoreLR();
    public ArmyLegionFealty2 ArmyLF2 = new ArmyLegionFealty2();
    public InfiniteLegionDC ILDC = new InfiniteLegionDC();
    public SeraphicWar_Story Seraph = new SeraphicWar_Story();
    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();
    public string OptionsStorage = "ArmyLR";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1),
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
        Core.SetOptions(disableClassSwap: true);
        Bot.Options.RestPackets = false;

        LR();

        Core.SetOptions(false);
    }

    public void LR()
    {
        Legion.JoinLegion();
        Seraph.SeraphicWar_Questline();
        Core.BankingBlackList.AddRange(new[] { "LR, LF1, LF2, LF3" });
        /*
        ********************************************************************************
        ********************************PREFARM ZONE************************************
        ********************************************************************************
        */
        /*Step 1: Evil Rank 10*/
        ArmyEvilGoodRepMax();
        /*Step 2: Hooded Legion Cowl funds and some change for enhancement costs*/
        ArmyGoldFarm(5500000);
        /*Step 3: 3000 Dage Favor*/
        ArmyDageFavor(3000);
        /*Step 4: 10 Emblem of Dage*/
        ArmyEmblemOfDage(10);
        /*Step 5: 300 Diamond Token of Dage*/
        ArmyDiamondTokenOfDage(300);
        /*Step 6: 600 Dark Token*/
        ArmyDarkTokenOfDage(600);
        /*
        ********************************************************************************
        **********************************FINISH****************************************
        ********************************************************************************
        */
        /*Step 7: LF1*/
        ArmyLF1(20);
        /*Step 9: LF2, thx tato :TatoGasm:*/
        ArmyLF2.Setup();
        /*Step 10: LF3 and Finish*/
        ArmyLF3(10);
        CoreLR.GetLR(true);
    }

    void ArmyHunt(string map = null, string[] monsters = null, string item = null, bool isTemp = false, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
            return;

        if (item == null)
            return;

        Bot.Drops.Add(item);

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Core.Join(map);
        WaitCheck();
        AggroNavigation(map);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);
    }

    void WaitCheck()
    {
        while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(5000);
        }
        Core.Logger($"Squad All Gathered [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
    }

    /*Define map specific party splitting here*/
    void AggroNavigation(string map = null)
    {
        if (Bot.Map.Name == null)
            return;

        if (Bot.Map.Name == "revenant")
        {
            Army.AggroMonCells("r2");
            Army.AggroMonStart("revenant");
            Army.DivideOnCells("r2"); 
        }
        if (Bot.Map.Name == "swordhavenbridge")
        {
            Army.AggroMonCells("Bridge", "End");
            Army.AggroMonStart("swordhavenbridge");
            Army.DivideOnCells("Bridge", "End");
        }
        if (Bot.Map.Name == "castleundead")
        {
            Army.AggroMonCells("Enter", "Bright", "Bleft");
            Army.AggroMonStart("castleundead");
            Army.DivideOnCells("Enter", "Bright", "Bleft");
        }
        if (Bot.Map.Name == "necrodungeon")
        {
            Army.AggroMonCells("r22");
            Army.AggroMonStart("necrodungeon");
            Army.DivideOnCells("r22");
        }
        if (Bot.Map.Name == "shadowrealmpast")
        {
            Army.AggroMonCells("Enter", "r2", "r3");
            Army.AggroMonStart("shadowrealmpast");
            Army.DivideOnCells("Enter", "r2", "r3");
        }
        if (Bot.Map.Name == "battlegrounde")
        {
            Army.AggroMonCells("r5", "r4", "r3", "r2");
            Army.AggroMonStart("battlegrounde");
            Army.DivideOnCells("r5", "r4", "r3", "r2");
        }
        if (Bot.Map.Name == "evilwarnul")
        {
            Army.AggroMonCells("r2", "r3", "r4", "r5", "r6", "r9");
            Army.AggroMonStart("evilwarnul");
            Army.DivideOnCells("r2", "r3", "r4", "r5", "r6", "r9");
        }
        if (Bot.Map.Name == "shadowblast")
        {
            Army.AggroMonCells("r6", "r8", "r10", "r11", "r12", "r18");
            Army.AggroMonStart("shadowblast");
            Army.DivideOnCells("r6", "r8", "r10", "r11", "r12", "r18");
        }
        if (Bot.Map.Name == "seraphicwardage")
        {
            Army.AggroMonCells("Enter", "r2", "r3", "r4");
            Army.AggroMonStart("seraphicwardage");
            Army.DivideOnCells("Enter", "r2", "r3", "r4");
        }
    }

    public void GetItems(string map = null, string[] Monsters = null, int questID = 000, string[] Loot = null, bool isTemp = false)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        if (Loot == null)
        {
            foreach (string Reward in Loot)
            {
                ItemBase item = Bot.Inventory.GetItem(Reward);
                if (item.Coins)
                    Core.AddDrop(Reward);
                else
                    Core.Logger($"{item} has been excluded as it is not a AC item.");
            }
        }
        else Core.AddDrop(Loot);

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(questID);

        Army.SmartAggroMonStart(map, Monsters);

        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
    }

    public void GetItem(string map = null, string Monster = null, int questID = 000, string item = null, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        Core.EquipClass(ClassType.Farm);
        if (!Bot.Quests.Active.Contains(QuestData))
            Core.EnsureAccept(questID);

        Army.SmartAggroMonStart(map, Monster);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Core.HuntMonster(map, Monster);
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    public void ArmyEvilGoodRepMax(int rank = 10)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        Farm.ToggleBoost(BoostType.Reputation);
        ArmyEvilGoodRank4();
        ArmyEvilGoodRankMax();

        Farm.ToggleBoost(BoostType.Reputation, false);
    }

    public void ArmyEvilGoodRank4()
    {
        Core.RegisterQuests(364, 369); //Youthanize 364, That Hero Who Chases Slimes 369
        while (!Bot.ShouldExit && (Farm.FactionRank("Good") < 4 && Farm.FactionRank("Evil") < 4))
            ArmyHunt("swordhavenbridge", new[] { "Slime" }, "Slime in a Jar", true, 6);

        Core.CancelRegisteredQuests();
    }

    public void ArmyEvilGoodRankMax()
    {
        Core.RegisterQuests(367, 372); //Bone-afide 367, Tomb with a View 372
        //ArmyHunt("castleundead", new[] {"Skeletal Viking", "Skeletal Warrior"}, 372, new[] {"Chaorrupted Skull"}, true, 5);
        while (!Bot.ShouldExit && (Farm.FactionRank("Good") < 10 && Farm.FactionRank("Evil") < 10))
            ArmyHunt("castleundead", new[] { "Skeletal Viking", "Skeletal Warrior" }, "Replacement Tibia", true, 6);

        Core.CancelRegisteredQuests();
    }

    public void ArmyGoldFarm(int quant)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        Farm.ToggleBoost(BoostType.Gold);
        Core.RegisterQuests(3991, 3992);
        while (!Bot.ShouldExit && Bot.Player.Gold < quant)
            ArmyHunt("battlegrounde", new[] { "Living Ice", "Ice Lord", "Ice Demon", "Glacial Horror", "Icy Dragon", "Permafrost Pummeler", "Icy Banshee", "Frozen Deserter" }, "Battleground E Opponent Defeated", true, 10);
        /*Farms 500k extra just to be safe with enhancement costs*/
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void ArmyDageFavor(int quant)
    {
        ArmyHunt("evilwarnul", new[] { "Skull Warrior", "Undead Infantry" }, "Dage's Favor", false, quant);
    }

    public void ArmyEmblemOfDage(int quant)
    {
        if (Core.CheckInventory("Emblem of Dage", quant))
            return;
        Core.AddDrop("Emblem of Dage");
        Core.Logger($"Farming {quant} Emblems");
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.gold);
        Core.AddDrop("Legion Seal", "Gem of Mastery");
        ArmyLegionRound4Medal();
        Core.RegisterQuests(4742);
        ArmyHunt("shadowblast", new[] { "Draconic DoomKnight, Minotaurofwar, Shadowrise Guard, DoomKnight Prime, Doombringer, Carnage, Crag and Bamboozle, Caesaristhedark, Shadow Destroyer", "Left", "*", "Legion Seal" }, "Legion Seal", false, (25*(10-Bot.Inventory.GetQuantity("Emblem of Dage"))));
        while (!Bot.ShouldExit && !Core.CheckInventory("Emblem of Dage", quant))
        {   /*Keeping second armyhunt in case gem of mastery doesn't drop within 25 legion seals, vhl experience tells me it doesn't always*/
            ArmyHunt("shadowblast", new[] { "Draconic DoomKnight, Minotaurofwar, Shadowrise Guard, DoomKnight Prime, Doombringer, Carnage, Crag and Bamboozle, Caesaristhedark, Shadow Destroyer", "Left", "*", "Legion Seal" }, "Gem of Mastery", false, 1);
            Bot.Wait.ForPickup("Emblem of Dage");
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyDiamondTokenOfDage(int quant)
    {
        if (Core.CheckInventory("Diamond Token of Dage", quant))
            return;
        if (!Core.CheckInventory("Legion Round 4 Medal"))
            ArmyLegionRound4Medal();
        if (!Core.CheckInventory("Legion Token", 50))
            ArmyLTs(50);
        /*Sell any existing Defeated Makai to sync up army before farming bosses*/
        if (Core.CheckInventory("Defeated Makai"))
            Core.SellItem("Defeated Makai", 0, true);
        Core.Logger("Defeated Makai sold to sync up your army!");
        Core.AddDrop("Diamond Token of Dage", "Legion Token");
        Core.RegisterQuests(4743);
        while (!Bot.ShouldExit && !Core.CheckInventory("Diamond Token of Dage", quant))
        {
            Core.EquipClass(ClassType.Farm);
            if (!Core.CheckInventory("Defeated Makai", 25))
                Core.PrivateRoomNumber = 100000;
                Core.KillMonster("tercessuinotlim", "m2", "Spawn", "Dark Makai", "Defeated Makai", 25, false, false,false);
            Core.PrivateRoomNumber = Army.getRoomNr();
            Core.EquipClass(ClassType.Solo);
            Adv.BestGear(GearBoost.Chaos);
            ArmyHunt("aqlesson", new[] {"Carnax"}, "Carnax Eye", true, 1);
            ArmyHunt("deepchaos", new[] {"Kathool"}, "Kathool Tentacle", true, 1);
            ArmyHunt("dflesson", new[] {"Fluffy the Dracolich"}, "Fluffy's Bones", true, 1);
            Adv.BestGear(GearBoost.Dragonkin);
            ArmyHunt("lair", new[] {"Red Dragon"}, "Red Dragon's Fang", true, 1);
            Adv.BestGear(GearBoost.Human);
            ArmyHunt("bloodtitan", new[] {"Blood Titan"}, "Blood Titan's Blade", true, 1);
            Bot.Drops.Pickup("Legion Token", "Diamond Token of Dage");
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyLegionRound4Medal()
    {
        if (Core.CheckInventory("Legion Round 4 Medal"))
            return;

        Core.AddDrop(legionMedals);
        Core.Logger("Farming Legion Round 4 Medal");

        /*Sell existing medals to sync up army. Not sure how to implement foreach to replace this mess*/
        if (Core.CheckInventory("Legion Round 4 Medal") || Core.CheckInventory("Legion Round 3 Medal") ||
            Core.CheckInventory("Legion Round 2 Medal") || Core.CheckInventory("Legion Round 1 Medal"))
        {
            Core.SellItem("Legion Round 1 Medal", 0, true);
            Core.SellItem("Legion Round 2 Medal", 0, true);
            Core.SellItem("Legion Round 3 Medal", 0, true);
            Core.SellItem("Legion Round 4 Medal", 0, true);
            Core.Logger("Legion Round Medals sold to sync up your army!");           
        }
        
        while (!Bot.ShouldExit && !Core.CheckInventory("Legion Round 4 Medal"))
        {
            Core.RegisterQuests(4738, 4739, 4740, 4741);
            if (!Core.CheckInventory("Legion Round 1 Medal"))
            {
                ArmyHunt("shadowblast", new[] {"Caesaristhedark"}, "Nation Rookie Defeated", true, 5);
                ArmyHunt("shadowblast", new[] {"Shadowrise Guard"}, "Shadowscythe Rookie Defeated", true, 5);
                Bot.Wait.ForDrop("Legion Round 1 Medal");
                Core.Logger("Medal 1 acquired");
            }
            if (!Core.CheckInventory("Legion Round 2 Medal"))
            {
                ArmyHunt("shadowblast", new[] {"Carnage"}, "Nation Veteran Defeated", true, 7);
                ArmyHunt("shadowblast", new[] {"Doombringer"}, "Shadowscythe Veteran Defeated", true, 7);
                Bot.Wait.ForDrop("Legion Round 2 Medal");
                Core.Logger("Medal 2 acquired");
            }
            if (!Core.CheckInventory("Legion Round 3 Medal"))
            {
                ArmyHunt("shadowblast", new[] {"Minotaurofwar"}, "Nation Elite Defeated", true, 10);
                ArmyHunt("shadowblast", new[] {"Draconic Doomknight"}, "Shadowscythe Elite Defeated", true, 10);
                Bot.Wait.ForDrop("Legion Round 3 Medal");
                Core.Logger("Medal 3 acquired");
            }
            ArmyHunt("shadowblast", new[] {"Thanatos"}, "Thanatos Vanquished", true, 1);
            Bot.Wait.ForDrop("Legion Round 4 Medal");
            Core.Logger("Medal 4 acquired");
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyDarkTokenOfDage(int quant)
    {
        if (Core.CheckInventory("Dark Token", quant))
            return;
        Core.AddDrop("Dark Token");
        Core.Logger($"Farming {quant} Dark Tokens");
        Core.EquipClass(ClassType.Farm);
        Adv.SmartEnhance(Bot.Player.CurrentClass.Name);
        Adv.BestGear(GearBoost.Human);
        Core.Logger($"Starting off with {Bot.Inventory.GetQuantity("Dark Token")} Dark Tokens");
        Core.RegisterQuests(6248, 6249, 6251);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Token", quant))
            ArmyHunt("seraphicwardage", new[] { "Seraphic Commander, Seraphic Soldier" }, "Seraphic Commanders Slain", true, 6);
        Core.CancelRegisteredQuests();
    }

    public void ArmyLTs(int quant)
    {
        if (Core.CheckInventory("Legion Token", quant))
            return;
        Core.AddDrop("Legion Token");
        Core.Logger($"Farming {quant} Legion Tokens");
        Core.EquipClass(ClassType.Farm);
        Adv.SmartEnhance(Bot.Player.CurrentClass.Name);
        Adv.BestGear(GearBoost.Human);
        Core.Logger($"Starting off with {Bot.Inventory.GetQuantity("Legion Token")} Legion Tokens");
        Core.RegisterQuests(4849);
        while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", 25000))
            ArmyHunt("dreadrock", new[] { "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary" }, "Dreadrock Enemy Recruited", true, 6);
        Core.CancelRegisteredQuests();
    }

    public void ArmyLF1(int quant)
    {
        if (Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

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
            ILDC.GetILDC(false);

        Core.AddDrop("Legion Token");
        Core.AddDrop(LRMaterials);
        Core.AddDrop(LF1);

        Core.Logger($"Farming {quant} Revenant's Spellscroll");
        Bot.Quests.UpdateQuest(2060);
        Core.RegisterQuests(6897);
        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory("Revenant's Spellscroll", quant))
        {
            Core.EquipClass(ClassType.Solo);
            Adv.BestGear(GearBoost.Undead);
            /*Sells non-full stacks to keep in sync for each LF1 quest item*/
            if (!Core.CheckInventory("Aeacus Empowered", 50))
                Core.SellItem("Aeacus Empowered", 0, true);
            ArmyHunt("judgement", new[] {"Ultra Aeacus"}, "Aeacus Empowered", false, 50);

            Core.EquipClass(ClassType.Farm);
            Adv.BestGear(GearBoost.dmgAll);
            
            if (!Core.CheckInventory("Tethered Soul", 300))
                Core.SellItem("Tethered Soul", 0 , true);
            ArmyHunt("revenant",  new[] {"Forgotten Soul"}, "Tethered Soul", false, 300);
            
            if (!Core.CheckInventory("Darkened Essence", 500))
                Core.SellItem("Darkened Essence", 0, true);
            ArmyHunt("shadowrealmpast", new[] { "Pure Shadowscythe, Shadow Guardian, Shadow Warrior" }, "Darkened Essence", false, 500);

            Bot.Quests.UpdateQuest(2061);
            Adv.BestGear(GearBoost.Undead);
            
            if (!Core.CheckInventory("Dracolich Contract", 1000))
                Core.SellItem("Dracolich Contract", 0, true);
            ArmyHunt("necrodungeon", new[] { "5 Headed Dracolich" }, "Dracolich Contract", false, 1000);

            Bot.Drops.Pickup("Revenant's Spellscroll");
            Core.Logger($"Completed x{i++}");
        }
        Core.CancelRegisteredQuests();
    }

    public void ArmyLF3(int quant)
    {
        Core.Logger($"Farming {quant} Exalted Crown");
        while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Crown", 10))
        {
            Core.RegisterQuests(6899);
            Core.BuyItem("underworld", 216, "Hooded Legion Cowl");
            /*This is the only not prefarmed item left to get*/
            ArmyDarkTokenOfDage(100);
            if (!Core.CheckInventory("Legion Token", 4000))
                ArmyLTs(4000);
            Bot.Drops.Pickup("Exalted Crown");
        }
        Core.CancelRegisteredQuests();
    }
}
