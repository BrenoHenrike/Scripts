/*
name: Tutorial Story
description: This will finish the Tutorial Story.
tags: story, quest, tutorial
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class Tutorial
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badges();

        Core.SetOptions(false);
    }

    public void Badges()
    {
        if (Core.HasAchievement(31))
            return;

        Core.Logger("Doing Tutorial Badges" +
            "\tStartup may take a minute.. blame ae");

        string[] Achievements =  {
            "Combat",
            "Interact",
            "Quest",
            "Skill",
            "Shop",
            "Enhance",
            "Rest",
            "World",
            "Emotes",
            "Travel"
        };

        Core.Logger("need to reset teh map (join > exit > rejoin) for it not to get stuck");
        Core.Join("oaklore", "r1", "Left");
        Core.Join("whitemap");
        Core.Join("oaklore", "r1", "Left");

        //insurance
        while (!Bot.ShouldExit && Bot.Player.Cell != "r1")
        {
            Core.Sleep();
            Core.Jump("r1", "Left");
        }

        for (int i = 0; Achievements.Length > i; i++)
        {
            Core.Logger("Achievement - " + Achievements[i]);
            Core.SetAchievement(22 + i);
            Core.Sleep();
        }
    }
}
