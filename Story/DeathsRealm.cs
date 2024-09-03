/*
name: Death's Realm Story
description: This will finish the storyline in /deathsrealm.
tags: story, quest, death, deaths, realm, deathsrealm, deaths realm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DeathsRealm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(2430))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Time is Running Out (2421)
        Story.KillQuest(2421, "swordhavenundead", "Skeletal Soldier");

        // Thief of Hours (2422)
        if (!Story.QuestProgression(2422))
        {
            Core.EnsureAccept(2422);
            Core.KillMonster("river", "Field1", "Left", "*", "Shiny Pearls", 7);
            Core.KillMonster("lair", "Hole", "Left", "*", "Dragon Claws", 4);
            Core.KillMonster("citadel", "m6", "Left", "*", "Tasty Cakes", 3);
            Core.HuntMonster("boxes", "Sneeviltron", "Massive Sneevil Box");
            Core.KillMonster("orecavern", "r2", "Left", "*", "Bag of Silver", 7);
            Core.HuntMonster("creatures", "Red Bird", "Red Herring");
            Core.EnsureComplete(2422);
        }

        // Death-dealing Blows (2423)
        if (!Story.QuestProgression(2423))
        {
            Core.Logger("You need to get 5 kills in /doomarena or /bludrutbrawl for [2423] \"Death-dealing Blows\" quest, run the script again when you've completed the quest.");
            return;
        }

        // Fear the Reaper (2424)
        Story.KillQuest(2424, "deathsrealm", "Undead Mage");

        // Master Death's Realm (2425)
        Story.KillQuest(2425, "deathsrealm", "Undead Mage");
        Story.MapItemQuest(2425, "deathsrealm", 1485);

        // Real Live Hero-Training (2426)
        Story.MapItemQuest(2426, "trainers", 1491, AutoCompleteQuest: false);

        // The Living Library (2427)
        Story.KillQuest(2427, "elemental", "Mana Imp");

        // Understanding Xhar Morghuil (2428)
        Story.MapItemQuest(2428, "deathsrealm", new[] { 1487, 1488, 1489 });

        // Door to Eternity (2429)
        Story.MapItemQuest(2429, "deathsrealm", 1490);

        // Death vs Death (2430)
        if (!Core.isCompletedBefore(2430))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(2430, "deathsrealm", "Death Alive");
            Core.EquipClass(ClassType.Farm);
        }
    }
}
