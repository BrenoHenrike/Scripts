/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class Leveling
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoLeveling();

        Core.SetOptions(false);
    }

    public void DoLeveling()
    {
        Adv.BestGear(GearBoost.exp);
        //Farm.UseBoost(ChangeToBoostID, Skua.Core.Models.Items.BoostType.Experience, true);
        Farm.Experience();
    }
}
