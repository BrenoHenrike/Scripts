/*
name: Legion Combat Trophy
description: This script will farm Legion Combat Trophies in /dagepvp.
tags: legion, combat, trophy, dage, pvp, dage pvp, dagepvp
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
    public CoreLegion Legion = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.AddRange(new[] { "Legion Trophy", "Technique Observed", "Sword Scroll Fragment" });
        DoLegionCombatTrophy();

        Core.SetOptions(false);
    }

    public void DoLegionCombatTrophy()
    {
        Bot.Options.LagKiller = false;
        Core.BankACMisc();
        // Core.DL_Enable();
        //Adv.BestGear(RacialGearBoost.Undead);
        //order of quants: Trophy - Technique - Scroll
        Legion.DagePvP(4000, 50, 1000);

    }
}
