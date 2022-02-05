//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
public class NorthpointeREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.NorthpointeREP();

        Core.SetOptions(false);
    }
}
