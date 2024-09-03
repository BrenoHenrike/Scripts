/*
name: Haku Village
description: This script will complete "Haku Village" storyline.
tags: story, quest, saga, dragons, dragon, haku, village, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;

public class HakuVillage
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDOY DOY = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DOY.HakuVillage();
        Core.SetOptions(false);
    }
}
