/*
name: Tyndarius
description: This will finish the Tyndarius quest.
tags: story, quest, shadow-war, tyndarius
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class Tyndarius
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoW SoW = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoW.Tyndarius();

        Core.SetOptions(false);
    }
}
