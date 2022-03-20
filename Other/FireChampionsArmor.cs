//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/WarfuryTraining.cs
//cs_include Scripts/Other/WarFuryEmblem.cs

using RBot;

public class FireChampionsArmor
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public DragonslayerGeneral DSG = new DragonslayerGeneral();
    public CoreLegion Legion = new CoreLegion();
    public WarTraining WFT = new WarTraining();
    public WarfuryEmblem WFE = new WarfuryEmblem();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Doall();

        Core.SetOptions(false);
    }

    public void Doall()
    {
        Story.PreLoad();

        PolishedDragonSlayer();
        WFE.WarfuryEmblemFarm();
        DSG.GetDSGeneral();
        FlameForgedMetal();
        VoidScale(13);
    }


    public void PolishedDragonSlayer()
    {
        // Merge the following:

        // Warfury Emblem x30
        WFE.WarfuryEmblemFarm(30);

        // Enchanted Scale x30
        DSG.EnchantedScaleandClaw(30, 0);

        // Dragon Scale (1) x30
        Core.HuntMonster("lair", "Water Draconian", "Dragon Scale", 30, false);

        // 1,000,000 Gold
        Farm.Gold(1000000);
    }


    public void FlameForgedMetal(int Metalquant = 10)
    {
        if (Core.CheckInventory("Flame-Forged Metal", Metalquant))
            return;

        Adv.BestGear(GearBoost.Undead);

        Core.Logger($"Farming \"Flame-Forged Metal\" {Core.CheckInventory("Flame-Forged Metal", toInv: false)}/{Metalquant}");

        int i = 1;

        while (!Core.CheckInventory("Flame-Forged Metal", Metalquant))
        {
            Core.EnsureAccept(6975);
            Core.HuntMonster("underworld", "Frozen Pyromancer", "Stolen Flame", log: false);
            Core.EnsureComplete(6975);
            Bot.Wait.ForDrop("Flame-Forged Metal");
            Core.Logger($"Completed x{i++}, \"Flame-Forged Metal\": {Core.CheckInventory("Flame-Forged Metal", toInv: false)}/{Metalquant}");
        }

    }

    public void VoidScale(int VoidScaleQuant)
    {
        if (Core.CheckInventory("Void Scale", VoidScaleQuant))
            return;

        Adv.BestGear(GearBoost.Dragonkin);

        Core.HuntMonster("underlair", "Archfiend Dragonlord", "Void Scale", VoidScaleQuant, isTemp: false);
    }


}