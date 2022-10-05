//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyLegionToken
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public CoreLegion Legion = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLegionToken";
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
        sCore.SkipOptions,
        new Option<Method>("Method", "Which method to get LTs?", "Choose your method", Method.Dreadrock)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
            Bot.Config.Configure();

        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions(disableClassSwap: false);
        bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<Method>("Method"));

        Core.SetOptions(false);
    }


    public void Setup(Method Method, int quant = 25000)
    {
        Legion.JoinLegion();
        
        Core.EquipClass(ClassType.Farm);
        if (Method.ToString() == "Dreadrock")
            GetItem("dreadrock", "*", 4850, "Legion Token", quant);
        if (Method.ToString() == "Shogun_Paragon_Pet")
            GetItem("fotia", "*", 5755, "Legion Token", quant);
        else
        {
            Core.EquipClass(ClassType.Solo);
            GetItem("legionarena", "Legion Fiend Rider", 6743, "Legion Token", quant);
        }
    }

    public void GetItem(string map = null, string Monster = null, int questID = 000, string item = null, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        if (!Bot.Quests.Active.Contains(QuestData))
            Core.RegisterQuests(questID);

        Army.SmartAggroMonStart(map, Monster);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Core.HuntMonster(map, Monster);
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
    private string[] Loot = { "Legion Token" };
    public enum Method
    {
        Dreadrock = 0,
        Shogun_Paragon_Pet = 1,
        Legion_Arena = 2,
    }
}