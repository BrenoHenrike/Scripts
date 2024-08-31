/*
name: 0MaxBags
description: Farms the max stack of all the nation materials using various methods based off the pets / quest you have avaiable.
tags: nation, nulgath, bags, all, 🖕
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
        Core.BankingBlackList.AddRange(Nation.bagDrops[..9]);
        Core.SetOptions();

        MaxBags();

        Core.SetOptions(false);
    }

    public void MaxBags()
    {
        Core.AddDrop(Nation.bagDrops[..9]);

        Nation.FarmBloodGem();
        Nation.FarmDarkCrystalShard();
        Nation.FarmDiamondofNulgath();
        Nation.FarmGemofNulgath();
        Nation.FarmTotemofNulgath();
        Nation.FarmTaintedGem();
        Nation.FarmUni10();
        Nation.FarmUni13();
        Nation.FarmVoucher(true);
        Nation.FarmVoucher(false);
    }
}
