/*
name: LeeryExchange(gold)
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreFarms.cs


using Skua.Core.Interfaces;

public class leeryExchange
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { Nation.Uni(13), "Diamond of Nulgath", "Relic of Chaos" });
        Core.SetOptions();

        Nation.LeeryExchangeGold();

        Core.SetOptions(false);
    }
}
