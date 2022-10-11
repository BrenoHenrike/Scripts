//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class HeartOfTheSeaStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HeartOfTheSea();

        Core.SetOptions(false);
    }

    public void HeartOfTheSea()
    {
        if (!Core.isSeasonalMapActive("HeartOfTheSea"))
            return;
        if (Core.isCompletedBefore(6520))
            return;

        Story.PreLoad(this);

        // Puff Those Fish 6511
        Story.KillQuest(6511, "HeartOfTheSea", "Pufferfish");

        // Question the Merdraconians 6512
        Story.KillQuest(6512, "HeartOfTheSea", "Merdraconian");

        // Reveal the Ship 6513
        Story.MapItemQuest(6513, "HeartOfTheSea", 5999, 5);
        Story.KillQuest(6513, "HeartOfTheSea", "Jellyfish");

        // Find the Way In 6514
        Story.MapItemQuest(6514, "HeartOfTheSea", 6000);

        // Take Some Treasure 6515
        Story.MapItemQuest(6515, "HeartOfTheSea", 6001, 5);

        // Get Rid of the Grislytooth Pirates 6516
        Story.KillQuest(6516, "HeartOfTheSea", "Grislytooth Pirate");

        // Interrogate the Pirates 6517
        Story.KillQuest(6517, "HeartOfTheSea", new[] { "Boatswain Rotbelly", "Quartermaster Greenfin", "First Mate Blackfang" });

        // Find the Hold 6518
        Story.MapItemQuest(6518, "HeartOfTheSea", 6002, 5);
        Story.KillQuest(6518, "HeartOfTheSea", "Jellyfish");

        // Get that Treasure!  6519
        Story.MapItemQuest(6519, "HeartOfTheSea", 6003);

        // Take out Scurvyfins McKrill 6520
        Story.KillQuest(6520, "HeartOfTheSea", "Scurvyfins McKrill");
    }
}
