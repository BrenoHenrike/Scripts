/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DeerHunt
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
        if (Core.isCompletedBefore(8432))
            return;

        Story.PreLoad(this);

        // Scout the Area 8423
        Story.MapItemQuest(8423, "deerhunt", 9372);
        Story.KillQuest(8423, "deerhunt", "Deer?");

        // Deer? 8424
        Story.KillQuest(8424, "deerhunt", "Deer?");

        // Comparing Claws 8425
        Story.MapItemQuest(8425, "deerhunt", 9373, 4);
        Story.KillQuest(8425, "deerhunt", new[] { "Scared Wolf", "Frightened Owl" });

        // Lair Investigated 8426
        Story.MapItemQuest(8426, "deerhunt", new[] { 9374, 9375 });

        // Fight or Flight or Freeze 8427
        Story.KillQuest(8427, "deerhunt", "Scared Wolf");

        // Not Deer Hunting 8428
        Story.KillQuest(8428, "deerhunt", "Deer?");

        // Monstrous Tracks 8429
        Story.MapItemQuest(8429, "deerhunt", 9376, 6);

        // Final Blessing 8430
        Story.KillQuest(8430, "deerhunt", new[] { "Deer?", "Scared Wolf", "Frightened Owl" });

        // The Zweinichthirsch 8431
        Story.KillQuest(8431, "deerhunt", "Zweinichthirsch");

        // Cries Investigated 8432
        Story.MapItemQuest(8432, "deerhunt", 9377);
    }
}
