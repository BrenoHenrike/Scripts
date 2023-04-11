/*
name: WaterWar
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class WaterWar
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
        if (Core.isCompletedBefore(6821))
            return;

        Story.PreLoad(this);

        //Sploosh Some Solars! 6814
        Story.KillQuest(6814, "waterwar", "Solar Elemental");

        //Sploosh Some Bigger Solars! 6816
        Story.KillQuest(6816, "waterwar", "Solar Elemental");

        //Curious Monkeys 6815
        Story.KillQuest(6815, "waterwar", "Temple Gibbon");

        //Owwie! 6817
        Story.KillQuest(6817, "waterwar", "Aloe");

        //Naughty Monkeys 6818
        Story.KillQuest(6818, "waterwar", "Temple Gibbon");

        //The Big One 6819
        Story.KillQuest(6819, "waterwar", "Solar Flare");

        // Ow, The Blisters 6820
        Story.KillQuest(6820, "waterwar", "Aloe");

        // Even Naughtier Monkeys 6821
        Story.KillQuest(6821, "waterwar", "Temple Gibbon");
    }
}
