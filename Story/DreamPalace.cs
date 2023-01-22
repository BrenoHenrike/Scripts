/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DreamPalace
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
        if (Core.isCompletedBefore(7874))
            return;

        Story.PreLoad(this);

        // Potent Ruby 7869
        Story.KillQuest(7869, "dreampalace", new[] { "Flaming Harpy|Mote of Power", "Golmoth" });

        // Mystic Sapphire 7870
        Story.KillQuest(7870, "dreampalace", new[] { "Lotus Spider|Mote of Power", "Zelkur" });

        // Living Emerald 7871
        Story.KillQuest(7871, "dreampalace", new[] { "Palace Hound|Mote of Power", "Gazeroth" });

        // Ethereal Diamond 7872
        Story.KillQuest(7872, "dreampalace", new[] { "Ethereal Harpy|Mote of Power", "Zal" });

        // Open the Door 7873
        Story.MapItemQuest(7873, "dreampalace", 7944);

        // Zahad 7874
        Story.KillQuest(7874, "dreampalace", new[] { "Guardian Hound|Mote of Power", "Zahad" });
    }
}
