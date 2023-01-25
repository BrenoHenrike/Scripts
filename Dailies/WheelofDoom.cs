/*
name: Wheel of Doom Daily
description: does the daily for: Wheel of Doom
tags: Daily, Wheel of Doom
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class WheelofDoom
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.WheelofDoom();

        Core.SetOptions(false);
    }
}
