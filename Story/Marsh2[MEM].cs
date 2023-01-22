/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Marsh2
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
        if (!Core.IsMember)
        {
            Core.Logger("Marsh 2 Storyline Is Member Only. Skipping this Script");
            return;
        }

        if (Core.isCompletedBefore(1481))
            return;

        Story.PreLoad(this);

        //Clueless 1474
        Story.MapItemQuest(1474, "marsh2", 720, 8);

        //Filling In The Puzzle 1475
        Story.KillQuest(1475, "marsh2", "Undead Berserker");

        //The Eyes (and Teeth) Have It 1476
        Story.KillQuest(1476, "marsh2", new[] { "Ghoul", "Marsh Lurker" });

        //Halfway There 1477
        Story.KillQuest(1477, "marsh2", new[] { "Lich", "Lesser Shadow Serpent" });

        //Charge It 1478
        Story.KillQuest(1478, "marsh2", new[] { "Thrax Ironhide", "Lesser Groglurk" });

        //What Was That?! 1479
        Story.MapItemQuest(1479, "marsh2", 723);

        //The Mudluk Vault 1480
        Story.KillQuest(1480, "marsh2", "Marsh Vault Guardian");

        //Recovering Mudluk History 1481
        Story.MapItemQuest(1481, "marsh2", 721);

    }
}
