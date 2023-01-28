/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class WillpowerExtraction
{
    public IScriptInterface Bot = IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Nation.tercessBags);
        Core.BankingBlackList.AddRange(new[] {"Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
            "Mortality Cape of Revontheus", "Facebreakers of Nulgath", "SightBlinder Axes of Nulgath", "Mystic Tribal Sword",
            "King Klunk's Crown", "Golden Shadow Breaker", "Shadow Terror Axe"});
        Core.SetOptions();

        Unidentified34(90);

        Core.SetOptions(false);
    }

    public void Unidentified34(int quant = 300)
    {
        if (Core.CheckInventory("Unidentified 34", quant))
            return;

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Nation.tercessBags);
        Core.AddDrop("Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
            "Mortality Cape of Revontheus", "Facebreakers of Nulgath", "SightBlinder Axes of Nulgath", "Mystic Tribal Sword",
            "King Klunk's Crown", "Golden Shadow Breaker", "Shadow Terror Axe");

        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory("Unidentified 34", quant))
        {
            Core.EnsureAccept(5258);

            Adv.BuyItem("shadowfall", 89, "Shadow Lich");
            Adv.BuyItem("arcangrove", 214, "Mystic Tribal Sword");

            uni19(1);

            Core.EquipClass(ClassType.Farm);

            if (!Core.CheckInventory("Necrot", 5))
            {
                Adv.BuyItem("tercessuinotlim", 1951, "Necrot", 10);
                Bot.Wait.ForItemBuy();
            }
            
            if (!Core.CheckInventory("Chaoroot", 5))
            {
                Adv.BuyItem("tercessuinotlim", 1951, "Chaoroot", 10);
                Bot.Wait.ForItemBuy();
            }

            if (!Core.CheckInventory("Doomatter", 5))
            {
                Adv.BuyItem("tercessuinotlim", 1951, "Doomatter", 10);
                Bot.Wait.ForItemBuy();
            }

            if (!Core.CheckInventory("Mortality Cape of Revontheus"))
            {
                Nation.ApprovalAndFavor(0, 35);
                Adv.BuyItem("evilwarnul", 452, 13167);
                Bot.Wait.ForItemBuy();
            }

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("evilwarnul", "Laken", "King Klunk's Crown", 1, false);

            Nation.ApprovalAndFavor(0, 90);

            Nation.FarmTotemofNulgath(1);

            Nation.EssenceofNulgath(10);


            if (!Core.CheckInventory("Facebreakers of Nulgath"))
            {
                while (!Bot.ShouldExit && !Core.CheckInventory("Facebreakers of Nulgath"))
                {
                    Core.EnsureAccept(3046);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("citadel", "Grand Inquisitor", "Golden Shadow Breaker", 1, false);
                    Core.HuntMonster("battleundera", "Bone Terror", "Shadow Terror Axe", 1, false);
                    Nation.FarmUni13(2);
                    Nation.FarmDarkCrystalShard(5);
                    Nation.SwindleBulk(5);
                    Nation.FarmDiamondofNulgath(1);
                    Core.EnsureComplete(3046);
                    Bot.Drops.Pickup("Facebreakers of Nulgath", "SightBlinder Axes of Nulgath");
                    Bot.Sleep(Core.ActionDelay);
                }
            }
            Nation.FarmUni13();

            Core.EnsureComplete(5258);
            Bot.Drops.Pickup("Unidentified 34");

            Core.Logger($"Completed x{i++}");
        }
    }

    public void uni19(int quant = 1)
    {
        if (Core.CheckInventory("Unidentified 19"))
            return;

        if (Core.IsMember)
            Adv.BuyItem("tercessuinotlim", 1951, "Unidentified 19");
            
        else Nation.Supplies("Unidentified 19");
    }
}
