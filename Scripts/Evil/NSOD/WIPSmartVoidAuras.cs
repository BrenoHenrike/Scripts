//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Evil/NSOD/WIPCoreSNOD.cs
using RBot;

public class SmartVoidAuras
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNSOD NSOD = new CoreNSOD();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        NSOD.VoidAuras();

        Core.SetOptions(false);
    }
}