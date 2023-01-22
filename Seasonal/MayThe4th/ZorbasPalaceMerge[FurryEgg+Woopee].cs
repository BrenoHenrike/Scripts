/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class ZorbasPalaceMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Woopee();

        Core.SetOptions(false);
    }

    public void Woopee(int quant = 300)
    {
        if (!Core.isSeasonalMapActive("zorbaspalace"))
            return;
        if (Core.CheckInventory("Woopee", quant))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("zorbaspalace", "r6", "Bottom", "Lem-or", "Furry Egg", 12, false);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("zorbaspalace", "r4", "Bottom", "*", "Woopee", quant, false);
    }
}
