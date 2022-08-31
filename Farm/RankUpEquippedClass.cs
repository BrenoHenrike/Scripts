//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class RankUpEquippedClass
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        //Farm.UseBoost(ChangeToBoostID, Skua.Core.Models.Items.BoostType.CP, true);
        Adv.rankUpClass(Bot.Player.CurrentClass.Name);

        Core.SetOptions(false);
    }
}