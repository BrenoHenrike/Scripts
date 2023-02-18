/*
name: null
description: null
tags: null
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

public class ArmyTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyTemplatev2";
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

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "stuff", "you", "don't", "want", "banked" });

        Core.SetOptions(disableClassSwap: true);

        Setup();
        Core.SetOptions(false);
    }

    public void Setup()
    {
        Core.EquipClass(ClassType.Farm);
        // Core.EquipClass(ClassType.Solo);
        Core.AddDrop("item");

        while (!Bot.ShouldExit && !Core.CheckInventory("item", 1))
        {
            Core.EnsureAccept(0000);
            Armykill("map", new[] { "mob", "mob" }, "item", isTemp: false, 1);
            Core.EnsureComplete(0000);
        }
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

        Core.Join(map);
        WaitCheck();
        AggroSetup(map);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);

        void WaitCheck()
        {
            while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
            {
                Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
                Bot.Sleep(5000);
            }
            Core.Logger($"Squad All Gathered [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
        }
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
