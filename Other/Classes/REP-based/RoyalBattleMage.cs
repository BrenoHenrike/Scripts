/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class RoyalBattleMage
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRBM();

        Core.SetOptions(false);
    }

    public void GetRBM(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Royal BattleMage"))
            return;

        Adv.BuyItem("castle", 702, "Royal BattleMage");

        if (rankUpClass)
            Adv.rankUpClass("Royal BattleMage");
    }
}
