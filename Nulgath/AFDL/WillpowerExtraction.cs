//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class WillpowerExtraction
{
    public ScriptInterface Bot = ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new();
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.BankingBlackList.AddRange(Nulgath.tercessBags);
        Core.BankingBlackList.AddRange(new[] {"Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
            "Mortality Cape of Revontheus", "Facebreakers of Nulgath", "SightBlinder Axes of Nulgath", "Mystic Tribal Sword",
            "King Klunk's Crown", "Golden Shadow Breaker", "Shadow Terror Axe"});
        Core.SetOptions();

        Unidentified34(90);

        Core.SetOptions(false);
    }

    public void Unidentified34(int quant)
    {
        if (Core.CheckInventory("Unidentified 34", quant))
            return;

        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop(Nulgath.tercessBags);
        Core.AddDrop("Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
            "Mortality Cape of Revontheus", "Facebreakers of Nulgath", "SightBlinder Axes of Nulgath", "Mystic Tribal Sword",
            "King Klunk's Crown", "Golden Shadow Breaker", "Shadow Terror Axe");

        int i = 1;
        while (!Core.CheckInventory("Unidentified 34", quant))
        {
            Core.EnsureAccept(5258);

            Farm.EvilREP();
            if (!Core.CheckInventory("Shadow Lich"))
            {
                Farm.Gold(60000);
                Adv.BuyItem("shadowfall", 89, "Shadow Lich");
            }

            Farm.ArcangroveREP();
            Adv.BuyItem("arcangrove", 214, "Mystic Tribal Sword");

            if (!Core.CheckInventory("Unidentified 19"))
            {
                while (!Core.CheckInventory("Receipt of Swindle", 6))
                    Nulgath.SwindleReturn();
                Adv.BuyItem("tercessuinotlim", 1951, "Unidentified 19");
            }

            Core.EquipClass(ClassType.Farm);


            if (!Core.CheckInventory("Necrot", 5))
            {
                Farm.Gold(300000);

                while (!Core.CheckInventory(7132, 3))
                {
                    if (!Core.CheckInventory("Gold Voucher 100k", 3))
                        Adv.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 3, 1);
                    Adv.BuyItem("alchemyacademy", 395, "Dragon Runestone", 3);
                    Bot.Wait.ForPickup("Dragon Runestone");
                }
                Adv.BuyItem("alchemyacademy", 397, "Necrot", 5, 2);
                Bot.Wait.ForPickup("Necrot");
            }

            if (!Core.CheckInventory("chaoroot", 5))
            {
                Farm.Gold(300000);
                Adv.BuyItem("tercessuinotlim", 1951, "Receipt of Swindle", 1);
                Adv.BuyItem("tercessuinotlim", 1951, "Chaoroot", 5, 10);
            }

            if (!Core.CheckInventory("Doomatter", 5))
            {
                Farm.Gold(300000);
                Adv.BuyItem("tercessuinotlim", 1951, "Receipt of Swindle");
                Adv.BuyItem("tercessuinotlim", 1951, "Doomatter", 10, 1);
            }

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("evilwarnul", "Laken", "King Klunk's Crown", 1, false);

            Nulgath.ApprovalAndFavor(0, 90);

            Nulgath.FarmTotemofNulgath(1);

            Nulgath.EssenceofNulgath(10);

            if (!Core.CheckInventory("Mortality Cape of Revontheus"))
            {
                Farm.Gold(10000);
                Nulgath.ApprovalAndFavor(0, 35);
                Adv.BuyItem("evilwarnul", 452, "Mortality Cape of Revontheus");
            }

            if (!Core.CheckInventory("Facebreakers of Nulgath"))
            {
                while (!Bot.Inventory.Contains("Facebreakers of Nulgath"))
                {
                    Core.EnsureAccept(3046);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("citadel", "Grand Inquisitor", "Golden Shadow Breaker", 1, false);
                    Core.HuntMonster("battleundera", "Bone Terror", "Shadow Terror Axe", 1, false);
                    Nulgath.FarmUni13(2);
                    Nulgath.FarmDarkCrystalShard(5);
                    Nulgath.SwindleBulk(5);
                    Nulgath.FarmDiamondofNulgath(1);
                    Core.EnsureComplete(3046);
                    Bot.Player.Pickup("Facebreakers of Nulgath", "SightBlinder Axes of Nulgath");
                    Bot.Sleep(Core.ActionDelay);
                }
            }
            Nulgath.FarmUni13();

            Core.EnsureComplete(5258);
            Bot.Player.Pickup("Unidentified 34");

            Core.Logger($"Completed x{i++}");
        }
    }
}