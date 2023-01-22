/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DarkWarLegionGold
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        DoDarkWarLegionGold();

        Core.SetOptions(false);
    }

    public void DoDarkWarLegionGold()
    {
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.gold);
        Bot.Drops.Start();

        Farm.DarkWarLegion();
    }
}
