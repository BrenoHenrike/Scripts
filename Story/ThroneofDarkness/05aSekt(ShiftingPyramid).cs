/*
name: (Sekt) Shifting Pyramid Story
description: This will finish the Shifting Pyramid story.
tags: shifting, pyramid, farm, story, sekt, throne, darkness
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;

public class ShiftingPyramid
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TOD.ShiftingPyramid();

        Core.SetOptions(false);
    }
}
