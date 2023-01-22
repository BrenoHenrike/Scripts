/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class VoidShogun
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public readonly string[] Rewards =
    {
        "Void Shogun",
        "Void Shogun Mask",
        "Void Shogun Helm",
        "Void Shogun Masked Helm",
        "Void Shogun Banner",
        "Void Shogun Runes",
        "Void Shogun Katana",
        "Void Shogun Naginata",
        "Mini Void Shogun",
        "Mini Void Shogun Battlepet",
        "Void Shogun Katanas on your Hip",
        "Dual Void Shogun Katanas"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        GetShogun();

        Core.SetOptions(false);
    }

    public void GetShogun()
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);
        Core.AddDrop("Void Voucher", "DaiTengu Blade of Wind", "Orochi's Shadow");

        if (!Core.CheckInventory("Void Monk of Nulgath"))
        {
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("quibblehunt", "r2", "Left", "*", "Void Voucher", 500, false);
            Core.JumpWait();
            Core.BuyItem("quibblehunt", 1421, "Void Monk of Nulgath");
        }

        Farm.YokaiREP();
        Nation.FarmVoucher(false);

        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Core.EnsureAccept(6484);

            Nation.FarmUni13();
            Nation.FarmBloodGem(7);
            Nation.Supplies("Unidentified 24");

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("hachiko", "Dai Tengu", "DaiTengu Blade of Wind", 1, false);
            Core.HuntMonster("shogunwar", "Orochi", "Orochi's Shadow", 1, false);
            Core.HuntMonster("necrocavern", "Shadowstone Support", "ShadowStone Rune");

            Core.EnsureCompleteChoose(6484, Rewards);
            Bot.Drops.Pickup(Rewards);
            Core.Logger($"Completed x{i++}");
        }
    }
}
