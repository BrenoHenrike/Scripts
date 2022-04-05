//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using RBot;

public class MineCrafting
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreBLOD BLOD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.UnlockMineCrafting();
        Daily.MineCrafting();

        Core.SetOptions(false);
    }
}