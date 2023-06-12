/*
name: Army Blacksmithing Rep
description: Farm reputation with your army. Faction: Blacksmithing
tags: army, reputation, blacksmithing
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyBlackSmithRep
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyBlackSmithRep";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
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

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Creature Shard", "Monster Trophy", "Hydra Scale Piece" });

        Core.OneTimeMessage("Urgent", "Please Make sure your goto is on in ingame settings so the buttlering system works properly [turn it back off after your done armying please.]", forcedMessageBox: true);
        
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        Core.AddDrop("Creature Shard", "Monster Trophy", "Hydra Scale Piece");
        Core.RegisterQuests(8736);
        while (!Bot.ShouldExit && Farm.FactionRank("Blacksmithing") < 11)
        {
            Core.EquipClass(ClassType.Solo);
            Armykill("maul", new[] { "Creature Creation" }, "Creature Shard", isTemp: false, 1);
            Armykill("towerofdoom", new[] { "Dread Klunk" }, "Monster Trophy", isTemp: false, 15);
            Core.EquipClass(ClassType.Farm);
            Armykill("hydrachallenge", new[] { "Hydra Head 75" }, "Hydra Scale Piece", isTemp: false, 75);
        }
        Core.CancelRegisteredQuests();
    }

    void Armykill(string map = null, string[] monsters = null, string item = null, bool isTemp = false, int quant = 1)
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

        Army.waitForParty(map, item);
        AggroSetup(map);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);

    }

    void AggroSetup(string map = null)
    {
        if (Bot.Map.Name == null)
            return;

        if (Bot.Map.Name == "MaptoAggro")
        {
            Army.AggroMonCells("cell1", "cell2", "cell3");
            Army.AggroMonStart("MaptoAggro");
            Army.DivideOnCells("cell1", "cell2", "cell3");
        }
    }
}

/*old stuff

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
    }
    
    
        GetItems("map", new[] { "mob", "mob" }, 000, new[] { "item", "item" }); //*ONLY* leave Loot Empty for AC(only) quest rewards."
        GetItem("map", "mob", 000, "item"); //*ONLY* leave Loot Empty for AC(only) quest rewards."

        //Examples;
        //Setup("map", new[] { "mob", "mob" }, 000); 
        //Setup("map", new[] { "mob", "mob" }, 000, new[] {"item", "item"});
}*/
