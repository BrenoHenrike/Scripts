//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class FireWarXP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        Leveling();

        Core.SetOptions(false);
    }

    public void Leveling(int level = 100)
    {
        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Experience, true);
        
        Core.AddDrop("");
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.exp);
        Farm.FireWarXP();
    }
}
