//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class Generated_ArmyLightCasterFull
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
        sArmy.player7,
        sArmy.player8,
        sArmy.player9,
        sArmy.player10,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ArmyLightCasterFull();

        Core.SetOptions(false);
    }

    public void ArmyLightCasterFull()
        => Army.RunGeneratedAggroMon(map, monNames, questIDs, classtype, drops);
    private List<int> questIDs = new() { 4510, 4511, 4512 };
    private List<string> monNames = new() { "Underworld Hound", "Infernal Imp", "Fallen Knight", "Fallen Knight", "Fallen Knight", "Diabolical Warlord" };
    private List<string> drops = new() { "Lance of Time", "Guardian of Spirits' Blade", "Avatar of Death's Scythe", "Burning Blade" };
    private string map = "lostruinswar-87539";
    private ClassType classtype = ClassType.Farm;
}
