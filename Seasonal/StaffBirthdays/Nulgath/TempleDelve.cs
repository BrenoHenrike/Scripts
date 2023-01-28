/*
name: Temple Delve
description: Completes the Temple Delve storyline.
tags: temple-delve, templedelve, nulgath, seasonal, nulgath-birthday, nulgath-story
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
        if (Core.isCompletedBefore(9089) || !Core.isSeasonalMapActive("templedelve"))
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
    }
}
