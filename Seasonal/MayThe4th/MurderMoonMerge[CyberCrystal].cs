/*
name: null
description: null
tags: null
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
        {
            Core.HuntMonster("murdermoon", "Tempest Soldier", "Tempest Soldier Badge", 5, log: false);
            Bot.Wait.ForPickup("Cyber Crystal");
        }
        Core.CancelRegisteredQuests();
    }
}
