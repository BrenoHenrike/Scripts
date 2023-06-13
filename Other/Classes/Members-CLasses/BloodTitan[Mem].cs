/*
name: BloodTitan[Mem]
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class BloodTitan
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Getclass();

        Core.SetOptions(false);
    }

    public void Getclass(bool rankUpClass = true)
    {
        if (Core.CheckInventory(16641, toInv: false))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Blood Titan Token", "Blood Titan's Tribute");
        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Titan Token", 200))
        {
            // Blood Titan Defender 2908
            Core.EnsureAccept(2908);
            Core.KillMonster("bloodtitan", "Enter", "Spawn", "Blood Titan", "Blood Titan's Phial", isTemp: false);
            Core.KillMonster("titandrakath", "Enter", "Spawn", "Titan Drakath", "Titanic Drakath's Blood", isTemp: false);
            Core.KillMonster("Desoloth", "Enter", "Spawn", "Desoloth", "Desoloth's Blood", isTemp: false);
            Core.KillMonster("ultracarnax", "Frame9", "Right", "Ultra Carnax", "Ultra Carnax's Blood", isTemp: false);
            Core.EnsureComplete(2908);

            // Blood Titan's Challenge 9253
            Core.EnsureAccept(9253);
            Core.KillMonster("bloodtitan", "Ultra", "Left", "Ultra Blood Titan", "Ultra Blood Titan Defeated", isTemp: false);
            Core.EnsureComplete(9253);
        }

        Core.BuyItem("classhalla", 617, 16641, shopItemID: 1772);

        if (rankUpClass)
            Adv.RankUpClass("Blood Titan");
    }
}
