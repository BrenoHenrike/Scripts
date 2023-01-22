/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;

public class LegionCombatTrophy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoLegionCombatTrophy();

        Core.SetOptions(false);
    }

    public void DoLegionCombatTrophy()
    {
        Bot.Options.LagKiller = false;
        Adv.BestGear(GearBoost.Undead);
        //order of quants: Trophy - Technique - Scroll
        Legion.DagePvP(400, 50, 1000);

    }
}
