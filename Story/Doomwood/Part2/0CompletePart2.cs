/*
name: Complete Doomwood Part 2
description: This will complete the Doomwood Part 2 quest.
tags: story, quest, doomwood, complete, part2
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;

public class DoomwoodPart2
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDoomwood DW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DW.DoomwoodPart2();

        Core.SetOptions(false);
    }
}
