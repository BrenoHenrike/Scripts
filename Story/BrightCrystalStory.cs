/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BrightCrystalStory
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CrystalBrightQuests();

        Core.SetOptions(false);
    }

    public void CrystalBrightQuests()
    {
        if (Core.isCompletedBefore(4967))
            return;

        Story.PreLoad(this);

        //Street Team 4953
        Story.MapItemQuest(4953, "dreamforest", 4326, 5);

        //De-Pesting 4954
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(4954, "dreamforest", new[] { "Green Rat", "Crow" });

        //Earning That Guest Pass 4955
        Story.MapItemQuest(4955, "dreamforest", 4327, 4);
        Story.MapItemQuest(4955, "dreamforest", 4328, 3);
        Story.MapItemQuest(4955, "dreamforest", 4329);

        //Low On Caffeine 4956
        Story.MapItemQuest(4956, "northpointe", 4330);

        //Feels like a Dream 4957
        Story.MapItemQuest(4957, "dreamforest", 4331, 5);

        //Give me a Ticket 4958
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(4958, "dreamforest", "Crow");

        //For an Elephant Ride? 4959
        Story.MapItemQuest(4959, "dreamforest", 4332);

        //Trainers 4960
        Story.MapItemQuest(4960, "dreamforest", 4333, 6);
        Story.MapItemQuest(4960, "dreamforest", 4334);

        //Tumblers 4961
        Story.KillQuest(4961, "dreamforest", new[] { "Acrobat", "Acrobat" });

        //Head to the Midway 4962
        Story.KillQuest(4962, "dreamforest", new[] { "Acrobat", "Fire Dancer", "Elephant Trainer" });

        //Balloons 4963
        if (!Story.QuestProgression(4963))
        {
            Core.EnsureAccept(4963);
            Core.HuntMonsterMapID("dreamforest", 19, "Prize Ticket", 10);
            Core.EnsureComplete(4963);
        }
        //Story.KillQuest(4963, "dreamforest", "Balloons");

        //Out of the Shadows 4964
        Story.KillQuest(4964, "dreamforest", new[] { "Living Shadow", "Dark Imp" });

        //Follow the Footprints 4965
        Story.MapItemQuest(4965, "dreamforest", 4335, 10);

        //Help Miranda 4967
        Story.KillQuest(4967, "dreamforest", "Mirandageist");
    }
}
