/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/SilverExaltedPaladin.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class PaladinTreasureHunterMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public SEP SEP = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Silver Exalted Paladin", "Ancient Alloy", "Silver Exalted Winged Helm", "Silver Exalted Winged Visor", "Silver Exalted Helmet", "Silver Exalted Visor", "Silver Exalted Haloed Wings", "Silver Exalted Paladin Spear", "Silver Exalted Paladin Blade", "Silver Exalted Paladin Poleaxe", "Silver Exalted Paladin Axe", "Silver Exalted Spears of Light" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SEP.SilverExaltedPaladin();
        Farm.TreasureHunterREP(5);
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("lightguard", 1897, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Silver Exalted Paladin":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7586);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("warhorc", "General Drox", "Paladin Armor Found");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ancient Alloy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.AddDrop(req.Name);
                    Core.RegisterQuests(7587);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shadowvault", "Shadowstryke", "Alloy Materials", quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Silver Exalted Winged Visor":
                case "Silver Exalted Winged Helm":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7582);
                        Core.HuntMonster("frozentower", "FrostDeep Dweller", "Paladin Helmet Wings");
                        Core.EnsureComplete(7582, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
                case "Silver Exalted Visor":
                case "Silver Exalted Helmet":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7581);
                        Core.HuntMonster("ectocave", "Ichor Dracolich", "Sticky Paladin Helm");
                        Core.EnsureComplete(7581, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Silver Exalted Haloed Wings":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7583);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("thirdspell", "Great Solar Elemental", "Wings Found");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Silver Exalted Spears of Light":
                case "Silver Exalted Paladin Poleaxe":
                case "Silver Exalted Paladin Spear":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7584);
                        Core.HuntMonster("table", "Roach", "Paladin Polearm Found");
                        Core.EnsureComplete(7584, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Silver Exalted Paladin Axe":
                case "Silver Exalted Paladin Blade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7585);
                        Core.HuntMonster("dracocon", "Singer", "Paladin Weapon Found");
                        Core.EnsureComplete(7585, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("55454", "Gold Exalted Paladin", "Mode: [select] only\nShould the bot buy \"Gold Exalted Paladin\" ?", false),
        new Option<bool>("55455", "Gold Exalted Winged Helm", "Mode: [select] only\nShould the bot buy \"Gold Exalted Winged Helm\" ?", false),
        new Option<bool>("55456", "Gold Exalted Winged Visor", "Mode: [select] only\nShould the bot buy \"Gold Exalted Winged Visor\" ?", false),
        new Option<bool>("55457", "Gold Exalted Helmet", "Mode: [select] only\nShould the bot buy \"Gold Exalted Helmet\" ?", false),
        new Option<bool>("55458", "Gold Exalted Visor", "Mode: [select] only\nShould the bot buy \"Gold Exalted Visor\" ?", false),
        new Option<bool>("55459", "Gold Exalted Haloed Wings", "Mode: [select] only\nShould the bot buy \"Gold Exalted Haloed Wings\" ?", false),
        new Option<bool>("55460", "Exalted Paladin's Shroud", "Mode: [select] only\nShould the bot buy \"Exalted Paladin's Shroud\" ?", false),
        new Option<bool>("55461", "Gold Exalted Paladin Spear", "Mode: [select] only\nShould the bot buy \"Gold Exalted Paladin Spear\" ?", false),
        new Option<bool>("55462", "Gold Exalted Paladin Blade", "Mode: [select] only\nShould the bot buy \"Gold Exalted Paladin Blade\" ?", false),
        new Option<bool>("55463", "Gold Exalted Paladin Poleaxe", "Mode: [select] only\nShould the bot buy \"Gold Exalted Paladin Poleaxe\" ?", false),
        new Option<bool>("55464", "Gold Exalted Paladin Axe", "Mode: [select] only\nShould the bot buy \"Gold Exalted Paladin Axe\" ?", false),
        new Option<bool>("55528", "Gold Exalted Spears of Light", "Mode: [select] only\nShould the bot buy \"Gold Exalted Spears of Light\" ?", false),
    };
}
