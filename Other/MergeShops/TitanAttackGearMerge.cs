/*
name: Titan Attack Gear Merge
description: This bot will farm the items belonging to the selected mode for the Titan Attack Gear Merge [2149] in /titanattack
tags: titan, attack, gear, merge, titanattack, paladin, paladins, cloak, , vindicator, xl, titans, primaris, cut, morph, chainsword, chainswords, hammer, hammers, powerfist
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TitanAttackGearMerge
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
        Core.BankingBlackList.AddRange(new[] { "AntiTitan Supplies", "Titanic Fluid", "Titan Paladin's Blade", "Vindicator Titan", "Vindicator Titan's Axe", "Golden Sun Seal", "Holy Wasabi Jar", "Holy Hand Grenade" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("titanattack", 2149, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "AntiTitan Supplies":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("titanattack", "r9", "Left", "*", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Golden Sun Seal":
                case "Titan Paladin's Blade":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Paladin", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Vindicator Titan":
                case "Vindicator Titan's Axe":
                case "Titanic Fluid":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;


                case "Holy Wasabi Jar":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Supply Caravan", req.Name, quant, isTemp: false);
                    break;

                case "Holy Hand Grenade":
                    Adv.BuyItem("castle", 88, 1843, quant, shopItemID: 1847);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("68025", "Titan Paladin", "Mode: [select] only\nShould the bot buy \"Titan Paladin\" ?", false),
        new Option<bool>("68026", "Titan Paladin's Helm", "Mode: [select] only\nShould the bot buy \"Titan Paladin's Helm\" ?", false),
        new Option<bool>("68027", "Titan Paladin's Cloak", "Mode: [select] only\nShould the bot buy \"Titan Paladin's Cloak\" ?", false),
        new Option<bool>("68028", "Titan Paladin's Cloak + Blade", "Mode: [select] only\nShould the bot buy \"Titan Paladin's Cloak + Blade\" ?", false),
        new Option<bool>("71441", "Titan Paladin's Blades", "Mode: [select] only\nShould the bot buy \"Titan Paladin's Blades\" ?", false),
        new Option<bool>("68036", "Vindicator Titan XL", "Mode: [select] only\nShould the bot buy \"Vindicator Titan XL\" ?", false),
        new Option<bool>("68037", "Vindicator Titan's Helm", "Mode: [select] only\nShould the bot buy \"Vindicator Titan's Helm\" ?", false),
        new Option<bool>("68038", "Vindicator Titan's Cloak", "Mode: [select] only\nShould the bot buy \"Vindicator Titan's Cloak\" ?", false),
        new Option<bool>("68040", "Vindicator Titan's Axes", "Mode: [select] only\nShould the bot buy \"Vindicator Titan's Axes\" ?", false),
        new Option<bool>("77740", "Primaris Paladin", "Mode: [select] only\nShould the bot buy \"Primaris Paladin\" ?", false),
        new Option<bool>("77741", "Primaris Paladin's Cut", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Cut\" ?", false),
        new Option<bool>("77742", "Primaris Paladin's Locks", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Locks\" ?", false),
        new Option<bool>("77743", "Primaris Paladin's Morph", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Morph\" ?", false),
        new Option<bool>("77744", "Primaris Paladin's Helmet", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Helmet\" ?", false),
        new Option<bool>("77745", "Primaris Paladin's Hooded Helmet", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Hooded Helmet\" ?", false),
        new Option<bool>("77746", "Primaris Paladin's Cape", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Cape\" ?", false),
        new Option<bool>("77748", "Primaris Paladin's Chainsword", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Chainsword\" ?", false),
        new Option<bool>("77749", "Primaris Paladin's Chainswords", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Chainswords\" ?", false),
        new Option<bool>("77750", "Primaris Paladin's Hammer", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Hammer\" ?", false),
        new Option<bool>("77751", "Primaris Paladin's Hammers", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Hammers\" ?", false),
        new Option<bool>("77752", "Primaris Paladin's Axe", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's Axe\" ?", false),
        new Option<bool>("77753", "Primaris Paladin's PowerFist", "Mode: [select] only\nShould the bot buy \"Primaris Paladin's PowerFist\" ?", false),
    };
}
