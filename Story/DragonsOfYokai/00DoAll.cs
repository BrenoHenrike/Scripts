/*
name: Dragons Of Yokai
description: This script will complete "Dragons Of Yokai" storyline.
tags: story, quest, saga, dragons, dragon, yokai, haku, village, pirate, treasure, war, portal, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;

public class DragonsOfYokai
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDOY DOY = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DOY.DoAll();
        Core.SetOptions(false);
    }
}
