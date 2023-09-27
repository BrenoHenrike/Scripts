/*
name: Yokai Treasure Story
description: This script will finish the storyline in /yokaitreasure.
tags: story, quest, yokai, tlapd,talk-like-a-pirate-day,seasonal, treasure
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\TalkLikeaPirateDay\YokaiPirateStory.cs
using Skua.Core.Interfaces;

public class YokaiTreasureStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    private YokaiPirateStory YPS = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(9405) || !Core.isSeasonalMapActive("yokaitreasure"))
            return;

        YPS.Storyline();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // All Hands Report (9396)
        Story.MapItemQuest(9396, "yokaitreasure", new[] { 12162, 12163, 12164 });

        // Starving Ghosts (9397)
        Story.KillQuest(9397, "yokaitreasure", "Needle Mouth");

        // Sudden Striker (9398)
        Story.KillQuest(9398, "yokaitreasure", "Quicksilver");

        // Onmyoji Maiden (9399)
        Story.MapItemQuest(9399, "yokaitreasure", new[] { 12165, 12166 });

        // Proper Parley (9400)
        Story.KillQuest(9400, "yokaitreasure", "Imperial Warrior");

        // Hunger Pains (9401)
        Story.KillQuest(9401, "yokaitreasure", "Needle Mouth");

        // Terror-cotta Warriors (9402)
        Story.KillQuest(9402, "yokaitreasure", "Imperial Warrior");

        // Indoor Fireworks (9403)
        Story.MapItemQuest(9403, "yokaitreasure", 12167, 4);

        // Sons of Biscuit Eaters (9404)
        Story.KillQuest(9404, "yokaitreasure", new[] { "Needle Mouth", "Imperial Warrior" });

        // Pearl of my Heart (9405)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9405, "yokaitreasure", "Admiral Zheng");
        Core.EquipClass(ClassType.Farm);
    }
}
