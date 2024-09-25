/*
name: Dage Recruit Story
description: This will complete the Dage Recruit story.
tags: story, quest, dage, recruit, legion, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DageRecruitStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CompleteDageRecruit();

        Core.SetOptions(false);
    }

    public void CompleteDageRecruit()
    {
        if (!Core.isSeasonalMapActive("dagerecruit"))
            return;
        if (Core.isCompletedBefore(8575))
            return;

        Story.PreLoad(this);

        Bot.Drops.Start();

        //Pop Goes the Makai
        Story.KillQuest(8556, "dagerecruit", "Dark Makai");

        //Dispel Spell
        if (!Story.QuestProgression(8557))
        {
            Core.EnsureAccept(8557);
            Core.KillMonster("dagerecruit", "Enter", "Spawn", "Dreadfiend", "Fiend Energy Collected", 4);
            Story.MapItemQuest(8557, "dagerecruit", 9883, 4);
        }

        //Dreadfiend Demolition
        if (!Story.QuestProgression(8558))
        {
            Core.EnsureAccept(8558);
            Core.KillMonster("dagerecruit", "Enter", "Spawn", "Dreadfiend", "Dreadfiend Defeated", 6);
            Core.EnsureComplete(8558);
        }

        //Graython Located
        Story.MapItemQuest(8559, "dagerecruit", 9884);

        //Defeat Graython
        if (!Story.QuestProgression(8560))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8560);
            while (!Bot.ShouldExit && Bot.Player.Cell != "r3")
            {
                Core.Join("dagerecruit", "r3", "Left");
                Core.Sleep();
            }
            Core.KillMonster("dagerecruit", "r3", "Left", "Graython", "Graython Defeated");
            Core.EnsureComplete(8560);
        }

        //Island Sightseeing
        Story.KillQuest(8561, "dagerecruit", "Scared Wildcat");
        Story.MapItemQuest(8561, "dagerecruit", 9885);

        //Lure Crafted
        if (!Story.QuestProgression(8562))
        {
            Core.EnsureAccept(8562);
            Core.KillMonster("dagerecruit", "Enter", "Spawn", "Dreadfiend", "Void Crystals", 3);
            Core.EnsureComplete(8562);
        }

        //Lure Charged
        Story.KillQuest(8563, "dagerecruit", "Scared Wildcat");

        //Place the Lure
        Story.MapItemQuest(8564, "dagerecruit", 9886);

        //Defeat Nuckelavee
        Story.KillQuest(8565, "dagerecruit", "Nuckelavee");

        //Bloody the Fiends
        Story.KillQuest(8566, "dagerecruit", "Bloodfiend");

        //Unstable Energies
        Story.KillQuest(8567, "dagerecruit", "Bloodfiend");

        //Plant the Bombs
        Story.MapItemQuest(8568, "dagerecruit", 9887, 4);

        //Those Infernal Fiends
        Story.KillQuest(8569, "dagerecruit", "Infernal Fiend");

        //Defeat Smaras
        Story.KillQuest(8570, "dagerecruit", "Smaras");

        //Understanding Yokai
        Story.KillQuest(8571, "dagerecruit", "Funa-yurei");

        // Covering Our Scent
        if (!Story.QuestProgression(8572))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8572);
            Core.HuntMonster("dagerecruit", "Funa-yurei", "Yokai Energy", 4);
            Story.MapItemQuest(8572, "dagerecruit", 9888, 4);
            Core.EquipClass(ClassType.Solo);
        }

        //Can't Escape the Shadows
        Story.KillQuest(8573, "dagerecruit", "Shadow Clone");

        //Last of the Defenses
        Story.KillQuest(8574, "dagerecruit", "Shadow Clone");

        // Defeat Hebimaru
        if (!Story.QuestProgression(8575))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8575);
            Core.HuntMonster("dagerecruit", "Hebimaru", "Hebimaru Defeated");
            Core.EnsureComplete(8575);
        }
        // Scorched Earth
        if (!Story.QuestProgression(8576))
        {
            Core.EnsureAccept(8576);
            Core.KillMonster("dagerecruit", "Enter", "Spawn", "Dreadfiend", "Dreadfiend Defeated", 6);
            Core.HuntMonster("dagerecruit", "Dark Makai", "Dark Makai Defeated", 6);
            Core.HuntMonster("dagerecruit", "Bloodfiend ", "Bloodfiend Defeated", 6);
            Core.HuntMonster("dagerecruit", "Infernal Fiend", "Infernal Fiend Defeated", 6);
            Core.EnsureComplete(8576);
        }

        // Nation Badges / Mega Nation BadgesDreadfiend
        if (!Story.QuestProgression(8585))
        {
            Core.EnsureAccept(8585);
            Core.KillMonster("darkwarlegion", "Enter", "Spawn", "Dreadfiend", "Dreadfiend Defeated", 6);
            Core.EnsureComplete(8585);
        }

        // A Nation Defeated
        if (!Story.QuestProgression(8586))
        {
            Core.EnsureAccept(8586);
            Core.KillMonster("darkwarlegion", "Enter", "Spawn", "Dreadfiend", "Nation's Dread", 5);
            Core.EnsureComplete(8586);
        }

        // ManSlayer? More Like ManSLAIN
        Story.KillQuest(8587, "darkwarlegion", "Manslayer Fiend");

        // Defeat Dirtlicker            
        Story.KillQuest(8588, "darkwarlegion", "Dirtlicker");
    }
}
