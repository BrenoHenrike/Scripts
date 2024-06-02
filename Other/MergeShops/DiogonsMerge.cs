/*
name: Diogons Merge
description: This bot will farm the items belonging to the selected mode for the Diogons Merge [2449] in /ectocave
tags: diogons, merge, ectocave, toxian, metal, strider, masked, panama, morph, radiation, vessel, radioactive, warning, synthesis, armblades
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DiogonsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Toxic Gem", "Gamma Toxic Gem" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ectocave", 2449, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Toxic Gem":
                    Core.FarmingLogger(req.Name, quant);
                    FarmToxicGem(quant);
                    break;

                case "Gamma Toxic Gem":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9751);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Adv.BuyItem("tercessuinotlim", 1951, "Doomatter", Log: false);
                        Adv.BuyItem("ectocave", 2449, "Toxian Metal", Log: false);
                        FarmToxicGem(5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public void FarmToxicGem(int quant = 500)
    {
        Core.EquipClass(ClassType.Solo);
        if (!Core.CheckInventory("Toxian Gas Mask"))
        {
            Core.Logger("Toxian Gas Mask is not in inventory. Farming it now.");
            Core.AddDrop("Toxian Gas Mask");
            Core.EnsureAccept(9748);
            Adv.BuyItem("ectocave", 2449, "Toxian Metal", 3);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("therift", "Noxious Fumes", "Noxious Fumes", 7);
            Core.HuntMonster("shadowattack", "Toxic Fiend", "Toxic Fiend Blood", 3);
            Core.HuntMonster("ectocave", "Sludge Beast", "Sludge Beast’s Tentacle");
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("ectocave", "Ektorax", "Ektorax’s Ectoplasm");
            Core.EnsureComplete(9748);
        }
        Core.RegisterQuests(9750);
        while (!Bot.ShouldExit && !Core.CheckInventory("Toxic Gem", quant))
        {
            Core.HuntMonster("extriki", "Extriki", "Extriki’s Shard", log: false);
            Bot.Wait.ForPickup("Toxic Gem");
        }
        Core.CancelRegisteredQuests();
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("85281", "Toxian Metal", "Mode: [select] only\nShould the bot buy \"Toxian Metal\" ?", false),
        new Option<bool>("85957", "Toxian Strider", "Mode: [select] only\nShould the bot buy \"Toxian Strider\" ?", false),
        new Option<bool>("85959", "Toxian Strider Hair", "Mode: [select] only\nShould the bot buy \"Toxian Strider Hair\" ?", false),
        new Option<bool>("85960", "Toxian Strider Locks", "Mode: [select] only\nShould the bot buy \"Toxian Strider Locks\" ?", false),
        new Option<bool>("85961", "Toxian Strider Masked Hair", "Mode: [select] only\nShould the bot buy \"Toxian Strider Masked Hair\" ?", false),
        new Option<bool>("85962", "Toxian Strider Masked Locks", "Mode: [select] only\nShould the bot buy \"Toxian Strider Masked Locks\" ?", false),
        new Option<bool>("85963", "Toxian Strider Hat", "Mode: [select] only\nShould the bot buy \"Toxian Strider Hat\" ?", false),
        new Option<bool>("85964", "Toxian Strider Panama", "Mode: [select] only\nShould the bot buy \"Toxian Strider Panama\" ?", false),
        new Option<bool>("85965", "Toxian Strider Morph", "Mode: [select] only\nShould the bot buy \"Toxian Strider Morph\" ?", false),
        new Option<bool>("85966", "Toxian Strider Visage", "Mode: [select] only\nShould the bot buy \"Toxian Strider Visage\" ?", false),
        new Option<bool>("85967", "Toxian Radiation Vessel", "Mode: [select] only\nShould the bot buy \"Toxian Radiation Vessel\" ?", false),
        new Option<bool>("85968", "Radioactive Warning", "Mode: [select] only\nShould the bot buy \"Radioactive Warning\" ?", false),
        new Option<bool>("85969", "Toxian Synthesis Sword", "Mode: [select] only\nShould the bot buy \"Toxian Synthesis Sword\" ?", false),
        new Option<bool>("85970", "Toxian Synthesis Swords", "Mode: [select] only\nShould the bot buy \"Toxian Synthesis Swords\" ?", false),
        new Option<bool>("85971", "Toxian Synthesis Armblades", "Mode: [select] only\nShould the bot buy \"Toxian Synthesis Armblades\" ?", false),
    };
}
