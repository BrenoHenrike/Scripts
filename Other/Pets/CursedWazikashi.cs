/*
name: Cursed Wazikashi Pet
description: gets the pet
tags: cursed wazikashi, pet
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class CursedWazikashi
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CursedWakizashiPet();

        Core.SetOptions(false);
    }

    public void CursedWakizashiPet()
    {
        if (Core.CheckInventory("Cursed Wakizashi Pet", toInv: false))
            return;

        Core.AddDrop(79316, 79317, 79318, 79319, 79320);

        // Get item to  start quest [required]
        if (!Core.CheckInventory(79316))
            Core.GetMapItem(12046, 1, "museum");

        // Crescent's Confession[DoomKitten]
        Adv.GearStore();
        Core.KillDoomKitten("Crescent's Confession");
        Adv.GearStore(true);

        //Steel Amulet
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("moonyardb", "r28", "Left", "*", "Steel Amulet");

        // Master Pockey Ball
        if (!Core.CheckInventory(79320))
            Core.GetMapItem(12047, 1, "superslayin");

        // Broken Bamboo Chunk
        Core.HuntMonster("shogunwar", "Bamboo Treeant", "Broken Bamboo Chunk", isTemp: false);

        // Combin
        Core.BuyItem("yokairiver", 2326, "Cursed Wakizashi Pet", shopItemID: 12048);

    }

}
