//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
using RBot;

public class ArchFiendEnchantedOrbs
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreHollowborn HB = new();

    public void ScriptMain(ScriptInterface bot)
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

        Core.AddDrop("ArchFiend Enchanted Orbs");

        if (!Core.CheckInventory("Unidentified 25"))
        {
            if (!Core.CheckInventory("Unmoulded Fiend Essence"))
            {
                Farm.Gold(15000000);
                Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
                Bot.Wait.ForItemBuy();
            }
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
            Bot.Wait.ForItemBuy();
        }

        Nation.FarmUni13(1);
        Nation.FarmDiamondofNulgath(150);
        HB.FreshSouls(100); // Also has the uni36
        Nation.FarmBloodGem(10);
        Nation.FarmVoucher(false);

        Core.BuyItem("tercessuinotlim", 1820, "ArchFiend Enchanted Orbs");
    }
}
