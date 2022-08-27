//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class YouMadBroBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        while (!Bot.ShouldExit && !Core.HasAchievement(15))
        {
            Farm.AlchemyREP(10);
            {
                Core.Logger($"Buying Reagents");
                GetRunestones();
                // 1 Runestone = 2 Reagents, 30 Runestones = 30 of each reagent
                Core.BuyItem("alchemyacademy", 397, 11475, 30, 2, 1232);
                Core.BuyItem("alchemyacademy", 397, 11478, 30, 2, 1235);
                Core.Logger($"Buying Runestones");
                GetRunestones();
                Core.Join("alchemy");
                Core.Logger($"Beginning RNG");
                Farm.AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Jera, loop: true);
            }
        }
    }

    private void GetRunestones()
    {
        if (!Core.CheckInventory("Dragon Runestone", 30))
        {
            int count = Bot.Inventory.GetQuantity("Dragon Runestone");
            int missing = 30 - count;
            Core.Logger($"Currently have {count} runestones, missing {missing} runestones.");
            Farm.Gold(100000 * missing);
            Core.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", missing);
            Core.BuyItem("alchemyacademy", 395, 7132, missing, 1, 8845);
        }
    }
}