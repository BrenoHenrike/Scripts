//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/EtherstormWastes.cs
using Skua.Core.Interfaces;

public class DesolothFreedBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public EtherStormWastes ESW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasAchievement(13))
            return;

        ESW.StoryLine();

        Core.EnsureAccept(1418);
        Core.HuntMonster("desoloth", "Desoloth", "Desoloth Freed!");
        Core.EnsureComplete(1418);
    }
}
