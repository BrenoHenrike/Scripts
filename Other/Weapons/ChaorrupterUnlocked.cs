/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other/MergeShops/ChaosWarMerge.cs
using Skua.Core.Interfaces;

public class ChaorrupterUnlocked
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    //public ChaosWarMerge CWM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetYourSword();

        Core.SetOptions(false);
    }

    public void GetYourSword()
    {
        if (Core.CheckInventory("Chaorrupter Unlocked"))
            return;

        Core.AddDrop("Chaos Eye", "Chaos Tentacle");

        // CWM.BuyAllMerge("Chaorrupter Unlocked", mergeOptionsEnum.select); //Other method until it's fixed
        Core.EquipClass(ClassType.Farm);
        if (Core.IsMember)
        {
            Core.Logger("Farming Chaorrupter Unlocked (Legend)");
            Core.KillMonster("chaoswar", "r2", "Spawn", "*", "Chaos Eye", 100, isTemp: false, log: false);
            Core.KillMonster("chaoswar", "r2", "Spawn", "*", "Chaos Tentacle", 100, isTemp: false, log: false);
            Core.BuyItem("chaoswar", 642, 17873, 1, 10987);
        }
        else
        {
            Core.Logger("Farming Chaorrupter Unlocked (Free Player)");
            Core.KillMonster("chaoswar", "r2", "Spawn", "*", "Chaos Eye", 250, isTemp: false, log: false);
            Core.KillMonster("chaoswar", "r2", "Spawn", "*", "Chaos Tentacle", 250, isTemp: false, log: false);
            Core.BuyItem("chaoswar", 642, 17932, 1, 10986);
        }
        Adv.EnhanceItem("Chaorrupter Unlocked", EnhancementType.Lucky);
    }
}
