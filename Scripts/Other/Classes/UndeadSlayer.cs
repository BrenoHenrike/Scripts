//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class UndeadSlayer
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

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

        Farm.DoomwoodREP();

        Core.BuyItem("necropolis", 408, "UndeadSlayer");

        if (rankUpClass)
            Farm.rankUpClass("UndeadSlayer");
    }
}