/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowofChaosMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoC Laguna = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "ShadowChaos Mote " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Laguna.LagunaBeach();
        Adv.StartBuyAllMerge("laguna", 1917, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "ShadowChaos Mote":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7700);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("lagunabeach", "Flying Fisheye|ShadowChaos Brigand", "Chaos-ShadowFlame Sample", 15);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("56158", "Chaotic Sorcerer", "Mode: [select] only\nShould the bot buy \"Chaotic Sorcerer\" ?", false),
        new Option<bool>("56159", "Chaotic Sorcerer's Hood", "Mode: [select] only\nShould the bot buy \"Chaotic Sorcerer's Hood\" ?", false),
        new Option<bool>("56160", "Chaotic Sorceress' Visage", "Mode: [select] only\nShould the bot buy \"Chaotic Sorceress' Visage\" ?", false),
        new Option<bool>("56161", "Sorceress' Visage + Horns", "Mode: [select] only\nShould the bot buy \"Sorceress' Visage + Horns\" ?", false),
        new Option<bool>("56162", "Chaotic Sorcerer's Visage", "Mode: [select] only\nShould the bot buy \"Chaotic Sorcerer's Visage\" ?", false),
        new Option<bool>("56163", "Sorcerer's Visage + Horns", "Mode: [select] only\nShould the bot buy \"Sorcerer's Visage + Horns\" ?", false),
        new Option<bool>("56164", "Chaotic Floating Crystals", "Mode: [select] only\nShould the bot buy \"Chaotic Floating Crystals\" ?", false),
        new Option<bool>("56165", "Chaotic Energy Claws", "Mode: [select] only\nShould the bot buy \"Chaotic Energy Claws\" ?", false),
        new Option<bool>("56166", "Chaotic Gem Staff", "Mode: [select] only\nShould the bot buy \"Chaotic Gem Staff\" ?", false),
        new Option<bool>("56167", "Chaotic Khopesh", "Mode: [select] only\nShould the bot buy \"Chaotic Khopesh\" ?", false),
        new Option<bool>("56228", "DeepSea Scourge", "Mode: [select] only\nShould the bot buy \"DeepSea Scourge\" ?", false),
        new Option<bool>("56229", "DeepSea Hunter", "Mode: [select] only\nShould the bot buy \"DeepSea Hunter\" ?", false),
        new Option<bool>("56230", "Tentacled Hat + Locks", "Mode: [select] only\nShould the bot buy \"Tentacled Hat + Locks\" ?", false),
        new Option<bool>("56231", "Tentacled Hat", "Mode: [select] only\nShould the bot buy \"Tentacled Hat\" ?", false),
        new Option<bool>("56232", "DeepSea Hat + Locks", "Mode: [select] only\nShould the bot buy \"DeepSea Hat + Locks\" ?", false),
        new Option<bool>("56233", "DeepSea Hat", "Mode: [select] only\nShould the bot buy \"DeepSea Hat\" ?", false),
        new Option<bool>("56234", "Tentacle-Wrapped Pistol", "Mode: [select] only\nShould the bot buy \"Tentacle-Wrapped Pistol\" ?", false),
        new Option<bool>("56235", "DeepSea Pistol", "Mode: [select] only\nShould the bot buy \"DeepSea Pistol\" ?", false),
        new Option<bool>("56236", "Dual DeepSea Pistols", "Mode: [select] only\nShould the bot buy \"Dual DeepSea Pistols\" ?", false),
    };
}
