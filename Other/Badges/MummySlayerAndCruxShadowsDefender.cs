//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/CruxShip.cs
using Skua.Core.Interfaces;

public class MummySlayerAndCruxShadowsDefender
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CruxShip CS = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge("Mummy Slayer") || Core.HasWebBadge("CruxShadows Defender")) 
        {
            Core.Logger("Already have the Mummy Slayer and CruxShadows Defender badge");
            return;
        }

        Core.Logger("Doing CruxShip story for Mummy Slayer and CruxShadows Defender badge");
        CS.StoryLine();

    }

}
