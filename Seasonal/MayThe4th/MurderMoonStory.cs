/*
name: Murder Moon Story
description: This will complete the Murder Moon story.
tags: story, quest, seasonal, murder, moon, may-the-4th
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MurderMoon
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MurderMoonStory();

        Core.SetOptions(false);
    }

    public void MurderMoonStory()
    {
        if (!Core.isSeasonalMapActive("murdermoon"))
            return;
        if (Core.isCompletedBefore(8064))
            return;

        //That Is The Way
        if (!Story.QuestProgression(8062))
        {
            Core.EnsureAccept(8062);
            Core.KillMonster("murdermoon", "r2", "Left", "Tempest Soldier", "Soldiers Defeated", 6);
            Core.EnsureComplete(8062);
        }

        //Murder Moon Plans
        if (!Story.QuestProgression(8063))
        {
            Core.EnsureAccept(8063);
            Core.KillMonster("murdermoon", "r2", "Left", "Tempest Soldier", "Murder Moon Plans");
            Story.MapItemQuest(8063, "murdermoon", 8373, 5);
        }
        
        //Revenge of the Fifth
        Story.KillQuest(8064, "murdermoon", "Fifth Sepulchure");
    }
}
