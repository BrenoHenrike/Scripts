/*
name: Murder Moon Merge
description: This bot will farm the items belonging to the selected mode for the Murder Moon Merge [1998] in /murdermoon
tags: murder, moon, merge, murdermoon, paladorian, apprentice, , antenna, paladu, paladurian, backbeam, riffle, jetpack, beam, rifle, dark, lord, lords, masked, underworld, cloak, laserblade, double, battle, droid, crimson, tempest, soldier, blaster, pet
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MurderMoonMergeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public MurderMoon Moon = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Cyber Crystal", "S Ring", "Fifth Lord’s Filtrinator", "Dark Helmet", "Dotty", "Dark Tempest Soldier", "Dark Tempest Soldier Helm", "Dark Tempest Soldier Jetpack", "Dark Tempest Soldier Blaster", "Dark Tempest Soldier Laserblade", "Dark Tempest Soldier Pet", "Dark Tempest Soldier Mask"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("murdermoon"))
            return;

        Moon.MurderMoonStory();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("murdermoon", 1998, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Cyber Crystal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8065);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("murdermoon", "Tempest Soldier", "Tempest Soldier Badge", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fifth Lord’s Filtrinator":
                case "S Ring":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("murdermoon", "Fifth Sepulchure", req.Name, quant, log: false);
                    break;

                case "Dotty":
                case "Dark Helmet":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("zorbaspalace", "Zorba the Bakk", req.Name, quant, log: false);
                    break;

                case "Dark Tempest Soldier":
                case "Dark Tempest Soldier Helm":
                case "Dark Tempest Soldier Jetpack":
                case "Dark Tempest Soldier Blaster":
                case "Dark Tempest Soldier Laserblade":
                case "Dark Tempest Soldier Pet":
                case "Dark Tempest Soldier Mask":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("murdermoon", "Tempest Soldier", req.Name, quant, log: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("60768", "Paladorian", "Mode: [select] only\nShould the bot buy \"Paladorian\" ?", false),
        new Option<bool>("60769", "Paladorian Apprentice Helm + Locks", "Mode: [select] only\nShould the bot buy \"Paladorian Apprentice Helm + Locks\" ?", false),
        new Option<bool>("60770", "Paladorian Apprentice Helm", "Mode: [select] only\nShould the bot buy \"Paladorian Apprentice Helm\" ?", false),
        new Option<bool>("60771", "Paladorian Helm + Antenna", "Mode: [select] only\nShould the bot buy \"Paladorian Helm + Antenna\" ?", false),
        new Option<bool>("60772", "Paladorian Helm", "Mode: [select] only\nShould the bot buy \"Paladorian Helm\" ?", false),
        new Option<bool>("60773", "PalaDu Apprentice Helm", "Mode: [select] only\nShould the bot buy \"PalaDu Apprentice Helm\" ?", false),
        new Option<bool>("60774", "PalaDurian Helm", "Mode: [select] only\nShould the bot buy \"PalaDurian Helm\" ?", false),
        new Option<bool>("60775", "Paladorian BackBeam Riffle", "Mode: [select] only\nShould the bot buy \"Paladorian BackBeam Riffle\" ?", false),
        new Option<bool>("60776", "Paladorian BackBeam Sword", "Mode: [select] only\nShould the bot buy \"Paladorian BackBeam Sword\" ?", false),
        new Option<bool>("60777", "Paladorian Cape", "Mode: [select] only\nShould the bot buy \"Paladorian Cape\" ?", false),
        new Option<bool>("60778", "Paladorian Jetpack", "Mode: [select] only\nShould the bot buy \"Paladorian Jetpack\" ?", false),
        new Option<bool>("60780", "Paladorian Beam Rifle", "Mode: [select] only\nShould the bot buy \"Paladorian Beam Rifle\" ?", false),
        new Option<bool>("61004", "Dark Lord", "Mode: [select] only\nShould the bot buy \"Dark Lord\" ?", false),
        new Option<bool>("60925", "Dark Lord Armor", "Mode: [select] only\nShould the bot buy \"Dark Lord Armor\" ?", false),
        new Option<bool>("60926", "Dark Lord's Hood + Hair", "Mode: [select] only\nShould the bot buy \"Dark Lord's Hood + Hair\" ?", false),
        new Option<bool>("60927", "Dark Lord's Hood", "Mode: [select] only\nShould the bot buy \"Dark Lord's Hood\" ?", false),
        new Option<bool>("60928", "Dark Lord's Masked Hood", "Mode: [select] only\nShould the bot buy \"Dark Lord's Masked Hood\" ?", false),
        new Option<bool>("60929", "Dark Lord's Underworld Hood", "Mode: [select] only\nShould the bot buy \"Dark Lord's Underworld Hood\" ?", false),
        new Option<bool>("60930", "Dark Lord's Cloak", "Mode: [select] only\nShould the bot buy \"Dark Lord's Cloak\" ?", false),
        new Option<bool>("60931", "Dark Lord's Laserblade", "Mode: [select] only\nShould the bot buy \"Dark Lord's Laserblade\" ?", false),
        new Option<bool>("60932", "Dark Lord's Double Blade", "Mode: [select] only\nShould the bot buy \"Dark Lord's Double Blade\" ?", false),
        new Option<bool>("60933", "Dark Lord's Battle Droid", "Mode: [select] only\nShould the bot buy \"Dark Lord's Battle Droid\" ?", false),
        new Option<bool>("60959", "Dark Lord's Droid", "Mode: [select] only\nShould the bot buy \"Dark Lord's Droid\" ?", false),
        new Option<bool>("60934", "Crimson Dark Lord Armor", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord Armor\" ?", false),
        new Option<bool>("60935", "Crimson Dark Lord's Hood + Hair", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Hood + Hair\" ?", false),
        new Option<bool>("60936", "Crimson Dark Lord's Hood", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Hood\" ?", false),
        new Option<bool>("60937", "Crimson Dark Lord's Masked Hood", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Masked Hood\" ?", false),
        new Option<bool>("60938", "Crimson Dark Lord's Underworld Hood", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Underworld Hood\" ?", false),
        new Option<bool>("60939", "Crimson Dark Lord's Cloak", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Cloak\" ?", false),
        new Option<bool>("60940", "Crimson Dark Lord's Laserblade", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Laserblade\" ?", false),
        new Option<bool>("60941", "Crimson Dark Lord's Double Blade", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Double Blade\" ?", false),
        new Option<bool>("60942", "Crimson Dark Lord's Dark Laserblade", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Dark Laserblade\" ?", false),
        new Option<bool>("60943", "Crimson Dark Lord's Battle Droid", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Battle Droid\" ?", false),
        new Option<bool>("60960", "Crimson Dark Lord's Droid", "Mode: [select] only\nShould the bot buy \"Crimson Dark Lord's Droid\" ?", false),
        new Option<bool>("60950", "Tempest Soldier", "Mode: [select] only\nShould the bot buy \"Tempest Soldier\" ?", false),
        new Option<bool>("60951", "Tempest Soldier Helm", "Mode: [select] only\nShould the bot buy \"Tempest Soldier Helm\" ?", false),
        new Option<bool>("60952", "Tempest Soldier Jetpack", "Mode: [select] only\nShould the bot buy \"Tempest Soldier Jetpack\" ?", false),
        new Option<bool>("60953", "Tempest Soldier Blaster", "Mode: [select] only\nShould the bot buy \"Tempest Soldier Blaster\" ?", false),
        new Option<bool>("60954", "Tempest Soldier Laserblade", "Mode: [select] only\nShould the bot buy \"Tempest Soldier Laserblade\" ?", false),
        new Option<bool>("60955", "Tempest Soldier Pet", "Mode: [select] only\nShould the bot buy \"Tempest Soldier Pet\" ?", false),
        new Option<bool>("60958", "Tempest Soldier Mask", "Mode: [select] only\nShould the bot buy \"Tempest Soldier Mask\" ?", false),
    };
}
