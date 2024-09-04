/*
name: Ice Storm
description: This script completes the questline in /icestorm.
tags: ice, storm, seasonal, frostvale, july, storyline
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class IceStorm
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
        if (Core.isCompletedBefore(8176) || !Core.isSeasonalMapActive("icestorm"))
            return;

        Story.PreLoad(this);

        // 6423|Gathering Information
        Story.MapItemQuest(6423, "icestorm", new[] { 5925, 5926, 5927 });

        // 6424|A Test of Alliance
        Story.MapItemQuest(6424, "lair", 5928);
        Story.KillQuest(6424, "lair", "Wyvern");

        // 6425|Inspiring the Young
        Story.KillQuest(6425, "icestorm", new[] { "Earth Dragonling", "Earth Dragonling" });

        // 6426|Following the Path
        Story.MapItemQuest(6426, "icestorm", 5929);
        Story.MapItemQuest(6426, "icestorm", 5930, 6);

        // 6427|Party Crashers
        Story.KillQuest(6427, "icestorm", "Dragon Hunter");

        // 6428|Protect the Dragonlings
        if (!Story.QuestProgression(6428))
        {
            Core.EnsureAccept(6428);
            Core.HuntMonster("icestorm", "Fire Dragonling", "Fire Dragonling Evacuated", 4);
            Core.HuntMonster("icestorm", "Wind Dragonling", "Wind Dragonling Evacuated", 4);
            Core.HuntMonster("icestorm", "Water Dragonling", "Water Dragonling Evacuated", 4);
            Core.HuntMonster("icestorm", "Earth Dragonling", "Earth Dragonling Evacuated", 4);
            Core.EnsureComplete(6428);
        }

        // 6429|The Ultimate Party Pooper
        Story.KillQuest(6429, "icestorm", "Duncan");

        // 6430|Returning the Gifts
        Story.MapItemQuest(6430, "icestorm", 5931, 4);

        // 8173|Bang, Smash, BOOM
        Story.KillQuest(8173, "brightlights", "Chaos Gemrald");

        // 8174|Fire and Fuses
        Story.KillQuest(8174, "brightlights", new[] { "Fire Imp", "Water Goblin" });

        // 8175|Light up the Night
        if (!Story.QuestProgression(8175))
        {
            Core.EnsureAccept(8175);
            Core.HuntMonster("kingcoal", "Frost King", "Gold Powder", 2);
            Core.HuntMonster("blindingsnow", "Nythera", "Indigo Powder", 5);
            Core.HuntMonster("northlandlight", "Santy Claws", "Red Powder", 5);
            Core.HuntMonster("brightlights", "Water Draconian", "Blue Powder", 15);
            Core.HuntMonster("brightlights", "HoliDrake", "Silver Powder", 2);
            Core.EnsureComplete(8175);
        }
    }
}
