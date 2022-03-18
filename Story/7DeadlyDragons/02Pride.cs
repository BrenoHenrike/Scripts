//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Pride
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        PrideSaga();

        Core.SetOptions(false);
    }

    public void PrideSaga()
    {
        if (Core.isCompletedBefore(5926))
            return;

        // Defeat the Drakel
        Story.KillQuest(5917, "pride", "Storm Drakel");
        // Don’t Get Zapped!
        Story.KillQuest(5918, "pride", "Ball Lightning");
        // Get Grounded
        Story.KillQuest(5919, "pride", "Rubber Treeant ");
        // Get the Key
        Story.KillQuest(5920, "pride", "Cellar Guard");
        // Get the Boots
        Story.MapItemQuest(5921, "pride", 5351, 1);
        Story.MapItemQuest(5921, "pride", 5352, 6);
        // Make Lightning Rods
        Story.KillQuest(5922, "pride", "Storm Drakel");
        Story.MapItemQuest(5922, "pride", 5353, 8);
        // Free the Villagers
        Story.KillQuest(5923, "pride", "Drakel Guard");
        Story.MapItemQuest(5923, "pride", 5354, 4);
        // Open the Gate
        Story.KillQuest(5924, "pride", "Elite Guard");
        Story.MapItemQuest(5924, "pride", 5355, 1);
        // Fight Your Way Throug
        Story.KillQuest(5925, "pride", "Storm Drakel");
        Story.MapItemQuest(5925, "pride", 5356, 1);
        // Defeat Valsarian
        Story.KillQuest(5926, "pride", "Valsarian");

    }
}
