/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class FiendEmblem
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FiendEmblemFarm();

        Core.SetOptions(false);
    }

    public void FiendEmblemFarm()
    {
        if (Core.CheckInventory("Fiend Emblem", 300))
        {
            Core.Logger("You already have 300 (Max Quantity) Fiend Emblem");
            return;
        }

        Core.AddDrop("Fiend Emblem");

        while (!Bot.ShouldExit && !Core.CheckInventory("Fiend Emblem", 300))
        {
            Core.EnsureAccept(7890);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("originul", "r5", "Right", "*", "Essence of The Citadel", 30);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("originul", "Fiend Champion", "Champion's Essence");

            Core.EnsureComplete(7890);
            Bot.Wait.ForPickup("Fiend Emblem");
        }
    }
}
