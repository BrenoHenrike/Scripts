/*
name: (Dread Space) Summer 2015 Adventure Map Story
description: This will finish the Dread Space story.
tags: dread, space, summer, 2015, adventure, map, farm, story, dread, space
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
using Skua.Core.Interfaces;

public class DreadSpace
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSummer Summer = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Summer.DreadSpace();

        Core.SetOptions(false);
    }
}
