/*
name: HollowbornREP
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
public class HollowbornREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Adv.BestGear(GenericGearBoost.dmgAll);
        Adv.BestGear(GenericGearBoost.rep);
        Farm.HollowbornREP();

        Core.SetOptions(false);
    }
}
