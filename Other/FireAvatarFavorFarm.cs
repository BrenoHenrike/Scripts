/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs

using Skua.Core.Interfaces;

public class FireAvatarFavorFarm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FAFavor();

        Core.SetOptions(false);
    }

    public void FAFavor(int quant = 300)
    {
        if (Core.CheckInventory("Fire Avatar's Favor", quant))
            return;

        Core.AddDrop("Fire Avatar's Favor");
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(8244);
        while (!Bot.ShouldExit && !Core.CheckInventory("Fire Avatar's Favor", quant))
        {
            Core.KillMonster("fireavatar", "r4", "Right", "*", "Onslaught Defeated", 6);
            Core.KillMonster("fireavatar", "r6", "Left", "*", "Elemental Defeated", 6);

            Bot.Wait.ForPickup("Fire Avatar's Favor");
        }
        Core.CancelRegisteredQuests();
    }
}
