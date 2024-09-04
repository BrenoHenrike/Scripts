/*
name: Yokai Treasure
description: This script will complete "Yokai Treasure" storyline.
tags: story, quest, saga, dragons, dragon, yokai, treasure, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;

public class YokaiTreasure
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDOY DOY = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DOY.YokaiTreasure();
        Core.SetOptions(false);
    }
}
