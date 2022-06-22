//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using RBot;

public class ShatterGlassMaze
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        TOD.ShatterGlassMaze();

        Core.SetOptions(false);
    }
}