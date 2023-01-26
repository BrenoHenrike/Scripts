/*
name: Mad Weapon Smith Daily
description: does the daily for: Mad Weapon Smith
tags: Daily, Mad Weapon Smith
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class MadWeaponSmith
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.MadWeaponSmith();

        Core.SetOptions(false);
    }
}
