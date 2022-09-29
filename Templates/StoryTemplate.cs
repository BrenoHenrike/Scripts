//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class StoryTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SagaName();

        Core.SetOptions(false);
    }

    public void SagaName()
    {
        if (Core.isCompletedBefore(000))
            return;

        Story.PreLoad(this);

        Story.KillQuest(000, "mapname", "MonsterName");
        Story.KillQuest(000, "mapname", new[] { "Monstername", "Monstername" });
        Story.MapItemQuest(000, "mapname", 1, 1);
        Story.MapItemQuest(000, "mapname", new[] { 000, 000, 000, });
        Story.ChainQuest(000);
    }
}