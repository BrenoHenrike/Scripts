/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class EvolvedShadowOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        GetEvolvedShadowOrb();
        Core.SetOptions(false);
    }

    public void GetEvolvedShadowOrb()
    {
        if (Core.CheckInventory("Evolved Shadow Orb") || !Core.IsMember)
            return;
        Nation.ApprovalAndFavor(200, 0);
        Nation.FarmUni13(3);
        Nation.FarmVoucher(false);
        Nation.FarmVoucher(true);
        Nation.FarmTotemofNulgath(10);
        Nation.FarmGemofNulgath(20);
        Core.BuyItem("archportal", 1211, "Evolved Shadow Orb");
        Core.Logger($"Done, you have Shadow ball");
    }

}
