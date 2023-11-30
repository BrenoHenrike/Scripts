/*
name: Dark War Legion and Nation
description: This will finish the Dark War Legion and Nation quest.
tags: story, quest, legion, dark-war-legion-and-nation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class DarkWarLegionandNation
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public Core13LoC LOC => new();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoBoth();

        Core.SetOptions(false);
    }

    public void DoBoth()
    {
        DarkWarLegion();
        DarkWarNation();
    }

    public void DarkWarLegion()
    {
        if (Core.isCompletedBefore(8588))
            return;

        Story.PreLoad(this);

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
        if (!Story.QuestProgression(8561))
        {
            Core.EnsureAccept(8561);
            Core.HuntMonster("dagerecruit", "Scared Wildcat", "Wildlife Cleared", 10);
            Story.MapItemQuest(8561, "dagerecruit", 9885);
        }

        //Lure Crafted
        if (!Story.QuestProgression(8562))
        {
            Core.EnsureAccept(8562);
            Core.KillMonster("dagerecruit", "Enter", "Spawn", "Dreadfiend", "Void Crystals", 3);
            Core.EnsureComplete(8562);
        }

        // Lure Charged
        if (!Story.QuestProgression(8563))
        {
            Core.EnsureAccept(8563);
            Core.HuntMonster("dagerecruit", "Scared Wildcat", "Death Charge", 10);
            Story.MapItemQuest(8563, "dagerecruit", 9885);
        }

        // Place the Lure
        Story.MapItemQuest(8564, "dagerecruit", 9886);

        // Defeat Nuckelavee
        Story.KillQuest(8565, "dagerecruit", "Nuckelavee");

        // Bloody the Fiends
        Story.KillQuest(8566, "dagerecruit", "Bloodfiend");

        // Unstable Energies
        Story.KillQuest(8567, "dagerecruit", "Bloodfiend");

        // Plant the Bombs
        Story.MapItemQuest(8568, "dagerecruit", 9887, 4);

        // Those Infernal Fiends
        Story.KillQuest(8569, "dagerecruit", "Infernal Fiend");

        // Defeat Smaras
        Story.KillQuest(8570, "dagerecruit", "Smaras");

        // Understanding Yokai
        if (!Story.QuestProgression(8571))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8571);
            Core.HuntMonster("dagerecruit", "Funa-yurei", "Funa-Yurei Defeated", 10);
            Core.EnsureComplete(8571);
        }

        // Covering Our Scent
        if (!Story.QuestProgression(8572))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8572);
            Core.HuntMonster("dagerecruit", "Funa-yurei", "Yokai Energy", 4);
            Story.MapItemQuest(8572, "dagerecruit", 9888, 4);
            Core.EquipClass(ClassType.Solo);
        }

        // Can't Escape the Shadows
        Story.KillQuest(8573, "dagerecruit", "Shadow Clone");

        // Last of the Defenses
        Story.KillQuest(8574, "dagerecruit", "Shadow Clone");

        // Defeat Hebimaru
        if (!Story.QuestProgression(8575))
        {
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

        // Nation Badges / Mega Nation Badges
        Story.KillQuest(8584, "darkwarlegion", "Dreadfiend");

        // A Nation Defeated
        Story.KillQuest(8586, "darkwarlegion", "Dreadfiend");

        // ManSlayer? More Like ManSLAIN
        Story.KillQuest(8587, "darkwarlegion", "Manslayer Fiend");

        // Defeat Dirtlicker            
        Story.KillQuest(8588, "darkwarlegion", "Dirtlicker");
    }

    public void DarkWarNation()
    {
        if (Core.isCompletedBefore(8583))
            return;

        LOC.Vath();

        // Legion Badges
        Story.KillQuest(8578, "darkwarnation", "High Legion Inquisitor");

        // Doomed Legion Warriors
        Story.KillQuest(8580, "darkwarnation", "Legion Doomknight");

        // Undead Legion Dread
        Story.KillQuest(8581, "darkwarnation", "Legion Dread Knight");

        // Defeat War
        Story.KillQuest(8582, "darkwarnation", "War");

        // The Traitor           
        Story.KillQuest(8583, "darkwarnation", "Dage the Evil");
    }

}
