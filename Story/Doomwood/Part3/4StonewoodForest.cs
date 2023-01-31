/*
name: Stone Wood Forest
description: This will finish the Stone Wood Forest quest.
tags: story, quest, doomwood, stonewood-forest, part3
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;

public class StonewoodForest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDoomwood DW = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DW.StonewoodForest();

        Core.SetOptions(false);
    }
}
