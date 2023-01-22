/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class KaijuWar
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        KaijuItems();

        Core.SetOptions(false);
    }

    public string[] AllLoot =
    {
        "Dark Corsair Eyepatch + Locks",
        "Dark Corsair Bandanna",
        "Dark Corsair Tricorn + Locks",
        "Dark Corsair Hair",
        "Crossed Silver Corsair Cutlasses",
        "Silver Corsair Cutlass",
        "Dark Corsair Hat and Locks",
        "Dark Corsair Eyepatch + Beard",
        "Dark Corsair Tricorn",
        "Dark Corsair Beard",
        "Crossed Dark Corsair Cutlasses",
        "Dark Corsair Cutlass",
        "Dark Corsair",
        "Crossed Golden Corsair Cutlasses",
        "Gold Corsair Cutlass",
        "Dual Golden Corsair Cutlasses",
        "Dual Silver Corsair Cutlasses",
        "Dual Dark Corsair Cutlasses",
        "Dark Corsair Silver Back Gear",
        "Dark Corsair Gold Back Gear",
        "Kaiju Cannoneer",
        "Cannoneer Bandana"
    };

    public string[] Booty =
    {
        "Dark Corsair Eyepatch + Locks",
        "Dark Corsair Bandanna",
        "Dark Corsair Tricorn + Locks",
        "Dark Corsair Hair",
        "Crossed Silver Corsair Cutlasses",
        "Silver Corsair Cutlass",
        "Dark Corsair Hat and Locks",
        "Dark Corsair Eyepatch + Beard",
        "Dark Corsair Tricorn",
        "Dark Corsair Beard",
        "Crossed Dark Corsair Cutlasses",
        "Dark Corsair Cutlass",
        "Dark Corsair",
        "Crossed Golden Corsair Cutlasses",
        "Gold Corsair Cutlass"
    };

    public string[] Mech = { "Dual Golden Corsair Cutlasses", "Dual Silver Corsair Cutlasses", "Dual Dark Corsair Cutlasses" };
    public string[] Capn = { "Dark Corsair Silver Back Gear", "Dark Corsair Gold Back Gear" };
    public string[] Kaiju = { "Kaiju Cannoneer", "Cannoneer Bandana" };
    public void KaijuItems()
    {
        if (!Core.isSeasonalMapActive("kaijuwar"))
            return;

        if (Core.CheckInventory(AllLoot, toInv: false))
            return;

        if (!Core.CheckInventory(Booty, toInv: false))
            FirstThree();

        if (!Core.CheckInventory(Mech, toInv: false))
            CallMech();

        if (!Core.CheckInventory(Capn, toInv: false))
            Captain();

        if (!Core.CheckInventory(Kaiju, toInv: false))
            Kraken();

        void FirstThree()
        {
            Core.AddDrop(Booty);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6006, 6007, 6008);
            while (!Bot.ShouldExit && (!Core.CheckInventory(Booty, toInv: false)))
                Core.KillMonster("kaijuwar", "r5", "Left", "*", log: false);
            Core.CancelRegisteredQuests();
            Core.ToBank(Booty);
        }

        void CallMech()
        {
            Core.AddDrop(Mech);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6009);
            while (!Bot.ShouldExit && (!Core.CheckInventory(Mech, toInv: false)))
                Core.HuntMonster("kaijuwar", "Ship's Mechanic", "Can o' Diesel", 5);
            Core.CancelRegisteredQuests();
            Core.ToBank(Mech);
        }

        void Captain()
        {
            Core.AddDrop(Capn);
            Core.EquipClass(ClassType.Solo);
            Core.RegisterQuests(6010);
            while (!Bot.ShouldExit && (!Core.CheckInventory(Capn, toInv: false)))
                Core.HuntMonster("kaijuwar", "Captain Kraylox", "Defeat Cap'n Kraylox");
            Core.CancelRegisteredQuests();
            Core.ToBank(Capn);
        }

        void Kraken()
        {
            Core.AddDrop(Kaiju);
            Core.EquipClass(ClassType.Solo);
            Core.RegisterQuests(6011);
            while (!Bot.ShouldExit && (!Core.CheckInventory(Kaiju, toInv: false)))
                Core.HuntMonster("kaijuwar", "Klashex", "Defeat Klashex");
            Core.CancelRegisteredQuests();
            Core.ToBank(Kaiju);
        }
    }
}
