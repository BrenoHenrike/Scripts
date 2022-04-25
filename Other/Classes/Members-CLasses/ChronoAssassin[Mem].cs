//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class ChronoAssassin
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetChronoAss();

        Core.SetOptions(false);
    }
    public void GetChronoAss()
    {
        if (Core.CheckInventory("Chrono Assassin"))
        {
            Adv.rankUpClass("Chrono Assassin");
            return;
        }
        if (!Core.IsMember)
            return;

        SaeculumGem(12);
        Core.BuyItem("tachyon", 1251, "Chrono Assassin");

        Bot.Wait.ForPickup("Chrono Assassin");
        Adv.GearStore();
        Adv.rankUpClass("Chrono Assassin");
        Adv.GearStore(true);
    }
    public void SaeculumGem(int GemQuant)
    {
        Core.AddDrop("Saeculum Gem");
        Core.Logger($"Farming {GemQuant} Saeculum Gem");
        int i = 1;

        while (!Core.CheckInventory("Saeculum Gem", GemQuant))
        {
            Core.EnsureAccept(5085);
            Core.HuntMonster("tachyon", "Svelgr the Devourer", "Svelgr Fang", isTemp: false);
            Core.HuntMonster("portalwar", "Chronorysa", "Sands of Time", 6, isTemp: false);
            Core.HuntMonster("portalmaze", "Time Wraith", "Wraith Wisp", 12, isTemp: false);
            Core.EnsureComplete(5085);
            Bot.Wait.ForPickup("Saeculum Gem");
            Core.Logger($"Complete Quest {i++} Time[s]");
        }


    }
}