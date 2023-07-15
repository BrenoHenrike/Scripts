/*
name: Abyssal Zone Merge
description: This bot will farm the items belonging to the selected mode for the Abyssal Zone Merge [2304] in /abyssalzone
tags: abyssal, zone, merge, abyssalzone, enchanted, shark, diver, suit, , morph, ashray, elf, warden, top, knot, horns, coastal, corona, golden, trident, dancers, sea, streams
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
//cs_include Scripts/Story\AgeOfRuin\CoreAOR.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AbyssalZoneMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Undine Base Scrip", "Water Elf Antler", "Waves of Tumult" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.AbyssalZone();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("abyssalzone", 2304, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Undine Base Scrip":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("abyssalzone", "Kitefin Shark Bait", req.Name, quant, false, false);
                    break;

                case "Water Elf Antler":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9316);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("abyssalzone", "The Ashray", "Ashray Artifacts", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("abyssalzone", "Necro Adipocere", "Adipocere Antler", 3, log: false);
                        Core.HuntMonster("abyssalzone", "Foam Scavenger");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Waves of Tumult":
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

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("78029", "Enchanted Shark Diver", "Mode: [select] only\nShould the bot buy \"Enchanted Shark Diver\" ?", false),
        new Option<bool>("78030", "Enchanted Shark Suit", "Mode: [select] only\nShould the bot buy \"Enchanted Shark Suit\" ?", false),
        new Option<bool>("78031", "Enchanted Shark Hood", "Mode: [select] only\nShould the bot buy \"Enchanted Shark Hood\" ?", false),
        new Option<bool>("78032", "Enchanted Shark Hood + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Shark Hood + Locks\" ?", false),
        new Option<bool>("78033", "Enchanted Shark Hood Morph", "Mode: [select] only\nShould the bot buy \"Enchanted Shark Hood Morph\" ?", false),
        new Option<bool>("78034", "Enchanted Shark Hood Visage", "Mode: [select] only\nShould the bot buy \"Enchanted Shark Hood Visage\" ?", false),
        new Option<bool>("67431", "Ashray Elf Warden", "Mode: [select] only\nShould the bot buy \"Ashray Elf Warden\" ?", false),
        new Option<bool>("67432", "Ashray Elf Top Knot", "Mode: [select] only\nShould the bot buy \"Ashray Elf Top Knot\" ?", false),
        new Option<bool>("67433", "Ashray Elf Locks", "Mode: [select] only\nShould the bot buy \"Ashray Elf Locks\" ?", false),
        new Option<bool>("67434", "Ashray Top Knot and Horns", "Mode: [select] only\nShould the bot buy \"Ashray Top Knot and Horns\" ?", false),
        new Option<bool>("67435", "Ashray Locks and Horns", "Mode: [select] only\nShould the bot buy \"Ashray Locks and Horns\" ?", false),
        new Option<bool>("67436", "Coastal Corona", "Mode: [select] only\nShould the bot buy \"Coastal Corona\" ?", false),
        new Option<bool>("67438", "Golden Ashray Trident", "Mode: [select] only\nShould the bot buy \"Golden Ashray Trident\" ?", false),
        new Option<bool>("67439", "Dancer's Sea Streams", "Mode: [select] only\nShould the bot buy \"Dancer's Sea Streams\" ?", false),
    };
}
