/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class VoidSpartan
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public readonly string[] Rewards =
    {
        "Void Spartan",
        "Void Spartan Daggers",
        "Void Spartan Blade and Shield",
        "Void Spartan Spear",
        "Void Spartan Blade",
        "Void Spartan Banners",
        "Void Spartan Cape",
        "Void Spartan Shielded Cape",
        "Void Spartan Spear and Shield",
        "Void Spartan Helm",
        "Void Spartan Helm and Scarf"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        GetSpartan();

        Core.SetOptions(false);
    }

    public void GetSpartan()
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);
        Core.AddDrop("Zee's Red Jasper", "Fiend Cloak of Nulgath");

        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Core.EnsureAccept(5982);

            Nation.FarmUni13();
            Nation.FarmBloodGem(5);
            Nation.FarmGemofNulgath(10);
            Core.HuntMonster("pyrewatch", "Flame Soldier", "Zee's Red Jasper", 1, false);
            Core.JumpWait();
            Farm.Gold(500000);
            Core.BuyItem("tercessuinotlim", 68, "Fiend Cloak of Nulgath");

            Core.EnsureCompleteChoose(5982, Rewards);
            Bot.Drops.Pickup(Rewards);
            Core.Logger($"Completed x{i++}");
        }
    }
}
