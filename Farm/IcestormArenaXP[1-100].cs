/*
name: IceStorm Arena XP [1-100]
description: This script will farm XP in /icestormarena.
tags: experience, xp, level, leveling, farm, icestormarena, icestorm, arena
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class IcestormArenaXP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoIcestormArenaXP();

        Core.SetOptions(false);
    }

    public void DoIcestormArenaXP()
    {
        //Adv.BestGear(GenericGearBoost.exp);
        //Farm.UseBoost(ChangeToBoostID, Skua.Core.Models.Items.BoostType.Experience, true);
        Farm.IcestormArena();
    }
}
