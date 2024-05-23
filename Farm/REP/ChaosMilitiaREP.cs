/*
name: Chaos Militia REP
description: This script will farm Chaos Militia reputation to rank 10.
tags: chaos, chaosmilitia, militia, rep, rank, farm, reputation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
public class ChaosMilitiaREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        //Adv.BestGear(GenericGearBoost.dmgAll);
        //Adv.BestGear(GenericGearBoost.rep);
        Farm.ChaosMilitiaREP();

        Core.SetOptions(false);
    }
}
