/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DarkAllianceMerge
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
        Core.BankingBlackList.AddRange(new[] { "Shadow Orb", "Dark Spirit", "Mana Gem" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("darkalliance", 1872, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Shadow Orb":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("innershadows", "Shadowcrow", req.Name, quant, false);
                    break;

                case "Dark Spirit":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("darkalliance", "Shadow Void", req.Name, quant, false);
                    break;

                case "Mana Gem":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("darkalliance", "Underflame Guardian", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54038", "ArchFiend of the ShadowFlame", "Mode: [select] only\nShould the bot buy \"ArchFiend of the ShadowFlame\" ?", false),
        new Option<bool>("54039", "ShadowFlame Fiend Morph", "Mode: [select] only\nShould the bot buy \"ShadowFlame Fiend Morph\" ?", false),
        new Option<bool>("54040", "ShadowFlame OverFiend Blade", "Mode: [select] only\nShould the bot buy \"ShadowFlame OverFiend Blade\" ?", false),
        new Option<bool>("54057", "Underworld Horror", "Mode: [select] only\nShould the bot buy \"Underworld Horror\" ?", false),
        new Option<bool>("54058", "Underworld Horror Hood", "Mode: [select] only\nShould the bot buy \"Underworld Horror Hood\" ?", false),
        new Option<bool>("54059", "Underworld Horror Cape", "Mode: [select] only\nShould the bot buy \"Underworld Horror Cape\" ?", false),
        new Option<bool>("54060", "Underworld Horror Wings", "Mode: [select] only\nShould the bot buy \"Underworld Horror Wings\" ?", false),
        new Option<bool>("54061", "Underworld Horror Blade", "Mode: [select] only\nShould the bot buy \"Underworld Horror Blade\" ?", false),
        new Option<bool>("54270", "ShadowScythe Warlock", "Mode: [select] only\nShould the bot buy \"ShadowScythe Warlock\" ?", false),
        new Option<bool>("54271", "Shadow Warlock's Hat", "Mode: [select] only\nShould the bot buy \"Shadow Warlock's Hat\" ?", false),
        new Option<bool>("54272", "Shadow Warlock's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Shadow Warlock's Hat + Locks\" ?", false),
        new Option<bool>("54274", "ShadowScythe Hat", "Mode: [select] only\nShould the bot buy \"ShadowScythe Hat\" ?", false),
        new Option<bool>("54275", "ShadowScythe Hat + Locks", "Mode: [select] only\nShould the bot buy \"ShadowScythe Hat + Locks\" ?", false),
        new Option<bool>("54284", "Rune of Light", "Mode: [select] only\nShould the bot buy \"Rune of Light\" ?", false),
        new Option<bool>("54286", "Dual Runes of Light", "Mode: [select] only\nShould the bot buy \"Dual Runes of Light\" ?", false),
        new Option<bool>("54276", "ShadowScythe Hat + Skull", "Mode: [select] only\nShould the bot buy \"ShadowScythe Hat + Skull\" ?", false),
        new Option<bool>("54277", "ShadowScythe Warlock Hair", "Mode: [select] only\nShould the bot buy \"ShadowScythe Warlock Hair\" ?", false),
        new Option<bool>("54278", "ShadowScythe Warlock Locks", "Mode: [select] only\nShould the bot buy \"ShadowScythe Warlock Locks\" ?", false),
        new Option<bool>("54279", "ShadowScythe Warlock Skull Morph", "Mode: [select] only\nShould the bot buy \"ShadowScythe Warlock Skull Morph\" ?", false),
        new Option<bool>("54282", "Lesser Rune of Light", "Mode: [select] only\nShould the bot buy \"Lesser Rune of Light\" ?", false),
        new Option<bool>("54283", "Lesser Rune of Darkness", "Mode: [select] only\nShould the bot buy \"Lesser Rune of Darkness\" ?", false),
        new Option<bool>("54285", "Rune of Darkness", "Mode: [select] only\nShould the bot buy \"Rune of Darkness\" ?", false),
        new Option<bool>("54287", "Dual Runes of Darkness", "Mode: [select] only\nShould the bot buy \"Dual Runes of Darkness\" ?", false),
    };
}
