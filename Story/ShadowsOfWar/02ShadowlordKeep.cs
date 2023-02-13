/*
name: Shadowlord Keep
description: This is the second part of the Shadow War story arc. It will take you to the Shadowlord Keep area and start the quest.
tags: story, quest, shadow-war, shadowlord-keep
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class ShadowlordKeep
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoW SoW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoW.ShadowlordKeep();

        Core.SetOptions(false);
    }
}
