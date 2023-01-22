/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EridaniMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDarkon Darkon = new();

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
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("eridani", 1912, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Teeth":
                    Darkon.Teeth(quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57268", "Re's Attire", "Mode: [select] only\nShould the bot buy \"Re's Attire\" ?", false),
        new Option<bool>("57277", "Re's Morph", "Mode: [select] only\nShould the bot buy \"Re's Morph\" ?", false),
        new Option<bool>("57278", "Re's Shades", "Mode: [select] only\nShould the bot buy \"Re's Shades\" ?", false),
        new Option<bool>("57292", "Re's Sharp Edges Arsenal", "Mode: [select] only\nShould the bot buy \"Re's Sharp Edges Arsenal\" ?", false),
        new Option<bool>("57293", "Re's Short Stabbers", "Mode: [select] only\nShould the bot buy \"Re's Short Stabbers\" ?", false),
        new Option<bool>("57294", "Re's Giant Slicers", "Mode: [select] only\nShould the bot buy \"Re's Giant Slicers\" ?", false),
        new Option<bool>("57295", "Re's Face Masher", "Mode: [select] only\nShould the bot buy \"Re's Face Masher\" ?", false),
        new Option<bool>("57297", "Re's Neck Snatcher", "Mode: [select] only\nShould the bot buy \"Re's Neck Snatcher\" ?", false),
        new Option<bool>("57298", "Re's Long Stabber", "Mode: [select] only\nShould the bot buy \"Re's Long Stabber\" ?", false),
        new Option<bool>("57300", "Re's Sharp Cutter", "Mode: [select] only\nShould the bot buy \"Re's Sharp Cutter\" ?", false),
        new Option<bool>("57302", "Re's Absolute Annihilator", "Mode: [select] only\nShould the bot buy \"Re's Absolute Annihilator\" ?", false),
    };
}
