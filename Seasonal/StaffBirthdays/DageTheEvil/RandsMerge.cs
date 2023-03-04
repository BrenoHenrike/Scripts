/*
name: Rand's Merge
description: This bot will farm Rand's Approval in /seraph and buy items from the merge shop
tags: legion, merge, staff, birthday, rands, approval, dage, evil, seasonal, event, darkbirthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class RandsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Rand's Approval", "Electric Underworld Katana" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("darkbirthday"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("darkbirthday", 2243, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Rand's Approval":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9129);
                    Core.HuntMonster("seraph", "Seraphic Recruit", req.Name, quant, false, false);
                    Core.CancelRegisteredQuests();
                    break;

                case "Electric Underworld Katana":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("abysslair", "Abyssal Underbeast", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76594", "Ripper Blades of the Legion Cape", "Mode: [select] only\nShould the bot buy \"Ripper Blades of the Legion Cape\" ?", false),
        new Option<bool>("76604", "Ripper Blade of the Underworld", "Mode: [select] only\nShould the bot buy \"Ripper Blade of the Underworld\" ?", false),
        new Option<bool>("76605", "Ripper Blades of the Underworld", "Mode: [select] only\nShould the bot buy \"Ripper Blades of the Underworld\" ?", false),
        new Option<bool>("76608", "Sheathed Soul Devouring Katana", "Mode: [select] only\nShould the bot buy \"Sheathed Soul Devouring Katana\" ?", false),
        new Option<bool>("76682", "Electric Underworld Katanas", "Mode: [select] only\nShould the bot buy \"Electric Underworld Katanas\" ?", false),
        new Option<bool>("76684", "Urban Underworld Rages", "Mode: [select] only\nShould the bot buy \"Urban Underworld Rages\" ?", false),
        new Option<bool>("76670", "Urban Underworld Outfit", "Mode: [select] only\nShould the bot buy \"Urban Underworld Outfit\" ?", false),
        new Option<bool>("76677", "Urban Underworld Hair + Mask", "Mode: [select] only\nShould the bot buy \"Urban Underworld Hair + Mask\" ?", false),
        new Option<bool>("76671", "Urban Underworld Morph", "Mode: [select] only\nShould the bot buy \"Urban Underworld Morph\" ?", false),
        new Option<bool>("76673", "Urban Underworld Morph + Mask", "Mode: [select] only\nShould the bot buy \"Urban Underworld Morph + Mask\" ?", false),
        new Option<bool>("76678", "Urban Underworld Locks + Mask", "Mode: [select] only\nShould the bot buy \"Urban Underworld Locks + Mask\" ?", false),
        new Option<bool>("76672", "Urban Underworld Visage", "Mode: [select] only\nShould the bot buy \"Urban Underworld Visage\" ?", false),
        new Option<bool>("76674", "Urban Underworld Visage + Mask", "Mode: [select] only\nShould the bot buy \"Urban Underworld Visage + Mask\" ?", false),
    };
}
