/*
name: Frozen Soul Merge
description: This will get all or selected items on this merge shop.
tags: frozen-soul-merge, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FrozenSoulMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Hammered Ice", "Frosted Heart", "Frozen Soul", "Frozen Rune of Kheimon", "Poleaxe of Kheimon", "Elegant Frostvale Suit", "Cheery Frostvale Hat + Locks", "Elegant Frostval Wrap", "Cheery Frostvale Hat", "Ruby Frostval Cane" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("frozensoul", 1815, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Elegant Frostval Wrap":
                case "Hammered Ice":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7262);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("frozensoul", "Frozen Minion", "Shard of Ice", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Frosted Heart":
                case "Cheery Frostvale Hat + Locks":
                case "Cheery Frostvale Hat":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7263);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("frozensoul", "Jack Frost", "Jack's Frosted Heart", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Elegant Frostvale Suit":
                case "Ruby Frostval Cane":
                case "Frozen Soul":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7264);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("frozensoul", "r4", "Left", "*", "Queen's Frozen Soul", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Poleaxe of Kheimon":
                case "Frozen Rune of Kheimon":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("frozensoul", "r4", "Left", "*", req.Name, isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("52419", "Kheimon's Floating Tome", "Mode: [select] only\nShould the bot buy \"Kheimon's Floating Tome\" ?", false),
        new Option<bool>("52420", "Bladed Rune of Kheimon", "Mode: [select] only\nShould the bot buy \"Bladed Rune of Kheimon\" ?", false),
        new Option<bool>("52421", "Runic Poleaxe of Kheimon", "Mode: [select] only\nShould the bot buy \"Runic Poleaxe of Kheimon\" ?", false),
        new Option<bool>("52422", "Hood of the Northlands Monk", "Mode: [select] only\nShould the bot buy \"Hood of the Northlands Monk\" ?", false),
        new Option<bool>("52423", "Northlands Monk", "Mode: [select] only\nShould the bot buy \"Northlands Monk\" ?", false),
        new Option<bool>("51680", "Elegant Frost Suit", "Mode: [select] only\nShould the bot buy \"Elegant Frost Suit\" ?", false),
        new Option<bool>("51681", "Elegant Frost Locks", "Mode: [select] only\nShould the bot buy \"Elegant Frost Locks\" ?", false),
        new Option<bool>("51682", "Elegant Frost Topper", "Mode: [select] only\nShould the bot buy \"Elegant Frost Topper\" ?", false),
        new Option<bool>("51683", "Snowmist Wrap", "Mode: [select] only\nShould the bot buy \"Snowmist Wrap\" ?", false),
        new Option<bool>("51684", "Diamond Frost Cane", "Mode: [select] only\nShould the bot buy \"Diamond Frost Cane\" ?", false),
    };
}
