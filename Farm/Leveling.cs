//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class Leveling
{
    public IScriptInterface Bot { get; set; }
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Adv.BestGear(GearBoost.exp);
        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Experience, true);
        Farm.Experience();

        Core.SetOptions(false);
    }
}