/*
name: Mad Scientist Lab
description: This will finish the Mad Scientist Lab quest.
tags: story, quest, doomwood, mad-scientist-lab, part1
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;

public class MadScientistsLab
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDoomwood DW = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DW.MadScientistsLab();

        Core.SetOptions(false);
    }
}
