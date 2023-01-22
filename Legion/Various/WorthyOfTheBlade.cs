/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class WorthyBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        WorthyOfTheBlade();

        Core.SetOptions(false);
    }

    public void WorthyOfTheBlade()
    {
        if (Core.CheckInventory("SoulKeeper Blade"))
            return;

        Core.AddDrop("SoulKeeper Blade", "SoulKeeper On your Back", "SoulKeeper Sword Pet");
        Core.EnsureAccept(6738);
        Nation.EssenceofNulgath(100);
        Core.AddDrop("Undead Essence", "Undead Energy");
        Farm.BattleUnderB("Undead Essence", 500);
        Farm.BattleUnderB("Undead Energy", 500);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Essence of Blade Master")))
        {
            Core.EquipClass(ClassType.Farm);
            Core.AddDrop("Essence of Blade Master");
            Core.HuntMonster("underworld", "Blade Master", "Essence of Blade Master", 1, false);
        }
        while (!Bot.ShouldExit && (!Core.CheckInventory("Binky's Uni-horn")))
        {
            Core.EquipClass(ClassType.Solo);
            Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Binky's Uni-horn", isTemp: false, publicRoom: true);
        }
        Bot.Sleep(1500);
        Core.EnsureComplete(6738);
    }
}
