//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class ArmyPrimaticSeams
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();
    private CoreSoW SoW = new();

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
        sArmy.player7,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        if (!Core.isCompletedBefore(8814))
        {
            SoW.CompleteCoreSoW();
        }
        
        ArmyPS();

        Core.SetOptions(false);
    }

    public void ArmyPS()
        => Army.RunGeneratedAggroMon(map, monNames, questIDs, classtype, drops);
    private List<int> questIDs = new() { 8814, 8815 };
    private List<string> monNames = new() { "Decaying Locust", "Distorted Imp", "Mumbler" };
    private List<string> drops = new() { "Prismatic Seams" };
    private string map = "streamwar";
    private ClassType classtype = ClassType.Farm;
}
