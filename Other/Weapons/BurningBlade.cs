/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class BurningBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBurningBlade();

        Core.SetOptions(false);
    }

    public void GetBurningBlade()
    {
        if (Core.CheckInventory(31058))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("lostruinswar", "r7", "Left", "Diabolical Warlord", "Burning Blade", isTemp: false, publicRoom: true);
        Bot.Wait.ForPickup("Burning Blade");
    }
}
