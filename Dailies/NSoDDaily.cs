//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class NSoDDaily
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Daily.NSoDDaily();

        Core.SetOptions(false);
    }
}


