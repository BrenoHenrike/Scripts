/*
name: Sea Base House Merge
description: This bot will farm the items belonging to the selected mode for the Sea Base House Merge [2289] in /twilightzone
tags: sea, base, house, merge, twilightzone, stern, song, guest, taras, temporary, rest, disgruntled, mi, undine, office, desk, ominous, hero, hydration, station, server, rack, ergonomic, engineer, setup, basic, infirmary, bed, flowering, shrub, table, golden, bouquet, medical, monitor, trio, songs, seaside, portrait
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SeaBaseHouseMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AoR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Sun Zone Chit", "Undine Visitor Badge", "Undine Base Scrip", "Sundered Tentacle", "Leviathan Scale"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AoR.TwilightZone();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("twilightzone", 2289, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Sun Zone Chit":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9252);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("sunlightzone", "Marine Snow", "Marine Sample", log:false);
                        Core.HuntMonster("sunlightzone", "Infernal Illusion", "Infernal Sample", 10, log:false);
                        Core.HuntMonster("sunlightzone", "Seraphic Illusion", "Seraphic Sample", 10, log:false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Undine Visitor Badge":
                    Core.FarmingLogger(req.Name, quant);
                        Core.HuntMonster("sunlightzone", "Astravian Illusion", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    break;

                case "Undine Base Scrip":
                    Core.FarmingLogger(req.Name, quant);
                        Core.HuntMonster("sunlightzone", "Marine Snow", req.Name, quant, false, false);
                    break;

                case "Sundered Tentacle":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9269);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("twilightzone", "Leviathan", "Leviathan Tentacle", 1, true, false);
                        Core.HuntMonster("twilightzone", "Decay Spirit", "Decay Essence", 8, true, false);
                        Core.HuntMonster("twilightzone", "Ice Guardian", "Tarnished Icicle", 8, true, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Leviathan Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.Logger("Better to use alts to farm it faster.");
                        Core.HuntMonster("twilightzone", "Leviathan", "Leviathan Scale", quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77955", "Stern Song House Guest", "Mode: [select] only\nShould the bot buy \"Stern Song House Guest\" ?", false),
        new Option<bool>("77993", "Tara's Temporary Rest Guest", "Mode: [select] only\nShould the bot buy \"Tara's Temporary Rest Guest\" ?", false),
        new Option<bool>("77996", "Disgruntled Mi Guest", "Mode: [select] only\nShould the bot buy \"Disgruntled Mi Guest\" ?", false),
        new Option<bool>("77997", "Disgruntled Song Guest", "Mode: [select] only\nShould the bot buy \"Disgruntled Song Guest\" ?", false),
        new Option<bool>("78248", "Undine Office Desk", "Mode: [select] only\nShould the bot buy \"Undine Office Desk\" ?", false),
        new Option<bool>("78249", "Ominous Undine Desk", "Mode: [select] only\nShould the bot buy \"Ominous Undine Desk\" ?", false),
        new Option<bool>("78247", "Hero Hydration Station", "Mode: [select] only\nShould the bot buy \"Hero Hydration Station\" ?", false),
        new Option<bool>("78250", "Sea Base Server Rack", "Mode: [select] only\nShould the bot buy \"Sea Base Server Rack\" ?", false),
        new Option<bool>("78246", "Ergonomic Engineer Setup", "Mode: [select] only\nShould the bot buy \"Ergonomic Engineer Setup\" ?", false),
        new Option<bool>("78244", "Basic Infirmary Bed", "Mode: [select] only\nShould the bot buy \"Basic Infirmary Bed\" ?", false),
        new Option<bool>("78254", "Flowering Shrub Table", "Mode: [select] only\nShould the bot buy \"Flowering Shrub Table\" ?", false),
        new Option<bool>("78255", "Golden Bouquet Table", "Mode: [select] only\nShould the bot buy \"Golden Bouquet Table\" ?", false),
        new Option<bool>("78242", "Medical Monitor", "Mode: [select] only\nShould the bot buy \"Medical Monitor\" ?", false),
        new Option<bool>("78243", "Medical Monitor Trio", "Mode: [select] only\nShould the bot buy \"Medical Monitor Trio\" ?", false),
        new Option<bool>("78241", "Song's Seaside Portrait", "Mode: [select] only\nShould the bot buy \"Song's Seaside Portrait\" ?", false),
    };
}
