/*
name: Wubbles
description: This will complete the Wubbles story quest.
tags: wubbles, seasonal, heros-heart-day, heart, hero, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Wubbles
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
        if (Core.isCompletedBefore(6737))
            return;

        if (!Core.isSeasonalMapActive("wubbles"))
            return;

        Story.PreLoad(this);

        // Get the Costume! (6727)
        Story.MapItemQuest(6727, "wubbles", 6211);

        // Get the Shinies! (6728)
        Story.KillQuest(6728, "catacombs", "Scorpion Cultist");

        // Best Smell EVER! (6729)
        if (!Story.QuestProgression(6729))
        {
            Core.EnsureAccept(6729);
            Core.KillMonster("wanders", "r5", "Left", "Lotus Spider", "Lotus Pollen", 8);
            Core.EnsureComplete(6729);
        }

        // Shimmer and Shine! (6730)
        Story.KillQuest(6730, "dreamnexus", "Aether Serpent");

        // Feel the WUB! (6731)
        Story.MapItemQuest(6731, "battleontown", new[] { 6212, 6213, 6214, 6215, 6216, 6217, 6218 });

        // The Cult of WUB (6732)
        Story.KillQuest(6732, "wubblevania", "Charmed Cultist");

        // Get the Wub Charms! (6733)
        Story.KillQuest(6733, "wubblevania", new[] { "Charmed Cysero", "Charmed Bev", "Charmed Alina", "Charmed Warlic", "Charmed Lim", "Charmed Niamara", "Charmed Murp" });

        // Squishies! (6734)
        Story.KillQuest(6734, "wubblevania", "Wubwub");

        // Get on his Nerves! (6735)
        Story.MapItemQuest(6735, "wubblevania", 6219, 9);

        // Take the Stone! (6736)
        Story.MapItemQuest(6736, "wubblevania", 6220);

        // Take out Mr. Wubbles! (6737)
        Story.KillQuest(6737, "wubblevania", "Mr. Wubbles");
    }
}
