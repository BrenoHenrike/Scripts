/*
name: Murder Moon Merge
description: This will farm the cybercrystals for merge items.
tags: farm, merge, seasonal, shop, dark, may-the-4th
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
using Skua.Core.Interfaces;

public class MurderMoonMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public MurderMoon Moon = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CyberCrystal();

        Core.SetOptions(false);
    }

    public void CyberCrystal(int quant = 300)
    {
        if (!Core.isSeasonalMapActive("murdermoon"))
            return;
        if (Core.CheckInventory("Cyber Crystal", quant))
            return;

        Moon.MurderMoonStory();

        int currentQuant = Bot.Inventory.GetQuantity("Cyber Crystal");
        Core.Logger($"Farming Cyber Crystals ({currentQuant}/{quant})");
        Core.EquipClass(ClassType.Farm);

        Core.AddDrop("Cyber Crystal");
        Core.RegisterQuests(8065);
        while (!Bot.ShouldExit && !Core.CheckInventory("Cyber Crystal", quant))
            Core.KillMonster("murdermoon", "r2", "Left", "*");
        Bot.Wait.ForPickup("Cyber Crystal");
        Core.CancelRegisteredQuests();
    }
}
