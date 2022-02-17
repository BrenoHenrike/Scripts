//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class DeathKnight
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetDK();

        Core.SetOptions(false);
    }

    public void GetDK(bool rankUpClass = true)
    {
        if (Core.CheckInventory("DeathKnight"))
            return;

        Farm.DoomwoodREP();

        Core.BuyItem("necropolis", 408, "DeathKnight", shopItemID: 8079);

        if (rankUpClass)
            Adv.rankUpClass("DeathKnight");
    }
}