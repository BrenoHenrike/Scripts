/*
name: Hollowborn 
description: This bot will farm the items belonging to the selected mode for the Hollowborn  [1887] in /hbchallenge
tags: hollowborn, , hbchallenge, adept, spirit, spite, locks, shag, blades, cleaver, executioner's, axe, druidic, soothsayer, soothsayer’s, antlered, skull, soothsayer's, hooded, visage, hood, runed, cape, runes, burning, aura, rune, gate, staff, spike, spikes, gauntlet
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HollowbornMerge
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
        Core.BankingBlackList.AddRange(new[] { "Hollowborn Adept", "Hollowborn Spirit", "Hollowborn Spite", "Hollowborn Locks", "Hollowborn Shag", "Hollowborn Blades", "Hollowborn Cleaver", "Hollowborn Executioner's Axe", "Druidic Soothsayer", "Druidic Soothsayer’s Antlered Skull", "Druidic Soothsayer's Hooded Visage", "Druidic Soothsayer Hood", "Druidic Soothsayer's Runed Cape", "Druidic Soothsayer's Cape", "Druidic Soothsayer's Runes", "Druidic Soothsayer's Burning Cape", "Druidic Soothsayer's Burning Aura", "Druidic Soothsayer Rune Gate", "Druidic Soothsayer Staff", "Druidic Soothsayer Spike", "Druidic Soothsayer Spikes", "Druidic Soothsayer Gauntlet" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hbchallenge", 1887, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hollowborn Adept":
                case "Hollowborn Locks":
                case "Hollowborn Shag":
                case "Hollowborn Blades":
                case "Hollowborn Cleaver":
                case "Hollowborn Executioner's Axe":
                    Core.HuntMonster("hbchallenge", "Shadow Rider", req.Name, isTemp: false);
                    break;

                case "Hollowborn Spirit":
                case "Hollowborn Spite":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7548);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("hbchallenge", "Enter", "Spawn", "Hollowborn Tamer", "Hollowborn Tamer Defeated", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Druidic Soothsayer":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Logger("This item is not setup yet");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Druidic Soothsayer’s Antlered Skull":
                case "Druidic Soothsayer Hood":
                case "Druidic Soothsayer's Hooded Visage":
                case "Druidic Soothsayer's Runed Cape":
                case "Druidic Soothsayer's Cape":
                case "Druidic Soothsayer's Runes":
                case "Druidic Soothsayer's Burning Aura":
                case "Druidic Soothsayer's Burning Cape":
                case "Druidic Soothsayer Rune Gate":
                case "Druidic Soothsayer Staff":
                case "Druidic Soothsayer Spike":
                case "Druidic Soothsayer Spikes":
                case "Druidic Soothsayer Gauntlet":
                    Adv.BuyItem($"dragonrune", 689, req.Name, quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54655", "Hollowborn Executioner", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner\" ?", false),
        new Option<bool>("54657", "Hollowborn Executioner's Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner's Hood\" ?", false),
        new Option<bool>("54660", "Hollowborn Gas Mask + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Gas Mask + Locks\" ?", false),
        new Option<bool>("54661", "Hollowborn Gas Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Gas Mask\" ?", false),
        new Option<bool>("54663", "Hollowborn Scarves", "Mode: [select] only\nShould the bot buy \"Hollowborn Scarves\" ?", false),
        new Option<bool>("54664", "Hollowborn Executioner's Bite", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner's Bite\" ?", false),
        new Option<bool>("54955", "Dual Hollowborn Cleavers", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Cleavers\" ?", false),
        new Option<bool>("54973", "Hollowborn Executioner's Bite + Axe", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner's Bite + Axe\" ?", false),
        new Option<bool>("77195", "Hollowborn Druid", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid\" ?", false),
        new Option<bool>("77196", "Hollowborn Druid's Antlered Skull", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid's Antlered Skull\" ?", false),
        new Option<bool>("77197", "Hollowborn Druid's Hooded Visage", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid's Hooded Visage\" ?", false),
        new Option<bool>("77198", "Hollowborn Druid's Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid's Hood\" ?", false),
        new Option<bool>("77199", "Hollowborn Druid's Runed Cape", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid's Runed Cape\" ?", false),
        new Option<bool>("77200", "Hollowborn Druid's Cape", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid's Cape\" ?", false),
        new Option<bool>("77201", "Hollowborn Druid’s Runes", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid’s Runes\" ?", false),
        new Option<bool>("77202", "Hollowborn Druid's Burning Cape", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid's Burning Cape\" ?", false),
        new Option<bool>("77203", "Hollowborn Druid's Burning Aura", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid's Burning Aura\" ?", false),
        new Option<bool>("77204", "Hollowborn Druid Rune Gate", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid Rune Gate\" ?", false),
        new Option<bool>("77205", "Hollowborn Druid Staff", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid Staff\" ?", false),
        new Option<bool>("77206", "Hollowborn Druid Spike", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid Spike\" ?", false),
        new Option<bool>("77207", "Hollowborn Druid Spikes", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid Spikes\" ?", false),
        new Option<bool>("77208", "Hollowborn Druid Gauntlet", "Mode: [select] only\nShould the bot buy \"Hollowborn Druid Gauntlet\" ?", false),
    };
}
