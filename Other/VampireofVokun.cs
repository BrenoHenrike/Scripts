/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class VampireofVokun
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArmor();

        Core.SetOptions(false);
    }

    public void GetArmor()
    {
        if (Core.CheckInventory("Vampire of Vokun"))
            return;

        Core.AddDrop("Vampire of Vokun");
        Core.EnsureAccept(3761);
        if (!Core.CheckInventory("Burn It Down"))
        {
            if (!Daily.CheckDaily(187, items: "Burn It Down"))
            {
                Core.Logger("Daily unavailable.");
                return;
            }
            Core.EnsureAccept(187);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("portalundead", "Enter", "Spawn", "*", "Fire Gem");
            Core.EnsureComplete(187);
            Bot.Wait.ForPickup("Burn It Down");
        }
        Core.KillEscherion("Escherion's Robe");
        Core.EnsureComplete(3761);
        Bot.Wait.ForPickup("Vampire of Vokun");

        if (!Core.CheckInventory("Vampire of Vokun"))
            Core.Logger("Vampire of Vokun didn't drop, try again tomorrow.");
    }
}
