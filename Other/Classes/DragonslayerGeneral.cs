//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
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
        if (Core.CheckInventory("Dragonslayer General"))
            return;

        Farm.Gold(30000);
        EnchantedScaleandClaw(75, 100);
        Core.BuyItem("dragontown", 1286, 35996);

        Bot.Wait.ForPickup("Dragonslayer General");
        Adv.GearStore();
        Adv.rankUpClass("Dragonslayer General");
        Adv.GearStore(true);
    }

    public void EnchantedScaleandClaw(int ScaleQuant, int CLawquant)
    {
        if (!Core.CheckInventory("dragonslayer"))
        {
            Core.BuyItem("lair", 38, "Dragonslayer");
            Adv.EnhanceItem("Dragonslayer", EnhancementType.Lucky);
            Adv.rankUpClass("Dragonslayer");
        }
        Adv.GearStore();
        if (Core.CheckInventory("dragonslayer"))
            Adv.rankUpClass("Dragonslayer");
        Adv.GearStore(true);
        Adv.BestGear(GearBoost.Dragonkin);
        Core.AddDrop("Enchanted Scale", "Dragon Claw");

        while (!Core.CheckInventory("Enchanted Scale", ScaleQuant))
        {
            Core.EnsureAccept(5294);
            Core.HuntMonster("dragontown", "Tempest Dracolich", "Dracolich Slain", 12);
            Core.EnsureComplete(5294);
            Bot.Wait.ForPickup("Enchanted Scale");
        }

        while (!Core.CheckInventory("Dragon Claw", CLawquant))
            Core.HuntMonster("dragontown", "Tempest Dracolich", "Dragon Claw", 100, isTemp: false);

    }
}