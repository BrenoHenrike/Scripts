/*
name: Exaltia Merge
description: This bot will farm the items belonging to the selected mode for the Exaltia Merge [2010] in /timeinn
tags: exaltia, merge, timeinn, celestial, magia, thaumaturgus, alpha, omega, ultima, exalted, unity, penultima, apotheosis, apostate, duality, ascendant, enlightened, vision, zodiac, hammer, scythe, tome, grand, riftwalker, horns, horned, battle, guard, anchored, portal, contemplative, portals, sentinel, spectral, density, cryptic, drone, battlepet
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ExaltiaMerge
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
        Core.BankingBlackList.AddRange(new[] { "Ezrajal Insignia", "Warden Insignia", "Engineer Insignia", "Exalted Node", "Exalted Relic Piece", "Exalted Artillery Shard", "Exalted Forgemetal", "Exalted Drone Pet" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("timeinn", 2010, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ezrajal Insignia":
                case "Warden Insignia":
                case "Engineer Insignia":
                case "Exalted Artillery Shard":
                case "Exalted Drone Pet":
                    Core.Logger($"{req.Name} needs to be farmed manually.");
                    break;

                case "Exalted Node":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("timeinn", "r3", "Bottom", "*", req.Name, quant, req.Temp, false);
                    break;

                case "Exalted Relic Piece":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("timeinn", "The Warden", req.Name, quant, req.Temp, false);
                    break;

                case "Exalted Forgemetal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("timeinn", "Ezrajal", req.Name, quant, req.Temp, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73342", "Celestial Magia", "Mode: [select] only\nShould the bot buy \"Celestial Magia\" ?", false),
        new Option<bool>("61918", "Thaumaturgus Alpha", "Mode: [select] only\nShould the bot buy \"Thaumaturgus Alpha\" ?", false),
        new Option<bool>("61919", "Thaumaturgus Omega", "Mode: [select] only\nShould the bot buy \"Thaumaturgus Omega\" ?", false),
        new Option<bool>("61920", "Thaumaturgus Ultima", "Mode: [select] only\nShould the bot buy \"Thaumaturgus Ultima\" ?", false),
        new Option<bool>("61924", "Exalted Unity", "Mode: [select] only\nShould the bot buy \"Exalted Unity\" ?", false),
        new Option<bool>("61925", "Exalted Penultima", "Mode: [select] only\nShould the bot buy \"Exalted Penultima\" ?", false),
        new Option<bool>("61926", "Exalted Apotheosis", "Mode: [select] only\nShould the bot buy \"Exalted Apotheosis\" ?", false),
        new Option<bool>("61921", "Apostate Alpha", "Mode: [select] only\nShould the bot buy \"Apostate Alpha\" ?", false),
        new Option<bool>("61922", "Apostate Omega", "Mode: [select] only\nShould the bot buy \"Apostate Omega\" ?", false),
        new Option<bool>("61923", "Apostate Ultima", "Mode: [select] only\nShould the bot buy \"Apostate Ultima\" ?", false),
        new Option<bool>("61927", "Exalted Duality", "Mode: [select] only\nShould the bot buy \"Exalted Duality\" ?", false),
        new Option<bool>("61826", "Ascendant", "Mode: [select] only\nShould the bot buy \"Ascendant\" ?", false),
        new Option<bool>("61831", "Ascendant Enlightened Vision", "Mode: [select] only\nShould the bot buy \"Ascendant Enlightened Vision\" ?", false),
        new Option<bool>("61832", "Ascendant Enlightened Locks", "Mode: [select] only\nShould the bot buy \"Ascendant Enlightened Locks\" ?", false),
        new Option<bool>("61833", "Ascendant Dagger Zodiac", "Mode: [select] only\nShould the bot buy \"Ascendant Dagger Zodiac\" ?", false),
        new Option<bool>("61835", "Ascendant Hammer Zodiac", "Mode: [select] only\nShould the bot buy \"Ascendant Hammer Zodiac\" ?", false),
        new Option<bool>("61836", "Ascendant Scythe Zodiac", "Mode: [select] only\nShould the bot buy \"Ascendant Scythe Zodiac\" ?", false),
        new Option<bool>("61837", "Ascendant Staff Zodiac", "Mode: [select] only\nShould the bot buy \"Ascendant Staff Zodiac\" ?", false),
        new Option<bool>("61838", "Ascendant Tome Zodiac", "Mode: [select] only\nShould the bot buy \"Ascendant Tome Zodiac\" ?", false),
        new Option<bool>("61839", "Ascendant Wand Zodiac", "Mode: [select] only\nShould the bot buy \"Ascendant Wand Zodiac\" ?", false),
        new Option<bool>("61834", "Ascendant Grand Zodiac", "Mode: [select] only\nShould the bot buy \"Ascendant Grand Zodiac\" ?", false),
        new Option<bool>("61845", "Riftwalker", "Mode: [select] only\nShould the bot buy \"Riftwalker\" ?", false),
        new Option<bool>("61847", "Riftwalker Horns", "Mode: [select] only\nShould the bot buy \"Riftwalker Horns\" ?", false),
        new Option<bool>("61849", "Riftwalker Horned Locks", "Mode: [select] only\nShould the bot buy \"Riftwalker Horned Locks\" ?", false),
        new Option<bool>("61850", "Riftwalker Battle Guard", "Mode: [select] only\nShould the bot buy \"Riftwalker Battle Guard\" ?", false),
        new Option<bool>("61851", "Riftwalker Anchored Portal", "Mode: [select] only\nShould the bot buy \"Riftwalker Anchored Portal\" ?", false),
        new Option<bool>("61852", "Riftwalker Contemplative Portals", "Mode: [select] only\nShould the bot buy \"Riftwalker Contemplative Portals\" ?", false),
        new Option<bool>("61854", "Riftwalker Sentinel Blade", "Mode: [select] only\nShould the bot buy \"Riftwalker Sentinel Blade\" ?", false),
        new Option<bool>("61855", "Riftwalker Spectral Blade", "Mode: [select] only\nShould the bot buy \"Riftwalker Spectral Blade\" ?", false),
        new Option<bool>("61857", "Riftwalker Density Blade", "Mode: [select] only\nShould the bot buy \"Riftwalker Density Blade\" ?", false),
        new Option<bool>("61870", "Cryptic", "Mode: [select] only\nShould the bot buy \"Cryptic\" ?", false),
        new Option<bool>("62025", "Cryptic Runes", "Mode: [select] only\nShould the bot buy \"Cryptic Runes\" ?", false),
        new Option<bool>("62026", "Cryptic Daggers", "Mode: [select] only\nShould the bot buy \"Cryptic Daggers\" ?", false),
        new Option<bool>("62020", "Exalted Drone Battlepet", "Mode: [select] only\nShould the bot buy \"Exalted Drone Battlepet\" ?", false),
        new Option<bool>("68455", "Dual Exalted Apotheosis", "Mode: [select] only\nShould the bot buy \"Dual Exalted Apotheosis\" ?", false),
    };
}
