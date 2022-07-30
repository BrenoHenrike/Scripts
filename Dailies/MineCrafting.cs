//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;

public class MineCrafting
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreBLOD BLOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.UnlockMineCrafting();
        Daily.MineCrafting(ToBank: true);

        Core.SetOptions(false);
    }
}