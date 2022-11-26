//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyDarkonErrands
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyDarkonErrands";
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
        CoreBots.Instance.SkipOptions,
        new Option<Method>("Method", "Which method to get Darkon's Receipt?", "Choose your method", Method.First_Errands_Weak_Team)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Darkon's Receipt");

        Core.SetOptions(disableClassSwap: false);
        Bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<Method>("Method"), 222);

        Core.SetOptions(false);
    }


    public void Setup(Method Method, int quant = 222)
    {
        Core.EquipClass(ClassType.Solo);
        if (Method.ToString() == "Third_Errands")
            GetItem("tercessuinotlim", new[] { "Nulgath" }, 7326, "Darkon's Receipt", quant);

        else if (Method.ToString() == "Second_Errands")
            GetItem("doomvault", new[] { "Binky" }, 7325, "Darkon's Receipt", quant);
            
        else if (Method.ToString() == "First_Errands_Strong_Team")
        {
            Core.EquipClass(ClassType.Farm);
            Bot.Quests.UpdateQuest(3481);
            GetItem("towerofdoom7", new[] { "Dread Gorillaphant" }, 7324, "Darkon's Receipt", quant);
        }
        else
        {
            Core.EquipClass(ClassType.Farm);
            GetItem("arcangrove", new[] { "Gorillaphant" }, 7324, "Darkon's Receipt", quant);
        }
    }

    public void GetItem(string map = null, string[] monsters = null, int questID = 000, string item = null, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        Core.RegisterQuests(questID);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monster);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    public enum Method
    {
        First_Errands_Weak_Team = 0,
        First_Errands_Strong_Team = 1,
        Second_Errands = 2,
        Third_Errands = 3,
    }
}