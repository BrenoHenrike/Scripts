/*
name: Temple Delve
description: Completes the Temple Delve storyline.
tags: story, quest, temple, delve, siegefortress, nulgath, seasonal, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
using Skua.Core.Interfaces;

public class TempleDelve
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private TempleSiege TS = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(9143) || !Core.isSeasonalMapActive("templedelve"))
            return;

        TS.CompleteTempleSiege();

        Story.PreLoad(this);

        //Mindless Waste (9080)
        Story.KillQuest(9080, "templedelve", "Overdriven Paladin");

        //Stone Proxy (9081)
        Story.KillQuest(9081, "templedelve", "Doomed Troll");
        Story.MapItemQuest(9081, "templedelve", new[] { 11160, 11161 }, 2);

        //The Deepness of Shadows... (9082)
        Story.KillQuest(9082, "templedelve", "Doomed Beast");
        Story.MapItemQuest(9082, "templedelve", 11162, 4);

        //...Begets the Glare of the Light (9083)
        Story.KillQuest(9083, "templedelve", new[] { "Delirious Elemental", "Infested Nation" });
        Story.MapItemQuest(9083, "templedelve", 11163);

        //Death Over Defeat (9084)
        Story.KillQuest(9084, "templedelve", "Infested Nation");

        //For a Brighter Tomorrow (9085)
        Story.KillQuest(9085, "templedelve", "Delirious Elemental");

        //Minions Anonymous (9086)
        Story.MapItemQuest(9086, "templedelve", 11164, 3);
        Story.MapItemQuest(9086, "templedelve", 11165);

        //Prephosphorus (9087)
        Story.KillQuest(9087, "templedelve", "Delirious Elemental");
        Story.MapItemQuest(9087, "templedelve", new[] { 11166, 11167 });

        //Sunspotty (9088)
        Story.KillQuest(9088, "templedelve", new[] { "Delirious Elemental", "Infested Nation" });

        //Feeding Time (9089)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9089, "templedelve", "Doomed Fiend");

        // Vile Vanguard 9134
        Story.KillQuest(9134, "siegefortress", "Legion Dreadmarch");

        // Fell Fealty 9135
        Story.KillQuest(9135, "siegefortress", "Legion Dread Knight");

        // Infestation Signs 9136
        Story.MapItemQuest(9136, "siegefortress", 11312, 4);
        Story.MapItemQuest(9136, "siegefortress", 11313);

        // Banal Barage 9137
        Story.KillQuest(9137, "siegefortress", new[] { "Legion Dread Knight", "Legion Dreadmarch" });

        // Bluebirds 9138
        Story.KillQuest(9138, "siegefortress", "Shadow Traitor");

        // Anatomical Example 9139
        Story.KillQuest(9139, "siegefortress", "Shadow Traitor");
        Story.MapItemQuest(9139, "siegefortress", 11314, 4);

        // Bloodless Daisy 9140
        Story.MapItemQuest(9140, "siegefortress", 11315);
        Story.KillQuest(9140, "siegefortress", "Enslaved Elemental");

        // Shadows of Espionage 9141
        Story.KillQuest(9141, "siegefortress", new[] { "Shadow Traitor", "Enslaved Elemental" });
        Story.MapItemQuest(9141, "siegefortress", 11316, 2);

        // Foul Duke of Light 9142
        Story.KillQuest(9142, "siegefortress", "Enslaved Astero");

        // Evil of Humanity 9143
        Story.KillQuest(9143, "siegefortress", "Dage the Evil");
    }
}
