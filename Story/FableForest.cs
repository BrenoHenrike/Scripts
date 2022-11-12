//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FableForest
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
        if (Core.isCompletedBefore(3313))
            return;

        Story.PreLoad(this);

        //Cowering Zardman Quests
        //Axe Marks the Spot 3300
        Story.KillQuest(3300, "greendragon", "Greenguard Dragon");

        //Get Choppin' 3301
        Story.MapItemQuest(3301, "fableforest", 2425);

        //Fae Quests
        //Fight Fire with Fire Elementals 3302
        Story.KillQuest(3302, "fableforest", "Fire Elemental");

        // Man's Best Fiend 3304
        Story.KillQuest(3304, "fableforest", "Bloodwolf");

        // Water Softener 3305
        Story.KillQuest(3305, "fableforest", "Water Elemental");

        // Swimming with Sneevils 3306
        Story.KillQuest(3306, "fableforest", "Aqueevil");

        // 100% Natural 3307
        Story.KillQuest(3307, "fableforest", "Earth Elemental");

        // Conquer the Satyr 3308
        Story.KillQuest(3308, "fableforest", "Undead Satyr");

        // Air Heads 3309
        Story.KillQuest(3309, "fableforest", "Wind Elemental");

        // The Forest & Furious 3310
        Story.KillQuest(3310, "fableforest", "Forest Fury");

        // Mystery Tree 3311
        Story.MapItemQuest(3311, "fableforest", 2424);

        // Guidance from the Guardian 3313
        Story.KillQuest(3313, "fableforest", "Forest Guardian");
    }
}