//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class DragonslayerGeneral
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailys Dailys = new CoreDailys();

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
        Core.BuyItem("dragontown", 1286, itemID: 35996, shopItemID: 4644);

        Bot.Wait.ForPickup("Dragonslayer General");
        Adv.GearStore();
        Adv.rankUpClass("Dragonslayer General");
        Adv.GearStore(true);
    }

    public void EnchantedScaleandClaw(int ScaleQuant, int CLawquant)
    {
        if (!Core.CheckInventory("Dragonslayer"))
        {
            Core.BuyItem("lair", 38, "Dragonslayer");
            Adv.GearStore();
            Adv.EnhanceItem("Dragonslayer", EnhancementType.Lucky);
            Adv.rankUpClass("Dragonslayer");
            Adv.GearStore(true);
        }
        Adv.GearStore();
        if (Core.CheckInventory("Dragonslayer"))
            Adv.rankUpClass("Dragonslayer");
        Adv.GearStore(true);
        Adv.BestGear(GearBoost.Dragonkin);
        Core.AddDrop("Enchanted Scale", "Dragon Claw");

        Core.Logger($"Farming {ScaleQuant} Enchanted Scales");

        while (!Core.CheckInventory("Enchanted Scale", ScaleQuant))
        {
            Core.EnsureAccept(5294);
            Core.HuntMonster("dragontown", "Tempest Dracolich", "Dracolich Slain", 12, log: false);
            Core.EnsureComplete(5294);
            Bot.Wait.ForPickup("Enchanted Scale");
        }

        Core.Logger($"Farming {CLawquant} Dragon Claw");

        while (!Core.CheckInventory("Dragon Claw", CLawquant))
            Core.HuntMonster("dragontown", "Tempest Dracolich", "Dragon Claw", 100, isTemp: false, log: false);

    }
}