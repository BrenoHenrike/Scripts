/*
name: Timekeep
description: This will finish the Timekeep quest.
tags: story, quest, shadow-war, timekeep
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class Timekeep
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoW SoW = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoW.Timekeep();

        Core.SetOptions(false);
    }
}
