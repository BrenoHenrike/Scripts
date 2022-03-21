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

        GetFireChampsArmor();

        Core.SetOptions(false);
    }

    public void GetFireChampsArmor()
    {
        if (Core.CheckInventory(62570))
            return;

        Story.PreLoad();

        PolishedDragonSlayer();
        DSG.EnchantedScaleandClaw(125, 0);
        WFE.WarfuryEmblemFarm(60);
        FlameForgedMetal(10);
        VoidScale(13);
        Farm.Gold(25000000);
        Core.BuyItem("wartraining", 2035, itemID: 61043, 50);
        Core.BuyItem("wartraining", 2035, itemID: 62570, shopItemID: 8759);
    }


    public void PolishedDragonSlayer()
    {
        if (Core.CheckInventory(58462))
            return;

        DSG.GetDSGeneral();
        Farm.Gold(1000000);
        WFE.WarfuryEmblemFarm(30);
        DSG.EnchantedScaleandClaw(30, 0);
        Core.HuntMonster("lair", "Water Draconian", "Dragon Scale", 30, false);
        Core.BuyItem("wartraining", 2035, itemID: 58462, shopItemID: 8756);
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

    public void VoidScale(int VoidScaleQuant = 13)
    {
        if (Core.CheckInventory("Void Scale", VoidScaleQuant))
            return;

        Adv.BestGear(GearBoost.Chaos);
        Core.EquipClass(ClassType.Solo);
        Bot.Skills.StartAdvanced(Core.SoloClass, true, RBot.Skills.ClassUseMode.Def);

        Core.HuntMonster("underlair", "Archfiend Dragonlord", "Void Scale", VoidScaleQuant, isTemp: false);
    }


}