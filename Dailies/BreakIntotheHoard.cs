/*
name: Break Into the Hoard Daily
description: does the quest `break into the hoard` for the rewards
tags: daily, break into the hoard
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BoneBreak.cs
using Skua.Core.Interfaces;

public class BreakIntotheHoard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public BoneBreak BoneBreak = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BoneBreak.StoryLine();
        Daily.BreakIntotheHoard(false, true);

        Core.SetOptions(false);
    }
}
