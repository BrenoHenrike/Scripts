/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/Shinkansen.cs
using Skua.Core.Interfaces;

public class Eden
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Shinkansen Shin = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(8801))
            return;

        Shin.Storyline();
        Story.PreLoad(this);

        // Welcome to Eden! 8795
        Story.KillQuest(8795, "eden", "Harmless Choir");

        // The Fashion and Arcade District 8796
        if (!Story.QuestProgression(8796))
        {
            Core.EnsureAccept(8796);
            Core.HuntMonster("eden", "Cosplayer", "Pictures Taken", 6);
            Core.HuntMonster("eden", "Klawaii Machine", "Prize Won");
            Core.EnsureComplete(8796);
        }

        // Eden City KotaMart 8797
        Story.KillQuest(8797, "eden", "SalaryMan");

        // Save the Citizens! 8798
        if (!Story.QuestProgression(8798))
        {
            Core.EnsureAccept(8798);
            Story.MapItemQuest(8798, "eden", 10448, 5);
            Core.HuntMonster("eden", "SalaryMan Anomaly", "Salarymen Anomalies Slain", 7);
            Core.HuntMonster("eden", "Cosplayer Anomaly", "Cosplayer Anomalies Slain", 7);
            Core.HuntMonster("eden", "Police Anomaly", "Police Anomalies Slain", 7);
            Core.EnsureComplete(8798);
        }

        // Armorchy 8799
        Story.KillQuest(8799, "eden", "CRC Power Armor");

        // Clear the Way! 8800
        Story.MapItemQuest(8800, "eden", 10449, 3);
        Story.KillQuest(8800, "eden", "Yokaified Experiment 1");

        // Protect the Reactor! 8801
        Story.KillQuest(8801, "eden", "Major Anomaly");
    }
}
