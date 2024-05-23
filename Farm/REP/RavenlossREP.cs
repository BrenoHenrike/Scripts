/*
name: Ravenloss REP
description: This script will farm Ravenloss reputation to rank 10.
tags: raven loss, ravenloss, raven, loss, rep, rank, reputation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/RavenlossSaga.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
public class RavenlossREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public RavenlossSaga RLS = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        //Adv.BestGear(GenericGearBoost.dmgAll);
        //Adv.BestGear(GenericGearBoost.rep);
        RLS.DoAll();
        Farm.RavenlossREP();

        Core.SetOptions(false);
    }
}
