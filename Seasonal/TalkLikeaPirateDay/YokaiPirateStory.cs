/*
name: Yokai Pirate Story
description: This script will finish the storyline in /yokaipirate.
tags: story, quest, yokai, tlapd,talk-like-a-pirate-day,seasonal, pirate
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class YokaiPirateStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(9387) || !Core.isSeasonalMapActive("yokaipirate"))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Wokou 9378
        Story.MapItemQuest(9378, "yokaipirate", new[] { 12133, 12134, 12135 });

        // Shanty Sha-N-Ti 9379
        Story.KillQuest(9379, "yokaipirate", "Disguised Pirate");

        // Yokai's Ark 9380
        if (!Story.QuestProgression(9380))
        {
            Core.EnsureAccept(9380);
            Core.HuntMonster("yokaipirate", "Serpent Warrior", "Serpent Badge", 7);
            Story.MapItemQuest(9380, "yokaipirate", new[] { 12136, 12137 });
        }

        // Fashion Fathoms 9381
        Story.KillQuest(9381, "yokaipirate", "Disguised Pirate");

        // Hoo Wants a Cracker 9382
        Story.KillQuest(9382, "yokaipirate", "Noble Owl");

        // Papers Please 9383
        Story.MapItemQuest(9383, "yokaipirate", 12138, 7);

        Core.EquipClass(ClassType.Farm);
        // King and Coral Snakes 9384
        if (!Story.QuestProgression(9384))
        {
            Core.EnsureAccept(9384);
            Core.HuntMonster("yokaipirate", "Disguised Pirate", "Pirate Interrogated", 6);
            Core.HuntMonster("yokaipirate", "Serpent Warrior", "Warrior Interrogated", 6);
            Story.MapItemQuest(9384, "yokaipirate", 12139);
        }

        // Horizon's Green Flash 9385
        Story.KillQuest(9385, "yokaipirate", "Noble Owl");

        // Highly Buoyant Metal Armor 9386
        if (!Story.QuestProgression(9386))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9386);
            Core.HuntMonsterMapID("yokaipirate", 1, "Knight Captured", 8);
            Core.EnsureComplete(9386);
        }

        // Salty Roots 9387
        if (!Story.QuestProgression(9387))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9387);
            Core.HuntMonsterMapID("yokaipirate", 11, "Neverglades Lord Dueled");
            Core.EnsureComplete(9387);
        }
    }
}
