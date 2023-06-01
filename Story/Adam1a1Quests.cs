/*
name: Poison Forest Story
description: This will finish the Poison Forest Story.
tags: story, quest, poison-forest
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs

using Skua.Core.Interfaces;

public class Adam1a1Quest
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
        if (Core.isCompletedBefore(8014))
            return;

        //Coffee for the Mind 8009
        if (!Story.QuestProgression(8009))
        {
            Core.EnsureAccept(8009);
            Core.HuntMonster("vendorbooths", "Caffeine Imp", "Coffee Beans", 10);
            Core.HuntMonster("djinn", "Lamia", "Tasty Poison", 10);
            Core.HuntMonster("charredpath", "Toxic Wisteria", "Necessary Antidote");
            Core.EnsureComplete(8009);
        }

        //Ectoplasm for the Body 8010
        if (!Story.QuestProgression(8010))
        {
            Core.EnsureAccept(8010);
            Core.HuntMonster("doomwood", "Doomwood Ectomancer", "Refined Ectoplasm", 10);
            Core.HuntMonster("ectocave", "Ektorax", "Ektorax's Ectoplasm");
            Core.EnsureComplete(8010);
        }

        //Secretteller's Building Materials 8011
        if (!Story.QuestProgression(8011))
        {
            Core.EnsureAccept(8011);
            Core.HuntMonster("extinction", "Slimed Drone", "Iron II.0", quant: 4, isTemp: false);
            Core.HuntMonster("doomwood", "Doomwood Treeant", "Wood", 10);
            Core.HuntMonster("crashsite", "Dwakel Blaster", "Big Iron Bolts", 10);
            Core.HuntMonster("portalmaze", "Time Wraith", "Piece of Cake", 5);
            Core.EnsureComplete(8011);
        }
    }
}
