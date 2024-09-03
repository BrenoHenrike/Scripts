/*
name: RepBoost
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class RepBoost
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "XREPUTATION Boost! (10 min)", "Fishing Dynamite" });
        Core.SetOptions();

        Farm.GetBoost(10997, "REPUTATION Boost! (10 min)", 9999, 1615, true);

        Core.SetOptions(false);
    }
}
