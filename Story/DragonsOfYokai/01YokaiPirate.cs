/*
name: Yokai Pirate
description: This script will complete "Yokai Pirate" storyline.
tags: story, quest, saga, dragons, dragon, yokai, pirate, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;

public class YokaiPirate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDOY DOY = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DOY.YokaiPirate();
        Core.SetOptions(false);
    }
}
