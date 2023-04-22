/*
name: Legion Fealty 3
description: Does the Legion Fealty 3 quest for Legion Revenant untill you have 10 Exalted Crowns
tags: legion, revenant, fealty, three, 3, LR, exalted, crown
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

public class LegionFealty3
{
    private CoreBots Core => CoreBots.Instance;
    private CoreLR LR = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        LR.ExaltedCrown();

        Core.SetOptions(false);
    }
}
