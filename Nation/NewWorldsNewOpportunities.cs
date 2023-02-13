/*
name: New Worlds, New Opportunities
description: Farms the resources of nation so instantly. Note that you must have Bounty Hunter's Drone Pet.
tags: bounty-hunters-pet, pet, nation-farm, farm-resources
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class NewWorldsNewOpportunities
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        Nation.NewWorldsNewOpportunities();

        Core.SetOptions(false);
    }
}
