/*
name: (Ziri) Laser Shark Invasion Story
description: This will finish the Laser Shark Invasion story.
tags: laser, shark, invasion, farm, story, ziri, throne, darkness
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;

public class LaserSharkInvasion
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TOD.LaserSharkInvasion();

        Core.SetOptions(false);
    }
}
