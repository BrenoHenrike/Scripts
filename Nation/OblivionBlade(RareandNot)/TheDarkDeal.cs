/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/OblivionBlade(RareandNot)/CoreOblivionBladeofNulgath.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TheDarkDeal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreOblivionBladeofNulgath COBoN = new();

    public string OptionsStorage = "TheDarkDeal";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("RelicQuant", "Relic Quant", "Relic of Chaos quant", 00),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        COBoN.TheDarkDeal(Bot.Config.Get<int>("RelicQuant"));

        Core.SetOptions(false);
    }
}
