/*
name: Skye Emissarys Merge
description: This bot will farm the items belonging to the selected mode for the Skye Emissarys Merge [2441] in /balemorale
tags: skye, emissarys, merge, balemorale, gold, voucher, k, halcyon, virtue, scepter, coronation, electrowave, scholar, shockwave, morph, scholars, ac, royal, dc, paralyzer, bow, longsword, longswords, hand
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SkyeEmissarysMerge
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
        Core.BankingBlackList.AddRange(new[] { "Pristine Deepsea Pearl", "Deepdark Pearl", "Electrojolt Scholar", "Tattered Court Mage Robe", "Victoria's Fletching", "Royal Electrojolt Scholar" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("balemorale", 2441, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Pristine Deepsea Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9718);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("ashray", "Enter", "Spawn", "*", "Deepsea Pearls", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Deepdark Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9715);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("midnightzone", "Shadow Viscera", "Viscera Sample", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Electrojolt Scholar":
                case "Royal Electrojolt Scholar":
                    Adv.BuyItem("balemorale", 2443, req.Name, quant);
                    break;

                case "Tattered Court Mage Robe":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("balemorale", "Chaos Crystal", req.Name, quant, req.Temp, false);
                    break;

                case "Victoria's Fletching":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("balemorale", "Queen Victoria", req.Name, quant, req.Temp, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("67387", "Halcyon Virtue Blade", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Blade\" ?", false),
        new Option<bool>("67388", "Halcyon Virtue Blades", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Blades\" ?", false),
        new Option<bool>("85944", "Halcyon Skye Blade", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Blade\" ?", false),
        new Option<bool>("85945", "Halcyon Skye Blades", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Blades\" ?", false),
        new Option<bool>("85942", "Halcyon Virtue Scepter", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Scepter\" ?", false),
        new Option<bool>("85943", "Halcyon Skye Scepter", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Scepter\" ?", false),
        new Option<bool>("85948", "Halcyon Virtue Axe", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Axe\" ?", false),
        new Option<bool>("85949", "Halcyon Virtue Axes", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Axes\" ?", false),
        new Option<bool>("67397", "Halcyon Skye Axe", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Axe\" ?", false),
        new Option<bool>("67398", "Halcyon Skye Axes", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Axes\" ?", false),
        new Option<bool>("85950", "Halcyon Coronation Axe", "Mode: [select] only\nShould the bot buy \"Halcyon Coronation Axe\" ?", false),
        new Option<bool>("85951", "Halcyon Coronation Axes", "Mode: [select] only\nShould the bot buy \"Halcyon Coronation Axes\" ?", false),
        new Option<bool>("85876", "Electrowave Scholar", "Mode: [select] only\nShould the bot buy \"Electrowave Scholar\" ?", false),
        new Option<bool>("85878", "Shockwave Scholar", "Mode: [select] only\nShould the bot buy \"Shockwave Scholar\" ?", false),
        new Option<bool>("85879", "Electrowave Scholar Morph", "Mode: [select] only\nShould the bot buy \"Electrowave Scholar Morph\" ?", false),
        new Option<bool>("85883", "Scholar's AC Gauntlet", "Mode: [select] only\nShould the bot buy \"Scholar's AC Gauntlet\" ?", false),
        new Option<bool>("85885", "Royal Electrowave Scholar", "Mode: [select] only\nShould the bot buy \"Royal Electrowave Scholar\" ?", false),
        new Option<bool>("85887", "Royal Shockwave Scholar", "Mode: [select] only\nShould the bot buy \"Royal Shockwave Scholar\" ?", false),
        new Option<bool>("85888", "Royal Electrowave Scholar Morph", "Mode: [select] only\nShould the bot buy \"Royal Electrowave Scholar Morph\" ?", false),
        new Option<bool>("85892", "Scholar's DC Gauntlet", "Mode: [select] only\nShould the bot buy \"Scholar's DC Gauntlet\" ?", false),
        new Option<bool>("85893", "Shockwave Paralyzer Bow", "Mode: [select] only\nShould the bot buy \"Shockwave Paralyzer Bow\" ?", false),
        new Option<bool>("67389", "Halcyon Skye Longsword", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Longsword\" ?", false),
        new Option<bool>("67390", "Halcyon Skye Longswords", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Longswords\" ?", false),
        new Option<bool>("67395", "Halcyon Skye Hand Axe", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Hand Axe\" ?", false),
        new Option<bool>("67396", "Halcyon Skye Hand Axes", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Hand Axes\" ?", false),
        new Option<bool>("86005", "Scholar's AC Gauntlets", "Mode: [select] only\nShould the bot buy \"Scholar's AC Gauntlets\" ?", false),
        new Option<bool>("86006", "Scholar's DC Gauntlets", "Mode: [select] only\nShould the bot buy \"Scholar's DC Gauntlets\" ?", false),
    };
}
