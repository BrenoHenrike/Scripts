/*
name: Haku War
description: This script will complete "Haku War" storyline.
tags: story, quest, saga, dragons, dragon, haku, war, hakuwar, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;

public class HakuWar
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDOY DOY = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DOY.HakuWar();
        Core.SetOptions(false);
    }
}
