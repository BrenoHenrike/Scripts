/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DragonslayerGeneral
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDSGeneral();

        Core.SetOptions(false);
    }

    public void GetDSGeneral()
    {
        Adv.GearStore();
        if (Core.CheckInventory(35996))
        {
            Adv.rankUpClass("Dragonslayer General");
            return;
        }

        Farm.Gold(30000);
        EnchantedScaleandClaw(75, 100);
        Core.BuyItem("dragontown", 1286, 35996, shopItemID: 4644);
        Bot.Wait.ForItemBuy();
        Adv.rankUpClass("Dragonslayer General");
        Adv.GearStore(true);
    }

    public void EnchantedScaleandClaw(int ScaleQuant, int ClawQuant)
    {
        if (!Core.CheckInventory(582))
            Core.BuyItem("lair", 38, "Dragonslayer");
        Adv.rankUpClass("Dragonslayer");

        Core.EquipClass(ClassType.Farm);

        if (ScaleQuant > 0)
        {
            Core.AddDrop("Enchanted Scale");
            Core.Logger($"Farming {ScaleQuant} Enchanted Scale, {Bot.Inventory.GetQuantity("Enchanted Scale")} / {ScaleQuant}");
            Core.RegisterQuests(5294);
        }

        Core.KillMonster("dragontown", "r4", "Right", "Tempest Dracolich", "Dragon Claw", ClawQuant, isTemp: false);

        while (!Bot.ShouldExit && !Core.CheckInventory("Enchanted Scale", ScaleQuant))
            Core.KillMonster("dragontown", "r4", "Right", "Tempest Dracolich", "Dracolich Slain", 12, log: false);
        Core.CancelRegisteredQuests();
    }
}
