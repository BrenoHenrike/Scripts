//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class CustomAggroMonTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();

    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMonTemplate";
    public List<IOption> Options = new List<IOption>()
    {
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

        CustomAggroMon();

        Core.SetOptions(false);
    }

    public void CustomAggroMon()
    {
        Bot.Drops.Stop();
        Core.EquipClass(classtype);

        if (questIDs.Count > 0)
            Core.RegisterQuests(questIDs.ToArray());

        Army.SmartAggroMonStart(map, monNames.ToArray());
        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);

        if (questIDs.Count > 0)
            Core.CancelRegisteredQuests();
    }
    private List<int> questIDs = new() { };
    private List<string> monNames = new() { };
    private string map = "";
    private ClassType classtype = ClassType.None;
}