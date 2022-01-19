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
            Core.KillQuest(QuestID: 5298, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
            Core.MapItemQuest(QuestID: 5299, MapName: "hedgemaze", MapItemID: 4679);
            Core.KillQuest(QuestID: 5299, MapName: "hedgemaze", MonsterNames: new[] { "Mirrored Shard", "Hedge Goblin", "Minotaur" } );
            Core.KillQuest(QuestID: 5300, MapName: "hedgemaze", MonsterName: "Knight's Reflection");
            Core.MapItemQuest(QuestID: 5301, MapName: "hedgemaze", MapItemID: 4680, hasFollowup: false);
    }
}