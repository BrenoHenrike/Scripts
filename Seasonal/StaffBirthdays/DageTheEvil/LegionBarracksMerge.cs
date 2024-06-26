/*
name: Legion Barracks Merge
description: This bot will farm the items belonging to the selected mode for the Legion Barracks Merge [2420] in /legionbarracks
tags: legion, barracks, merge, legionbarracks, skeletal, sacrificer, scowling, black, sun, penacho, skull, immortal, tepoztopilli, underworld, sentinel, ponytail, shroud, decima, render, renders, ptolomea, necromancer, blindfold, morph, possessed, kokytos, grand, antaeus, spear
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LegionBarracksMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDageBirthday CDB = new();
    private CoreLegion Legion = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "Underworld Drachma", "Grand Antaeus Spear" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        CDB.LegionBarracks();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("legionbarracks", 2420, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Underworld Drachma":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9620);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.KillMonster("legionbarracks", "r4", "Left", "*", "Legion Cocytus Engraving", 6, log: false);
                        Core.HuntMonster("legionbarracks", "Overdriven paladin", "Paladin's Death Tag", 6, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("legionbarracks", "Paladin Arondight", "Arondight's Starlight", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Grand Antaeus Spear":
                    Adv.BuyItem("legionbarracks", 1982, req.Name, quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("84429", "Skeletal Sacrificer", "Mode: [select] only\nShould the bot buy \"Skeletal Sacrificer\" ?", false),
        new Option<bool>("84432", "Scowling Black Sun Penacho Skull", "Mode: [select] only\nShould the bot buy \"Scowling Black Sun Penacho Skull\" ?", false),
        new Option<bool>("84449", "Immortal Sacrificer Tepoztopilli", "Mode: [select] only\nShould the bot buy \"Immortal Sacrificer Tepoztopilli\" ?", false),
        new Option<bool>("84311", "Underworld Sentinel", "Mode: [select] only\nShould the bot buy \"Underworld Sentinel\" ?", false),
        new Option<bool>("84312", "Underworld Sentinel Helm", "Mode: [select] only\nShould the bot buy \"Underworld Sentinel Helm\" ?", false),
        new Option<bool>("84313", "Underworld Sentinel Hair", "Mode: [select] only\nShould the bot buy \"Underworld Sentinel Hair\" ?", false),
        new Option<bool>("84314", "Underworld Sentinel Ponytail", "Mode: [select] only\nShould the bot buy \"Underworld Sentinel Ponytail\" ?", false),
        new Option<bool>("84318", "Underworld Sentinel Shroud and Blade", "Mode: [select] only\nShould the bot buy \"Underworld Sentinel Shroud and Blade\" ?", false),
        new Option<bool>("84320", "Decima Render", "Mode: [select] only\nShould the bot buy \"Decima Render\" ?", false),
        new Option<bool>("84321", "Decima Renders", "Mode: [select] only\nShould the bot buy \"Decima Renders\" ?", false),
        new Option<bool>("84422", "Ptolomea Necromancer", "Mode: [select] only\nShould the bot buy \"Ptolomea Necromancer\" ?", false),
        new Option<bool>("84423", "Ptolomea Necromancer Blindfold Morph", "Mode: [select] only\nShould the bot buy \"Ptolomea Necromancer Blindfold Morph\" ?", false),
        new Option<bool>("84424", "Ptolomea Necromancer Blindfold Visage", "Mode: [select] only\nShould the bot buy \"Ptolomea Necromancer Blindfold Visage\" ?", false),
        new Option<bool>("84425", "Ptolomea Necromancer Morph", "Mode: [select] only\nShould the bot buy \"Ptolomea Necromancer Morph\" ?", false),
        new Option<bool>("84426", "Ptolomea Necromancer Visage", "Mode: [select] only\nShould the bot buy \"Ptolomea Necromancer Visage\" ?", false),
        new Option<bool>("84427", "Possessed Kokytos Staff", "Mode: [select] only\nShould the bot buy \"Possessed Kokytos Staff\" ?", false),
        new Option<bool>("84603", "Grand Antaeus Spear", "Mode: [select] only\nShould the bot buy \"Grand Antaeus Spear\" ?", false),
    };
}
