/*
name: ChronoForge Story
description: This script will complete the storyline in /chronogem
tags: chronogem, chrono forge, gem, new year, seasonal, story, forgemaster
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/NewYear/CoreNewYear.cs
using Skua.Core.Interfaces;

public class ChronoForge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreNewYear NY = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        NY.ChronoForge();

        Core.SetOptions(false);
    }
}
