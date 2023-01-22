/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class HireNulgathLarvae
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        getthething();

        Core.SetOptions(false);
    }

    public void getthething()
    {
        if (Core.CheckInventory("Nulgath Larvae"))
            return;

        Nation.HireNulgathLarvae();
    }
}
