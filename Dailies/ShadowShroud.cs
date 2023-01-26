/*
name: Shadow Shroud Daily
description: does the daily for: Shadow Shroud
tags: Daily, Shadow Shroud
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class ShadowShroud
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.ShadowShroud();

        Core.SetOptions(false);
    }
}
