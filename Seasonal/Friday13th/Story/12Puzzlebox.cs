/*
name: Puzzlebox Story
description: This will finish the Puzzlebox Storyline.
tags: puzzlebox-story, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;

public class Puzzlebox
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFriday13th CoreFriday13th = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreFriday13th.Puzzlebox();

        Core.SetOptions(false);
    }
}
