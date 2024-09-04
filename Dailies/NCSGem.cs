/*
name: NCS Gem
description: This script will complete the daily quest for NCS Gem.
tags: daily, ncs, hollowborn, a lovely quest-ion, shadow realm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class NCSGem
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.NCSGem();

        Core.SetOptions(false);
    }
}
