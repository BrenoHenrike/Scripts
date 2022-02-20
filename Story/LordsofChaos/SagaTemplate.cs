//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class SagaTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.AcceptandCompleteTries = 5;
        Core.SetOptions();

        SagaName();

        Core.SetOptions(false);
    }

    public void SagaName()
    {
        if (Story.QuestProgression(000))
            return;

        Story.KillQuest(QuestID: 000, MapName: "mapname", MonsterName: "MonsterName");
        Story.KillQuest(QuestID: 000, MapName: "mapname", MonsterNames: new[] { "Monstername", "Monstername" });
        Story.MapItemQuest(QuestID: 000, MapName: "mapname", MapItemID: 1, Amount: 1);
        Story.ChainQuest(QuestID: 000);
    }
}