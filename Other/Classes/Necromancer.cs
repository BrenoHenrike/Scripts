/*
name: Necromancer
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class Necromancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetNecromancer();

        Core.SetOptions(false);
    }

    public void GetNecromancer(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Necromancer"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Necromancer");

            return;
        }

        Farm.DoomWoodREP();
        Core.BuyItem("lightguard", 277, "NUE Necronomicon");
        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("maul", "r3", "Down", "Creature Creation", "Creature Shard", isTemp: false, publicRoom: true);
        Core.BuyItem("necrotower", 285, "Necromancer");

        if (rankUpClass)
            Adv.RankUpClass("Necromancer");
    }
}
