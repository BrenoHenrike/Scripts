//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class RoyalBattleMage
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetRBM();

        Core.SetOptions(false);
    }

    public void GetRBM(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Royal BattleMage"))
            return;

        Adv.BuyItem("castle", 702, "Royal BattleMage");

        if (rankUpClass)
            Adv.rankUpClass("Royal BattleMage");
    }
}