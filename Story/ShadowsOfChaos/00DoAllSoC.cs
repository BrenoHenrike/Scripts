/*
name: Complete Shadows of Chaos Story
description: This will complete the Shadows of Chaos story arc.
tags: story, quest, shadows-of-chaos, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
using Skua.Core.Interfaces;

public class DoAllSoC
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoC CoreSoC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreSoC.CompleteCoreSoC();

        Core.SetOptions(false);
    }
}
