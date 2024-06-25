/*
name: ChronoGem Merge
description: This bot will farm the items belonging to the selected mode for the ChronoGem Merge [2386] in /chronogem
tags: new year, chronogem, merge, chronogem, quantum, centurion, galea, chrono, circlet, coronal, countdown, meridian, epoch, definer, pilum, destructive, decennium, shield
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/NewYear/CoreNewYear.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ChronoGemMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreNewYear NY = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Chrono Gem" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        NY.ChronoForge();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("chronogem", 2386, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Chrono Gem":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9536);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("chronogem", "Gem Forgemaster");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("82913", "Quantum Centurion", "Mode: [select] only\nShould the bot buy \"Quantum Centurion\" ?", false),
        new Option<bool>("82914", "Quantum Centurion Galea", "Mode: [select] only\nShould the bot buy \"Quantum Centurion Galea\" ?", false),
        new Option<bool>("82915", "Quantum Centurion Helm", "Mode: [select] only\nShould the bot buy \"Quantum Centurion Helm\" ?", false),
        new Option<bool>("82721", "Chrono Centurion Circlet", "Mode: [select] only\nShould the bot buy \"Chrono Centurion Circlet\" ?", false),
        new Option<bool>("82722", "Chrono Centurion Coronal", "Mode: [select] only\nShould the bot buy \"Chrono Centurion Coronal\" ?", false),
        new Option<bool>("82723", "Chrono Centurion Hair", "Mode: [select] only\nShould the bot buy \"Chrono Centurion Hair\" ?", false),
        new Option<bool>("82724", "Chrono Centurion Locks", "Mode: [select] only\nShould the bot buy \"Chrono Centurion Locks\" ?", false),
        new Option<bool>("82916", "Quantum Centurion Cape", "Mode: [select] only\nShould the bot buy \"Quantum Centurion Cape\" ?", false),
        new Option<bool>("82917", "Quantum Centurion Countdown", "Mode: [select] only\nShould the bot buy \"Quantum Centurion Countdown\" ?", false),
        new Option<bool>("82918", "Quantum Centurion Meridian", "Mode: [select] only\nShould the bot buy \"Quantum Centurion Meridian\" ?", false),
        new Option<bool>("82920", "Epoch Definer Blade", "Mode: [select] only\nShould the bot buy \"Epoch Definer Blade\" ?", false),
        new Option<bool>("82921", "Epoch Definer Pilum", "Mode: [select] only\nShould the bot buy \"Epoch Definer Pilum\" ?", false),
        new Option<bool>("82922", "Destructive Decennium", "Mode: [select] only\nShould the bot buy \"Destructive Decennium\" ?", false),
        new Option<bool>("82923", "Epoch Definer Blade and Shield", "Mode: [select] only\nShould the bot buy \"Epoch Definer Blade and Shield\" ?", false),
        new Option<bool>("82924", "Epoch Definer Pilum and Shield", "Mode: [select] only\nShould the bot buy \"Epoch Definer Pilum and Shield\" ?", false),
    };
}
