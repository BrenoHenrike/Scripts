/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
using Skua.Core.Interfaces;

public class ArchfiendDragonEgg
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreHollowborn HB = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAFDE();

        Core.SetOptions(false);
    }

    public void GetAFDE()
    {
        if (!Core.IsMember)
            return;

        if (Core.CheckInventory("ArchFiend Baby Dragon Pet"))
            return;

        Core.AddDrop("ArchFiend Baby Dragon Pet");

        Core.EnsureAccept(7296);
        Core.BuyItem("Airstorm", 357, "Breath of Life");
        Core.HuntMonster("queenspire", "Fire Guardian Dragon", "Fire Guardian Dragon Soul", isTemp: false);
        HB.FreshSouls(1, 10);
        Core.HuntMonster("Underlair", "ArchFiend DragonLord", "Fiendish Brimstone", isTemp: false);
        Core.BuyItem("Ariapet", 12, "ArchFiend Dragon Egg");
        Core.EnsureComplete(7296);
        Bot.Wait.ForPickup("ArchFiend Baby Dragon Pet");
    }
}
