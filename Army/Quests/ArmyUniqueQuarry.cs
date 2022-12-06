//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyUniqueQuarry
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyUniqueQuarry";
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
        Core.BankingBlackList.AddRange(new[]
        {"Chaos Sphinx", "Chaoroot", "Kathool Annihilator", "Chaotic Manticore Head", "Chaos Tentacle", "Chaos Dragon Slayer", "HarpyHunter", "Naga Baas Pet"});

        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        Quest QuestData = Core.EnsureLoad(9000);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        // Core.EquipClass(ClassType.Solo);
        // Core.AddDrop("Chaos Sphinx", "Chaoroot", "Kathool Annihilator", "Chaotic Manticore Head", "Chaos Tentacle", "Chaos Dragon Slayer", "HarpyHunter", "Naga Baas Pet");
        foreach (ItemBase item in RequiredItems.Concat(QuestReward))
            Bot.Drops.Add(item.ID);

        Core.EnsureAccept(9000);

        Core.EquipClass(ClassType.Farm);
        Armykill("chaoswar", new[] { "*" }, "Chaos Tentacle", isTemp: false, 300);
        Core.EquipClass(ClassType.Solo);
        Armykill("sandcastle", new[] { "Chaos Sphinx" }, "Chaos Sphinx", isTemp: false);
        Armykill("deepchaos", new[] { "Kathool" }, "Kathool Annihilator", isTemp: false);
        Armykill("castleroof", new[] { "Chaos Dragon" }, "Chaos Dragon Slayer", isTemp: false);
        Armykill("mirrorportal", new[] { "Chaos Harpy" }, "HarpyHunter", isTemp: false);
        Armykill("orecavern", new[] { "Naga Baas" }, "Naga Baas Pet", isTemp: false);
        Armykill("venomvaults", new[] { "Ultra Manticore" }, "Treasure Vault Key", isTemp: false);
        Adv.BuyItem("venomvaults", 585, "Chaotic Manticore Head");
        Adv.BuyItem("tercessuinotlim", 1951, "Chaoroot", 30);
        Core.EnsureComplete(9000);
        foreach (ItemBase item in QuestReward)
            Core.ToBank(item.ID);
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

        if (Bot.Map.Name == "chaoswar")
        {
            if (Bot.Config.Get<int>("armysize") <= 3)
            {
                Army.AggroMonCells("r2");
                Army.AggroMonStart("chaoswar");
                Army.DivideOnCells("r2");
            }
            else 
            {                
                Army.AggroMonCells("r1", "r2");
                Army.AggroMonStart("chaoswar");
                Army.DivideOnCells("r1", "r2");
            }
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