/*
name: Shadow Off
description: This will finish the Shadow Off quest.
tags: story, quest, shadow-chaos, shadow-off
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs

using Skua.Core.Interfaces;

public class ShadowOff
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoC CoreSoC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreSoC.ShadowOff();

        Core.SetOptions(false);
    }
}
