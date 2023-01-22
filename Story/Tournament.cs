/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Tournament
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
        if (Core.isCompletedBefore(2717))
            return;

        Story.PreLoad(this);

        //Guard Swordhaven 2708
        Story.MapItemQuest(2708, "tournament", 1682, 5);
        Story.KillQuest(2708, "tournament", "Suspicious Spy Bot");

        //Stalkers and Mercenaries 2709
        Story.KillQuest(2709, "tournament", new[] { "SandStalker", "Mercenary" });

        //Brave the Bandits 2710
        Story.KillQuest(2710, "tournament", "Greenguard Bandit");

        //Find the Princesses! 2711
        Story.MapItemQuest(2711, "tournament", 1683);
        Story.KillQuest(2711, "tournament", "Princess Tara");

        //Find Princess Brittany! 2712
        Story.MapItemQuest(2712, "tournament", 1684);

        //Battle Khai Kaldun 2713
        Story.KillQuest(2713, "tournament", "Khai Khaldun");

        //Battle Johann Wryce 2714
        Story.KillQuest(2714, "tournament", "Johann Wryce");

        //Battle the Knight of Thorns 2715
        Story.KillQuest(2715, "tournament", "Knight of Thorns");

        //Battle Roderick 2716
        Story.KillQuest(2716, "tournament", "Roderick");

        //Battle Lord Brentan 2717
        Story.KillQuest(2717, "tournament", "Lord Brentan");


    }
}

