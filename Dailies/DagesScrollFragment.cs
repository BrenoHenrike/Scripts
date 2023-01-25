/*
name: Dages Scroll Fragment Daily
description: does the daily for: Dages Scroll Fragment
tags: Daily, Dages Scroll Fragment
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class DagesScrollFragment
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.DagesScrollFragment();

        Core.SetOptions(false);
    }
}
