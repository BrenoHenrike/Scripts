//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class RangdaSeasonal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(7622))
            return;

        Story.PreLoad(this);

        // 7617 Gather Flowers
        if (!Story.QuestProgression(7617))
        {
            Core.EnsureAccept(7617);
            Core.HuntMonster("rangda", "Flowering Bush", "Red Rose");
            Core.HuntMonster("rangda", "Flowering Bush", "White Rose");
            Core.HuntMonster("rangda", "Flowering Bush", "Kanthil Flower");
            Core.HuntMonster("rangda", "Flowering Bush", "Telon Flower");
            Core.HuntMonster("rangda", "Flowering Bush", "Orchid");
            Core.HuntMonster("rangda", "Flowering Bush", "Jasmine");
            Core.HuntMonster("rangda", "Flowering Bush", "Kenanga Flower");
            Core.EnsureComplete(7617);
        }

        // 7618 Defeat the Minions
        Story.KillQuest(7618, "rangda", "Leyak");

        // 7619 Create a Barrier
        Story.MapItemQuest(7619, "rangda", 7526, 4);
        Story.KillQuest(7619, "rangda", "Leyak");

        // 7620 Purify the Tuyul
        Story.KillQuest(7620, "rangda", "Tuyul");

        // 7621 Give an Offering
        Story.MapItemQuest(7621, "rangda", 7527);
        Story.KillQuest(7621, "rangda", "Leyak");

        // 7622 Defeat Rangda
        Story.KillQuest(7622, "rangda", "Rangda");

        Core.Logger("Rangda Storyline Complete");
    }
}