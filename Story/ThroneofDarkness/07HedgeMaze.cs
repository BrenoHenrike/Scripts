//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class HedgeMaze
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        HedgeMaze_Questline();

        Core.SetOptions(false);
    }

    public void HedgeMaze_Questline()
    {
        Core.MapItemQuest(QuestID: 5298, MapName: "hedgemaze", MapItemID: 4678);
        Core.KillQuest(QuestID: 5298, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
        Core.MapItemQuest(QuestID: 5299, MapName: "hedgemaze", MapItemID: 4679);
        Core.KillQuest(QuestID: 5299, MapName: "hedgemaze", MonsterNames: new[] { "Mirrored Shard", "Hedge Goblin", "Minotaur" });
        Core.KillQuest(QuestID: 5300, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
        Core.MapItemQuest(QuestID: 5301, MapName: "hedgemaze", MapItemID: 4680);
        Core.KillQuest(QuestID: 5302, MapName: "hedgemaze", MonsterName: "Hedge Goblin");
        Core.MapItemQuest(QuestID: 5303, MapName: "hedgemaze", MapItemID: 4681, Amount: 12);
        Core.KillQuest(QuestID: 5304, MapName: "hedgemaze", MonsterName: "Mirrored Shard");
        Core.MapItemQuest(QuestID: 5305, MapName: "hedgemaze", MapItemID: 4682);
        Core.KillQuest(QuestID: 5306, MapName: "hedgemaze", MonsterName: "Minotaur Prime");
        Core.MapItemQuest(QuestID: 5307, MapName: "hedgemaze", MapItemID: 4683);
        Core.MapItemQuest(QuestID: 5308, MapName: "hedgemaze", MapItemID: 4684);
        Core.MapItemQuest(QuestID: 5309, MapName: "hedgemaze", MapItemID: 4685, Amount: 5);
        Core.KillQuest(QuestID: 5310, MapName: "hedgemaze", MonsterName: "Hedge Goblin");
        Core.MapItemQuest(QuestID: 5311, MapName: "hedgemaze", MapItemID: 4686);
        Core.KillQuest(QuestID: 5312, MapName: "hedgemaze", MonsterName: "Shattered Knight");
        Core.KillQuest(QuestID: 5313, MapName: "hedgemaze", MonsterName: "Resurrected Minotaur");
        Core.Join("party");
    }
}