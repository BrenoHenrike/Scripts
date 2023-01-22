/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Ubear
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
        if (Core.isCompletedBefore(7442))
            return;

        Story.PreLoad(this);

        //Ubear Token 7430
        Story.BuyQuest(7430, "ubear", 1851, "Ubear Token");

        //WHY ARE THERE BEES?! 7431
        Story.KillQuest(7431, "ubear", "Giant Bee");

        //That’s How We’ll Get Ants 7432
        Story.MapItemQuest(7432, "ubear", 7199, 5);
        Story.KillQuest(7432, "ubear", "Honey Glob");

        //Door Jam 7433
        Story.MapItemQuest(7433, "ubear", 7200);
        Story.KillQuest(7433, "ubear", "Giant Bee");

        //Oh... gross 7434
        Story.KillQuest(7434, "ubear", "Lil' Poot");

        //Blockage 7435
        Story.KillQuest(7435, "ubear", "Cornholio");

        //LIMft Token 7436
        Story.BuyQuest(7436, "limft", 1852, "LIMFT Token");

        //FIRE! 7437
        Story.KillQuest(7437, "limft", "Electrical Fire");

        //Smoke! 7438
        Story.MapItemQuest(7438, "limft", 7201, 6);
        Story.KillQuest(7438, "limft", "Smoke Elemental");

        //Scavengers 7439
        Story.MapItemQuest(7439, "limft", 7202, 5);
        Story.KillQuest(7439, "limft", new[] { "Smoke Elemental", "Sock Golem" });

        //Repair Time 7440
        Story.MapItemQuest(7440, "limft", 7203, 6);

        //Get Proof 7441
        Story.KillQuest(7441, "limft", "Sock Golem");

        //Stop the (U)bear! 7442
        Story.KillQuest(7442, "limft", "Ubear");

    }
}
