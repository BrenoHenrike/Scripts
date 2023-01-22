/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
using Skua.Core.Interfaces;

public class ArchFiendEnchantedOrbs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();
    public CoreHollowborn HB = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Nation.tercessBags);
        Core.BankingBlackList.AddRange(new[] {"Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
            "Mortality Cape of Revontheus", "Facebreakers of Nulgath", "SightBlinder Axes of Nulgath", "Mystic Tribal Sword",
            "King Klunk's Crown", "Golden Shadow Breaker", "Shadow Terror Axe"});
        Core.SetOptions();

        GetAFEO();

        Core.SetOptions(false);
    }

    public void GetAFEO()
    {
        if (Core.CheckInventory("ArchFiend Enchanted Orbs"))
            return;

        Adv.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        Nation.FarmUni13(1);
        Nation.FarmDiamondofNulgath(150);
        HB.FreshSouls(1, 100); // Also has the uni36
        Nation.FarmBloodGem(10);
        Nation.FarmVoucher(false);

        Core.BuyItem("tercessuinotlim", 1820, "ArchFiend Enchanted Orbs");
    }
}
