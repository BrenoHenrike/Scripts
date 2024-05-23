/*
name: Hollowborn REP
description: This script will farm Hollowborn reputation to rank 10.
tags: hollow, born, hollowborn, hollow born, lae, rep, rank, reputation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
public class HollowbornREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        //Adv.BestGear(GenericGearBoost.dmgAll);

        //Rep boost type here crahes bestgear uncomment when fixed.
        // //Adv.BestGear(GenericGearBoost.rep);

        Farm.HollowbornREP();

        Core.SetOptions(false);
    }
}
