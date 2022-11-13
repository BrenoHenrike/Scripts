//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DreadForest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(8722))
            return;

        Story.PreLoad(this);

        // Empty Fealty 8711
        Story.KillQuest(8711, "dreadforest", "Treacherous Bandit");

        // Bitter Sweet-Talker 8712
        Story.MapItemQuest(8712, "dreadforest", 10267);
        Story.KillQuest(8712, "dreadforest", $"Noble’s Servant");

        // Fake It Till You Make It 8713
        Story.KillQuest(8713, "dreadforest", "Noble's Knight");

        // Decimation 8714
        Story.KillQuest(8714, "dreadforest", "Treacherous Bandit");

        // Warning Beacon 8715
        Story.MapItemQuest(8715, "dreadforest", 10268);
        Story.KillQuest(8715, "dreadforest", "Noble's Knight");

        // Strategy and Utility 8716
        Story.KillQuest(8716, "dreadforest", "Reignolds' Knight");

        // Runaway 8717
        Story.MapItemQuest(8717, "dreadforest", new[] { 10269, 10270, 10271 });

        // Make - Up Artist 8718
        Story.KillQuest(8718, "dreadforest", "Taxidermied Servant");

        // Piercing the Veil 8719
        Story.KillQuest(8719, "dreadforest", new[] { "Taxidermied Servant", "Reignolds' Knight" });

        // Any Means Necessary 8720
        Story.KillQuest(8720, "dreadforest", "Lord Reignolds");

        // Out of Sight, Out of Mind 8721
        Story.MapItemQuest(8721, "dreadforest", 10272);

        // Vulture Pickings 8722
        Story.KillQuest(8722, "dreadforest", new[] { "Reignolds' Knight", "Taxidermied Servant", "Lord Reignolds" });


    }
}
