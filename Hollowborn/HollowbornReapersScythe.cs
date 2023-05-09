/*
name: HollowbornReapersScythe
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other\MergeShops\ShadowrealmMerge.cs
using Skua.Core.Interfaces;

public class HollowbornScythe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public ShadowrealmMerge SRM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SRM.BuyAllMerge("Hollowborn Reaper's Scythe");

        Core.SetOptions(false);
    }
}
