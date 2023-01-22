/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class CompleteTheWorld
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAstravia Astravia => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Astravia.TheWorld();

        Core.SetOptions(false);
    }
}
