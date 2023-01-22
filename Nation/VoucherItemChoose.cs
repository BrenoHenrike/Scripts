/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class VoucherItem
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public string OptionsStorage = "VoucherItem";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<ChooseReward>("VoucherItem", "Choose Your Item", "Extra stuff to choose, if you have any suggestions -form in disc, and put it under request. or dm Tato(the retarded one on disc)", ChooseReward.TaintedGem),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Choose();

        Core.SetOptions(false);
    }

    public void Choose()
    {
        while (!Bot.ShouldExit)
            Nation.VoucherItemTotemofNulgath(Bot.Config.Get<ChooseReward>("VoucherItem"));
    }
}
