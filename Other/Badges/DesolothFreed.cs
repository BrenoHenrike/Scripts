//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/EtherstormWastes.cs
using RBot;

public class DesolothFreedBadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public EtherStormWastes ESW = new();

    public void ScriptMain(ScriptInterface bot)
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
