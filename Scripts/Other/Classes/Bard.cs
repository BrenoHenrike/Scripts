//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class Bard
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBard();

        Core.SetOptions(false);
    }

    public void GetBard(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Bard"))
            return;

        Farm.MythsongREP(4);

        Core.BuyItem("mythsong", 186, "Bard");

        if (rankUpClass)
            Farm.rankUpClass("Bard");
    }
}