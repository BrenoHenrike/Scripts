/*
name: Valarian's Merge
description: This will get all or selected items on this merge shop.
tags: valarians-merge, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ValariansMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private CoreFriday13th F13 = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Zenobia’s Moglinberry Juice" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!F13.Friday13thCheck("Valarians Merge"))
            return;

        F13.BlackMaze();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("blackmaze", 2218, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Zenobia’s Moglinberry Juice":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9056);
                    //9056 | Magic Dance
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("blackmaze", "Globlin", "Globlin Wings", 7);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("blackmaze", "Vi'eel Dreaddacovra", "White Scale");
                        Core.HuntMonster("blackmaze", "Shadow Fernando", "Purple Flame");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("75022", "The Harvester", "Mode: [select] only\nShould the bot buy \"The Harvester\" ?", false),
        new Option<bool>("75023", "The Harvester's Drill Morph", "Mode: [select] only\nShould the bot buy \"The Harvester's Drill Morph\" ?", false),
        new Option<bool>("75024", "The Harvester's Grin", "Mode: [select] only\nShould the bot buy \"The Harvester's Grin\" ?", false),
        new Option<bool>("75026", "The Harvester's Grin + Locks", "Mode: [select] only\nShould the bot buy \"The Harvester's Grin + Locks\" ?", false),
        new Option<bool>("75034", "Zenobia's BerryStain Splash", "Mode: [select] only\nShould the bot buy \"Zenobia's BerryStain Splash\" ?", false),
        new Option<bool>("75035", "Zenobia's BerryStain Blade", "Mode: [select] only\nShould the bot buy \"Zenobia's BerryStain Blade\" ?", false),
        new Option<bool>("75036", "Zenobia's BerryStain Blades", "Mode: [select] only\nShould the bot buy \"Zenobia's BerryStain Blades\" ?", false),
        new Option<bool>("75037", "The Harvester's Katana", "Mode: [select] only\nShould the bot buy \"The Harvester's Katana\" ?", false),
        new Option<bool>("75038", "The Harvester's Katanas", "Mode: [select] only\nShould the bot buy \"The Harvester's Katanas\" ?", false),
        new Option<bool>("75041", "The Harvester's Gauntlet", "Mode: [select] only\nShould the bot buy \"The Harvester's Gauntlet\" ?", false),
        new Option<bool>("75042", "The Harvester's XL Gauntlets", "Mode: [select] only\nShould the bot buy \"The Harvester's XL Gauntlets\" ?", false),
        new Option<bool>("75155", "Goblin King's Formalwear", "Mode: [select] only\nShould the bot buy \"Goblin King's Formalwear\" ?", false),
        new Option<bool>("75156", "Goblin King's Mirrored Outfit", "Mode: [select] only\nShould the bot buy \"Goblin King's Mirrored Outfit\" ?", false),
        new Option<bool>("75159", "Goblin King's Spikes + Hat", "Mode: [select] only\nShould the bot buy \"Goblin King's Spikes + Hat\" ?", false),
        new Option<bool>("75160", "Goblin Queen's Locks + Hat", "Mode: [select] only\nShould the bot buy \"Goblin Queen's Locks + Hat\" ?", false),
        new Option<bool>("75162", "Li'l Dewdo Pet", "Mode: [select] only\nShould the bot buy \"Li'l Dewdo Pet\" ?", false),
        new Option<bool>("75166", "Mystical Glass Orb Wand", "Mode: [select] only\nShould the bot buy \"Mystical Glass Orb Wand\" ?", false),
    };
}
