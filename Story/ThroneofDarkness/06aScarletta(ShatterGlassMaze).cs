//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class HedgeMaze
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        HedgeMaze_Questline();

        Core.SetOptions(false);
    }

    public void HedgeMaze_Questline()
    {
        if (Core.isCompletedBefore(5313))
            return;

        Story.MapItemQuest(QuestID: 5298, MapName: "hedgemaze", MapItemID: 4678);
        Story.KillQuest(QuestID: 5298, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
        Story.MapItemQuest(QuestID: 5299, MapName: "hedgemaze", MapItemID: 4679);
        Story.KillQuest(QuestID: 5299, MapName: "hedgemaze", MonsterNames: new[] { "Mirrored Shard", "Hedge Goblin", "Minotaur" });
        Story.KillQuest(QuestID: 5300, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
        Story.MapItemQuest(QuestID: 5301, MapName: "hedgemaze", MapItemID: 4680);
        Story.KillQuest(QuestID: 5302, MapName: "hedgemaze", MonsterName: "Hedge Goblin");
        Story.MapItemQuest(QuestID: 5303, MapName: "hedgemaze", MapItemID: 4681, Amount: 12);
        Story.KillQuest(QuestID: 5304, MapName: "hedgemaze", MonsterName: "Mirrored Shard");
        Story.MapItemQuest(QuestID: 5305, MapName: "hedgemaze", MapItemID: 4682);
        Story.KillQuest(QuestID: 5306, MapName: "hedgemaze", MonsterName: "Minotaur Prime");
        Story.MapItemQuest(QuestID: 5307, MapName: "hedgemaze", MapItemID: 4683);
        Story.MapItemQuest(QuestID: 5308, MapName: "hedgemaze", MapItemID: 4684);
        Story.MapItemQuest(QuestID: 5309, MapName: "hedgemaze", MapItemID: 4685, Amount: 5);
        Story.KillQuest(QuestID: 5310, MapName: "hedgemaze", MonsterName: "Hedge Goblin");
        Story.MapItemQuest(QuestID: 5311, MapName: "hedgemaze", MapItemID: 4686);
        Story.KillQuest(QuestID: 5312, MapName: "hedgemaze", MonsterName: "Shattered Knight");
        Story.KillQuest(QuestID: 5313, MapName: "hedgemaze", MonsterName: "Resurrected Minotaur");
    }
}