/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class RandomWepofNul
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetWep();

        Core.SetOptions(false);
    }

    public void GetWep()
    {
        if (Core.CheckInventory("Random Weapon of Nulgath"))
            return;

        Nation.Supplies("Random Weapon of Nulgath");
    }
}
