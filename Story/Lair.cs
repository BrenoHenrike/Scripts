/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Lair
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Galanoth();
        Duncan();
        Ferzana();
    }

    public void Galanoth()
    {
        if (Core.isCompletedBefore(169))
            return;

        Story.PreLoad(this);

        // Dragonbane 109
        Story.KillQuest(109, "lair", "Wyvern");

        // Dragon Scales 110
        Story.KillQuest(110, "lair", "Wyvern");

        // Dragon Souvenirs 111
        Story.KillQuest(111, "lair", "Wyvern");

        // Dragonslayer Veteran 165
        Story.KillQuest(165, "lair", "Wyvern");

        // Dragonslayer Sergeant 166
        Story.KillQuest(166, "lair", "Bronze Draconian");

        // Dragonslayer Captain 167
        Story.KillQuest(167, "lair", "Dark Draconian");

        // Dragonslayer Marshal 168
        Story.KillQuest(168, "lair", "Red Dragon");

        // Dragonslayer Reward 169
        Story.KillQuest(169, "lair", "Wyvern");
    }

    public void Duncan()
    {
        if (!Core.IsMember)
        {
            Core.Logger("You must be a Member to complete the quests.");
            return;
        }

        if (Core.isCompletedBefore(1495))
            return;

        Story.PreLoad(this);

        // To Be A DragonHunter 1492
        Story.KillQuest(1492, "lair", "Bronze Draconian");

        // The Easy Path 1493
        Story.MapItemQuest(1493, "lair", 755, 10);

        // The Red Trophy 1494
        Story.KillQuest(1494, "lair", "Red Dragon");

        // Scramble Em! 1495
        Story.MapItemQuest(1495, "lair", 753);
    }

    public void Ferzana()
    {
        if (!Core.IsMember)
        {
            Core.Logger("You must be a Member to complete the quests.");
            return;
        }

        if (Core.isCompletedBefore(1507))
            return;

        Story.PreLoad(this);

        // Do Not Crush The Eggs 1502
        Story.ChainQuest(1502);

        // Heat ‘Em Up! 1503
        Story.MapItemQuest(1503, "lair", 754, 12);

        // Baby Food 1504
        Story.KillQuest(1504, "lair", "Wyvern");

        // Molting 1505
        Story.KillQuest(1505, "lair", "Bronze Draconian");

        // Fatherly Insanity 1506
        Story.KillQuest(1506, "lair", "Dark Draconian");

        // What Needs To Be Done 1507
        Story.KillQuest(1507, "lair", "Onyx Lava Dragon");
    }
}
