/*
name: GL-24Ks Loot Merge
description: This bot will farm the items belonging to the selected mode for the GL-24Ks Loot Merge [2437] in /twigguhunt
tags: gl-24ks,gl 24ks, loot, merge, twigguhunt, gold, voucher, k, glk, cloaked, visor, cloak, pronged, spear, spears, gilded, seraphic, fourth, morph, beard, beacon, glst
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
//cs_include Scripts/Seasonal/MayThe4th/TwigguHunt.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GL24KsLootMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private TwigguHunt TH = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Salvaged Droid Part", "GL-1ST", "GL-1ST Helm", "GL-1ST Mask", "GL-1ST Visor", "GL-1ST Pronged Spear", "GL-1ST Pronged Spears", "GL-1ST Salvage Axe", "GL-1ST Salvage Axes", "GL-1ST Salvage Gun", "GL-1ST Salvage Guns", "Seraphic Fourth Morph", "Seraphic Fourth Beard Morph", "Seraphic Fourth Visage" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        TH.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("twigguhunt", 2437, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Salvaged Droid Part":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9703);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("twigguhunt", "r2", "Down", "*", "Broken Droid Part", 300, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "GL-1ST":
                case "GL-1ST Helm":
                case "GL-1ST Mask":
                case "GL-1ST Visor":
                case "Seraphic Fourth Morph":
                case "Seraphic Fourth Beard Morph":
                case "Seraphic Fourth Visage":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("twigguhunt", "Twiggu", req.Name, quant, false, false);
                    break;

                case "GL-1ST Pronged Spear":
                case "GL-1ST Pronged Spears":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("twigguhunt", "Bodyguard Droid", req.Name, quant, false, false);
                    break;

                case "GL-1ST Salvage Axe":
                case "GL-1ST Salvage Axes":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("twigguhunt", "Scout Droid", req.Name, quant, false, false);
                    break;

                case "GL-1ST Salvage Gun":
                case "GL-1ST Salvage Guns":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("twigguhunt", "Infantry Droid", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("85548", "GL-24K", "Mode: [select] only\nShould the bot buy \"GL-24K\" ?", false),
        new Option<bool>("85549", "Cloaked GL-24K", "Mode: [select] only\nShould the bot buy \"Cloaked GL-24K\" ?", false),
        new Option<bool>("85550", "GL-24K Helm", "Mode: [select] only\nShould the bot buy \"GL-24K Helm\" ?", false),
        new Option<bool>("85551", "GL-24K Mask", "Mode: [select] only\nShould the bot buy \"GL-24K Mask\" ?", false),
        new Option<bool>("85552", "GL-24K Visor", "Mode: [select] only\nShould the bot buy \"GL-24K Visor\" ?", false),
        new Option<bool>("85555", "GL-24K Cloak", "Mode: [select] only\nShould the bot buy \"GL-24K Cloak\" ?", false),
        new Option<bool>("85557", "GL-24K Pronged Spear", "Mode: [select] only\nShould the bot buy \"GL-24K Pronged Spear\" ?", false),
        new Option<bool>("85558", "GL-24K Pronged Spears", "Mode: [select] only\nShould the bot buy \"GL-24K Pronged Spears\" ?", false),
        new Option<bool>("85559", "GL-24K Gilded Axe", "Mode: [select] only\nShould the bot buy \"GL-24K Gilded Axe\" ?", false),
        new Option<bool>("85560", "GL-24K Gilded Axes", "Mode: [select] only\nShould the bot buy \"GL-24K Gilded Axes\" ?", false),
        new Option<bool>("85561", "GL-24K Gilded Gun", "Mode: [select] only\nShould the bot buy \"GL-24K Gilded Gun\" ?", false),
        new Option<bool>("85562", "GL-24K Gilded Guns", "Mode: [select] only\nShould the bot buy \"GL-24K Gilded Guns\" ?", false),
        new Option<bool>("85571", "Seraphic Fourth Hooded Morph", "Mode: [select] only\nShould the bot buy \"Seraphic Fourth Hooded Morph\" ?", false),
        new Option<bool>("85573", "Seraphic Fourth Hooded Beard Morph", "Mode: [select] only\nShould the bot buy \"Seraphic Fourth Hooded Beard Morph\" ?", false),
        new Option<bool>("85575", "Seraphic Fourth Hooded Visage", "Mode: [select] only\nShould the bot buy \"Seraphic Fourth Hooded Visage\" ?", false),
        new Option<bool>("85553", "GL-24K Beacon Cloak", "Mode: [select] only\nShould the bot buy \"GL-24K Beacon Cloak\" ?", false),
        new Option<bool>("85554", "GL-24K Beacon", "Mode: [select] only\nShould the bot buy \"GL-24K Beacon\" ?", false),
        new Option<bool>("85536", "GL-1ST Beacon", "Mode: [select] only\nShould the bot buy \"GL-1ST Beacon\" ?", false),
    };
}
