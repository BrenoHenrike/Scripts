/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CoralBeachMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("coralbeach"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("coralbeach", 79, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Moon Rock Fragments":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("lunacove", "r2", "Right", "*", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30267", "Chillin Cap", "Mode: [select] only\nShould the bot buy \"Chillin Cap\" ?", false),
        new Option<bool>("30268", "Salty Cap", "Mode: [select] only\nShould the bot buy \"Salty Cap\" ?", false),
        new Option<bool>("30269", "Burnin' Cap", "Mode: [select] only\nShould the bot buy \"Burnin' Cap\" ?", false),
        new Option<bool>("30270", "Burnin' Cap and Glasses", "Mode: [select] only\nShould the bot buy \"Burnin' Cap and Glasses\" ?", false),
        new Option<bool>("30272", "Air Tank", "Mode: [select] only\nShould the bot buy \"Air Tank\" ?", false),
        new Option<bool>("30273", "Diving Gear", "Mode: [select] only\nShould the bot buy \"Diving Gear\" ?", false),
        new Option<bool>("30274", "Snorkel Mask", "Mode: [select] only\nShould the bot buy \"Snorkel Mask\" ?", false),
        new Option<bool>("30276", "Shady Sunhat and Glasses", "Mode: [select] only\nShould the bot buy \"Shady Sunhat and Glasses\" ?", false),
        new Option<bool>("30277", "Floppy Beach Hat", "Mode: [select] only\nShould the bot buy \"Floppy Beach Hat\" ?", false),
        new Option<bool>("30278", "Sassy Sunhat", "Mode: [select] only\nShould the bot buy \"Sassy Sunhat\" ?", false),
        new Option<bool>("30279", "Sunny", "Mode: [select] only\nShould the bot buy \"Sunny\" ?", false),
        new Option<bool>("30280", "Lazy Summertime", "Mode: [select] only\nShould the bot buy \"Lazy Summertime\" ?", false),
        new Option<bool>("30283", "Snazzy Umbrella Lance", "Mode: [select] only\nShould the bot buy \"Snazzy Umbrella Lance\" ?", false),
        new Option<bool>("30284", "Orange Water Balloon", "Mode: [select] only\nShould the bot buy \"Orange Water Balloon\" ?", false),
        new Option<bool>("30285", "Green Water Balloon", "Mode: [select] only\nShould the bot buy \"Green Water Balloon\" ?", false),
        new Option<bool>("30286", "Pink Water Balloon", "Mode: [select] only\nShould the bot buy \"Pink Water Balloon\" ?", false),
        new Option<bool>("30281", "Slammin' Swimwear", "Mode: [select] only\nShould the bot buy \"Slammin' Swimwear\" ?", false),
        new Option<bool>("30287", "Snorkel Mask Ponytail", "Mode: [select] only\nShould the bot buy \"Snorkel Mask Ponytail\" ?", false),
        new Option<bool>("30282", "Beach Umbrella Lance", "Mode: [select] only\nShould the bot buy \"Beach Umbrella Lance\" ?", false),
    };
}
