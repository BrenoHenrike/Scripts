//cs_include Scripts/CoreBots.cs
using RBot;

public class Collection
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

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

        // This Town in a Desktop Globe
        Core.MapItemQuest(1293, "Terrarium", 586);

        // Dust Bunnies Can't Hide
        Core.KillQuest(1294, "Terrarium", "Dustbunny of Doom");

        // Au Contr-air holes
        Core.MapItemQuest(1295, "Terrarium", 587, 10);

        // Hero Eats Everything. Even Pellets
        Core.MapItemQuest(1296, "Terrarium", 588, 12);

        // The Moth Defeats the Man?
        Core.KillQuest(1297, "Terrarium", "Death on Wings");

        // Can you find them?
        Core.MapItemQuest(1298, "Terrarium", 589);
        Core.MapItemQuest(1298, "Terrarium", 590);
        Core.MapItemQuest(1298, "Terrarium", 591);
        Core.MapItemQuest(1298, "Terrarium", 592);
        Core.MapItemQuest(1298, "Terrarium", 593);

        // You've Been Looking Everywhere!
        Core.MapItemQuest(1299, "Terrarium", 593);
        Core.MapItemQuest(1299, "Terrarium", 594);
        Core.MapItemQuest(1299, "Terrarium", 595);
        Core.MapItemQuest(1299, "Terrarium", 596);
        Core.MapItemQuest(1299, "Terrarium", 604, AutoCompleteQuest: false);

        // Go for Grease!
        Core.KillQuest(1300, "Terrarium", "Death on Wings");

        // Doppelganger Wants to Hit You
        Core.KillQuest(1308, "Terrarium", new[] { "Doppleganger of Will", "Doppleganger of Fred" });

        // Catapult Climb
        Core.MapItemQuest(1309, "Terrarium", 604);

        // The Treasure that You Seek - 976
        Core.KillQuest(1339, "prehistoric", "Gigantosaurus");

        // These Trees - 1340
        Core.MapItemQuest(1340, "prehistoric", 630, 11);

        // Ewwww... Totally Gross - 1341
        Core.KillQuest(1341, "prehistoric", "Gigantosaurus");

        // The Eggs, Exciting and New - 1342
        Core.MapItemQuest(1342, "prehistoric", 631, 4);

        // Someday (Today) Their Mother Will Die - 1343
        Core.KillQuest(1343, "prehistoric", "Mother TerrorDOOMKill");

        // More Aliens Than You Can Handle?
        Core.KillQuest(1344, "Future", "Alien Dino|Alien Juggernaut|Furious 4-Eyed Guard|Red-Eyed Alien");

        // Spy Eyes
        Core.MapItemQuest(1345, "Future", 632, 2);

        // Smashed to Pieces
        Core.MapItemQuest(1346, "Future", 633, 7);

        // Tired of My Vigilette Dreams
        if (!Core.QuestProgression(1347))
        {
            Core.EnsureAccept(1347);
            Core.HuntMonster("Future", "Red-Eyed Alien", "Small Fragment Acquired", 5);
            Core.HuntMonster("Future", "Red-Eyed Alien", "Medium Piece Acquired", 5);
            Core.EnsureComplete(1347);
        }

        // You're Not the Boss of Me Now
        Core.KillQuest(1348, "Future", "The Collector");
    }
}