//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class YouMadBroBadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        while (!Bot.ShouldExit() && !Core.HasAchievement(15))
        {
            Farm.AlchemyREP(10);
            {
                if (!Core.CheckInventory("Dragon Runestone", 10))
                {
                    Farm.Gold(1000000);
                    Core.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 10);
                }
                Core.BuyItem("alchemyacademy", 395, 7132, 10, 10);
                Core.BuyItem("alchemyacademy", 397, 11475, 5, 2);
                Core.BuyItem("alchemyacademy", 397, 11478, 5, 2);
                Core.Join("alchemy");
                Farm.AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Jera, loop: true);
            }
        }
    }
}