//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using RBot;

public class FarmAllDailys
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new CoreDailies();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Daily.DoAllDailys();

        Core.SetOptions(false);
    }
}