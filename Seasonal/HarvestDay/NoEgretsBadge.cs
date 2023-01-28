/*
name: No Egrets Badge
description: This will get the No Egrets Badge.
tags: no-egrets-badge, seasonal, harvest-day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;

public class NoEgrets
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreHarvestDay HarvestDay = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        if (!Core.isSeasonalMapActive("birdswithharms"))
        {
            Core.Logger($"Can't get {badge} badge at this moment, because this is a seasonal badge (Harvest Day Event)");
            return;
        }

        Core.Logger($"Doing Birds With Harm story for {badge} badge");
        HarvestDay.BirdsWithHarms();
    }

    private string badge = "No Egrets";
}
