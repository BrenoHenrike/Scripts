/*
name: Ebil Corp HQ Story
description: This will finish the Ebil Corp HQ storyline.
tags: ebil-corp-hq-story, seasonal, harvest-day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;

public class EbilCorpHQStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreHarvestDay HarvestDay = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HarvestDay.EbilCorpHQ();

        Core.SetOptions(false);
    }
}
