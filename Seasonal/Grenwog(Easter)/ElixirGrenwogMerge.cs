/*
name: Elixir Grenwog Merge
description: This script will farm all the materials needed for Elixir Grenwog Merge in elixirgrenwog.
tags: elixir, seasonal, easter, grenwog, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ElixirGrenwogMerge
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
        Core.BankingBlackList.AddRange(new[] { "Longevity Egg" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("elixirgrenwog", 2252, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Longevity Egg":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("elixirgrenwog", "Elixir Grenwog", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76026", "Pink Grenwog Garb", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Garb\" ?", false),
        new Option<bool>("76027", "Pink-Band Battle Hair", "Mode: [select] only\nShould the bot buy \"Pink-Band Battle Hair\" ?", false),
        new Option<bool>("76028", "Pink-Barrette Battle Locks", "Mode: [select] only\nShould the bot buy \"Pink-Barrette Battle Locks\" ?", false),
        new Option<bool>("76029", "Pink Grenwog Tophat", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Tophat\" ?", false),
        new Option<bool>("76030", "Pink Grenwog Tophat + Locks", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Tophat + Locks\" ?", false),
        new Option<bool>("76031", "Pink Grenwog Ears", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Ears\" ?", false),
        new Option<bool>("76032", "Pink Grenwog Locks", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Locks\" ?", false),
        new Option<bool>("76033", "Pink Grenwog Carrot Kunai", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Carrot Kunai\" ?", false),
        new Option<bool>("76034", "Pink Grenwog Carrot Kunais", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Carrot Kunais\" ?", false),
        new Option<bool>("76035", "Pink Grenwog Watergun", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Watergun\" ?", false),
        new Option<bool>("76036", "Pink Grenwog Waterguns", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Waterguns\" ?", false),
        new Option<bool>("76037", "Pink Fists of Fury", "Mode: [select] only\nShould the bot buy \"Pink Fists of Fury\" ?", false),
        new Option<bool>("76038", "Pink Grenwog Cane", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Cane\" ?", false),
        new Option<bool>("76039", "Pink Grenwog Canes", "Mode: [select] only\nShould the bot buy \"Pink Grenwog Canes\" ?", false),
        new Option<bool>("76054", "Pastel Grenwog Garb", "Mode: [select] only\nShould the bot buy \"Pastel Grenwog Garb\" ?", false),
        new Option<bool>("76055", "White-Band Battle Hair", "Mode: [select] only\nShould the bot buy \"White-Band Battle Hair\" ?", false),
        new Option<bool>("76056", "Pastel-Barrette Battle Locks", "Mode: [select] only\nShould the bot buy \"Pastel-Barrette Battle Locks\" ?", false),
        new Option<bool>("76057", "White Grenwog Tophat", "Mode: [select] only\nShould the bot buy \"White Grenwog Tophat\" ?", false),
        new Option<bool>("76058", "White Grenwog Tophat + Locks", "Mode: [select] only\nShould the bot buy \"White Grenwog Tophat + Locks\" ?", false),
        new Option<bool>("76059", "Pastel Grenwog Ears", "Mode: [select] only\nShould the bot buy \"Pastel Grenwog Ears\" ?", false),
        new Option<bool>("76060", "Pastel Grenwog Locks", "Mode: [select] only\nShould the bot buy \"Pastel Grenwog Locks\" ?", false),
        new Option<bool>("76061", "Pastel Grenwog Kunai", "Mode: [select] only\nShould the bot buy \"Pastel Grenwog Kunai\" ?", false),
        new Option<bool>("76062", "Pastel Grenwog Kunais", "Mode: [select] only\nShould the bot buy \"Pastel Grenwog Kunais\" ?", false),
        new Option<bool>("76063", "Pastel Grenwog Watergun", "Mode: [select] only\nShould the bot buy \"Pastel Grenwog Watergun\" ?", false),
        new Option<bool>("76064", "Pastel Grenwog Waterguns", "Mode: [select] only\nShould the bot buy \"Pastel Grenwog Waterguns\" ?", false),
        new Option<bool>("76065", "Pastel Fists of Fury", "Mode: [select] only\nShould the bot buy \"Pastel Fists of Fury\" ?", false),
    };
}
