//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class GlacialBerserker
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetGB();

        Core.SetOptions(false);
    }

    public void GetGB(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Glacial Berserker"))
            return;

        Farm.GlaceraREP();

        Core.BuyItem("icewindpass", 1339, "Glacial Berserker", 22266);

        if (rankUpClass)
            Farm.rankUpClass("Glacial Berserker");
    }
}