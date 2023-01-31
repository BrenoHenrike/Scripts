/*
name: (Scarletta) Tower of Mirrors Story
description: This will finish the Tower of Mirrors story.
tags: tower, mirrors, farm, story, scarletta, throne, darkness
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;

public class TowerofMirrors
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TOD.TowerofMirrors();

        Core.SetOptions(false);
    }
}
