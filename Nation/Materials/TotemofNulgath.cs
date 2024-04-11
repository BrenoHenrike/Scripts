/*
name: TotemofNulgath
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TotemofNulgath
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    // public string OptionsStorage = "TotemofNulgath";
    // public bool DontPreconfigure = true;
    // public List<IOption> Options = new()
    // {
    //     new Option<bool>("Choose", "Taro[true]/VoucherItem[False]", "Pets will be Checked first, then; Choose Between the 2 methods (Taro[true] is harder, Voucher Item[false] takes longer)", false),
    //     CoreBots.Instance.SkipOptions
    // };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.FarmTotemofNulgath();

        Core.SetOptions(false);
    }
}
