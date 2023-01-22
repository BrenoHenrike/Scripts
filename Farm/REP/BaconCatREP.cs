/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;
public class BaconCatREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoRep();

        Core.SetOptions(false);
    }

    public void DoRep()
    {
        TOD.BaconCatFortress();
        TOD.LaserSharkInvasion();

        Farm.BaconCatREP();

    }
}
