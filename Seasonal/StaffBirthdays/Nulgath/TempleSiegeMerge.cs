/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TempleSiegeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreNation Nation = new();
    private DragonBladeofNulgath DB = new();
    private TempleSiege TS = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Gem of Nulgath", "Shadow Extract", "Unidentified 13", "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath", "Blood Gem of the Archfiend", "Warden of Light", "Conqueror of Shadow", "Crimson Plate of Nulgath", "Behemoth Blade of Light", "Behemoth Blade of Shadow", "DragonFire of Nulgath", "Light Warden Helm", "Shadow Conqueror Helm", "Crimson Face Plate of Nulgath" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("templesiege"))
            return;

        TS.CompleteTempleSiege();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("templesiege", 2227, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Gem of Nulgath":
                    Nation.FarmGemofNulgath(quant);
                    break;

                case "Shadow Extract":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9068);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("templesiege", "Doomed Oblivion", "Oblivion's Gem", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("templesiege", "Doomed Beast", "Dark Remnants", 7, log: false);
                        Core.HuntMonster("templesiege", "Overdriven Paladin", "Paladin Armament", 7, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Unidentified 13":
                    Nation.FarmUni13(quant);
                    break;

                case "Tainted Gem":
                    Nation.SwindleBulk(quant);
                    break;

                case "Dark Crystal Shard":
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Blood Gem of the Archfiend":
                    Nation.FarmBloodGem(quant);
                    break;

                case "Warden of Light":
                    Core.FarmingLogger(req.Name, quant);
                    Farm.BludrutBrawlBoss(quant: 500);
                    Core.BuyItem("battleon", 222, req.Name);
                    break;

                case "Conqueror of Shadow":
                    Core.FarmingLogger(req.Name, quant);
                    Farm.BludrutBrawlBoss(quant: 350);
                    Core.BuyItem("battleon", 222, req.Name);
                    break;

                case "Crimson Plate of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.EnsureAccept(765);
                    Nation.FarmTotemofNulgath(3);
                    Core.HuntMonster("underworld", "Skull Warrior", "Skull Warrior Rune");
                    Core.EnsureComplete(765, 4695);
                    break;

                case "Behemoth Blade of Light":
                    DB.BehemothBladeof("Light");
                    break;

                case "Behemoth Blade of Shadow":
                    DB.BehemothBladeof("Shadow");
                    break;

                case "DragonFire of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.EnsureAccept(765);
                    Nation.FarmTotemofNulgath(3);
                    Core.HuntMonster("underworld", "Skull Warrior", "Skull Warrior Rune");
                    Core.EnsureComplete(765, 1316);
                    break;

                case "Light Warden Helm":
                    Core.FarmingLogger(req.Name, quant);
                    Farm.BludrutBrawlBoss(quant: 150);
                    Core.BuyItem("battleon", 222, req.Name);
                    break;

                case "Shadow Conqueror Helm":
                    Core.FarmingLogger(req.Name, quant);
                    Farm.BludrutBrawlBoss(quant: 100);
                    Core.BuyItem("battleon", 222, req.Name);
                    break;

                case "Crimson Face Plate of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.EnsureAccept(765);
                    Nation.FarmTotemofNulgath(3);
                    Core.HuntMonster("underworld", "Skull Warrior", "Skull Warrior Rune");
                    Core.EnsureComplete(765, 4961);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("75653", "Beetle General Pet", "Mode: [select] only\nShould the bot buy \"Beetle General Pet\" ?", false),
        new Option<bool>("75315", "Awakened Fiendslasher", "Mode: [select] only\nShould the bot buy \"Awakened Fiendslasher\" ?", false),
        new Option<bool>("75316", "Awakened Fiendslashers", "Mode: [select] only\nShould the bot buy \"Awakened Fiendslashers\" ?", false),
        new Option<bool>("75317", "Awakened Fiend's Staff", "Mode: [select] only\nShould the bot buy \"Awakened Fiend's Staff\" ?", false),
        new Option<bool>("75318", "Awakened Fiend's Spear", "Mode: [select] only\nShould the bot buy \"Awakened Fiend's Spear\" ?", false),
        new Option<bool>("75776", "Abyssal Makai", "Mode: [select] only\nShould the bot buy \"Abyssal Makai\" ?", false),
        new Option<bool>("69138", "Evolved Warden of Light", "Mode: [select] only\nShould the bot buy \"Evolved Warden of Light\" ?", false),
        new Option<bool>("69143", "Evolved Conqueror of Shadows", "Mode: [select] only\nShould the bot buy \"Evolved Conqueror of Shadows\" ?", false),
        new Option<bool>("69148", "Evolved Crimson Plate of Nulgath", "Mode: [select] only\nShould the bot buy \"Evolved Crimson Plate of Nulgath\" ?", false),
        new Option<bool>("69141", "Warden of Light's Blade", "Mode: [select] only\nShould the bot buy \"Warden of Light's Blade\" ?", false),
        new Option<bool>("69146", "Conqueror of Shadows's Blade", "Mode: [select] only\nShould the bot buy \"Conqueror of Shadows's Blade\" ?", false),
        new Option<bool>("69151", "Evolved DragonFire of Nulgath", "Mode: [select] only\nShould the bot buy \"Evolved DragonFire of Nulgath\" ?", false),
        new Option<bool>("69139", "Evolved Warden of Light's Mask", "Mode: [select] only\nShould the bot buy \"Evolved Warden of Light's Mask\" ?", false),
        new Option<bool>("69140", "Evolved Warden of Light's Mask + Locks", "Mode: [select] only\nShould the bot buy \"Evolved Warden of Light's Mask + Locks\" ?", false),
        new Option<bool>("69144", "Evolved Conqueror of Shadows's Mask", "Mode: [select] only\nShould the bot buy \"Evolved Conqueror of Shadows's Mask\" ?", false),
        new Option<bool>("69145", "Evolved Conqueror of Shadows's Mask + Locks", "Mode: [select] only\nShould the bot buy \"Evolved Conqueror of Shadows's Mask + Locks\" ?", false),
        new Option<bool>("69149", "Evolved Crimson Mask of Nulgath", "Mode: [select] only\nShould the bot buy \"Evolved Crimson Mask of Nulgath\" ?", false),
        new Option<bool>("69150", "Evolved Crimson Mask + Locks of Nulgath", "Mode: [select] only\nShould the bot buy \"Evolved Crimson Mask + Locks of Nulgath\" ?", false),
    };
}
