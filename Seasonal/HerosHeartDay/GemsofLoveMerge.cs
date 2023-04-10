/*
name: Gems of Love Merge Shop
description: This script farms the items from Gems of Love Merge Shop.
tags: seasonal, love, merge, gemsoflove, heros heart day, beleen
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GemsofLoveMerge
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
        Core.BankingBlackList.AddRange(new[] { "White Oval", "White Box", "Oval Setting", "Sparkles", "Blush Brilliant", "Blush Box", "Brilliant Setting", "Pink Pear", "Pink Box", "Pear Setting", "Half Rose", "Rose Box", "Half Rose Setting", "Cerise Trillian", "Cerise Box", "Trillian Setting", "Ruby Heart", "Ruby Box", "Heart Setting" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("love", 1835, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "White Oval":
                case "Blush Brilliant":
                case "Pink Pear":
                case "Half Rose":
                case "Cerise Trillian":
                case "Ruby Heart":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7375);
                        Core.HuntMonster("greed", "Goregold", "Stolen Gem Found", log: false);
                        Core.EnsureComplete(7375, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "White Box":
                case "Oval Setting":
                case "Sparkles":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7369);
                        Core.HuntMonster("pastelia", "Cutie Makai", "White Box Found", log: false);
                        Core.EnsureComplete(7369, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Blush Box":
                case "Brilliant Setting":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7370);
                        Core.HuntMonster("dwarfhold", "Gemrald", "Blush Box Found", log: false);
                        Core.EnsureComplete(7370, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Pink Box":
                case "Pear Setting":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7371);
                        Core.HuntMonster("earthstorm", "Amethite", "Pink Box Found", log: false);
                        Core.EnsureComplete(7371, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Rose Box":
                case "Half Rose Setting":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7372);
                        Core.HuntMonster("stalagbite", "Stalagbite", "Rose Box Found", log: false);
                        Core.EnsureComplete(7372, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Cerise Box":
                case "Trillian Setting":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7373);
                        Core.HuntMonster("castleofglass", "Chihuly", "Cerise Box Found", log: false);
                        Core.EnsureComplete(7373, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Ruby Box":
                case "Heart Setting":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7374);
                        Core.HuntMonster("beleensdream", "Heart Elemental", "Ruby Box Found", log: false);
                        Core.EnsureComplete(7374, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("53363", "Ring of White Diamonds Cape", "Mode: [select] only\nShould the bot buy \"Ring of White Diamonds Cape\" ?", false),
        new Option<bool>("53357", "White Diamond Gem Pet", "Mode: [select] only\nShould the bot buy \"White Diamond Gem Pet\" ?", false),
        new Option<bool>("53364", "Ring of Blush Brilliants Cape", "Mode: [select] only\nShould the bot buy \"Ring of Blush Brilliants Cape\" ?", false),
        new Option<bool>("53358", "Blush Brilliant Gem Pet", "Mode: [select] only\nShould the bot buy \"Blush Brilliant Gem Pet\" ?", false),
        new Option<bool>("53365", "Ring of Pink Pears Cape", "Mode: [select] only\nShould the bot buy \"Ring of Pink Pears Cape\" ?", false),
        new Option<bool>("53359", "Pink Pear Gem Pet", "Mode: [select] only\nShould the bot buy \"Pink Pear Gem Pet\" ?", false),
        new Option<bool>("53366", "Ring of Rose Hexagons Cape", "Mode: [select] only\nShould the bot buy \"Ring of Rose Hexagons Cape\" ?", false),
        new Option<bool>("53360", "Rose Hexagon Gem Pet", "Mode: [select] only\nShould the bot buy \"Rose Hexagon Gem Pet\" ?", false),
        new Option<bool>("53367", "Ring of Cerise Trillians Cape", "Mode: [select] only\nShould the bot buy \"Ring of Cerise Trillians Cape\" ?", false),
        new Option<bool>("53361", "Cerise Trillian Gem Pet", "Mode: [select] only\nShould the bot buy \"Cerise Trillian Gem Pet\" ?", false),
        new Option<bool>("53368", "Ring of Ruby Hearts Cape", "Mode: [select] only\nShould the bot buy \"Ring of Ruby Hearts Cape\" ?", false),
        new Option<bool>("53362", "Ruby Heart Gem Pet", "Mode: [select] only\nShould the bot buy \"Ruby Heart Gem Pet\" ?", false),
        new Option<bool>("53369", "Ring of Gems Cape", "Mode: [select] only\nShould the bot buy \"Ring of Gems Cape\" ?", false),
        new Option<bool>("53371", "Ring of Gems", "Mode: [select] only\nShould the bot buy \"Ring of Gems\" ?", false),
    };
}
