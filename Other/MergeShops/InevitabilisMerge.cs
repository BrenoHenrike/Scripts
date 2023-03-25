/*
name: Inevitabilis Merge
description: Farms the required materials and buys the selected item/mode from the merge shop in /timeinn
tags: timeinn, merge, eternal scale, shop
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class InevitabilisMerge
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
        Core.BankingBlackList.AddRange(new[] { "Eternal Scale"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("timeinn", 2249, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Eternal Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9175);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                    Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("venomvaults", "Manticore", "Manticore Stinger", 3);
                    Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("worldscore", "Elemental Attempt", "Attempt's Essence", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73247", $"Warrior of Time", "Mode: [select] only\nShould the bot buy \"Warrior of Time\" ?", false),
        new Option<bool>("73248", $"Warrior of Time’s Helm", "Mode: [select] only\nShould the bot buy \"Warrior of Time’s Helm\" ?", false),
        new Option<bool>("73249", $"Warrior of Time’s Wrap", "Mode: [select] only\nShould the bot buy \"Warrior of Time’s Wrap\" ?", false),
        new Option<bool>("73250", $"Warrior of Time’s Wrap + Blade", "Mode: [select] only\nShould the bot buy \"Warrior of Time’s Wrap + Blade\" ?", false),
        new Option<bool>("73251", $"Warrior of Time's Blade Cape", "Mode: [select] only\nShould the bot buy \"Warrior of Time's Blade Cape\" ?", false),
        new Option<bool>("73253", $"Rogue of Time", "Mode: [select] only\nShould the bot buy \"Rogue of Time\" ?", false),
        new Option<bool>("73254", $"Rogue of Time’s Mask", "Mode: [select] only\nShould the bot buy \"Rogue of Time’s Mask\" ?", false),
        new Option<bool>("73255", $"Rogue of Time’s Hair", "Mode: [select] only\nShould the bot buy \"Rogue of Time’s Hair\" ?", false),
        new Option<bool>("73256", $"Rogue of Time’s Quiver", "Mode: [select] only\nShould the bot buy \"Rogue of Time’s Quiver\" ?", false),
        new Option<bool>("73257", $"Rogue of Time’s Battle Gear", "Mode: [select] only\nShould the bot buy \"Rogue of Time’s Battle Gear\" ?", false),
        new Option<bool>("73258", $"Rogue of Time’s Dirk", "Mode: [select] only\nShould the bot buy \"Rogue of Time’s Dirk\" ?", false),
        new Option<bool>("73259", $"Rogue of Time’s Daggers", "Mode: [select] only\nShould the bot buy \"Rogue of Time’s Daggers\" ?", false),
        new Option<bool>("73260", $"Rogue of Time’s Bow", "Mode: [select] only\nShould the bot buy \"Rogue of Time’s Bow\" ?", false),
        new Option<bool>("73261", $"Mage of Time", "Mode: [select] only\nShould the bot buy \"Mage of Time\" ?", false),
        new Option<bool>("73262", $"Mage of Time’s Hat", "Mode: [select] only\nShould the bot buy \"Mage of Time’s Hat\" ?", false),
        new Option<bool>("73263", $"Mage of Time’s Tome Cape", "Mode: [select] only\nShould the bot buy \"Mage of Time’s Tome Cape\" ?", false),
        new Option<bool>("73264", $"Mage of Time’s Temporal Rune", "Mode: [select] only\nShould the bot buy \"Mage of Time’s Temporal Rune\" ?", false),
        new Option<bool>("73265", $"Mage of Time’s Tome Pet", "Mode: [select] only\nShould the bot buy \"Mage of Time’s Tome Pet\" ?", false),
        new Option<bool>("73267", $"Mage of Time’s Staff", "Mode: [select] only\nShould the bot buy \"Mage of Time’s Staff\" ?", false),
        new Option<bool>("73268", $"Healer of Time", "Mode: [select] only\nShould the bot buy \"Healer of Time\" ?", false),
        new Option<bool>("73269", $"Healer of Time's Hood", "Mode: [select] only\nShould the bot buy \"Healer of Time's Hood\" ?", false),
        new Option<bool>("73270", $"Healer of Time's Hourglass Cape", "Mode: [select] only\nShould the bot buy \"Healer of Time's Hourglass Cape\" ?", false),
        new Option<bool>("73271", $"Healer of Time's Staff", "Mode: [select] only\nShould the bot buy \"Healer of Time's Staff\" ?", false),
        new Option<bool>("73272", $"Healer of Time's Compendium", "Mode: [select] only\nShould the bot buy \"Healer of Time's Compendium\" ?", false),
        new Option<bool>("73273", $"Healer of Time's Battle Gear", "Mode: [select] only\nShould the bot buy \"Healer of Time's Battle Gear\" ?", false),
        new Option<bool>("77143", $"Warrior of Time’s Blades", "Mode: [select] only\nShould the bot buy \"Warrior of Time’s Blades\" ?", false),
        new Option<bool>("73252", $"Warrior of Time’s Blade", "Mode: [select] only\nShould the bot buy \"Warrior of Time’s Blade\" ?", false),
    };
}
