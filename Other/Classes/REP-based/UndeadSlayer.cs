//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class UndeadSlayer
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetUS();

        Core.SetOptions(false);
    }

    public void GetUS(bool rankUpClass = true)
    {
        if (Core.CheckInventory("UndeadSlayer"))
            return;
        if (!Core.IsMember)
            return;

        Farm.DoomwoodREP();

        Core.BuyItem("necropolis", 408, "UndeadSlayer");

        if (rankUpClass)
            Adv.rankUpClass("UndeadSlayer");
    }
}