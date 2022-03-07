//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/03aZiri(BaconCatFortress).cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using RBot;
public class GetAllRanks
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public FourthDimensionalPyramid FDP = new FourthDimensionalPyramid();
    public FlyingBaconCatFortress BCF = new FlyingBaconCatFortress();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FDP.FourthDimensionalPyramidSaga();
        BCF.FlyingBaconCatFortressSaga();
        LOC.Wolfwing();
        LOC.Kimberly();
        LOC.Lionfang();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, true);

        Farm.GetAllRanks();

        Core.SetOptions(false);
    }
}