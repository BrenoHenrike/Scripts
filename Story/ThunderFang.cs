/*
name: Thunderfang Story
description: does the quest from dragonmaster stormscythe in /thunderfang
tags: story, thunderfang, stormcachemerge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ThunderFang
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(4246))
            return;

        Story.PreLoad(this);

        // Hailstone Ore 4240
        Story.KillQuest(4240, "thunderfang", "Storm Draconian");

        // Scroll of Fury 4241
        Story.KillQuest(4241, "thunderfang", "Lightning Ball");

        // Shards and Sparks 4242
        Story.KillQuest(4242, "thunderfang", new[] { "Lightning Ball", "Energy Elemental" });

        // Hunt for the Storm Cache 4243
        Story.KillQuest(4243, "thunderfang", "Energy Elemental");

        // Lord of Thunderfang Spire 4244
        if (Core.IsMember)
            Story.KillQuest(4244, "thunderfang", "Tonitru");

        // Storm Strike Shards 4245
        Story.KillQuest(4245, "thunderfang", "Lightning Ball");

        // Claim the Tempestas Egg 4246
        Story.KillQuest(4246, "thunderfang", "Storm Draconian");

    }
}
