/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class EvolvedBloodOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        GetEvolvedBloodOrb();
        Core.SetOptions(false);
    }

    public void GetEvolvedBloodOrb()
    {
        if (Core.CheckInventory("Evolved Blood Orb"))
            return;
        Nation.ApprovalAndFavor(200, 0);
        Nation.FarmUni13(3);
        Nation.FarmVoucher(false);
        Nation.FarmTotemofNulgath(10);
        Nation.FarmDiamondofNulgath(30);
        Core.BuyItem("archportal", 1211, "Evolved Blood Orb");
        Core.Logger($"Done, you have Blood ball");
    }

}
