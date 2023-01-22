/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
using Skua.Core.Interfaces;

public class Greed
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Core7DD DD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DD.Greed();

        Core.SetOptions(false);
    }
}
