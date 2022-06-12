//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nulgath/AFDL/WillpowerExtraction.cs
using RBot;

public class ArchFiendSpear
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreNulgath Nulgath = new();
    public CoreHollowborn HB = new();
    public WillpowerExtraction Will = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.BankingBlackList.AddRange(Nulgath.tercessBags);
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

        Core.AddDrop("ArchFiend Spear");

        if (!Core.CheckInventory("Unidentified 25"))
        {
            if (!Core.CheckInventory("Unmoulded Fiend Essence"))
            {
                Farm.Gold(15000000);
                Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
                Bot.Wait.ForPickup("Unmoulded Fiend Essence");
            }
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }

        Will.Unidentified34(1);
        Nulgath.FarmDiamondofNulgath(200);
        HB.FreshSouls(100); // Also has the uni36
        Nulgath.FarmBloodGem(20);
        Nulgath.FarmVoucher(false);

        Core.BuyItem("tercessuinotlim", 1820, "ArchFiend Spear");
    }
}
