//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class Arachnomancer
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetArach();

        Core.SetOptions(false);
    }

    public void GetArach(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Arachnomancer"))
            return;

        Farm.RavenlossREP();

        Core.BuyItem("ravenloss", 850, "Arachnomancer", shopItemID: 14837);

        if (rankUpClass)
            Farm.rankUpClass("Arachnomancer");
    }
}