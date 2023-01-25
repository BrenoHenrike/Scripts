/*
name: Elders Blood Daily
description: does the daily for: Elders Blood
tags: Daily, Elders Blood, nulgath, roentgenium
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class EldersBlood
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.EldersBlood();

        Core.SetOptions(false);
    }
}
