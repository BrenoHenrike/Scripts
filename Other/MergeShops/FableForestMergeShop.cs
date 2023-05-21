/*
name: Fable Forest Merge
description: This bot will farm the items belonging to the selected mode for the Fable Forest Merge [815] in /fableforest
tags: fable, forest, merge, fableforest, wind, dragon, tail, oakheart, back, shield, magic, armblades, guardian, not, quite, chaos, shape, shadowscythe, morph, ultra, hydra, dreadspider, combo, dwakel, tech, fire, water, nature, prismatic, faerie, botanis, elements
*/
//cs_include Scripts\Story\FableForest.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FableForestMergeMerge
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
    //If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Plain Dragon Tail", "Wind Stone", "Earth Stone", "OakHeart Helm", "Fire Stone", "Water Stone", "OakHeart ArmBlades", "Chaos Stone", "Not Quite Dread Mask", "Not Quite Dread Shape", "Hydra Cape", "Dreadspider Cape", "Dreadspider Abdomen", "Red Dragon Morph", "Faerie Botanis Sword"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("fableforest", 815, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Plain Dragon Tail":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Plain Dragon Tail", 1);
                    }
                    break;

                case "Wind Stone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3316);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fableforest", "Wind Elemental", "Wind Aura", 5);
                        Core.HuntMonster("fableforest", "Forest Fury", "Forest Fury Feather", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Earth Stone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(3317);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fableforest", "Earth Elemental", "Earth Aura", 5);
                        Core.HuntMonster("fableforest", "Undead Satyr", "Satyr Hoof", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "OakHeart Helm":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "OakHeart Helm", 1);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Fire Stone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(3314);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fableforest", "Fire Elemental", "Fire Aura", 5);
                        Core.HuntMonster("fableforest", "Bloodwolf", "Bloodwolf Pelt", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Water Stone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(3315);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fableforest", "Water Elemental", "Water Aura", 5);
                        Core.HuntMonster("fableforest", "Aqueevil", "Aqueevil Spirit", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "OakHeart ArmBlades":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "OakHeart ArmBlades", 1);
                    }
                    break;

                case "Chaos Stone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(3318);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fableforest", "Forest Guardian", "Chaos Aura", 1);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Not Quite Dread Mask":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Not Quite Dread Mask", 1);
                    }
                    break;

                case "Not Quite Dread Shape":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Not Quite Dread Shape", 1);
                    }
                    break;

                case "Hydra Cape":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Hydra Cape", 1);
                    }
                    break;

                case "Dreadspider Cape":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Dreadspider Cape", 1);
                    }
                    break;

                case "Dreadspider Abdomen":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Dreadspider Abdomen", 1);
                    }
                    break;

                case "Red Dragon Morph":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Red Dragon Morph", 1);
                    }
                    break;

                case "Faerie Botanis Sword":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fableforest", 814, "Faerie Botanis Sword", 1);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("22324", "Wind Dragon Tail", "Mode: [select] only\nShould the bot buy \"Wind Dragon Tail\" ?", false),
        new Option<bool>("22274", "OakHeart Back Shield", "Mode: [select] only\nShould the bot buy \"OakHeart Back Shield\" ?", false),
        new Option<bool>("22273", "OakHeart Magic Helm", "Mode: [select] only\nShould the bot buy \"OakHeart Magic Helm\" ?", false),
        new Option<bool>("22271", "OakHeart Magic ArmBlades", "Mode: [select] only\nShould the bot buy \"OakHeart Magic ArmBlades\" ?", false),
        new Option<bool>("22269", "OakHeart Guardian", "Mode: [select] only\nShould the bot buy \"OakHeart Guardian\" ?", false),
        new Option<bool>("22254", "Not Quite Chaos Mask", "Mode: [select] only\nShould the bot buy \"Not Quite Chaos Mask\" ?", false),
        new Option<bool>("22253", "Not Quite Chaos Shape", "Mode: [select] only\nShould the bot buy \"Not Quite Chaos Shape\" ?", false),
        new Option<bool>("22237", "Chaos Shadowscythe Morph", "Mode: [select] only\nShould the bot buy \"Chaos Shadowscythe Morph\" ?", false),
        new Option<bool>("22233", "Ultra Hydra Cape", "Mode: [select] only\nShould the bot buy \"Ultra Hydra Cape\" ?", false),
        new Option<bool>("22230", "Dreadspider Combo", "Mode: [select] only\nShould the bot buy \"Dreadspider Combo\" ?", false),
        new Option<bool>("22222", "Dwakel Tech Spikes", "Mode: [select] only\nShould the bot buy \"Dwakel Tech Spikes\" ?", false),
        new Option<bool>("22221", "Fire Dragon Tail", "Mode: [select] only\nShould the bot buy \"Fire Dragon Tail\" ?", false),
        new Option<bool>("22220", "Water Dragon Tail", "Mode: [select] only\nShould the bot buy \"Water Dragon Tail\" ?", false),
        new Option<bool>("22219", "Nature Dragon Tail", "Mode: [select] only\nShould the bot buy \"Nature Dragon Tail\" ?", false),
        new Option<bool>("22210", "Prismatic Dragon Morph", "Mode: [select] only\nShould the bot buy \"Prismatic Dragon Morph\" ?", false),
        new Option<bool>("22154", "Faerie Botanis Staff", "Mode: [select] only\nShould the bot buy \"Faerie Botanis Staff\" ?", false),
        new Option<bool>("43451", "FableForest Elements Cape", "Mode: [select] only\nShould the bot buy \"FableForest Elements Cape\" ?", false),
    };
}
