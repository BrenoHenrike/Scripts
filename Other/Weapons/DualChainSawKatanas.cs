/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class DualChainSawKatanas
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetWep();

        Core.SetOptions(false);
    }

    public void GetWep()
    {
        if (Core.CheckInventory("Dual Chainsaw Katanas", toInv: false))
            return;

        Core.EnsureAccept(8670);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("darkoviahorde", "r8", "Right", "Zombie", "Zombie Defeated", 100);
        Core.EnsureComplete(8670);
        Core.JumpWait();
        Bot.Sleep(Core.ActionDelay);
        Core.SetAchievement(10);
        Bot.Sleep(Core.ActionDelay);
        Core.BuyItem("Darkoviahorde", 1171, "Dual Chainsaw Katanas");
    }
}

