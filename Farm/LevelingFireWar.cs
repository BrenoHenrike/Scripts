//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class GoldFarm
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        //Core.ActionDelay = 1000; //if script is having issues turning in or is slow; un // this line ^_^

        //Farm.UseBoost(BoostIDs.Gold20, RBot.Items.BoostType.Gold);

        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.exp);
        bot.Drops.Start();

        Farm.FireWarXP();

        Core.SetOptions(false);
    }
}
