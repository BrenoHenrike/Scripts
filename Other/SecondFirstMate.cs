/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class SecondFirstMate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FirstMate();

        Core.SetOptions(false);
    }

    public string[] Loot =
    {
        "First Mate",
        "First Mate’s Hair",
        "First Mate’s Beard",
        "First Mate’s Hat + Beard",
        "First Mate’s Hat + Patch",
        "First Mate’s Locks",
        "First Mate’s Locks + Patch",
        "First Mate’s Morph Locks",
        "Escaping Elder Beast Portal",
        "First Mate’s Sword",
        "First Mate’s Gun",
        "First Mate’s Octopus"
    };

    public void FirstMate()
    {
        if (Core.CheckInventory(Loot, toInv: false))
            return;

        Core.AddDrop(Loot);
        Core.RegisterQuests(7714);
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.CheckInventory(Loot, toInv: false))
            Core.HuntMonster("pirates", "Undead Pirate", "Undead Pirates Cleared", 35);
        Core.ToBank(Loot);
    }
}
