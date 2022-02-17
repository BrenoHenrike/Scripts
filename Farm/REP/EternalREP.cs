//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/06bSekt(FourthDimensionalPyramid).cs
using RBot;
public class EternalREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public FourthDimensionalPyramid FDP = new FourthDimensionalPyramid();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FDP.FourthDimensionalPyramidSaga();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.EternalREP();

        Core.SetOptions(false);
    }
}