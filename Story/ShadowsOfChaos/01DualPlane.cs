/*
name: Dual Plane
description: This will finish the Dual Plane quest.
tags: story, quest, shadow-chaos, dual-plane
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs

using Skua.Core.Interfaces;

public class DualPlane
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoC CoreSoC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreSoC.DualPlane();

        Core.SetOptions(false);
    }
}
