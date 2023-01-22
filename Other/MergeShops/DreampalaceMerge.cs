/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DreampalaceMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Axe of Golmoth", "Scales of Golmoth", "Token of Fire", "Zahad's Ancient Gem", "Scythe of Gazeroth", "Souls of Gazeroth", "Token of Earth", "Bow of Zelkur", "Claws of Zelkur", "Token of Water", "Scimitar of Zal", "Feathers of Zal", "Token of Air " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dreampalace", 1961, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Axe of Golmoth":
                case "Scales of Golmoth":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("DreamPalace", "Golmoth", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Token of Air":
                case "Token of Water":
                case "Token of Earth":
                case "Token of Fire":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("DreamPalace", "Mote of Power", req.Name, quant, isTemp: false, log: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Zahad's Ancient Gem":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("DreamPalace", "Zahad", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Scythe of Gazeroth":
                case "Souls of Gazeroth":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("DreamPalace", "Gazeroth", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Claws of Zelkur":
                case "Bow of Zelkur":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("DreamPalace", "Zelkur", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Feathers of Zal":
                case "Scimitar of Zal":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("DreamPalace", "Zal", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("58693", "Strong Axe of Golmoth", "Mode: [select] only\nShould the bot buy \"Strong Axe of Golmoth\" ?", false),
        new Option<bool>("58694", "Vibrant Axe of Golmoth", "Mode: [select] only\nShould the bot buy \"Vibrant Axe of Golmoth\" ?", false),
        new Option<bool>("58695", "Awakened Axe of Golmoth", "Mode: [select] only\nShould the bot buy \"Awakened Axe of Golmoth\" ?", false),
        new Option<bool>("58701", "Strong Scythe of Gazeroth", "Mode: [select] only\nShould the bot buy \"Strong Scythe of Gazeroth\" ?", false),
        new Option<bool>("58702", "Vibrant Scythe of Gazeroth", "Mode: [select] only\nShould the bot buy \"Vibrant Scythe of Gazeroth\" ?", false),
        new Option<bool>("58703", "Awakened Scythe of Gazeroth", "Mode: [select] only\nShould the bot buy \"Awakened Scythe of Gazeroth\" ?", false),
        new Option<bool>("58705", "Strong Bow of Zelkur", "Mode: [select] only\nShould the bot buy \"Strong Bow of Zelkur\" ?", false),
        new Option<bool>("58706", "Vibrant Bow of Zelkur", "Mode: [select] only\nShould the bot buy \"Vibrant Bow of Zelkur\" ?", false),
        new Option<bool>("58707", "Awakened Bow of Zelkur", "Mode: [select] only\nShould the bot buy \"Awakened Bow of Zelkur\" ?", false),
        new Option<bool>("58697", "Strong Scimitar of Zal", "Mode: [select] only\nShould the bot buy \"Strong Scimitar of Zal\" ?", false),
        new Option<bool>("58698", "Vibrant Scimitar of Zal", "Mode: [select] only\nShould the bot buy \"Vibrant Scimitar of Zal\" ?", false),
        new Option<bool>("58699", "Awakened Scimitar of Zal", "Mode: [select] only\nShould the bot buy \"Awakened Scimitar of Zal\" ?", false),
    };
}
