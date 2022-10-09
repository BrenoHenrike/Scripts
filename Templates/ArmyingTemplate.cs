//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class ArmyTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyTemplate";
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
        sCore.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "add loot here" }); // if loot is Empty for Quest(ac) rewards, delete this.

        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        GetItems("map", new[] { "mob", "mob" }, 000, new[] { "item", "item" }); //*ONLY* leave Loot Empty for AC(only) quest rewards."
        GetItem("map", "mob", 000, "item"); //*ONLY* leave Loot Empty for AC(only) quest rewards."

        //Examples;
        //Setup("map", new[] { "mob", "mob" }, 000); 
        //Setup("map", new[] { "mob", "mob" }, 000, new[] {"item", "item"});

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
                    Core.Logger($"{item} Has Been Excluded as it is not a AC item.");
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
}