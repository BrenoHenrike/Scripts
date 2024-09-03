/*
name: XPBoost
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class XpBoost
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "XP Boost! (10 min)", "Fishing Dynamite" });
        Core.SetOptions();

        Farm.GetBoost(10850, "XP Boost! (10 min)", 9999, 1614, true);

        Core.SetOptions(false);
    }
}
