/*
name: Approval And Favor
description: This script will farm Nulgath's Approval and Archfiend's Favor.
tags: nulgath, approval, favor, archfiend, farm, mats
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class ApprovalAndFavor
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.ApprovalAndFavor();

        Core.SetOptions(false);
    }
}
