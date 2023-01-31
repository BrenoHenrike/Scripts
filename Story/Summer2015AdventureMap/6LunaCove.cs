/*
name: (Summer) Luna Cove
description: This will finish the Luna Cove story.
tags: luna, cove, summer, 2015, adventure, map, farm, story, luna, cove, summer, 2015, adventure, map
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
using Skua.Core.Interfaces;

public class LunaCove
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSummer Summer = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Summer.LunaCove();

        Core.SetOptions(false);
    }
}
