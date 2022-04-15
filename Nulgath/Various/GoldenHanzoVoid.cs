//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class GoldenHanzoVoid
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNulgath Nulgath = new();

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

        Nulgath.ApprovalAndFavor(50, 200);
        Farm.BattleGroundE(100000);
        Nulgath.SwindleBulk(30);
        Nulgath.TheAssistant("Dark Crystal Shard", 15);
        Nulgath.FarmDiamondofNulgath(50);

        Core.BuyItem("evilwarnul", 456, "Golden Hanzo Void");
        Bot.Wait.ForItemBuy();
    }
}

