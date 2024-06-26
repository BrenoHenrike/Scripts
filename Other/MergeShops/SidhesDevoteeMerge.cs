/*
name: Sidhes Devotee Merge
description: This bot will farm the items belonging to the selected mode for the Sidhes Devotee Merge [2447] in /castleeblana
tags: sidhes, devotee, merge, castleeblana, wrap, ensnared, devotees, goblet, mugs, brigand, bandana, misty, faes, moon, pistol, pistols, leanan, minions
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SidhesDevoteeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Sidhe's Silk", "Leanan Sidhe's Butterflies" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.Castleeblana();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("castleeblana", 2447, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Sidhe's Silk":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9746);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("castleeblana", "Leanan Sidhe", "Glassy Wings", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Leanan Sidhe's Butterflies":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("castleeblana", "Leanan Sidhe", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73755", "Sidhe's Devotee", "Mode: [select] only\nShould the bot buy \"Sidhe's Devotee\" ?", false),
        new Option<bool>("73756", "Sidhe's Devotee Hair", "Mode: [select] only\nShould the bot buy \"Sidhe's Devotee Hair\" ?", false),
        new Option<bool>("73757", "Sidhe's Devotee Locks", "Mode: [select] only\nShould the bot buy \"Sidhe's Devotee Locks\" ?", false),
        new Option<bool>("73758", "Sidhe's Devotee Wrap", "Mode: [select] only\nShould the bot buy \"Sidhe's Devotee Wrap\" ?", false),
        new Option<bool>("73759", "Sidhe's Devotee Daggers", "Mode: [select] only\nShould the bot buy \"Sidhe's Devotee Daggers\" ?", false),
        new Option<bool>("86218", "Sidhe's Devotee Dagger", "Mode: [select] only\nShould the bot buy \"Sidhe's Devotee Dagger\" ?", false),
        new Option<bool>("71315", "Ensnared Devotee's Goblet", "Mode: [select] only\nShould the bot buy \"Ensnared Devotee's Goblet\" ?", false),
        new Option<bool>("71316", "Ensnared Devotee's Mugs", "Mode: [select] only\nShould the bot buy \"Ensnared Devotee's Mugs\" ?", false),
        new Option<bool>("71320", "Sidhe's Brigand Devotee", "Mode: [select] only\nShould the bot buy \"Sidhe's Brigand Devotee\" ?", false),
        new Option<bool>("71322", "Sidhe's Brigand Bandana", "Mode: [select] only\nShould the bot buy \"Sidhe's Brigand Bandana\" ?", false),
        new Option<bool>("71324", "Misty Fae's Moon", "Mode: [select] only\nShould the bot buy \"Misty Fae's Moon\" ?", false),
        new Option<bool>("71327", "Sidhe's Brigand Pistol", "Mode: [select] only\nShould the bot buy \"Sidhe's Brigand Pistol\" ?", false),
        new Option<bool>("71328", "Sidhe's Brigand Pistols", "Mode: [select] only\nShould the bot buy \"Sidhe's Brigand Pistols\" ?", false),
        new Option<bool>("86221", "Leanan Sidhe's Minions", "Mode: [select] only\nShould the bot buy \"Leanan Sidhe's Minions\" ?", false),
        new Option<bool>("86171", "Leanan Sidhe's Aura", "Mode: [select] only\nShould the bot buy \"Leanan Sidhe's Aura\" ?", false),
    };
}
