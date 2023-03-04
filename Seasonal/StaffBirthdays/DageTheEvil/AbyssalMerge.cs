/*
name: Abyssal Merge
description: This bot will farm materials in /abysslair and buy items from the merge shop
tags: legion, merge, staff, birthday, dage, evil, seasonal, event, darkbirthday, abysslair, abyssal, medalion, scale, coldfire, gem
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AbyssalMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreLegion Legion = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Abyssal Medallion", "Abyssal Scale", "Coldfire Gem", "Legion Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("abysslair"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("abysslair", 1841, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Abyssal Medallion":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7392);
                    Core.HuntMonster("abysslair", "Devourer of Souls", req.Name, quant, false, false);
                    Core.CancelRegisteredQuests();
                    break;

                case "Abyssal Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7391);
                    Core.HuntMonster("abysslair", "Abyssal Underbeast", req.Name, quant, false, false);
                    Core.CancelRegisteredQuests();
                    break;

                case "Coldfire Gem":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7390);
                    Core.HuntMonster("abysslair", "Abyssal Guard", req.Name, quant, false, false);
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("53276", "Underworld Evoker", "Mode: [select] only\nShould the bot buy \"Underworld Evoker\" ?", false),
        new Option<bool>("53277", "Empowered Underworld Evoker", "Mode: [select] only\nShould the bot buy \"Empowered Underworld Evoker\" ?", false),
        new Option<bool>("53278", "Underworld Evoker Shag", "Mode: [select] only\nShould the bot buy \"Underworld Evoker Shag\" ?", false),
        new Option<bool>("53279", "Underworld Evoker Ponytails", "Mode: [select] only\nShould the bot buy \"Underworld Evoker Ponytails\" ?", false),
        new Option<bool>("53280", "Underworld Evoker Cape", "Mode: [select] only\nShould the bot buy \"Underworld Evoker Cape\" ?", false),
        new Option<bool>("53281", "Underworld Evoker Vortex", "Mode: [select] only\nShould the bot buy \"Underworld Evoker Vortex\" ?", false),
        new Option<bool>("53282", "Underworld Evoker Vortices", "Mode: [select] only\nShould the bot buy \"Underworld Evoker Vortices\" ?", false),
        new Option<bool>("53602", "Bestial Sword of Fiend Control", "Mode: [select] only\nShould the bot buy \"Bestial Sword of Fiend Control\" ?", false),
        new Option<bool>("53574", "Legion Minister", "Mode: [select] only\nShould the bot buy \"Legion Minister\" ?", false),
        new Option<bool>("53575", "Legion Minister's Horned Locks", "Mode: [select] only\nShould the bot buy \"Legion Minister's Horned Locks\" ?", false),
        new Option<bool>("53576", "Legion Minister's Locks", "Mode: [select] only\nShould the bot buy \"Legion Minister's Locks\" ?", false),
        new Option<bool>("53577", "Legion Minister's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Legion Minister's Hat + Locks\" ?", false),
        new Option<bool>("53579", "Legion Minister's Hair", "Mode: [select] only\nShould the bot buy \"Legion Minister's Hair\" ?", false),
        new Option<bool>("53580", "Legion Minister's Hat", "Mode: [select] only\nShould the bot buy \"Legion Minister's Hat\" ?", false),
        new Option<bool>("53581", "DarkCaster's Ministry", "Mode: [select] only\nShould the bot buy \"DarkCaster's Ministry\" ?", false),
        new Option<bool>("53582", "Legion Minister's Flight", "Mode: [select] only\nShould the bot buy \"Legion Minister's Flight\" ?", false),
        new Option<bool>("53583", "DarkCaster's Winged Ministry", "Mode: [select] only\nShould the bot buy \"DarkCaster's Winged Ministry\" ?", false),
        new Option<bool>("53584", "Legion Minister's Fiend Hide", "Mode: [select] only\nShould the bot buy \"Legion Minister's Fiend Hide\" ?", false),
        new Option<bool>("53585", "Legion Minister's Authority", "Mode: [select] only\nShould the bot buy \"Legion Minister's Authority\" ?", false),
        new Option<bool>("53586", "Zealith Busters", "Mode: [select] only\nShould the bot buy \"Zealith Busters\" ?", false),
        new Option<bool>("53588", "Legion Minister's Stick", "Mode: [select] only\nShould the bot buy \"Legion Minister's Stick\" ?", false),
        new Option<bool>("53589", "Soul Sipper far0", "Mode: [select] only\nShould the bot buy \"Soul Sipper far0\" ?", false),
        new Option<bool>("76592", "Devouring Legion Rune", "Mode: [select] only\nShould the bot buy \"Devouring Legion Rune\" ?", false),
        new Option<bool>("76601", "Reaping Scythe of the Legion", "Mode: [select] only\nShould the bot buy \"Reaping Scythe of the Legion\" ?", false),
        new Option<bool>("76593", "Reaping Scythe of the Legion Cape", "Mode: [select] only\nShould the bot buy \"Reaping Scythe of the Legion Cape\" ?", false),
        new Option<bool>("68529", "Blade of Malevolence", "Mode: [select] only\nShould the bot buy \"Blade of Malevolence\" ?", false),
        new Option<bool>("68530", "Blades of Malevolence", "Mode: [select] only\nShould the bot buy \"Blades of Malevolence\" ?", false),
        new Option<bool>("68531", "Legion Malignant Blade", "Mode: [select] only\nShould the bot buy \"Legion Malignant Blade\" ?", false),
        new Option<bool>("68532", "Legion Malignant Blades", "Mode: [select] only\nShould the bot buy \"Legion Malignant Blades\" ?", false),
        new Option<bool>("76685", "Ascended Legion Evoker", "Mode: [select] only\nShould the bot buy \"Ascended Legion Evoker\" ?", false),
        new Option<bool>("76686", "Ascended Legion Evoker’s Hair", "Mode: [select] only\nShould the bot buy \"Ascended Legion Evoker’s Hair\" ?", false),
        new Option<bool>("76687", "Ascended Legion Evoker’s Locks", "Mode: [select] only\nShould the bot buy \"Ascended Legion Evoker’s Locks\" ?", false),
        new Option<bool>("76688", "Ascended Legion Evoker’s Staff", "Mode: [select] only\nShould the bot buy \"Ascended Legion Evoker’s Staff\" ?", false),
    };
}
