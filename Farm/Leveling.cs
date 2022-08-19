//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class Leveling
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Adv.BestGear(GearBoost.exp);
        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Experience, true);
        Farm.Experience();

        Core.SetOptions(false);
    }
}