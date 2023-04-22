/*
name: Legion Fealty 2
description: Does the Legion Fealty 2 quest for Legion Revenant untill you have 6 Conquest Wraths
tags: legion, revenant, fealty, three, 2, LR, conquest, wreath
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
using Skua.Core.Interfaces;

public class LegionFealty2
{
    private CoreBots Core => CoreBots.Instance;
    private CoreLR LR = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        LR.ConquestWreath();

        Core.SetOptions(false);
    }
}
