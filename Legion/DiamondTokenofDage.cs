/*
name: Diamond Token of Dage
description: This script will farm Diamond Tokens of Dage.
tags: diamond token, dage, diamond, token, lf3, legion fealty 3, lr, legion loyalty rewarded, shadowblast arena
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DiamondTokenofDage
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new();
    public CoreAdvanced Adv = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Legion.DiamondTokenofDage();

        Core.SetOptions(false);
    }
}
