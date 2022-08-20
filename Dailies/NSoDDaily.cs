//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class NSODDaily
{
    public IScriptInterface Bot { get; set; }
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.NSoDDaily();

        Core.SetOptions(false);
    }
}


