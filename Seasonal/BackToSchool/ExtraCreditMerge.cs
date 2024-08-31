/*
name: Extra Credit Merge
description: This bot will farm the items belonging to the selected mode for the Extra Credit Merge [2158] in /extracredit
tags: extra, credit, merge, extracredit, ultra, fierce, outfit, super, cute, academic, attitude, curls, power, puffs, female, morph, becky, ben, beckys, bens, academy, jacket, jeans, autumn, days, violet, puffed, coat, grape, chill, package, fiercer, chewy, cookie, melty, crunchy, gourmet, amethyst, akibakei, headset, wings, havoc, hammer, hammers, crystallis, gymwear, trainer
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ExtraCreditMerge
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
        Core.BankingBlackList.AddRange(new[] { "Golden Apple", "Silver Ruler", "Plastic Toy", "Bronze Plaque", "Guava Sip", "Purple Paddlepop", "DogEar's Snack Serum", "Raw Cookie Dough Blade", "Crystallis Trainer Hair", "Crystallis Trainer Locks" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("extracredit"))
            return;
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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Golden Apple":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8793);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Dogear", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Silver Ruler":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8790);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Supply Locker", "Bookbag", log: false);
                        Core.HuntMonster("extracredit", "Supply Locker", "Pencil", 3, log: false);
                        Core.HuntMonster("extracredit", "Supply Locker", "Notebook", 3, log: false);
                        Core.HuntMonster("extracredit", "Grade A Bully", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Plastic Toy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8791);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Grade A Bully", "Bully Defeated", 5, log: false);
                        Core.HuntMonster("extracredit", "Meanest Girl", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bronze Plaque":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8792);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extracredit", "Videogame Console", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Guava Sip":
                case "Purple Paddlepop":
                case "Crystallis Trainer Locks":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("extracredit", "Meanest Girl", req.Name, quant, false, false);
                    break;

                case "DogEar's Snack Serum":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("extracredit", "Dogear", req.Name, quant, false, false);
                    break;

                case "Raw Cookie Dough Blade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.GetMapItem(11646, quant, "oaklore");
                    break;

                case "Crystallis Trainer Hair":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("extracredit", "Grade A Bully", req.Name, quant, false, false);
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
        new Option<bool>("79458", "Violet Puffed Coat", "Mode: [select] only\nShould the bot buy \"Violet Puffed Coat\" ?", false),
        new Option<bool>("79461", "Grape Chill Package", "Mode: [select] only\nShould the bot buy \"Grape Chill Package\" ?", false),
        new Option<bool>("71688", "Ultra Fiercer Outfit", "Mode: [select] only\nShould the bot buy \"Ultra Fiercer Outfit\" ?", false),
        new Option<bool>("28001", "Chewy Cookie Sword", "Mode: [select] only\nShould the bot buy \"Chewy Cookie Sword\" ?", false),
        new Option<bool>("28002", "Melty Cookie Sword", "Mode: [select] only\nShould the bot buy \"Melty Cookie Sword\" ?", false),
        new Option<bool>("28003", "Crunchy Cookie Sword", "Mode: [select] only\nShould the bot buy \"Crunchy Cookie Sword\" ?", false),
        new Option<bool>("28004", "Gourmet Cookie Sword", "Mode: [select] only\nShould the bot buy \"Gourmet Cookie Sword\" ?", false),
        new Option<bool>("86980", "Amethyst Akiba-kei Outfit", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Outfit\" ?", false),
        new Option<bool>("86985", "Amethyst Akiba-kei Headset Hair", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Headset Hair\" ?", false),
        new Option<bool>("86986", "Amethyst Akiba-kei Headset Locks", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Headset Locks\" ?", false),
        new Option<bool>("86987", "Amethyst Akiba-kei Morph", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Morph\" ?", false),
        new Option<bool>("86988", "Amethyst Akiba-kei Visage", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Visage\" ?", false),
        new Option<bool>("86991", "Amethyst Akiba-kei Headset Morph", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Headset Morph\" ?", false),
        new Option<bool>("86992", "Amethyst Akiba-kei Headset Visage", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Headset Visage\" ?", false),
        new Option<bool>("86994", "Amethyst Akiba-kei Aura Wings", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Aura Wings\" ?", false),
        new Option<bool>("87000", "Amethyst Akiba-kei Havoc Hammer", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Havoc Hammer\" ?", false),
        new Option<bool>("87001", "Amethyst Akiba-kei Havoc Hammers", "Mode: [select] only\nShould the bot buy \"Amethyst Akiba-kei Havoc Hammers\" ?", false),
        new Option<bool>("83700", "Crystallis Gymwear", "Mode: [select] only\nShould the bot buy \"Crystallis Gymwear\" ?", false),
        new Option<bool>("83703", "Crystallis Trainer Morph", "Mode: [select] only\nShould the bot buy \"Crystallis Trainer Morph\" ?", false),
        new Option<bool>("83704", "Crystallis Trainer Visage", "Mode: [select] only\nShould the bot buy \"Crystallis Trainer Visage\" ?", false),
    };
}
