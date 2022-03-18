//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Greed
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GreedSaga();

        Core.SetOptions(false);
    }

    public void GreedSaga()
    {
        if (Core.isCompletedBefore(5943))
            return;

        // Looting is for Sneevils
        Story.KillQuest(5934, "greed", "Sneevil Looter");
        // Jumping in Puddles
        Story.MapItemQuest(5935, "greed", 5372, 1);
        // Pick the Right Chest
        Story.MapItemQuest(5936, "greed", 5373, 1);
        // Gelatin is Nasty
        Story.KillQuest(5937, "greed", "Jelly-Like Cube");
        // Explore the Cave
        Story.MapItemQuest(5938, "greed", 5374, 5);
        // Disarm the Trap
        Story.MapItemQuest(5939, "greed", 5377, 1);
        Story.MapItemQuest(5939, "greed", 5378, 1);
        // Crystal-eyes
        Story.KillQuest(5940, "greed", new[] { "Ice Crystal", "Glacial Horror"});
        // Go-bolds?
        Story.KillQuest(5941, "greed", "Kobold");
        Story.MapItemQuest(5941, "greed", 5375, 1);
        // Follow the Trail
        Story.MapItemQuest(5942, "greed", 5376, 3);
        // Get Rid of Greed
        Story.KillQuest(5943, "greed", "Goregold");

    }
}
