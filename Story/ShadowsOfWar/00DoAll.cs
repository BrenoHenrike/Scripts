//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ShadowWar.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
//cs_include Scripts/Story/Legion/DarkAlliance.cs
using RBot;

public class ShadowWarAll
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public SOW SoW = new();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SoW.CompleteCoreSoW();

        Core.SetOptions(false);
    }
}
