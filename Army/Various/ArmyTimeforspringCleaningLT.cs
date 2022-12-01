//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class Generated_ArmyTimeforspringCleaningLT
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ArmyTimeforspringCleaningLT();

        Core.SetOptions(false);
    }

    public void ArmyTimeforspringCleaningLT()
    {
        if (Core.CheckInventory("Shogun Paragon Pet"))
            questIDs = new() { 5755 };
        else if (Core.CheckInventory("Shogun Dage Pet"))
            questIDs = new() { 5756 };
        else if (Core.CheckInventory("Paragon Fiend Quest Pet"))
        {
            if (Bot.Inventory.GetItem("Paragon Fiend Quest Pet").ID == 47578)
                questIDs = new() { 6750 };
            else if (Bot.Inventory.GetItem("Paragon Fiend Quest Pet").ID == 47614)
                questIDs = new() { 6756 };
        }
        else if (Core.CheckInventory("Paragon Ringbearer"))
            questIDs = new() { 7073 };

        Army.RunGeneratedAggroMon(map, monNames, questIDs, classtype, drops);
    }
    private List<int> questIDs = new();
    private List<string> monNames = new() { "Fotia Elemental", "Fotia Spirit" };
    private List<string> drops = new() { "Legion Token" };
    private string map = "fotia";
    private ClassType classtype = ClassType.Farm;
}
