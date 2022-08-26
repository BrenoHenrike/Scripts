//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs

using Skua.Core.Interfaces;

public class BrightShadow
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public SoC SoC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoC.BrightShadow();

        Core.SetOptions(false);
    }
}
