//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class Generated_ArmyDreadRockLT
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

        ArmyDreadRockLT();

        Core.SetOptions(false);
    }

    public void ArmyDreadRockLT()
        => Army.RunGeneratedAggroMon(map, monNames, questIDs, classtype, drops);
    private List<int> questIDs = new() { 4849 };
    private List<string> monNames = new() { "Legion Sentinel", "Shadowknight", "Void Mercenary" };
    private List<string> drops = new() { "Legion Token" };
    private string map = "dreadrock";
    private ClassType classtype = ClassType.Farm;
}
