/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Asylum
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
        if (Core.isCompletedBefore(2466))
            return;

        Story.PreLoad(this);

        //Reflect Upon the Situation 2454
        Story.KillQuest(2454, "mirrormaze", "Insane Ghoul");

        //Escape the Insanity! 2455
        Story.MapItemQuest(2455, "mirrormaze", 1522);

        //Smoke and Mirrors 2456
        Story.MapItemQuest(2456, "mirrormaze", 1523, 6);

        //Shot in the Dark 2457
        Story.KillQuest(2457, "mirrormaze", "Insane Ghoul");

        //Cyser - Oh Where Art Thou? 2458
        Story.MapItemQuest(2458, "mirrormaze", 1524);

        // [[Catacombs]]

        //Tricks Trail Trackin 2459
        Story.MapItemQuest(2459, "catacombs", new[] { 1525, 1526, 1527, 1528, 1529, 1530 });

        //Cultivate Answers 2460
        Story.KillQuest(2460, "catacombs", "Scorpion Cultist");

        //One Found Doll 2461
        Story.MapItemQuest(2461, "catacombs", 1531);

        //The Key to Saving Kimberly 2462
        Story.KillQuest(2462, "catacombs", "Scorpion Cultist");

        //Right Tool for the Job 2463
        Story.MapItemQuest(2463, "catacombs", 1532, 10);

        //Pick the Lock 2464
        Story.MapItemQuest(2464, "catacombs", 1533);

        //Destroy DeSawed 2465
        if (!Story.QuestProgression(2465))
        {
            Core.EnsureAccept(2465);
            Core.KillMonster("catacombs", "Frame6", "Left", "Dr. De'Sawed", "Disarmed De'Sawed");
            Core.EnsureComplete(2465);
        }

        //Demolish DeSawed FOR REAL 2466
        if (!Story.QuestProgression(2466))
        {
            Core.EnsureAccept(2466);
            Core.KillMonster("catacombs", "Boss2", "Left", "Dr. De'Sawed", "De'Sawed Defeated");
            Core.EnsureComplete(2466);
        }
    }
}
