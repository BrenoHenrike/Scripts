//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class Shaman
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetShaman();

        Core.SetOptions(false);
    }

    public void GetShaman(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Shaman"))
            return;

        Farm.ArcangroveREP();

        Core.BuyItem("arcangrove", 214, "Shaman");

        if (rankUpClass)
            Farm.rankUpClass("Shaman");
    }
}