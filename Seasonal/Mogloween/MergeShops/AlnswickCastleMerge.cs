/*
name: Alnswick Castle Merge
description: This bot will farm the items belonging to the selected mode for the Alnswick Castle Merge [2360] in /cursedcastle
tags: alnswick, castle, merge, cursedcastle, vampiric, gentlefolk, red, eyed, aristohat, morph, stache, aristocap, dapper, bloodshot, crimson, vampire, wings, winged, cloak, wind, soul, starved, jiangshi, cap, talisman, sedge, bandaged, glare, sealed, coffin, haunted, tuanshan, jiangshis, weaponry, claw, claws
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AlnswickCastleMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreMogloween CoreMogloween = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Candy Dragon Egg", "Jiangshi", "Jiangshi Hair", "Jiangshi Locks", "Jiangshi Hat", "Jiangshi Cap", "Jiangshi Talisman Hair", "Jiangshi Talisman Locks", "Jiangshi Bandages" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("cursedcastle"))
            return;

        CoreMogloween.CursedCastle();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("cursedcastle", 2360, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Candy Dragon Egg":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    // Host of Honor 9455
                    Core.RegisterQuests(9455);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("cursedcastle", "Luminous Fungus", "iGrilled Shroom Capstem", 6);
                        Core.HuntMonster("cursedcastle", "Noble Gargoyle", "Decorated Gargoyle", 6);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("cursedcastle", "Unborn Brood Defeated ", "Unborn Brood");

                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Jiangshi":
                case "Jiangshi Hair":
                case "Jiangshi Cap":
                case "Jiangshi Locks":
                case "Jiangshi Bandages":
                case "Jiangshi Talisman Locks":
                case "Jiangshi Talisman Hair":
                case "Jiangshi Hat":
                    Core.HuntMonster("cursedcastle", "Noble Ghost", req.Name, quant, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("81206", "Vampiric Gentlefolk", "Mode: [select] only\nShould the bot buy \"Vampiric Gentlefolk\" ?", false),
        new Option<bool>("81209", "Vampiric Red Eyed Aristohat Morph", "Mode: [select] only\nShould the bot buy \"Vampiric Red Eyed Aristohat Morph\" ?", false),
        new Option<bool>("81210", "Vampiric Aristohat Stache Morph", "Mode: [select] only\nShould the bot buy \"Vampiric Aristohat Stache Morph\" ?", false),
        new Option<bool>("81211", "Vampiric Aristohat Morph", "Mode: [select] only\nShould the bot buy \"Vampiric Aristohat Morph\" ?", false),
        new Option<bool>("81212", "Vampiric Aristocap Visage", "Mode: [select] only\nShould the bot buy \"Vampiric Aristocap Visage\" ?", false),
        new Option<bool>("81213", "Vampiric Red Eyed Aristocap Visage", "Mode: [select] only\nShould the bot buy \"Vampiric Red Eyed Aristocap Visage\" ?", false),
        new Option<bool>("81214", "Dapper Vampiric Aristocap Visage", "Mode: [select] only\nShould the bot buy \"Dapper Vampiric Aristocap Visage\" ?", false),
        new Option<bool>("81215", "Vampiric Bloodshot Aristocap Visage", "Mode: [select] only\nShould the bot buy \"Vampiric Bloodshot Aristocap Visage\" ?", false),
        new Option<bool>("81216", "Vampiric Aristocap", "Mode: [select] only\nShould the bot buy \"Vampiric Aristocap\" ?", false),
        new Option<bool>("81217", "Dapper Vampiric Aristohat Morph", "Mode: [select] only\nShould the bot buy \"Dapper Vampiric Aristohat Morph\" ?", false),
        new Option<bool>("81219", "Crimson Vampire Wings", "Mode: [select] only\nShould the bot buy \"Crimson Vampire Wings\" ?", false),
        new Option<bool>("81220", "Crimson Winged Cloak", "Mode: [select] only\nShould the bot buy \"Crimson Winged Cloak\" ?", false),
        new Option<bool>("81226", "Staff of Crimson Wind", "Mode: [select] only\nShould the bot buy \"Staff of Crimson Wind\" ?", false),
        new Option<bool>("80452", "Soul Starved Jiangshi", "Mode: [select] only\nShould the bot buy \"Soul Starved Jiangshi\" ?", false),
        new Option<bool>("80455", "Jiangshi Morph", "Mode: [select] only\nShould the bot buy \"Jiangshi Morph\" ?", false),
        new Option<bool>("80456", "Jiangshi Visage", "Mode: [select] only\nShould the bot buy \"Jiangshi Visage\" ?", false),
        new Option<bool>("80459", "Jiangshi Hat Morph", "Mode: [select] only\nShould the bot buy \"Jiangshi Hat Morph\" ?", false),
        new Option<bool>("80460", "Jiangshi Cap Visage", "Mode: [select] only\nShould the bot buy \"Jiangshi Cap Visage\" ?", false),
        new Option<bool>("80463", "Jiangshi Talisman Morph", "Mode: [select] only\nShould the bot buy \"Jiangshi Talisman Morph\" ?", false),
        new Option<bool>("80464", "Jiangshi Talisman Visage", "Mode: [select] only\nShould the bot buy \"Jiangshi Talisman Visage\" ?", false),
        new Option<bool>("80465", "Jiangshi Sedge Hat", "Mode: [select] only\nShould the bot buy \"Jiangshi Sedge Hat\" ?", false),
        new Option<bool>("80467", "Jiangshi Bandaged Glare", "Mode: [select] only\nShould the bot buy \"Jiangshi Bandaged Glare\" ?", false),
        new Option<bool>("80468", "Talisman Sealed Coffin", "Mode: [select] only\nShould the bot buy \"Talisman Sealed Coffin\" ?", false),
        new Option<bool>("80476", "Haunted Tuanshan", "Mode: [select] only\nShould the bot buy \"Haunted Tuanshan\" ?", false),
        new Option<bool>("80477", "Jiangshi's Haunted Weaponry", "Mode: [select] only\nShould the bot buy \"Jiangshi's Haunted Weaponry\" ?", false),
        new Option<bool>("80478", "Jiangshi Claw", "Mode: [select] only\nShould the bot buy \"Jiangshi Claw\" ?", false),
        new Option<bool>("80479", "Jiangshi Claws", "Mode: [select] only\nShould the bot buy \"Jiangshi Claws\" ?", false),
    };
}
