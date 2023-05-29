/*
name: Fell Beast Merge
description: This bot will farm the items belonging to the selected mode for the Fell Beast Merge [879] in /darkdungeon
tags: fell, beast, merge, darkdungeon, lumenomancer, righteous, deliverance, lumenomancers, gift, woe, undead, th, egg, pet, cockatrice, hatchling
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DarkDungeon.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FellBeastMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    private DarkDungeon Dark = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dungeon Token"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Dark.storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("darkdungeon", 879, findIngredients, buyOnlyThis, buyMode: buyMode);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.TempInv.GetQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Dungeon Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.KillMonster("darkdungeon", "r9", "Left", "Cockatrice", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("24111", "Lumenomancer", "Mode: [select] only\nShould the bot buy \"Lumenomancer\" ?", false),
        new Option<bool>("24117", "Righteous Deliverance", "Mode: [select] only\nShould the bot buy \"Righteous Deliverance\" ?", false),
        new Option<bool>("24115", "Lumenomancer's Gift", "Mode: [select] only\nShould the bot buy \"Lumenomancer's Gift\" ?", false),
        new Option<bool>("24118", "Lumenomancer's Woe", "Mode: [select] only\nShould the bot buy \"Lumenomancer's Woe\" ?", false),
        new Option<bool>("24113", "Lumenomancer's Helm", "Mode: [select] only\nShould the bot buy \"Lumenomancer's Helm\" ?", false),
        new Option<bool>("24114", "Lumenomancer's Undead Visage", "Mode: [select] only\nShould the bot buy \"Lumenomancer's Undead Visage\" ?", false),
        new Option<bool>("24112", "Lumenomancer's Cape and Sword", "Mode: [select] only\nShould the bot buy \"Lumenomancer's Cape and Sword\" ?", false),
        new Option<bool>("24142", "13th Egg Pet", "Mode: [select] only\nShould the bot buy \"13th Egg Pet\" ?", false),
        new Option<bool>("24140", "Cockatrice Hatchling", "Mode: [select] only\nShould the bot buy \"Cockatrice Hatchling\" ?", false),
    };
}
