//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class GoldFarm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        //Core.ActionDelay = 1000; //if script is having issues turning in or is slow; un // this line ^_^

        // Farm.UseBoost(BoostIDs.Gold20, Skua.Core.Models.Items.BoostType.Gold);

        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.gold);
        Bot.Drops.Start();

        Farm.Gold();

        Core.SetOptions(false);
    }
}
