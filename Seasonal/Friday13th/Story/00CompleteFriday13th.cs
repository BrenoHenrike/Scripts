/*
name: Complete Friday the 13th Stories
description: This will finish all of the Friday the 13th Stories.
tags: friday-the-13th, all-stories, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;

public class CompleteFriday13th
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFriday13th CoreFriday13th = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreFriday13th.CompleteFriday13th();

        Core.SetOptions(false);
    }
}
