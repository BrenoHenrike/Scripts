/*
name: Crocriver Merge
description: This bot will farm the items belonging to the selected mode for the Crocriver Merge [2387] in /crocriver
tags: crocriver, merge, crocriver, sphinx, sentinel, commander, desher, desert, black, moon, criosphinx, sun, noble, anubian, overseer, resting, skyfall, spear, tail, keys, sandsea, life, oasis, storm, armblade, armblades
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CrocriverMerge
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
        Core.BankingBlackList.AddRange(new[] { "Sobekemsaph's Hieroglyph", "Sobekemsaph's Scale", "Sphinx Sentinel Helm", "Sheathed Black Moon Blades", "Sphinx Sentinel Cape" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("crocriver", 2387, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Sobekemsaph's Hieroglyph":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9538);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreampalace", "Golmoth", "Hieroglyph Ruby", log: false);
                        Core.HuntMonster("dreampalace", "Flaming Harpy", "Flame Glyph", 6, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Sobekemsaph's Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9539);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("crocriver", "Sobekemsaph", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Sphinx Sentinel Helm":
                case "Sheathed Black Moon Blades":
                case "Sphinx Sentinel Cape":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("crocriver", "Sobekemsaph", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("83065", "Sphinx Sentinel", "Mode: [select] only\nShould the bot buy \"Sphinx Sentinel\" ?", false),
        new Option<bool>("83066", "Sphinx Commander Helm", "Mode: [select] only\nShould the bot buy \"Sphinx Commander Helm\" ?", false),
        new Option<bool>("83067", "Sphinx Sentinel Desher Helm", "Mode: [select] only\nShould the bot buy \"Sphinx Sentinel Desher Helm\" ?", false),
        new Option<bool>("83069", "Sphinx Desher Helm and Hair", "Mode: [select] only\nShould the bot buy \"Sphinx Desher Helm and Hair\" ?", false),
        new Option<bool>("83070", "Sphinx Sentinel Helm and Hair", "Mode: [select] only\nShould the bot buy \"Sphinx Sentinel Helm and Hair\" ?", false),
        new Option<bool>("83073", "Sphinx Sentinel Cape and Blades", "Mode: [select] only\nShould the bot buy \"Sphinx Sentinel Cape and Blades\" ?", false),
        new Option<bool>("83074", "Desert Black Moon Blade", "Mode: [select] only\nShould the bot buy \"Desert Black Moon Blade\" ?", false),
        new Option<bool>("83075", "Desert Black Moon Blades", "Mode: [select] only\nShould the bot buy \"Desert Black Moon Blades\" ?", false),
        new Option<bool>("83076", "Criosphinx Sun Sword", "Mode: [select] only\nShould the bot buy \"Criosphinx Sun Sword\" ?", false),
        new Option<bool>("83077", "Criosphinx Sun Swords", "Mode: [select] only\nShould the bot buy \"Criosphinx Sun Swords\" ?", false),
        new Option<bool>("83101", "Noble Anubian Overseer", "Mode: [select] only\nShould the bot buy \"Noble Anubian Overseer\" ?", false),
        new Option<bool>("83102", "Noble Anubian Overseer Helm", "Mode: [select] only\nShould the bot buy \"Noble Anubian Overseer Helm\" ?", false),
        new Option<bool>("83104", "Resting Anubian Skyfall Spear", "Mode: [select] only\nShould the bot buy \"Resting Anubian Skyfall Spear\" ?", false),
        new Option<bool>("83105", "Noble Anubian Tail and Keys", "Mode: [select] only\nShould the bot buy \"Noble Anubian Tail and Keys\" ?", false),
        new Option<bool>("83106", "Noble Anubian Tail", "Mode: [select] only\nShould the bot buy \"Noble Anubian Tail\" ?", false),
        new Option<bool>("83107", "Sandsea Keys of Life", "Mode: [select] only\nShould the bot buy \"Sandsea Keys of Life\" ?", false),
        new Option<bool>("83108", "Oasis Storm Armblade", "Mode: [select] only\nShould the bot buy \"Oasis Storm Armblade\" ?", false),
        new Option<bool>("83109", "Oasis Storm Armblades", "Mode: [select] only\nShould the bot buy \"Oasis Storm Armblades\" ?", false),
        new Option<bool>("83110", "Anubian Skyfall Spear", "Mode: [select] only\nShould the bot buy \"Anubian Skyfall Spear\" ?", false),
        new Option<bool>("83621", "Pharaoh of Storms", "Mode: [select] only\nShould the bot buy \"Pharaoh of Storms\" ?", false),
        new Option<bool>("83622", "Sandstorm Wings", "Mode: [select] only\nShould the bot buy \"Sandstorm Wings\" ?", false),
        new Option<bool>("83623", "Sandstorm Sceptre", "Mode: [select] only\nShould the bot buy \"Sandstorm Sceptre\" ?", false),
        new Option<bool>("83624", "Enchanted Pharaoh of Storms", "Mode: [select] only\nShould the bot buy \"Enchanted Pharaoh of Storms\" ?", false),
        new Option<bool>("83625", "Pharaoh Djeba Morph", "Mode: [select] only\nShould the bot buy \"Pharaoh Djeba Morph\" ?", false),
        new Option<bool>("83626", "Enchanted Pharaoh Djeba Morph", "Mode: [select] only\nShould the bot buy \"Enchanted Pharaoh Djeba Morph\" ?", false),
        new Option<bool>("83627", "Veiled Pharaoh Tebtunis Visage", "Mode: [select] only\nShould the bot buy \"Veiled Pharaoh Tebtunis Visage\" ?", false),
        new Option<bool>("83628", "Pharaoh Tebtunis Visage", "Mode: [select] only\nShould the bot buy \"Pharaoh Tebtunis Visage\" ?", false),
        new Option<bool>("83629", "Pharaoh Tebtunis Veiled Ponytail", "Mode: [select] only\nShould the bot buy \"Pharaoh Tebtunis Veiled Ponytail\" ?", false),
        new Option<bool>("83630", "Pharaoh Tebtunis Ponytail", "Mode: [select] only\nShould the bot buy \"Pharaoh Tebtunis Ponytail\" ?", false),
        new Option<bool>("83631", "Pharaoh Tebtunis Veiled Locks", "Mode: [select] only\nShould the bot buy \"Pharaoh Tebtunis Veiled Locks\" ?", false),
        new Option<bool>("83632", "Pharaoh Tebtunis Locks", "Mode: [select] only\nShould the bot buy \"Pharaoh Tebtunis Locks\" ?", false),
        new Option<bool>("83633", "Enchanted Sandstorm Wings", "Mode: [select] only\nShould the bot buy \"Enchanted Sandstorm Wings\" ?", false),
        new Option<bool>("83634", "Enchanted Sandstorm Sceptre", "Mode: [select] only\nShould the bot buy \"Enchanted Sandstorm Sceptre\" ?", false),
    };
}