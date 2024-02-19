/*
name: FiendSeal
description: Farms fiend seal at shadowblast arena, must have nation recruits seal your fate unlocked
tags: farm, nation, fiend, seal, shadowblast, arena
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class BloodGem
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.FarmFiendSeal();

        Core.SetOptions(false);
    }
}
