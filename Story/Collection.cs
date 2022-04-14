//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class Collection
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CollectionStory();

        Core.SetOptions(false);
    }

    public void CollectionStory()
    {
        if (Core.isCompletedBefore(1348))
            return;

        Story.PreLoad();

        // This Town in a Desktop Globe
        Story.MapItemQuest(1293, "Terrarium", 586);

        // Dust Bunnies Can't Hide
        Story.KillQuest(1294, "Terrarium", "Dustbunny of Doom");

        // Au Contr-air holes
        Story.MapItemQuest(1295, "Terrarium", 587, 10);

        // Hero Eats Everything. Even Pellets
        Story.MapItemQuest(1296, "Terrarium", 588, 12);

        // The Moth Defeats the Man?
        Story.KillQuest(1297, "Terrarium", "Death on Wings");

        // Can you find them?
        Story.MapItemQuest(1298, "Terrarium", 589);
        Story.MapItemQuest(1298, "Terrarium", 590);
        Story.MapItemQuest(1298, "Terrarium", 591);
        Story.MapItemQuest(1298, "Terrarium", 592);
        Story.MapItemQuest(1298, "Terrarium", 593);

        // You've Been Looking Everywhere!
        Story.MapItemQuest(1299, "Terrarium", 593);
        Story.MapItemQuest(1299, "Terrarium", 594);
        Story.MapItemQuest(1299, "Terrarium", 595);
        Story.MapItemQuest(1299, "Terrarium", 596);
        Story.MapItemQuest(1299, "Terrarium", 604, AutoCompleteQuest: false);

        // Go for Grease!
        Story.KillQuest(1300, "Terrarium", "Death on Wings");

        // Doppelganger Wants to Hit You
        Story.KillQuest(1308, "Terrarium", new[] { "Doppleganger of Will", "Doppleganger of Fred" });

        // Catapult Climb
        Story.MapItemQuest(1309, "Terrarium", 604);

        // The Treasure that You Seek - 976
        Story.KillQuest(1339, "prehistoric", "Gigantosaurus");

        // These Trees - 1340
        Story.MapItemQuest(1340, "prehistoric", 630, 11);

        // Ewwww... Totally Gross - 1341
        Story.KillQuest(1341, "prehistoric", "Gigantosaurus");

        // The Eggs, Exciting and New - 1342
        Story.MapItemQuest(1342, "prehistoric", 631, 4);

        // Someday (Today) Their Mother Will Die - 1343
        Story.KillQuest(1343, "prehistoric", "Mother TerrorDOOMKill");

        // More Aliens Than You Can Handle?
        Story.KillQuest(1344, "Future", "Alien Dino|Alien Juggernaut|Furious 4-Eyed Guard|Red-Eyed Alien");

        // Spy Eyes
        Story.MapItemQuest(1345, "Future", 632, 2);

        // Smashed to Pieces
        Story.MapItemQuest(1346, "Future", 633, 7);

        // Tired of My Vigilette Dreams
        if (!Story.QuestProgression(1347))
        {
            Core.EnsureAccept(1347);
            Core.HuntMonster("Future", "Red-Eyed Alien", "Small Fragment Acquired", 5);
            Core.HuntMonster("Future", "Red-Eyed Alien", "Medium Piece Acquired", 5);
            Core.EnsureComplete(1347);
        }

        // You're Not the Boss of Me Now
        if (!Story.QuestProgression(1348))
        {
            Core.EnsureAccept(1348);
            Core.HuntMonster("Future", "The Collector", "Collector Vanquished");
            Core.EnsureComplete(1348);
        }
    }
}