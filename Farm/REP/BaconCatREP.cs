//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/03FlyingBaconCatFortress.cs
using RBot;
public class BaconCatREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public FlyingBaconCatFortress BCF = new FlyingBaconCatFortress();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BCF.FlyingBaconCatFortressSaga();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.BaconCatREP();

        Core.SetOptions(false);
    }
}