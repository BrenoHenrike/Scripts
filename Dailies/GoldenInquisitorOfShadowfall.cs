/*
name:  Golden Inquisitor Daily
description:  Golden Inquisitor
tags: daily, golden inquisitor
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class GoldenInquisitor
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.GoldenInquisitor();

        Core.SetOptions(false);
    }
}
