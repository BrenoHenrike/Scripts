//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/06FourthDimensionalPyramid.cs
//cs_include Scripts/Story/LordofChaos/Core13LoC.cs
using RBot;
public class GetAllRanks
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public FourthDimensionalPyramid FDP = new FourthDimensionalPyramid();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FDP.FourthDimensionalPyramidSaga();
        LOC.Wolfwing();
        LOC.Kimberly();
        LOC.Lionfang();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, true);

        Farm.GetAllRanks();

        Core.SetOptions(false);
    }
}