/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/Various/ArchFiendEnchantedOrbs.cs
using Skua.Core.Interfaces;

public class ArchFiendSpear
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreNation Nation = new();
    public CoreHollowborn HB = new();
    public WillpowerExtraction Will = new();
    public ArchFiendEnchantedOrbs AFEO = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Nation.tercessBags);
        Core.BankingBlackList.AddRange(new[] {"Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
            "Mortality Cape of Revontheus", "Facebreakers of Nulgath", "SightBlinder Axes of Nulgath", "Mystic Tribal Sword",
            "King Klunk's Crown", "Golden Shadow Breaker", "Shadow Terror Axe"});
        Core.SetOptions();

        GetAFS();

        Core.SetOptions(false);
    }

    public void GetAFS()
    {
        if (Core.CheckInventory("ArchFiend Spear"))
            return;

        Adv.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        AFEO.GetAFEO();
        Will.Unidentified34(1);
        Nation.FarmDiamondofNulgath(200);
        HB.FreshSouls(1, 100); // Also has the uni36
        Nation.FarmBloodGem(20);
        Nation.FarmVoucher(false);

        Core.BuyItem("tercessuinotlim", 1820, "ArchFiend Spear");
    }
}
