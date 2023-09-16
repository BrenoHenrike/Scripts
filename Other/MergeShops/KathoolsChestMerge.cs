/*
name: Kathool's Chest Merge
description: This bot will farm the items belonging to the selected mode for the Kathools Chest Merge [2322] in /kathooldepths
tags: kathool, chest, merge, kathooldepths, adeptus, kathooli, morph, sacrosanct, tendril, fins, barbarous, mindsmasher, mindsmashers, apex, leviathan, domination, spear, spears, thyllian
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/AgeofRuin/CoreAOR.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
//cs_include Scripts/Other\MergeShops\UndineCommissaryMerge.cs
//cs_include Scripts/Other\MergeShops\SeaviewSouvenirsMerge.cs
//cs_include Scripts/Other\MergeShops\TwilightZoneMerge.cs
//cs_include Scripts/Other\MergeShops\AbyssalZoneMerge.cs
//cs_include Scripts/Other\MergeShops\TrenchObserveMerge.cs
//cs_include Scripts/Other\MergeShops\SeaVoiceMerge.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class KathoolsChestMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private UndineCommissaryMerge UCM = new();
    private SeaviewSouvenirsMerge SSM = new();
    private TwilightZoneMerge TZM = new();
    private AbyssalZoneMerge AZM = new();
    private TrenchObserveMerge TOM = new();
    private SeaVoiceMerge SVM = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Remnant of the Deep", "Ashray Villager", "Undine Defence Director", "Evacuation Protocol Suit", "Ashray Elf Warden", "DeepWater Drow", "Midnight Glaucus Sage", "Kathool Acolyte", "Adeptus Relic", "Adeptus Kathooli Hair", "Adeptus Kathooli Locks", "MindSmasher Blade", "MindSmasher Blades", "Psychic Domination Spear", "Psychic Domination Spears" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("kathooldepths", 2322, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Remnant of the Deep":
                case "Adeptus Relic":
                case "Adeptus Kathooli Hair":
                case "Adeptus Kathooli Locks":
                case "MindSmasher Blade":
                case "MindSmasher Blades":
                case "Psychic Domination Spear":
                case "Psychic Domination Spears":
                    Core.Logger($"{req.Name}" + " requires ultra boss, you need to prefarm it yourself.");
                    break;

                case "Ashray Villager":
                    SSM.BuyAllMerge(req.Name);
                    break;

                case "Undine Defence Director":
                    UCM.BuyAllMerge(req.Name);
                    break;

                case "Evacuation Protocol Suit":
                    TZM.BuyAllMerge(req.Name);
                    break;

                case "Ashray Elf Warden":
                    AZM.BuyAllMerge(req.Name);
                    break;

                case "DeepWater Drow":
                    TOM.BuyAllMerge(req.Name);
                    break;

                case "Midnight Glaucus Sage":
                    SVM.BuyAllMerge(req.Name);
                    break;

                case "Kathool Acolyte":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("deepchaos", "Kathool", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("78153", "Adeptus Kathooli", "Mode: [select] only\nShould the bot buy \"Adeptus Kathooli\" ?", false),
        new Option<bool>("78156", "Adeptus Kathooli Morph", "Mode: [select] only\nShould the bot buy \"Adeptus Kathooli Morph\" ?", false),
        new Option<bool>("78157", "Adeptus Kathooli Visage", "Mode: [select] only\nShould the bot buy \"Adeptus Kathooli Visage\" ?", false),
        new Option<bool>("78158", "Sacrosanct Tendril Morph", "Mode: [select] only\nShould the bot buy \"Sacrosanct Tendril Morph\" ?", false),
        new Option<bool>("78159", "Sacrosanct Tendril Visage", "Mode: [select] only\nShould the bot buy \"Sacrosanct Tendril Visage\" ?", false),
        new Option<bool>("78160", "Adeptus Kathooli Hood", "Mode: [select] only\nShould the bot buy \"Adeptus Kathooli Hood\" ?", false),
        new Option<bool>("78161", "Adeptus Kathooli Fins", "Mode: [select] only\nShould the bot buy \"Adeptus Kathooli Fins\" ?", false),
        new Option<bool>("78164", "Barbarous MindSmasher", "Mode: [select] only\nShould the bot buy \"Barbarous MindSmasher\" ?", false),
        new Option<bool>("78165", "Barbarous MindSmashers", "Mode: [select] only\nShould the bot buy \"Barbarous MindSmashers\" ?", false),
        new Option<bool>("78166", "Apex MindSmasher Blade", "Mode: [select] only\nShould the bot buy \"Apex MindSmasher Blade\" ?", false),
        new Option<bool>("78167", "Apex MindSmasher Blades", "Mode: [select] only\nShould the bot buy \"Apex MindSmasher Blades\" ?", false),
        new Option<bool>("78170", "Leviathan Domination Spear", "Mode: [select] only\nShould the bot buy \"Leviathan Domination Spear\" ?", false),
        new Option<bool>("78171", "Leviathan Domination Spears", "Mode: [select] only\nShould the bot buy \"Leviathan Domination Spears\" ?", false),
        new Option<bool>("78172", "Thyllian Domination Spear", "Mode: [select] only\nShould the bot buy \"Thyllian Domination Spear\" ?", false),
        new Option<bool>("78173", "Thyllian Domination Spears", "Mode: [select] only\nShould the bot buy \"Thyllian Domination Spears\" ?", false),
    };
}
