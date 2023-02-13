/*
name: Chopping Maul
description: This will finish the Chopping Maul quest.
tags: story, quest, doomwood, chopping-maul, part1
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;

public class ChoppingMaul
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDoomwood DW = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DW.ChoppingMaul();

        Core.SetOptions(false);
    }
}
