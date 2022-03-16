//cs_include Scripts/CoreBots.cs
using RBot;

public class Adam1a1Merge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        //Needed AddDrop
        Core.AddDrop("Fresh Ectoplasm", "IOU Slip");

        while (!Core.CheckInventory("IOU Slip", 100) | !Core.CheckInventory("Fresh Ectoplasm", 300))
        {
            //Fresh Ecotplasm & IOU Slip
            Core.EnsureAccept(8009);
            Core.HuntMonster("vendorbooths", "Caffeine Imp", "Coffee Beans", 10);
            Core.HuntMonster("djinn", "Lamia", "Tasty Poison", 10);
            Core.HuntMonster("charredpath", "Toxic Wisteria", "Necessary Antidote");
            Core.EnsureComplete(8009);
        }
    }
}