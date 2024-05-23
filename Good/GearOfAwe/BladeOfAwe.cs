/*
name: Blade of Awe
description: This bot will farm the Blade of Awe reputation up to rank 6 and get the Blade of Awe itself
tags: blade, awe, enhancements, boost, experience, reputation, class, points, gold, cp
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class BladeOfAwe
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBoA();

        Core.SetOptions(false);
    }

    public void GetBoA()
    {
        Farm.BladeofAweREP(6, true);
    }
}
