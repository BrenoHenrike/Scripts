//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class EvolvedShaman
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetES();

        Core.SetOptions(false);
    }

    public void GetES(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Evolved Shaman"))
            return;

        Farm.ArcangroveREP();

        Core.BuyItem("arcangrove", 214, "Evolved Shaman", shopItemID: 6396);

        if (rankUpClass)
            Farm.rankUpClass("Evolved Shaman");
    }
}