/*
name: Tech Fortress
description: This will complete the Tech Fortress quest.
tags: story, quest, doomwood, tech-fortress, part3
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;

public class TechFortress
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDoomwood DW = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DW.TechFortress();

        Core.SetOptions(false);
    }
}
