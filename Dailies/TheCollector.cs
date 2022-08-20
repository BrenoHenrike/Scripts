//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class CollectorClass
{
    public IScriptInterface Bot { get; set; }
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.CollectorClass();

        Core.SetOptions(false);
    }
}