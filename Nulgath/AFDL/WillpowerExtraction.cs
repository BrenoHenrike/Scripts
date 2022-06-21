//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class WillpowerExtraction
{
    public ScriptInterface Bot = ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreNulgath Nulgath = new();

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
        while (!Bot.ShouldExit() && !Core.CheckInventory("Unidentified 34", quant))
        {
            Core.EnsureAccept(5258);

            Adv.BuyItem("shadowfall", 89, "Shadow Lich");
            Bot.Wait.ForItemBuy();
            Adv.BuyItem("arcangrove", 214, "Mystic Tribal Sword");
            Bot.Wait.ForItemBuy();

            if (!Core.CheckInventory("Unidentified 19"))
            {
                if (Core.IsMember)
                {
                    while (!Bot.ShouldExit() && !Core.CheckInventory("Receipt of Swindle", 6))
                        Nulgath.SwindleReturn();
                    Core.BuyItem("tercessuinotlim", 1951, "Unidentified 19");
                    Bot.Wait.ForItemBuy();
                }
                else Nulgath.Supplies("Unidentified 19");
            }

            Core.EquipClass(ClassType.Farm);
            if (!Core.CheckInventory("Necrot", 5))
            {
                Adv.BuyItem("alchemyacademy", 395, "Dragon Runestone", 3, 1);
                Adv.BuyItem("alchemyacademy", 397, "Necrot", 5, 2);
            }

            Adv.BuyItem("tercessuinotlim", 1951, "Chaoroot", 5, 10);
            Bot.Wait.ForItemBuy();
            Adv.BuyItem("tercessuinotlim", 1951, "Doomatter", 5, 10);
            Bot.Wait.ForItemBuy();

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("evilwarnul", "Laken", "King Klunk's Crown", 1, false);

            Nulgath.ApprovalAndFavor(0, 90);

            Nulgath.FarmTotemofNulgath(1);

            Nulgath.EssenceofNulgath(10);

            if (!Core.CheckInventory("Mortality Cape of Revontheus"))
            {
                Nulgath.ApprovalAndFavor(0, 35);
                Adv.BuyItem("evilwarnul", 452, "Mortality Cape of Revontheus");
                Bot.Wait.ForItemBuy();
            }

            if (!Core.CheckInventory("Facebreakers of Nulgath"))
            {
                while (!Bot.ShouldExit() && !Core.CheckInventory("Facebreakers of Nulgath"))
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