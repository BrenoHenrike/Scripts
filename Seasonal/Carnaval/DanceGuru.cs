/*
name: Dance Guru
description: This script farms Diego's and Batista's Quests in danceguru.
tags: dance guru, carnaval, seasonal, storyline, batista, diego
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DanceGuru
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(7956) || !Core.isSeasonalMapActive("danceguru"))
            return;

        Story.PreLoad(this);

        //Coconutty (7943)
        Story.KillQuest(7943, "danceguru", new[] { "Palm Treeant", "Palm Treeant" });

        //Gift Giving (7944)
        Story.KillQuest(7944, "danceguru", "Crow");
        Story.MapItemQuest(7944, "danceguru", 8177, 5);

        //Clear the Forest (7945)
        Story.KillQuest(7945, "danceguru", "Crow");

        //Songs for Dancing (7946)
        Story.KillQuest(7946, "danceguru", "Crow");

        //Search! (7947)
        Story.MapItemQuest(7947, "danceguru", 8178);

        //Below the Parade (7948)
        Story.MapItemQuest(7948, "danceguru", 8179);

        //Light it Up (7949)
        Story.KillQuest(7949, "danceguru", "Sewer Shroom");

        //Imp-terrogation (7950)
        Story.KillQuest(7950, "danceguru", "Voodoo Doll");

        //Snack Time? (7951)
        Story.KillQuest(7951, "danceguru", "Sewer Rat");

        //Grime Time (7952)
        Story.KillQuest(7952, "danceguru", "Sewer Rat");

        //Save Them (7953)
        Story.MapItemQuest(7953, "danceguru", 8180, 6);

        //Skeleton Party (7954)
        Story.KillQuest(7954, "danceguru", "Skele-Tones");

        //Monstrous Feathers (7955)
        Story.KillQuest(7955, "danceguru", "Carnaval Harpy");

        //Remove the Mask (7956)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(7956, "danceguru", "Bicho Papao");
    }
}
