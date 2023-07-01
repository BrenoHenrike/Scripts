/*
name: Sea Base House Merge
description: This bot will farm the items belonging to the selected mode for the Sea Base House Merge [2289] in /twilightzone
tags: sea, base, house, merge, twilightzone, stern, song, guest, taras, temporary, rest, disgruntled, mi, undine, office, desk, ominous, hero, hydration, station, server, rack, ergonomic, engineer, setup, basic, infirmary, bed, flowering, shrub, table, golden, bouquet, medical, monitor, trio, songs, seaside, portrait, sunlight, zone, workstation, chair, seabase, soft, reading, light, blue, ray, bookshelf, corner, fuel, vat, sleek, midnight, memorial, stones, occupied, experimentation, conference, couch, porthole, window, windows, slim
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
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Sun Zone Chit", "Undine Visitor Badge", "Undine Base Scrip", "Sundered Tentacle", "Leviathan Scale", "Water Elf Pearl", "Undine Coffee Table", "Scattered Bones", "Experimentation Chair", "Sleeping Monitor"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AoR.TwilightZone();
        AoR.MidnightZone();
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

                case "Water Elf Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9302);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("midnightzone", "Shadow Viscera", "Fleshy Shadows", 8, log: false);
                        Core.HuntMonster("midnightzone", "Venerated Wraith", "Wraith Memento", 8, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("midnightzone", "Sparagmos", "Memory Card", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Undine Coffee Table":
                case "Sleeping Monitor":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("midnightzone", "Sparagmos", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    break;

                case "Scattered Bones":
                case "Experimentation Chair":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("midnightzone", "Undead Prisoner", req.Name, quant, false, false);
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
        new Option<bool>("78312", "Sunlight Zone Workstation", "Mode: [select] only\nShould the bot buy \"Sunlight Zone Workstation\" ?", false),
        new Option<bool>("78311", "Sunlight Zone Office Desk", "Mode: [select] only\nShould the bot buy \"Sunlight Zone Office Desk\" ?", false),
        new Option<bool>("78313", "Sunlight Zone Ergonomic Chair", "Mode: [select] only\nShould the bot buy \"Sunlight Zone Ergonomic Chair\" ?", false),
        new Option<bool>("78245", "Ergonomic Engineer Chair", "Mode: [select] only\nShould the bot buy \"Ergonomic Engineer Chair\" ?", false),
        new Option<bool>("78668", "Seabase Undine", "Mode: [select] only\nShould the bot buy \"Seabase Undine\" ?", false),
        new Option<bool>("78684", "Soft Reading Light", "Mode: [select] only\nShould the bot buy \"Soft Reading Light\" ?", false),
        new Option<bool>("78683", "Blue Ray Light", "Mode: [select] only\nShould the bot buy \"Blue Ray Light\" ?", false),
        new Option<bool>("78679", "Bookshelf Corner", "Mode: [select] only\nShould the bot buy \"Bookshelf Corner\" ?", false),
        new Option<bool>("78678", "Undine Fuel Vat", "Mode: [select] only\nShould the bot buy \"Undine Fuel Vat\" ?", false),
        new Option<bool>("78677", "Sleek Table Monitor", "Mode: [select] only\nShould the bot buy \"Sleek Table Monitor\" ?", false),
        new Option<bool>("78676", "Midnight Memorial Desk", "Mode: [select] only\nShould the bot buy \"Midnight Memorial Desk\" ?", false),
        new Option<bool>("78674", "Undine Stones", "Mode: [select] only\nShould the bot buy \"Undine Stones\" ?", false),
        new Option<bool>("78671", "Occupied Experimentation Chair", "Mode: [select] only\nShould the bot buy \"Occupied Experimentation Chair\" ?", false),
        new Option<bool>("78669", "Undine Conference Couch", "Mode: [select] only\nShould the bot buy \"Undine Conference Couch\" ?", false),
        new Option<bool>("78692", "Seabase Undine Monitor", "Mode: [select] only\nShould the bot buy \"Seabase Undine Monitor\" ?", false),
        new Option<bool>("78691", "Undine Porthole Window", "Mode: [select] only\nShould the bot buy \"Undine Porthole Window\" ?", false),
        new Option<bool>("78690", "Undine Dual Windows", "Mode: [select] only\nShould the bot buy \"Undine Dual Windows\" ?", false),
        new Option<bool>("78689", "Slim Undine Window", "Mode: [select] only\nShould the bot buy \"Slim Undine Window\" ?", false),
        new Option<bool>("78688", "Seabase Undine Window", "Mode: [select] only\nShould the bot buy \"Seabase Undine Window\" ?", false),
    };
}
