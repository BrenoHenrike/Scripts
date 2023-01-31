/*
name: (Beleen) Summer 2015 Adventure Map Story
description: This will finish the Beleen story.
tags: beleen, summer, 2015, adventure, map, farm, story, beleen, summer, 2015, adventure, map
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
using Skua.Core.Interfaces;

public class Beleen
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSummer Summer = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Summer.Beleen();

        Core.SetOptions(false);
    }
}
