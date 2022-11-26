//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Army/ArmyLegionFealty2.cs
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
        sCore.SkipOptions
    };
    public string[] LR =
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
    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions(disableClassSwap: true);
        Bot.Options.RestPackets = false;
        
        FarmLR();
        
        Core.SetOptions(false);
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
    /* public void GetItem(string map = null, string Monster = null, int questID = 000, string item = null, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

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
    } */
    public void GetItem(string map = null, string[] monsters = null, int questID = 000, string item = null, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        if (!Bot.Quests.Active.Contains(QuestData))
            Core.RegisterQuests(questID);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monster);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
    public void GoldFarm()
    {   Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        Farm.ToggleBoost(BoostType.Gold);
        Core.RegisterQuests(3991, 3992);
        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
        Army.AggroMonStart("battlegrounde");
        Army.DivideOnCells("r5", "r4", "r3", "r2");
        /*Farms 500k extra just to be safe with enhancement costs*/
        while (!Bot.ShouldExit && Bot.Player.Gold < 5500000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }
    public void RevenantSpellscroll(int quant = 20)
    {
        if (Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

        Legion.JoinLegion();

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
            ILDC.GetILDC(false);
        }

        Core.AddDrop("Legion Token");
        Core.AddDrop(LR);
        Core.AddDrop(LF1);

        Farm.EvilREP();

        int i = 1;
        Core.Logger($"Farming {quant} Revenant's Spellscroll");
        Bot.Quests.UpdateQuest(2060);
        while (!Bot.ShouldExit && !Core.CheckInventory("Revenant's Spellscroll", quant))
        {
            Core.EnsureAccept(6897);

            Core.EquipClass(ClassType.Solo);
            Adv.BestGear(GearBoost.Undead);
            Core.KillMonster("judgement", "r10a", "Left", "Ultra Aeacus", "Aeacus Empowered", 50, false, publicRoom: true);

            Core.EquipClass(ClassType.Farm);
            Adv.BestGear(GearBoost.dmgAll);
            Core.KillMonster("revenant", "r2", "Left", "*", "Tethered Soul", 300, false);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Darkened Essence", 500, false);
            Adv.BestGear(GearBoost.Undead);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Dracolich Contract", 1000, false, publicRoom: true);

            Core.EnsureComplete(6897);
            Bot.Drops.Pickup("Revenant's Spellscroll");
            Core.Logger($"Completed x{i++}");
        }
    }
    public void ArmyLF3(int quant = 10)
    {
        int i = 1;
        Core.Logger($"Farming {quant} Exalted Crown");
        while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Crown", 10))
        {
            Core.EnsureAccept(6899);
            Core.BuyItem("underworld", 216, "Hooded Legion Cowl");

            Legion.DarkToken(100);

            Core.EnsureComplete(6899);
            Bot.Drops.Pickup("Exalted Crown");
            Core.Logger($"Completed x{i++}");
        }
    }
    public void FarmLR()
    {
        Legion.JoinLegion();
        Seraph.SeraphicWar_Questline();
        Core.BankingBlackList.AddRange(new[] { "LR, LF1, LF2, LF3" }); // if loot is Empty for Quest(ac) rewards, delete this.

        /*
        ********************************************************************************
        ******************************  PREFARM ZONE************************************
        ********************************************************************************
        
        Step 1: Evil Rank 10 prefarm (need to handle 5 player room limit somehow)*/
        /*Step 2: 24000 LTs Dreadrock Aggromon prefarm*/
        Core.EquipClass(ClassType.Farm);
        GetItem("dreadrock", new[] { "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary" }, 4849, "Legion Token", 24000);
        /*Step 3: 5,000,000+ gold for LF3 Helmet buying prefarm*/
        GoldFarm();
        /*Step 4: 3000 Dage Favor prefarm*/
        GetItem("evilwarnul", new[] { "Skull Warrior", "Undead Infantry" }, 6897, "Dage Favor", 3000);
        /*Step 5: 10 Emblem of Dage prefarm*/
        Legion.EmblemofDage(10);
        /*Step 6: 300 Diamond Token of Dage prefarm*/
        Legion.DiamondTokenofDage(300);
        /*Step 7: 600 Dark Token prefarm*/
        Legion.DarkToken(600);
        /*Step 8: LF1 1 run (skip Shadow Essences (4 man limit) and Tethered Soul (3 man limit))*/
        CoreLR.RevenantSpellscroll(1);
        /*Step 9: LF2 1 run*/
        CoreLR.ConquestWreath(1);
        /*Step 10: LF3 1 run*/
        CoreLR.ExaltedCrown(1);
        /*Step 11: 12000 LTs Dreadrock Aggromon prefarm*/
        Core.EquipClass(ClassType.Farm);
        GetItem("dreadrock", new[] { "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary" }, 4849, "Legion Token", 12000);
        /*
        ********************************************************************************
        **********************************FINISH****************************************
        ********************************************************************************
        */
        ArmyLF2.Setup();
        CoreLR.RevenantSpellscroll();
        ArmyLF3();
        CoreLR.GetLR(true);
    }
}