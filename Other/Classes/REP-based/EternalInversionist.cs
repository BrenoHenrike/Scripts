//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs
using RBot;
using RBot.Items;

public class EternalInversionist
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public FourthDimensionalPyramid FDP = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetEI();

        Core.SetOptions(false);
    }

    public void GetEI(bool rankUpClass = true)
    {
        InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == "Eternal Inversionist".ToLower() && i.Category == ItemCategory.Class) ?? new();

        if (Core.CheckInventory("Eternal Inversionist") && (rankUpClass ? itemInv.Quantity == 302500 : true))
            return;

        FDP.FourthDimensionalPyramidSaga();
        Farm.EternalREP();

        Core.BuyItem("fourdpyramid", 1275, "Eternal Inversionist", shopItemID: 21138);
        Bot.Wait.ForPickup("Eternal Inversionist");

        if (rankUpClass)
        {
            Adv.GearStore();
            Core.Equip("Eternal Inversionist");
            Adv.EnhanceItem("Eternal Inversionist", EnhancementType.Lucky);
            Adv.rankUpClass("Eternal Inversionist");
            Adv.GearStore(true);
        }
    }
}
