/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Manor
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
        if (Core.isCompletedBefore(1062))
            return;

        Story.PreLoad(this);

        //Hard Water 1058
        Story.KillQuest(1058, "Manor", "Amethyst Golem");

        //Raising the Stakes 1059
        Story.MapItemQuest(1059, "Manor", 403, 10);

        //Stop! Potion Time. 1060
        Story.MapItemQuest(1060, "Manor", 402);
        Story.KillQuest(1060, "Manor", "Frigid Frogdrake");

        //Inscrutable Motivation 1061
        Story.MapItemQuest(1061, "Manor", 404, 10);

        //Paradise is Not So Nice 1062
        Story.KillQuest(1062, "Manor", "Bird of Paradise");

    }

}
