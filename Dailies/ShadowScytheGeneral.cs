/*
name:  Shadow Scythe Class Daily
description:  Shadow Scythe Class
tags: daily, shadow scythe class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class ShadowScytheClassDaily
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.ShadowScytheClass();

        Core.SetOptions(false);
    }
}
