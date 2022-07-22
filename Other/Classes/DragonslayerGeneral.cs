//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;

public class DragonslayerGeneral
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
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

    public void EnchantedScaleandClaw(int ScaleQuant, int ClawqQant)
    {
        InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == ("DragonSlayer").ToLower() && i.Category == ItemCategory.Class);

        Adv.GearStore();

        if (!Core.CheckInventory(582))
            Core.BuyItem("lair", 38, "Dragonslayer");
        Bot.Wait.ForItemBuy();

        if (Core.CheckInventory(582) && itemInv.Quantity != 302500)
            Adv.rankUpClass("Dragonslayer");

        Adv.GearStore(true);

        Core.EquipClass(ClassType.Farm);

        if (ClawqQant > 0)
        {
            if (ScaleQuant > 0)
            {
                Core.AddDrop("Enchanted Scale", "Dragon Claw");
                Core.RegisterQuests(5294);
                Core.Logger($"Farming {ScaleQuant} Enchanted Scale, {Bot.Inventory.GetQuantity("Enchanted Scale")} / {ScaleQuant}");
            }
            else Core.AddDrop("Dragon Claw");
            Core.Logger($"Farming {ClawqQant} Dragon Claw, {Bot.Inventory.GetQuantity("Enchanted Scale")} / {ClawqQant}");
            Core.KillMonster("dragontown", "r4", "Right", "Tempest Dracolich", "Dragon Claw", ClawqQant, isTemp: false);
            Core.CancelRegisteredQuests();
        }

        if (ScaleQuant > 0 && !Core.CheckInventory("Enchanted Scale", ScaleQuant))
        {
            Core.AddDrop("Enchanted Scale");
            Core.Logger($"Farming {ScaleQuant} Enchanted Scale, {Bot.Inventory.GetQuantity("Enchanted Scale")} / {ScaleQuant}");
            Core.RegisterQuests(5294);
            while (!Bot.ShouldExit() && !Core.CheckInventory("Enchanted Scale", ScaleQuant))
                Core.KillMonster("dragontown", "r4", "Right", "Tempest Dracolich", "Dracolich Slain", 12, log: false);

            Core.CancelRegisteredQuests();
        }
    }
}