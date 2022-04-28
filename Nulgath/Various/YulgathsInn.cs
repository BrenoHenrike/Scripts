//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class YulgathsInn
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GettInn();

        Core.SetOptions(false);
    }

    public void GettInn()
    {
        if (Core.CheckInventory("Yulgath's Inn"))
            return;

        // Merge the following:
        Core.HuntMonster("originul", "Fiend Champion", "Yulgath's Hut", isTemp: false);
        Bot.Wait.ForPickup("Yulgath's Hut");
        Nulgath.FarmUni10(400);
        Nulgath.TheAssistant("Tainted Gem", 200);
        Nulgath.FarmDarkCrystalShard(250);
        Nulgath.FarmDiamondofNulgath(200);
        Nulgath.FarmUni13();
        Nulgath.FarmVoucher(false);
        // Into:
        Core.BuyItem("archportal", 1211, "Yulgath's Inn");
    }
}
