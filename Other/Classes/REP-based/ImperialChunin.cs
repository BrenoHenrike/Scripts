/*
name: ImperialChunin
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class ImperialChunin
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetIC();

        Core.SetOptions(false);
    }

    public void GetIC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Imperial Chunin"))
            return;

        Farm.YokaiREP();

        Core.BuyItem("dragonkoiz", 95, "Imperial Chunin");

        if (rankUpClass)
            Adv.RankUpClass("Imperial Chunin");
    }
}
