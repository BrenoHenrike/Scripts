//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/06FourthDimensionalPyramid.cs
using RBot;

public class EternalInversionist
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public FourthDimensionalPyramid FDP = new FourthDimensionalPyramid();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetEI();

        Core.SetOptions(false);
    }

    public void GetEI(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Eternal Inversionist"))
            return;

        FDP.FourthDimensionalPyramidSaga();
        Farm.EternalREP();

        Core.BuyItem("fourdpyramid", 1275, "Eternal Inversionist", shopItemID: 21138);

        if (rankUpClass)
            Farm.rankUpClass("Eternal Inversionist");
    }
}