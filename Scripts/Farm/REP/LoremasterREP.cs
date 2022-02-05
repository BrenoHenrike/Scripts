//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
public class LoremasterREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.LoremasterREP();

        Core.SetOptions(false);
    }
}