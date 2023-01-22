/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class VoidWarlock
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.BankingBlackList.AddRange(Rewards2);
        Core.SetOptions();

        GetWarlock();

        Core.SetOptions(false);
    }

    public readonly string[] Rewards =
    {
        "Void Warlock",
        "Void Warlock Scythe",
        "Void Warlock Staff",
        "Void Warlock Overfiend Blade",
        "Void Warlock Overfiend Blade Pet",
        "Void Warlock Crown"
    };

    public readonly string[] Rewards2 =
    {
        "Void Warlock Helm",
        "Void Warlock Hood",
        "Void Warlock Cape",
        "Void Warlock Tendrils Cape",
        "Void Warlock Tendrils",
        "Void Warlock Horns",
    };

    public void GetWarlock()
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);
        Core.AddDrop(Rewards2);
        Core.AddDrop("Brittney's Winter Diamond");

        int i = 1;
        Core.Logger("Starting [Tools for the Job] Quest");
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Nation.FarmVoucher(false);
            Nation.FarmBloodGem(90);
            Nation.SwindleBulk(100);
            Core.HuntMonster("northlands", "Aisha's Drake", "Brittney's Winter Diamond", 1, false);
            Nation.FarmUni13(2);

            Core.EnsureAccept(6683);
            Core.EnsureCompleteChoose(6683);
            Core.Logger($"Completed x{i++}");
        }
        Core.Logger("All drops acquired from [Tools for the Job] Quest");
        i = 1;
        Core.Logger("Starting [Corrupted Touch] Quest");
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards2, toInv: false))
        {
            Nation.FarmUni13();
            Nation.FarmVoucher(true);
            Nation.FarmDiamondofNulgath(75);
            Nation.FarmGemofNulgath(100);
            Nation.SwindleBulk(75);
            Nation.ApprovalAndFavor(1000, 0);
            Core.HuntMonster("northlands", "Aisha's Drake", "Brittney's Winter Diamond", 1, false);

            Core.EnsureAccept(6684);
            Core.EnsureCompleteChoose(6684);
            Core.Logger($"Completed x{i++}");
        }
        Core.Logger("All drops acquired from [Corrupted Touch] Quest");
    }
}
