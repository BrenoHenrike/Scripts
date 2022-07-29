//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class GoldenHanzoVoid
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetGHV();

        Core.SetOptions(false);
    }

    public void GetGHV()
    {
        if (Core.CheckInventory("Golden Hanzo Void"))
            return;

        Nation.ApprovalAndFavor(50, 200);
        Farm.BattleGroundE(100000);
        Nation.SwindleBulk(30);
        Nation.TheAssistant("Dark Crystal Shard", 15);
        Nation.FarmDiamondofNulgath(50);
        Nation.FarmVoucher(false);

        Core.BuyItem("evilwarnul", 456, "Golden Hanzo Void");
        Bot.Wait.ForItemBuy();
    }
}

