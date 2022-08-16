//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class HorcEvader
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetHE();

        Core.SetOptions(false);
    }

    public void GetHE(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Horc Evader"))
            return;

        Adv.BuyItem("bloodtusk", 308, "Horc Evader");

        if (rankUpClass)
            Adv.rankUpClass("Horc Evader");
    }
}