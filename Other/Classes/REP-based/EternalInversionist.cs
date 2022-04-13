//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs
using RBot;

public class EternalInversionist
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public FourthDimensionalPyramid FDP = new FourthDimensionalPyramid();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetEI(true);

        Core.SetOptions(false);
    }

    public void GetEI(bool rankUpClass = true, string ClassName = "Eternal Inversionist")
    {
        InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == ClassName.ToLower() && i.Category == ItemCategory.Class);

        if (Core.CheckInventory("Eternal Inversionist") & (itemInv.Quantity == 302500))
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
