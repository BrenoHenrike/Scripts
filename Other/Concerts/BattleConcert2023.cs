/*
name: Battle Concert Class Quests
description: does the battle concert even quests for the classes.
tags: concert, metal necro, doom metal necro, neo metal necro
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BattleConcertClassQuests
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        BattleConcertQuests();

        Core.SetOptions(false);
    }

    public void BattleConcertQuests()
    {
        if (Core.isCompletedBefore(9327))
            return;

        Story.PreLoad(this);

        // Soulless Paps 9317
        Story.KillQuest(9317, "skulldome", "Photoghoulpher");

        // Vanquished from the Premises 9318
        Story.KillQuest(9318, "skullhall", "Necroupie");

        // Clear Space, Clear Mind  9319
        Story.MapItemQuest(9319, "skullhall", new[] { 11948, 11949 });
        Story.MapItemQuest(9319, "skullhall", 11950, 6);

        // Rusty Metal 9320
        Story.KillQuest(9320, "skullhall", new[] { "Armored Zombie" });

        // Concert Crush 9324
        Story.KillQuest(9324, "skullhall", new[] { "Photoghoulpher", "Necroupie" });

        // Hashtag Dead Ugly 9322
        Story.KillQuest(9322, "skullhall", "Photoghoulpher");

        // Ghoulish Fan Culture 9323
        Story.KillQuest(9323, "skullhall", "Necroupie");

        // No Honor Among Rockers 9321
        Story.KillQuest(9321, "skullarena", new[] { "Laryn", "Bellum" });

        // Musical Altercations 9325
        Story.KillQuest(9325, "skullarena", new[] { "Pit", "Medullos" });

        // Lacking Brain Substance 9326
        if (!Story.QuestProgression(9326))
        {
            Core.EnsureAccept(9326);

            Core.HuntMonster("brainmeat", "Larythesia", "Laryn's Shiny Mic");
            Core.HuntMonster("brainmeat", "Belluthesia", "Bellum's Shiny Toothpick");
            Core.HuntMonster("brainmeat", "Pitesthesia", "Pit's Shiny Guitar");
            Core.HuntMonster("brainmeat", "Medullothesia", "Medullos' Shiny Drumstick");
            Core.EnsureComplete(9326);
        }

        // Farming Quest
        // Revive the Encore 9327
        Core.AddDrop("Bone Pick");
        Story.KillQuest(9327, "brainmeat", "Brain Matter");


        //Class Quest
        // Doom Metal Necro Class 9328


    }
}
