/*
name:  Sparrows Blood Daily
description:  Sparrows Blood
tags: daily, sparrows blood, void highlord, VHL, nulgath, elders blood
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class SparrowsBlood
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.SparrowsBlood();

        Core.SetOptions(false);
    }
}
