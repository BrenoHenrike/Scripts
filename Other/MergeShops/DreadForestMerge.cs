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

public class DreadForestMerge
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
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dreadforest", 2140, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Enchanted Crystal":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.RegisterQuests(8722);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("dreadforest", "Reignolds' Knight", "Valuable Metals", 8);
                        Core.HuntMonster("dreadforest", "Taxidermied Servant", "Gold Pouches", 8);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("dreadforest", "Lord Reignolds", "Reignolds' Brooch", 1);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Blade of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Noble's Knight", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Greatsword of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Treacherous Bandit", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dagger of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Treacherous Bandit", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Daggers of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Treacherous Bandit", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "High Axe of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", $"Noble’s Servant", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Greataxe of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8722);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Taxidermied Servant", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Poleaxe of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8722);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Lord Reignolds", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Axes of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8722);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Reignolds' Knight", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Handaxe of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Noble's Knight", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Handaxes of Dread":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadforest", "Noble's Knight", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70484", "Runed Knight", "Mode: [select] only\nShould the bot buy \"Runed Knight\" ?", false),
        new Option<bool>("70486", "Runed Knight's Morph", "Mode: [select] only\nShould the bot buy \"Runed Knight's Morph\" ?", false),
        new Option<bool>("70488", "Runed Knight's Horned Helm", "Mode: [select] only\nShould the bot buy \"Runed Knight's Horned Helm\" ?", false),
        new Option<bool>("70490", "Runed Knight's Wings + Cape", "Mode: [select] only\nShould the bot buy \"Runed Knight's Wings + Cape\" ?", false),
        new Option<bool>("70491", "Runed Knight's Portal Cape", "Mode: [select] only\nShould the bot buy \"Runed Knight's Portal Cape\" ?", false),
        new Option<bool>("70507", "Enchanted Blade of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Blade of Dread\" ?", false),
        new Option<bool>("70508", "Enchanted Greatsword of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Greatsword of Dread\" ?", false),
        new Option<bool>("70509", "Enchanted Dagger of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Dagger of Dread\" ?", false),
        new Option<bool>("70510", "Enchanted Daggers of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Daggers of Dread\" ?", false),
        new Option<bool>("70512", "Enchanted High Axe of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted High Axe of Dread\" ?", false),
        new Option<bool>("70513", "Enchanted Greataxe of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Greataxe of Dread\" ?", false),
        new Option<bool>("70514", "Enchanted Poleaxe of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Poleaxe of Dread\" ?", false),
        new Option<bool>("70517", "Enchanted Axe of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Axe of Dread\" ?", false),
        new Option<bool>("70518", "Enchanted Axes of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Axes of Dread\" ?", false),
        new Option<bool>("70519", "Enchanted Handaxe of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Handaxe of Dread\" ?", false),
        new Option<bool>("70520", "Enchanted Handaxes of Dread", "Mode: [select] only\nShould the bot buy \"Enchanted Handaxes of Dread\" ?", false),
    };
}
