/*
name: Eddens Treasures Merge
description: This bot will farm the items belonging to the selected mode for the Eddens Treasures Merge [2455] in /greed
tags: eddens, treasures, merge, greed, hollowborn, bodyguard, fists, claws, battle, companion, treasure, hunter, bag, necklace, necklaces, krenos, spirit, katanas
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EddensTreasuresMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreHollowbornStory HBStory = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Cursed Ring", "Frozen Diamond", "Krenos Spirit Katana", "Energy Dragon Scale" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        HBStory.Shadowrealm();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("greed", 2455, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Cursed Ring":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9794);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("greed", "Cursed Treasure", "Ring Found", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Frozen Diamond":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9795);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("greed", "Ice Crystal", "Frozen Diamond Found", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Krenos Spirit Katana":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9804);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("shimazu", "Shimazu", "First Rune", log: false);
                        Core.GetMapItem(13328, map: "evilmarsh");
                        Core.HuntMonster("seraphicwarlaken", "Rayce", "Third Rune", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("pyrewatch", "Firestorm Major", "Fourth Rune", log: false);
                        Core.GetMapItem(13329, map: "icewindpass");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Energy Dragon Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("thunderfang", "Tonitru", req.Name, quant, req.Temp, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57304", "Gold Voucher 25k", "Mode: [select] only\nShould the bot buy \"Gold Voucher 25k\" ?", false),
        new Option<bool>("61043", "Gold Voucher 500k", "Mode: [select] only\nShould the bot buy \"Gold Voucher 500k\" ?", false),
        new Option<bool>("86701", "Hollowborn Bodyguard", "Mode: [select] only\nShould the bot buy \"Hollowborn Bodyguard\" ?", false),
        new Option<bool>("86702", "Hollowborn Bodyguard Helm", "Mode: [select] only\nShould the bot buy \"Hollowborn Bodyguard Helm\" ?", false),
        new Option<bool>("86705", "Hollowborn Bodyguard Fists", "Mode: [select] only\nShould the bot buy \"Hollowborn Bodyguard Fists\" ?", false),
        new Option<bool>("86706", "Hollowborn Bodyguard Claws", "Mode: [select] only\nShould the bot buy \"Hollowborn Bodyguard Claws\" ?", false),
        new Option<bool>("86704", "Hollowborn Battle Bodyguard", "Mode: [select] only\nShould the bot buy \"Hollowborn Battle Bodyguard\" ?", false),
        new Option<bool>("86703", "Hollowborn Bodyguard Companion", "Mode: [select] only\nShould the bot buy \"Hollowborn Bodyguard Companion\" ?", false),
        new Option<bool>("86735", "Hollowborn Treasure Hunter", "Mode: [select] only\nShould the bot buy \"Hollowborn Treasure Hunter\" ?", false),
        new Option<bool>("86736", "Hollowborn Treasure Hunter Hat", "Mode: [select] only\nShould the bot buy \"Hollowborn Treasure Hunter Hat\" ?", false),
        new Option<bool>("86737", "Hollowborn Treasure Hunter Bag", "Mode: [select] only\nShould the bot buy \"Hollowborn Treasure Hunter Bag\" ?", false),
        new Option<bool>("86738", "Hollowborn Treasure Hunter Necklace", "Mode: [select] only\nShould the bot buy \"Hollowborn Treasure Hunter Necklace\" ?", false),
        new Option<bool>("86739", "Hollowborn Treasure Hunter Necklaces", "Mode: [select] only\nShould the bot buy \"Hollowborn Treasure Hunter Necklaces\" ?", false),
        new Option<bool>("67461", "Krenos Spirit Katanas", "Mode: [select] only\nShould the bot buy \"Krenos Spirit Katanas\" ?", false),
    };
}
