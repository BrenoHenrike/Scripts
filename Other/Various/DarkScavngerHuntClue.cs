/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class DarkScavengerHuntClue
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        NewSword();
        Core.SetOptions(false);
    }

    public void NewSword()
    {
        if (Core.CheckInventory("Shadow Warrior Sword"))
            return;

        Core.HuntMonster("battleontown", "frogzard", "Dark Scavenger Hunt Clue", isTemp: false, log: false);
        Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Shadow Warrior Sword", isTemp: false);
    }
}
