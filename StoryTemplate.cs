//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class StoryTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SagaName();

        Core.SetOptions(false);
    }

    public void SagaName()
    {
        if (Story.QuestProgression(000))
            return;

        Story.PreLoad();

        Story.KillQuest(000, "mapname", "MonsterName");
        Story.KillQuest(000, "mapname", new[] { "Monstername", "Monstername" });
        Story.MapItemQuest(000, "mapname", 1, 1);
        Story.ChainQuest(000);
    }
}