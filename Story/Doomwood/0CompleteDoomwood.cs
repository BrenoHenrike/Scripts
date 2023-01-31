/*
name: Complete Doomwood Story
description: This will complete the Doomwood story.
tags: story, quest, doomwood, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;

public class CompleteDoomwood
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDoomwood DW = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DW.CompleteDoomwood();

        Core.SetOptions(false);
    }
}
