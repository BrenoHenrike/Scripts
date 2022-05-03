//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Story/WarfuryTraining.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
using RBot;

public class FireChampionsArmor
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public DragonslayerGeneral DSG = new();
    public WarfuryEmblem WFE = new();

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

        PolishedDragonSlayer();
        DSG.EnchantedScaleandClaw(125, 0);
        WFE.WarfuryEmblemFarm(60);
        FlameForgedMetal(10);
        VoidScale(13);
        Farm.Gold(25000000);
        Core.BuyItem("wartraining", 2035, "Gold Voucher 500k", 50);
        Core.BuyItem("wartraining", 2035, "Fire Champion's Armor", shopItemID: 62570);
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
        Core.BuyItem("wartraining", 2035, "Polished DragonSlayer");
    }


    public void FlameForgedMetal(int Metalquant = 10)
    {
        if (Core.CheckInventory("Flame-Forged Metal", Metalquant))
            return;

        Adv.BestGear(GearBoost.Undead);
        Core.AddDrop("Flame-Forged Metal");

        Core.Logger($"Farming \"Flame-Forged Metal\" {Core.CheckInventory("Flame-Forged Metal", toInv: false)}/{Metalquant}");

        Core.RegisterQuests(6975);
        while (!Core.CheckInventory("Flame-Forged Metal", Metalquant))
            Core.HuntMonster("underworld", "Frozen Pyromancer", "Stolen Flame", log: false);

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
