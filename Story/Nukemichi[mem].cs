/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Nukemichi
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        NukemichiQuests();

        Core.SetOptions(false);
    }

    public void NukemichiQuests()
    {
        if (!Core.IsMember)
        {
            Core.Logger("You must be a Member to access Nukemichi's quests!");
            return;
        }

        if (Core.isCompletedBefore(1592))
            return;

        Story.PreLoad(this);

        // Trusted Sources 1587
        Story.MapItemQuest(1587, "akiba", 794, 10);

        // Shades Of Gray 1588
        Story.MapItemQuest(1588, "akiba", 795);

        // Hiding In Shadows 1589
        Story.KillQuest(1589, "akiba", "Kage Nopperabo");

        // Fading Light 1590
        Story.KillQuest(1590, "akiba", "Kage Nopperabo");

        // Candles in the Dark 1591
        Story.MapItemQuest(1591, "akiba", 796, 8);

        // Shadow Battle! 1592
        Story.KillQuest(1592, "akiba", "Shadow Nukemichi");
    }
}
