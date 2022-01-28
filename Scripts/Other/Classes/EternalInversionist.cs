//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class EternalInversionist
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetEI();

        Core.SetOptions(false);
    }

    public void GetEI(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Eternal Inversionist"))
            return;

        Farm.EternalREP();

        Core.BuyItem("fourdpyramid", 1275, "Eternal Inversionist");

        if (rankUpClass)
            Farm.rankUpClass("Eternal Inversionist");
    }
}