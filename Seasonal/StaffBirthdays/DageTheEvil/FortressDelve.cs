/*
name: Fortress Delve Story
description: This will complete the Fortress Delve story.
tags: story, quest, fortress, delve, legion, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelve.cs
using Skua.Core.Interfaces;

public class FortressDelve
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private TempleDelve TD = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoStory();
        Core.SetOptions(false);
    }

    public void DoStory()
    {
        TD.Storyline();
        if (Core.isCompletedBefore(9169))
            return;
        if (!Core.isSeasonalMapActive("fortressdelve"))
            return;

        Story.PreLoad(this);

        //Budding Scolex 9160
        Story.KillQuest(9160, "fortressdelve", "Doom Worm");

        //Scuffling Shadows 9161
        Story.MapItemQuest(9161, "fortressdelve", 11345);
        Story.MapItemQuest(9161, "fortressdelve", 11346, 2);
        Story.MapItemQuest(9161, "fortressdelve", 11347, 3);

        //Humans DNI 9162
        Story.KillQuest(9162, "fortressdelve", "Doomed Beast");
        Story.MapItemQuest(9162, "fortressdelve", 11348);

        //Scion's Privilege 9163
        Story.KillQuest(9163, "fortressdelve", "Doom Worm");
        Story.MapItemQuest(9163, "fortressdelve", 11349);

        //A Man-Cave and a Box of Scraps 9164
        Story.MapItemQuest(9164, "fortressdelve", 11350);
        Story.KillQuest(9164, "fortressdelve", "Legion Dreadmarch");

        //The Glare of the Light... 9165
        Story.MapItemQuest(9165, "fortressdelve", 11351);
        Story.KillQuest(9165, "fortressdelve", "Delirious Elemental");

        //...Pierces the Deepest Shadowsl 9166
        Story.MapItemQuest(9166, "fortressdelve", 11352);
        Story.KillQuest(9166, "fortressdelve", "Enlightened Shadow");

        //Lonely Supremacy 9167
        Story.MapItemQuest(9167, "fortressdelve", 11353);
        Story.KillQuest(9167, "fortressdelve", "Legion Dreadmarch");

        //The Condemner 9168
        Story.KillQuest(9168, "fortressdelve", new[] { "Enlightened Shadow", "Delirious Elemental" });

        //Lost Light, Astero 9169
        Story.KillQuest(9169, "fortressdelve", "Astero");
    }
}
