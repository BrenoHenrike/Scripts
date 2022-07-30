//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class YulgathsInn
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
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
        Nation.FarmUni10(400);
        Nation.TheAssistant("Tainted Gem", 200);
        Nation.FarmDarkCrystalShard(250);
        Nation.FarmDiamondofNulgath(200);
        Nation.FarmUni13();
        Nation.FarmVoucher(false);
        // Into:
        Core.BuyItem("archportal", 1211, "Yulgath's Inn");
    }
}
