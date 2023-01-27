/*
name: Brethwren REP Farm
description: This will farm Brethwren Faction to Rank 10.
tags: brethwren-rep, farm, seasonal, harvest-day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;
public class BrethwrenREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new();

    CoreHarvestDay HarvestDay = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoRep();

        Core.SetOptions(false);
    }

    public void DoRep()
    {
        HarvestDay.BirdsWithHarms();

        Farm.BrethwrenREP();

    }
}
