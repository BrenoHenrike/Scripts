/*
name: ChronoAssassin[Mem]
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class ChronoAssassin
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetChronoAss();

        Core.SetOptions(false);
    }
    public void GetChronoAss(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Chrono Assassin"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Chrono Assassin");
            return;
        }
        if (!Core.IsMember)
            return;

        SaeculumGem(12);
        Core.BuyItem("tachyon", 1251, "Chrono Assassin");

        Bot.Wait.ForPickup("Chrono Assassin");

        if (rankUpClass)
            Adv.RankUpClass("Chrono Assassin");
    }
    public void SaeculumGem(int GemQuant)
    {
        Core.AddDrop("Saeculum Gem");
        Core.Logger($"Farming {GemQuant} Saeculum Gem");
        int i = 1;

        while (!Bot.ShouldExit && !Core.CheckInventory("Saeculum Gem", GemQuant))
        {
            Core.EnsureAccept(5085);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("tachyon", "Svelgr the Devourer", "Svelgr Fang", isTemp: false);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("portalwar", "Chronorysa", "Sands of Time", 6, isTemp: false);
            Core.HuntMonster("portalmaze", "Time Wraith", "Wraith Wisp", 12, isTemp: false);

            Core.EnsureComplete(5085);
            Bot.Wait.ForPickup("Saeculum Gem");
            Core.Logger($"Complete Quest {i++} Time[s]");
        }

    }
}
