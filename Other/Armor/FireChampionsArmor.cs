/*
name: Fire Champion's Armor
description: This script will farm Fire Champion's Armor.
tags: fire-champion-s-armor, fire-champions-armor, polished-dragon-slayer, flame-forged-metal, void-scale, damage-dragon, hero-s-valiance
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class FireChampionsArmor
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public DragonslayerGeneral DSG = new();
    public WarfuryEmblem WFE = new();

    public void ScriptMain(IScriptInterface bot)
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
        Core.BuyItem("wartraining", 2035, "Fire Champion's Armor", shopItemID: 8759);
    }


    public void PolishedDragonSlayer()
    {
        if (Core.CheckInventory(58462))
            return;

        if (!Core.CheckInventory(582))
            Core.BuyItem("lair", 38, "Dragonslayer");

        Adv.rankUpClass("Dragonslayer");

        Farm.Gold(1000000);
        WFE.WarfuryEmblemFarm(30);
        DSG.EnchantedScaleandClaw(30, 0);
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.CheckInventory(11475, 30))
            Core.HuntMonster("lair", "Water Draconian", log: false);
        Core.BuyItem("wartraining", 2035, "Polished DragonSlayer");
    }


    public void FlameForgedMetal(int Metalquant = 10)
    {
        if (Core.CheckInventory("Flame-Forged Metal", Metalquant))
            return;

        Adv.BestGear(GearBoost.Undead);
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Flame-Forged Metal");

        Core.Logger($"Farming \"Flame-Forged Metal\" {Core.CheckInventory("Flame-Forged Metal", toInv: false)}/{Metalquant}");

        Core.RegisterQuests(6975);
        while (!Bot.ShouldExit && !Core.CheckInventory("Flame-Forged Metal", Metalquant))
            Core.HuntMonster("underworld", "Frozen Pyromancer", "Stolen Flame", log: false);
        Core.CancelRegisteredQuests();
    }

    public void VoidScale(int VoidScaleQuant = 13)
    {
        if (Core.CheckInventory("Void Scale", VoidScaleQuant))
            return;

        Adv.BestGear(GearBoost.Chaos);
        Core.EquipClass(ClassType.Solo);
        Bot.Skills.StartAdvanced(Core.SoloClass, true, Skua.Core.Models.Skills.ClassUseMode.Def);

        Core.HuntMonster("underlair", "Archfiend Dragonlord", "Void Scale", VoidScaleQuant, isTemp: false);
    }


}
