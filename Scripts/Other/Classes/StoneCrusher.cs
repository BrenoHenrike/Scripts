//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class StoneCrusher
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetSC();

        Core.SetOptions(false);
    }

    public void GetSC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("StoneCrusher"))
            return;

        Farm.MythsongREP();
        Farm.ArcangroveREP();
        Farm.BrightoakREP();

        Core.BuyItem("Gaiazor", 1210, "Earth's Song Token");
        Core.BuyItem("Gaiazor", 1210, "Shaman Armor");
        Core.BuyItem("Gaiazor", 1210, "StoneCrusher");

        if (rankUpClass)
            Farm.rankUpClass("StoneCrusher");
    }
}