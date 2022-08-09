//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using RBot;
using RBot.Items;

public class EternalInversionist
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreToD TOD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetEI();

        Core.SetOptions(false);
    }

    public void GetEI(bool rankUpClass = true)
    {
        if (Core.CheckInventory(35602))
            return;

        TOD.FourthDimensionalPyramid();
        Farm.EternalREP();
        Core.BuyItem("fourdpyramid", 1275, "Eternal Inversionist", shopItemID: 21138);
        if (rankUpClass)
        {
            Adv.GearStore();
            Core.Equip("Eternal Inversionist");
            Adv.rankUpClass("Eternal Inversionist");
            Adv.GearStore(true);
        }

    }
}
