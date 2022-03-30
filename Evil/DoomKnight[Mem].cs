//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class DoomKnight
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetDoomKnight();

        Core.SetOptions(false);
    }

    public void GetDoomKnight(bool rankUpClass = true)
    {
        if (Core.CheckInventory("DoomKnight"))
            return;

        Core.BuyItem("necropolis", 26, "Warrior");
        Core.BuyItem("necropolis", 26, "Healer");

        Adv.rankUpClass("Warrior");
        Adv.rankUpClass("Healer");

        Farm.EvilREP(5);

        Core.BuyItem("shadowfall", 100, "DoomKnight");

        if (rankUpClass)
            Adv.rankUpClass("DoomKnight");
    }
}