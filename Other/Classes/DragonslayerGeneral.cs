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
        if (Core.CheckInventory(35996))
        {
            Adv.rankUpClass("Dragonslayer General");
            return;
        }

        Farm.Gold(30000);
        EnchantedScaleandClaw(75, 100);
        Core.BuyItem("dragontown", 1286, 35996, shopItemID: 4644);

        Adv.GearStore();
        Adv.rankUpClass("Dragonslayer General");
        Adv.GearStore(true);
    }

    public void EnchantedScaleandClaw(int ScaleQuant, int CLawquant)
    {

        if (!Core.CheckInventory(582))
        {
            Core.BuyItem("lair", 38, "Dragonslayer");
            Adv.GearStore();
            Adv.EnhanceItem("Dragonslayer", EnhancementType.Lucky);
            Adv.rankUpClass("Dragonslayer");
            Adv.GearStore(true);
        }


        Adv.GearStore();

        InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == ("DragonSlayer").ToLower() && i.Category == ItemCategory.Class);

        if (Core.CheckInventory(582) && itemInv.Quantity != 302500)
            Adv.rankUpClass("Dragonslayer");

        Adv.GearStore(true);
        Adv.BestGear(GearBoost.Dragonkin);
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Enchanted Scale", "Dragon Claw");

        Core.Logger($"Farming {CLawquant} Dragon Claw");
        Core.HuntMonster("dragontown", "Tempest Dracolich", "Dragon Claw", CLawquant, isTemp: false, log: false);

        Core.Logger($"Farming {ScaleQuant} Enchanted Scales");
        while (!Bot.ShouldExit() && !Core.CheckInventory("Enchanted Scale", ScaleQuant))
        {
            Core.EnsureAccept(5294);
            Core.HuntMonster("dragontown", "Tempest Dracolich", "Dracolich Slain", 12, log: false);
            Core.EnsureComplete(5294);
        }
    }
}