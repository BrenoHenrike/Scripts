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

        Story.PreLoad();

        Story.MapItemQuest(5298, "hedgemaze", 4678);

        Story.KillQuest(5298, "hedgemaze", "Knight's Reflection");

        Story.MapItemQuest(5299, "hedgemaze", 4679);

        Story.KillQuest(5299, "hedgemaze", new[] { "Mirrored Shard", "Hedge Goblin", "Minotaur" });

        Story.KillQuest(5300, "hedgemaze", "Knight's Reflection");

        Story.MapItemQuest(5301, "hedgemaze", 4680);

        Story.KillQuest(5302, "hedgemaze", "Hedge Goblin");

        Story.MapItemQuest(5303, "hedgemaze", 4681, 12);

        Story.KillQuest(5304, "hedgemaze", "Mirrored Shard");

        Story.MapItemQuest(5305, "hedgemaze", 4682);

        Story.KillQuest(5306, "hedgemaze", "Minotaur Prime");

        Story.MapItemQuest(5307, "hedgemaze", 4683);

        Story.MapItemQuest(5308, "hedgemaze", 4684);

        Story.MapItemQuest(5309, "hedgemaze", 4685, 5);

        Story.KillQuest(5310, "hedgemaze", "Hedge Goblin");

        Story.MapItemQuest(5311, "hedgemaze", 4686);

        Story.KillQuest(5312, "hedgemaze", "Shattered Knight");

        Story.KillQuest(5313, "hedgemaze", "Resurrected Minotaur");
    }
}