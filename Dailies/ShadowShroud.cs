/*
name:  Shadow Shroud Daily
description:  Shadow Shroud
tags: daily, shadow shroud
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
