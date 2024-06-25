/*
name: Dark Path Story
description: This script completes the storyline in darkpath and voidvault.
tags: darkpath, voidvault, seasonal, dage, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
using Skua.Core.Interfaces;

public class DarkPath
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDageBirthday Dage = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Dage.DarkPath();
        Core.SetOptions(false);
    }
}
