/*
name: Harvest Day All Stories
description: This will finish the entire Harvest Day storylines.
tags: harvest-day-stories, seasonal, harvest-day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs

using Skua.Core.Interfaces;

public class DoAllHarvestDay
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreHarvestDay HarvestDay = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HarvestDay.DoAll();

        Core.SetOptions(false);
    }

}
