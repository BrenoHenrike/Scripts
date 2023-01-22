/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ExtraCreditMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("extracredit", 2158, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Golden Apple":
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8793);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Dogear", "Exam Passed");
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Silver Ruler":
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8790);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Supply Locker", "Bookbag");
                        Core.HuntMonster("extracredit", "Supply Locker", "Pencil", 3);
                        Core.HuntMonster("extracredit", "Supply Locker", "Notebook", 3);
                        Core.HuntMonster("extracredit", "Grade A Bully", "Stolen Book", 5);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Plastic Toy":
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8791);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Grade A Bully", "Bully Defeated", 5);
                        Core.HuntMonster("extracredit", "Meanest Girl", "Plastic Crushed", 5);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bronze Plaque":
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8792);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Videogame Console", "Console Bricked", 5);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("61620", "Ultra Fierce Outfit", "Mode: [select] only\nShould the bot buy \"Ultra Fierce Outfit\" ?", false),
        new Option<bool>("61622", "Super Cute Hair", "Mode: [select] only\nShould the bot buy \"Super Cute Hair\" ?", false),
        new Option<bool>("61627", "Ultra Fierce Locks", "Mode: [select] only\nShould the bot buy \"Ultra Fierce Locks\" ?", false),
        new Option<bool>("61629", "Academic Attitude", "Mode: [select] only\nShould the bot buy \"Academic Attitude\" ?", false),
        new Option<bool>("61630", "Curls of Power", "Mode: [select] only\nShould the bot buy \"Curls of Power\" ?", false),
        new Option<bool>("61631", "Power Puffs Locks", "Mode: [select] only\nShould the bot buy \"Power Puffs Locks\" ?", false),
        new Option<bool>("61632", "Power Puffs Female Morph", "Mode: [select] only\nShould the bot buy \"Power Puffs Female Morph\" ?", false),
        new Option<bool>("61633", "Curls of Power Morph", "Mode: [select] only\nShould the bot buy \"Curls of Power Morph\" ?", false),
        new Option<bool>("61634", "Becky", "Mode: [select] only\nShould the bot buy \"Becky\" ?", false),
        new Option<bool>("61635", "Ben", "Mode: [select] only\nShould the bot buy \"Ben\" ?", false),
        new Option<bool>("61636", "Becky's Morph", "Mode: [select] only\nShould the bot buy \"Becky's Morph\" ?", false),
        new Option<bool>("61637", "Ben's Morph", "Mode: [select] only\nShould the bot buy \"Ben's Morph\" ?", false),
        new Option<bool>("61882", "Academy Jacket Outfit", "Mode: [select] only\nShould the bot buy \"Academy Jacket Outfit\" ?", false),
        new Option<bool>("61884", "Academy Jeans Outfit", "Mode: [select] only\nShould the bot buy \"Academy Jeans Outfit\" ?", false),
        new Option<bool>("62185", "Autumn Days Outfit", "Mode: [select] only\nShould the bot buy \"Autumn Days Outfit\" ?", false),
        new Option<bool>("62186", "Autumn Days Hat", "Mode: [select] only\nShould the bot buy \"Autumn Days Hat\" ?", false),
        new Option<bool>("62187", "Autumn Days Hat and Locks", "Mode: [select] only\nShould the bot buy \"Autumn Days Hat and Locks\" ?", false),
    };
}
