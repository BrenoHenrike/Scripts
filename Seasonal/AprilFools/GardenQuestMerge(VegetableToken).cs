/*
name: Vegetable Token Farm
description: Farms the max vegetable token quantity.
tags: vegetable-token, seasonal, april-fools
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class GardenQuestMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Vegetable Token");

        Core.SetOptions();

        GetVegetableTokens();

        Core.SetOptions(false);
    }

    public void GetVegetableTokens()
    {
        if (!Core.isSeasonalMapActive("gardenquest"))
            return;
        Core.AddDrop("Vegetable Token");
        Core.Logger($"Hunting For: Vegetable Token, {Bot.Inventory.GetQuantity("Vegetable Token")}/300");
        while (!Bot.ShouldExit && !Core.CheckInventory("Vegetable Token", 300))
        {

            Core.EnsureAccept(8002);
            Core.HuntMonster("GardenQuest", "Angry Broccoli", "Roasted Broccoli", log: false);
            Core.EnsureComplete(8002);
            Core.Logger($"Vegetable Token: {Bot.Inventory.GetQuantity("Vegetable Token")}/300");
        }
    }
}
