//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class ChaosQueenBeleen
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BeleenQuests();

        Core.SetOptions(false);
    }
    public void BeleenQuests(bool ChaosFuzzyUnlockOnly = false)
    {
        //Quest Progress Check
        if (Core.isCompletedBefore(4330))
            return;

        //Needed AddDrop
        Core.AddDrop("Chaos Fuzzies");

        //Drearia on Demand - 4312
        Story.MapItemQuest(4312, "Drearia", 3485);
        Story.KillQuest(4312, "Drearia", new[] { "Dark Makai", "Evil Elemental", "Green Rat" });

        //Plant a Little Seed and Nature Grows - 4313
        Story.KillQuest(4313, "Drearia", "Dark Makai");

        //A Key Discovery - 4314
        Story.MapItemQuest(4314, "Drearia", 3466);
        Story.KillQuest(4314, "Drearia", "Dark Makai");

        //Creepy House... Yay! - 4315
        Story.MapItemQuest(4315, "Drearia", 3467);
        Story.KillQuest(4315, "Drearia", "Green Rat");

        //Sparkling Books - 4316
        Story.MapItemQuest(4316, "Drearia", 3468);

        //A Paladin in Peril - 4317
        Story.MapItemQuest(4317, "SwordHavenPink", 3469);

        //Pink Stinks! - 4318
        Story.MapItemQuest(4318, "SwordHavenPink", 3486, 5);
        Story.KillQuest(4318, "SwordHavenPink", "Pink Slime");

        //Rats, RATS! - 4319
        Story.KillQuest(4319, "SewerPink", "Pink Rat");

        //AdventureQuest Worm - 4320
        Story.KillQuest(4320, "SewerPink", "Cutie Grumbley");

        //UnBEARable Sight - 4321
        Story.MapItemQuest(4321, "PineWoodPink", 3470);
        Story.KillQuest(4321, "PineWoodPink", "Pink Grizzly");

        //Too Much Pink in Pinewood! - 4322
        Story.MapItemQuest(4322, "PineWoodPink", 3471, 5);
        Story.KillQuest(4322, "PineWoodPink", "Pink Shell Turtle");

        //Kill Sparkletooth - 4323
        Story.KillQuest(4323, "PineWoodPink", "Sparkletooth");

        //The Citadorable Plot - 4324
        Story.MapItemQuest(4324, "Citadel", 3472);
        if (ChaosFuzzyUnlockOnly)
            return;

        //Fuzzy Run Minigame - 4325
        if (!Core.isCompletedBefore(4325))
        {
            Core.EnsureAccept(4325);
            while (!Core.CheckInventory("Chaos Fuzzies", 30))
                Core.GetMapItem(3481, map: "Citadel");
            Core.EnsureComplete(4325);
        }

        //Fluffy Clouds - 4326
        Story.KillQuest(4326, "Pastelia", "Happy Cloud");

        //Super Model Makai Hair - 4327
        Story.KillQuest(4327, "Pastelia", "Cutie Makai");

        //Squeaky Business - 4328
        Story.KillQuest(4328, "Pastelia", "Pink Rat");

        //The Queen's Offering - 4329
        Story.MapItemQuest(4329, "Pastelia", 3473);

        //Boss Fight Beleen! - 4330
        Story.KillQuest(4330, "Pastelia", "Chaos Queen Beleen");
    }
}
