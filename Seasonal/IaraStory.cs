/*
name: Iara Story
description: This will finish the Iara seasonal storyline.
tags: iara, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Iara
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
        if (Core.isCompletedBefore(8288) && Core.isSeasonalMapActive("iara"))
            return;

        Story.PreLoad(this);

        //Elemental Balance 8258
        Story.KillQuest(8258, "iara", "Earth Elemental");

        //Bandits and Treasure Hunters 8259
        Story.KillQuest(8259, "iara", new[] { "Bandit", "Treasure Hunter" });

        //Plants be Spooky 8260
        Story.KillQuest(8260, "iara", "Seed Spitter");

        //The Test of Will 8261
        Story.KillQuest(8261, "iara", "Iara");

    }
}
