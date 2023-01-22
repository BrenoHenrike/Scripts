/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class BladeOfAwe
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

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
