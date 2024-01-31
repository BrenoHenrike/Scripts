/*
name: Love
description: This will complete the Love story quest.
tags: love, seasonal, heros-heart-day, heart, hero, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Love
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteStory();
        Core.SetOptions(false);
    }

    public void CompleteStory()
    {
        if (Core.isCompletedBefore(513))
            return;
            
        if (!Core.isSeasonalMapActive("love"))
            return;

        Story.PreLoad(this);

        // In The Mood (506)
        Story.KillQuest(506, "love", "Mood Slime");

        // Hedge Trimming (507)
        Story.KillQuest(507, "love", "Love Shrub");

        // Bear Hugs (508)
        Story.KillQuest(508, "love", "Huggy-Bear");

        // Stoopid Kewpid (509)
        Story.KillQuest(509, "love", "Kewpid");

        // Paper Heart (510)
        Story.KillQuest(510, "citadel", "Inquisitor Guard");

        // Imp Ink (511)
            Core.JoinSWF("mobius", "ChiralValley/town-Mobius-21Feb14.swf", "Slugfit", "Bottom");
        Story.KillQuest(511, "mobius", "Slugfit");

        // Heart Shaped Scale (512)
        Story.KillQuest(512, "lair", "Red Dragon");

        // Special Delivery (513)
        Story.MapItemQuest(513, "love", 98);
    }
}
