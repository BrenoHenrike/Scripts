/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class MaxItems
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        MaxBags();

        Core.SetOptions(false);
    }

    public void MaxBags()
    {
        Core.AddDrop(Nation.bagDrops);

        Nation.FarmBloodGem();
        Nation.FarmDarkCrystalShard();
        Nation.FarmDiamondofNulgath();
        Nation.FarmGemofNulgath();
        Nation.FarmTotemofNulgath();
        Nation.FarmUni10();
        Nation.FarmUni13(13);
        Nation.FarmVoucher(true);
        Nation.FarmVoucher(false);
    }
}
