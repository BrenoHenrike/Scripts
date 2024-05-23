/*
name: Emblem of Dage
description: This script will farm Emblems of Dage.
tags: emblem, dage, lf3, legion fealty 3, shadowblast arena, legion recruits, the dark path waits
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class EmblemofDage
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new();
    public CoreAdvanced Adv = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Legion.EmblemofDage();

        Core.SetOptions(false);
    }
}
