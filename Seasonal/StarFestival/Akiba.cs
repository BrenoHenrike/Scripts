/*
name: Akiba
description: This script will complete the storyline in /akiba.
tags: star, festival, akiba, story, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Akiba
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
        if (Core.isCompletedBefore(6442) || !Core.isSeasonalMapActive("akiba"))
            return;

        Story.PreLoad(this);

        // Decoration Preparation (6432)
        if (!Story.QuestProgression(6432))
        {
            Core.EnsureAccept(6432);
            Core.HuntMonsterMapID("junkyard", 1, "Red Paper", 3, log: false);
            Core.HuntMonsterMapID("junkyard", 2, "Yellow Paper", 3, log: false);
            Core.HuntMonsterMapID("junkyard", 3, "Blue Paper", 3, log: false);
            Core.HuntMonsterMapID("junkyard", 4, "White Paper", 3, log: false);
            Core.HuntMonsterMapID("junkyard", 5, "Black Paper", 3, log: false);
            Core.HuntMonsterMapID("junkyard", 6, "Green Paper", 3, log: false);
            Core.HuntMonster("junkyard", "Onibaba", "Dark String", 5, log: false);
            Core.EnsureComplete(6432);
        }

        // Tanabata Tree of Wishes (6433)
        Story.MapItemQuest(6433, "bamboo", 5946, 7);
        Story.KillQuest(6433, "bamboo", "Bamboo Wisp");

        // Write a Wish Upon a Star (6434)
        Story.KillQuest(6434, "yokaigrave", "Ninja Nopperabo");

        // Festival of Stars (6435)
        Story.MapItemQuest(6435, "akiba", 5951, 8);
        Story.MapItemQuest(6435, "akiba", new[] { 5952, 5953 }, 5);
        Story.MapItemQuest(6435, "akiba", 5954, 3);
        Story.MapItemQuest(6435, "akiba", 5955, 2);

        // The First Wish (6436)
        Story.MapItemQuest(6436, "dragonrune", 5947);

        // The Second Wish (6437)
        Story.KillQuest(6437, "beleensdream", "Bluddron the Betrayer");

        // The Third Wish (6438)
        if (!Story.QuestProgression(6438))
        {
            Core.EnsureAccept(6438);
            Core.KillMonster("wanders", "r5", "Left", "Kalestri Worshiper", "The Third Wish");
            Core.EnsureComplete(6438);
        }

        // The Fourth Wish (6439)
        Story.KillQuest(6439, "cleric", "Chaos Dragon");

        // The Fifth Wish (6440)
        Story.KillQuest(6440, "skytower", "Moonstone");

        // The Sixth Wish (6441)
        Story.MapItemQuest(6441, "librarium", 5948);

        // The Seventh Wish (6442)
        Story.KillQuest(6442, "lair", "Red Dragon");
    }
}
