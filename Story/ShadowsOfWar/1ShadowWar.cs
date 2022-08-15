//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ShadowWar.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
//cs_include Scripts/Story/Legion/DarkAlliance.cs
using RBot;

public class ShadowWar1
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public SOW SoW = new();
    public ShadowWar SW = new();
    public DarkAlly_Story DAlly = new();
    public DarkAlliance_Story DAlliance = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SoW.ShadowWar();

        Core.SetOptions(false);
    }
}
