//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class ChaorrupterUnlocked
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetYourSword();

        Core.SetOptions(false);
    }

    public void GetYourSword()
    {
        Core.AddDrop("Chaorrupter Unlocked");

        Core.AddDrop("Chaorrupter Unlocked", "ExampleItem2", "ExampleItem3", "ExampleItem4");
        if (Core.IsMember)
            Core.BuyItem("chaoswar", 641, "Chaorrupter Unlocked", shopItemID: 11172);
        Adv.EnhanceItem("Chaorrupter Unlocked", EnhancementType.Lucky);
        if (!Core.IsMember)
            Core.HuntMonster("chaoswar", "High Chaos Knight", "Chaorrupter Unlocked");
        Adv.EnhanceItem("Chaorrupter Unlocked", EnhancementType.Lucky);
    }
}