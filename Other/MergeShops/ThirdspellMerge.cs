/*
name: Thirdspell Merge
description: This bot will farm the items belonging to the selected mode for the Thirdspell Merge [892] in /thirdspell
tags: thirdspell, merge, thirdspell, fiery, flaring, flame, floating, sun, flare, loyal, solar, entity, morph, ephemerite, mace, stingers, lance
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ThirdspellMerge
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
        Core.BankingBlackList.AddRange(new[] { "Thirdspell Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("thirdspell", 892, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Thirdspell Token":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("thirdspell", "Pure Fire Elemental", req.Name, req.Quantity, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30829", "Fiery Flaring Flame", "Mode: [select] only\nShould the bot buy \"Fiery Flaring Flame\" ?", false),
        new Option<bool>("30830", "Floating Sun Flare", "Mode: [select] only\nShould the bot buy \"Floating Sun Flare\" ?", false),
        new Option<bool>("30831", "Loyal Sun Flare", "Mode: [select] only\nShould the bot buy \"Loyal Sun Flare\" ?", false),
        new Option<bool>("30832", "Solar Entity", "Mode: [select] only\nShould the bot buy \"Solar Entity\" ?", false),
        new Option<bool>("30833", "Solar Morph", "Mode: [select] only\nShould the bot buy \"Solar Morph\" ?", false),
        new Option<bool>("30834", "Dual Fiery Flame", "Mode: [select] only\nShould the bot buy \"Dual Fiery Flame\" ?", false),
        new Option<bool>("30882", "Fiery Ephemerite Mace", "Mode: [select] only\nShould the bot buy \"Fiery Ephemerite Mace\" ?", false),
        new Option<bool>("30890", "Solar Stingers", "Mode: [select] only\nShould the bot buy \"Solar Stingers\" ?", false),
        new Option<bool>("30904", "Armor of the Sun", "Mode: [select] only\nShould the bot buy \"Armor of the Sun\" ?", false),
        new Option<bool>("30905", "Cape of the Sun", "Mode: [select] only\nShould the bot buy \"Cape of the Sun\" ?", false),
        new Option<bool>("30906", "Helmet of the Sun", "Mode: [select] only\nShould the bot buy \"Helmet of the Sun\" ?", false),
        new Option<bool>("30907", "Lance of the Sun", "Mode: [select] only\nShould the bot buy \"Lance of the Sun\" ?", false),
        new Option<bool>("30912", "Solar Entity", "Mode: [select] only\nShould the bot buy \"Solar Entity\" ?", false),
        new Option<bool>("30913", "Solar Morph", "Mode: [select] only\nShould the bot buy \"Solar Morph\" ?", false),
    };
}
