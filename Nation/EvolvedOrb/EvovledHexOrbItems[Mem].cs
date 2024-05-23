/*
name: EvovledHexOrbItems[Mem]
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedHexOrb.cs
//cs_include Scripts/Other/Various/Potions.cs
using Skua.Core.Interfaces;

public class EvovledHexOrbItems
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreNation Nation = new();
    private EvolvedHexOrb EHO = new();
    private PotionBuyer Potion = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        if (!Core.IsMember || Core.CheckInventory(Rewards, toInv: false))
            return;

        EHO.GetEvolvedHexOrb();
        Core.Unbank(33197); //make sure ya have it

        UnlockedevovledHexArmor();
        CutOffOneHead();
        EmpoweringtheEmpowered();
        //yet to be added:
        // Unlock the Evolved Hex (Rare)
        // Unlock the Evolved Hex (Arcane)
    }

    public void UnlockedevovledHexArmor()
    {
        #region Required Items

        if (!Core.CheckInventory("Platinum Coin of Nulgath: 2500") || !Core.CheckInventory(33197))
            return;

        Adv.BuyItem(Bot.Map.Name, 299, "Oracle");
        Adv.RankUpClass("Oracle");

        #endregion

        // Unlock the Evolved Hex Armor - 4764
        Core.AddDrop("Evolved Hex of Nulgath");
        Core.EnsureAccept(4764);

        Nation.FarmUni13(3);
        Nation.TheAssistant("Unidentified 22", 1);
        Nation.TheAssistant("4th Betrayal Blade of Nulgath", 1);
        Core.HuntMonster("tercessuinotlim", "Taro Blademaster", "Polish's Book of Avalon", isTemp: false);
        Farm.BludrutBrawlBoss(quant: 500);
        Adv.BuyItem(Bot.Map.Name, 222, "Warden of Light");

        Core.EnsureComplete(4764);
        Bot.Wait.ForPickup("Evolved Hex of Nulgath");
    }

    public void CutOffOneHead()
    {
        // Unlock the Evolved Hex Armor 4769
        if (!Core.CheckInventory("Platinum Coin of Nulgath: 300") || !Core.CheckInventory(33197))
            return;

        Core.EnsureAccept(4769);

        Nation.FarmDiamondofNulgath(10);
        Nation.FarmVoucher(false);
        Nation.FarmBloodGem(5);
        Nation.FarmTotemofNulgath(1);
        Nation.FarmBloodGem(5);
        Core.HuntMonster("marsh2", "Soulseeker", "Deathdoll enchant", isTemp: false);
        Core.HuntMonster("void", "Void Dragon", "Heart of the Void", isTemp: false);

        Core.EnsureComplete(4769);
        Bot.Wait.ForPickup("Evolved Hex Helm");
    }

    public void EmpoweringtheEmpowered()
    {
        if (!Core.CheckInventory("Platinum Coin of Nulgath: 2500") || !Core.CheckInventory(33197))
            return;

        //Blood Tears - 4783
        Core.EnsureAccept(4783);
        Nation.FarmUni10(30);
        Nation.SwindleBulk(30);
        Nation.FarmDarkCrystalShard(30);
        Nation.FarmVoucher(false);
        Nation.FarmTotemofNulgath(10);
        Nation.Supplies("Unidentified 29");
        Adv.BuyItem("alchemyacademy", 2114, 11626, 10, shopItemID: 9724);

        Core.EnsureComplete(4783);

        Bot.Wait.ForPickup("Evolved Hex Staf");
    }

    private readonly string[] Rewards =
    {
        "Evolved Hex of Nulgath",
        "Evolved Hex Helm",
        "Evolved Hex Staf"
    };
}
