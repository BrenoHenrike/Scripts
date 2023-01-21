/*
path: Seasonal/HarvestDay/02TurdrakenStory[Member].cs
fileName: 02TurdrakenStory[Member].cs
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs

using Skua.Core.Interfaces;

public class TurdrakenStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreHarvestDay HarvestDay = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HarvestDay.Turdraken();

        Core.SetOptions(false);
    }

}
