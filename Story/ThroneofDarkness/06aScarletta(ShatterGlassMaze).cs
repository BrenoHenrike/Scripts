/*
name: (Scarletta) Shatter Glass Maze Story
description: This will finish the Shatter Glass Maze story.
tags: shatter, glass, maze, farm, story, scarletta, throne, darkness
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;

public class ShatterGlassMaze
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TOD.ShatterGlassMaze();

        Core.SetOptions(false);
    }
}
