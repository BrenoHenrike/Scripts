/*
name: Advanced Weapon Kit
description: This script farms the max quantity of Advanced Weapon Kits.
tags: advanced, weapon, kit, BLOD, blinding, light, destiny
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;

public class AdvancedWeaponKit
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new();
    public CoreStory Story = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.AdvancedWK(1000);

        Core.SetOptions(false);
    }
}
