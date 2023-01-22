/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class EvolvedHexOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        GetEvolvedHexOrb();
        Core.SetOptions(false);
    }

    public void GetEvolvedHexOrb()
    {
        if (Core.CheckInventory("Evolved Hex Orb"))
            return;
        Nation.ApprovalAndFavor(200, 0);
        Nation.FarmUni13(3);
        Nation.FarmVoucher(false);
        Nation.FarmTotemofNulgath(10);
        Nation.FarmDarkCrystalShard(30);
        Nation.Supplies("Tainted Gem", 30);
        Core.BuyItem("archportal", 1211, "Evolved Hex Orb");
        Core.Logger($"Done, you have Hex ball");
    }

}
