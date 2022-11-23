//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class MineCrafting
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreBLOD BLOD = new();

    public List<IOption> Options = new()
    {
        new Option<MineCraftingMetalsEnum>("metals", "Which Metal", "Select the metal you wish to get here")
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.UnlockMineCrafting();
        Daily.MineCrafting(new[] { Bot.Config!.Get<MineCraftingMetalsEnum>("metals").ToString() }, 10, false);

        Core.SetOptions(false);
    }
}