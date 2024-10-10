/*
name: Tutorial Story
description: This will finish the Tutorial Story.
tags: story, quest, tutorial
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models;

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

        Core.Logger("Doing Tutorial Badges\n\tStartup may take a minute... blame AE");

        string[] achievements = {
        "Combat", "Interact", "Quest", "Skill", "Shop",
        "Enhance", "Rest", "World", "Emotes", "Travel"
    };

        Core.Join("battleon");
        Core.Join("oaklore", "r1", "Left");

        // Ensure player is in the correct cell
        while (!Bot.ShouldExit && Bot.Player.Cell != "r1")
        {
            Core.Sleep();
            Core.Jump("r1", "Left");
        }

        // Set achievements
        for (int i = 0; i < achievements.Length; i++)
        {
            Core.Logger($"Achievement - {achievements[i]}");
            Core.SetAchievement(22 + i);
            Core.Sleep();
        }
    }

}
