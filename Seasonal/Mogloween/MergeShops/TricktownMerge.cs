/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TricktownMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreMogloween CoreMogloween = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Treats", "Ghastly Gummy " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("tricktown"))
            return;
        CoreMogloween.TrickTown();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("tricktown", 2179, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Treats":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.AddDrop("Treats");
                        Core.Join("tricktown");
                        Core.KillMonster("trickortreat", "Enter", "Spawn", "Trick or Treater");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Ghastly Gummy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(8936); // Ghoul Gang 8936
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("tricktown", "Madam Ester", "Crystalized Slime", 1);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("tricktown", "Decay Spirit", "Decay Essence", 10);
                        Core.HuntMonster("tricktown", "Rotting Mound", "Melty Scabs", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("72073", "Eventide Sorcerer", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer\" ?", false),
        new Option<bool>("72074", "Eventide Sorcerer's Cut", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Cut\" ?", false),
        new Option<bool>("72075", "Eventide Sorcerer's Layered Bobcut", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Layered Bobcut\" ?", false),
        new Option<bool>("72076", "Eventide Sorcerer's Awakened Cut", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Awakened Cut\" ?", false),
        new Option<bool>("72077", "Eventide Sorcerer Awakened Bobcut", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer Awakened Bobcut\" ?", false),
        new Option<bool>("72078", "Eventide Sorcerer's Hat", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Hat\" ?", false),
        new Option<bool>("72079", "Eventide Sorcerer's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Hat + Locks\" ?", false),
        new Option<bool>("72080", "Eventide Sorcerer's Wings", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Wings\" ?", false),
        new Option<bool>("72081", "Eventide Sorcerer's Rune", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Rune\" ?", false),
        new Option<bool>("72082", "Eventide Sorcerer's Grimoire", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Grimoire\" ?", false),
        new Option<bool>("72083", "Eventide Sorcerer's Broom", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Broom\" ?", false),
        new Option<bool>("72084", "Eventide Sorcerer's Runed Broom", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Runed Broom\" ?", false),
        new Option<bool>("72085", "Eventide Sorcerer's Offense Rune", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Offense Rune\" ?", false),
        new Option<bool>("72086", "Eventide Sorcerer's Offense Runes", "Mode: [select] only\nShould the bot buy \"Eventide Sorcerer's Offense Runes\" ?", false),
        new Option<bool>("71842", "DoomFire Club Shirt", "Mode: [select] only\nShould the bot buy \"DoomFire Club Shirt\" ?", false),
        new Option<bool>("71843", "Twilleven", "Mode: [select] only\nShould the bot buy \"Twilleven\" ?", false),
        new Option<bool>("71844", "Rebel Twilleven", "Mode: [select] only\nShould the bot buy \"Rebel Twilleven\" ?", false),
        new Option<bool>("71630", "Diabolical Noble Garb", "Mode: [select] only\nShould the bot buy \"Diabolical Noble Garb\" ?", false),
        new Option<bool>("71631", "Diabolical Noble Morph", "Mode: [select] only\nShould the bot buy \"Diabolical Noble Morph\" ?", false),
        new Option<bool>("71632", "Diabolical Noble Spikes", "Mode: [select] only\nShould the bot buy \"Diabolical Noble Spikes\" ?", false),
        new Option<bool>("71633", "Diabolical Noble Morph + Bangs", "Mode: [select] only\nShould the bot buy \"Diabolical Noble Morph + Bangs\" ?", false),
        new Option<bool>("71634", "Diabolical Noble Bangs", "Mode: [select] only\nShould the bot buy \"Diabolical Noble Bangs\" ?", false),
        new Option<bool>("71635", "Devoured Diabolical Sigil", "Mode: [select] only\nShould the bot buy \"Devoured Diabolical Sigil\" ?", false),
        new Option<bool>("71636", "Diabolical Ornate Shrine", "Mode: [select] only\nShould the bot buy \"Diabolical Ornate Shrine\" ?", false),
        new Option<bool>("71637", "Devoured Sinner's Blade", "Mode: [select] only\nShould the bot buy \"Devoured Sinner's Blade\" ?", false),
        new Option<bool>("71638", "Devoured Sinner's Blades", "Mode: [select] only\nShould the bot buy \"Devoured Sinner's Blades\" ?", false),
        new Option<bool>("71639", "Diabolical Sinner’s Blade", "Mode: [select] only\nShould the bot buy \"Diabolical Sinner’s Blade\" ?", false),
        new Option<bool>("71640", "Diabolical Sinner’s Blades", "Mode: [select] only\nShould the bot buy \"Diabolical Sinner’s Blades\" ?", false),
        new Option<bool>("71641", "Diabolical ManSplitter", "Mode: [select] only\nShould the bot buy \"Diabolical ManSplitter\" ?", false),
    };
}
