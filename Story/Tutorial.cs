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

        Core.Join("oaklore");
        for (int i = 0; Achievements.Count() > i; i++)
        {
            Core.Logger("Achievement - " + Achievements[i]);
            Core.SetAchievement(22 + i);
            Bot.Sleep(Core.ActionDelay);
        }
    }
}
