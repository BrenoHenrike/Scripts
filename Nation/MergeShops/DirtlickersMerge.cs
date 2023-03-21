/*
name: Dirtlicker's Merge Shop
description: This script farms all the materials needed for Dirtlicker's Merge Shop in archportal.
tags: dirtlicker, archportal, merge, nulgath, ennh, enchanted nulgath nation house
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Nation\Various\TheLeeryContract[Member].cs
//cs_include Scripts/Nation\Various\JuggernautItems.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DirtlickersMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreDailies Daily = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreNation Nation = new();
    private CoreBLOD BLOD = new();
    private TheLeeryContract TLC = new();
    private JuggernautItemsofNulgath Jugger = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Nulgath Nation House", "Pink Star Diamond of Nulgath", "Musgravite of Nulgath", "Nulgath's Approval", "Totem of Nulgath", "Voucher of Nulgath (non-mem)", "Unidentified 13", "Dark Crystal Shard", "Tainted Gem", "Gem of Nulgath", "Corpse Maker of Nulgath", "Overfiend Blade of Nulgath", "Archfiend's Favor", "Hadean Onyx of Nulgath", "Essence of Nulgath", "Letter from Asuka and Tendou", "Chain of Nulgath", "Yulgath's Hut", "Unidentified 10" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("archportal", 1211, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Nulgath Nation House":
                    Core.FarmingLogger(req.Name, quant);
                    Core.AddDrop("Cemaros' Amethyst", "Aluminium", "NUE Necronomicon");
                    Nation.FarmUni10(400);
                    Nation.FarmUni13();
                    Nation.FarmVoucher(false);
                    Nation.FarmDiamondofNulgath(300);
                    Nation.FarmDarkCrystalShard(250);
                    Nation.FarmTotemofNulgath(30);
                    Nation.FarmGemofNulgath(150);
                    Nation.SwindleBulk(200);
                    Nation.FarmBloodGem(100);
                    Nation.ApprovalAndFavor(1000, 0);

                    Farm.ChaosREP(2);
                    Core.BuyItem("mountdoomskull", 776, "Cemaros' Amethyst");

                    BLOD.UnlockMineCrafting();
                    Daily.MineCrafting(new[] { "Aluminum" });

                    Farm.DoomWoodREP();
                    Farm.Gold(999);
                    Core.BuyItem("lightguard", 277, "NUE Necronomicon");

                    Core.EnsureAccept(4779);
                    if (!Core.EnsureComplete(4779))
                    {
                        Core.Logger("Could not complete the quest, stopping bot", messageBox: true);
                        return;
                    }
                    Bot.Drops.Pickup(req.Name);
                    break;

                case "Pink Star Diamond of Nulgath":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("guru", "Guru Chest", req.Name, quant, false);
                    break;

                case "Musgravite of Nulgath":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("timelibrary", "Ancient Chest", req.Name, quant, false);
                    break;

                case "Nulgath's Approval":
                    Nation.ApprovalAndFavor(quant, 0);
                    break;

                case "Totem of Nulgath":
                    Nation.FarmTotemofNulgath(quant);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Nation.FarmVoucher(false);
                    break;

                case "Unidentified 13":
                    Nation.FarmUni13(quant);
                    break;

                case "Dark Crystal Shard":
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Tainted Gem":
                    if (Core.CheckInventory("Gemstone of Nulgath") && Core.IsMember)
                        Nation.ForgeTaintedGems(quant);
                    else
                        Nation.SwindleBulk(quant);
                    break;

                case "Gem of Nulgath":
                    Nation.FarmGemofNulgath(quant);
                    break;

                case "Corpse Maker of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    TLC.QuestItems(TheLeeryContract.RewardsSelection.Corpse_Maker_of_Nulgath);
                    break;

                case "Overfiend Blade of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Jugger.JuggItems(JuggernautItemsofNulgath.RewardsSelection.Overfiend_Blade_of_Nulgath);
                    break;

                case "Archfiend's Favor":
                    Nation.ApprovalAndFavor(0, quant);
                    break;

                case "Hadean Onyx of Nulgath":
                    Core.HuntMonster("tercessuinotlim", "Shadow of Nulgath", req.Name, quant, false);
                    break;

                case "Essence of Nulgath":
                    Nation.EssenceofNulgath(quant);
                    break;

                case "Letter from Asuka and Tendou":
                    Core.HuntMonster("citadel", "Burning Witch", req.Name, quant, false);
                    break;

                case "Chain of Nulgath":
                    Core.HuntMonster("necrocavern", "Shadow Dragon", req.Name, quant, false);
                    break;

                case "Yulgath's Hut":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("originul", "Fiend Champion", req.Name, quant, false);
                    break;

                case "Unidentified 10":
                    Nation.FarmUni10(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("33283", "Enchanted Nulgath Nation House", "Mode: [select] only\nShould the bot buy \"Enchanted Nulgath Nation House\" ?", false),
        new Option<bool>("33196", "Evolved Blood Orb", "Mode: [select] only\nShould the bot buy \"Evolved Blood Orb\" ?", false),
        new Option<bool>("33197", "Evolved Hex Orb", "Mode: [select] only\nShould the bot buy \"Evolved Hex Orb\" ?", false),
        new Option<bool>("33198", "Evolved Shadow Orb", "Mode: [select] only\nShould the bot buy \"Evolved Shadow Orb\" ?", false),
        new Option<bool>("33155", "Evolved Soulreaper Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Evolved Soulreaper Blade of Nulgath\" ?", false),
        new Option<bool>("33165", "Raw Dreadsaw", "Mode: [select] only\nShould the bot buy \"Raw Dreadsaw\" ?", false),
        new Option<bool>("33164", "Iron Dreadsaw", "Mode: [select] only\nShould the bot buy \"Iron Dreadsaw\" ?", false),
        new Option<bool>("33270", "Shadow Legacy of Nulgath", "Mode: [select] only\nShould the bot buy \"Shadow Legacy of Nulgath\" ?", false),
        new Option<bool>("33271", "Shadow Legacy of Nulgath Helm", "Mode: [select] only\nShould the bot buy \"Shadow Legacy of Nulgath Helm\" ?", false),
        new Option<bool>("43230", "Shadow Legacy of Nulgath Hair", "Mode: [select] only\nShould the bot buy \"Shadow Legacy of Nulgath Hair\" ?", false),
        new Option<bool>("58542", "Yulgath's Inn", "Mode: [select] only\nShould the bot buy \"Yulgath's Inn\" ?", false),
    };
}
