/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\ThroneofDarkness\CoreToD.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TachyonMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    private CoreToD TOD = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Tachyon Core Piece", "Blue Powercell", "Blue Overdrive", "Blue Tachyon Grip", "Blue Tachyon Trigger", "Orange Powercell", "Orange Overdrive", "Orange Tachyon Grip", "Orange Tachyon Trigger", "Saeculum Gem " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.IsMember)
        {
            Core.Logger("This Merge Requires Membership.");
            return;
        }

        TOD.DeepSpace();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("tachyon", 1251, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Blue Overdrive":
                case "Blue Powercell":
                case "Blue Tachyon Trigger":
                case "Blue Tachyon Grip":
                    if (!Core.CheckInventory("Orange Tachyon Blade"))
                        BuyAllMerge("Orange Tachyon Blade");
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5084);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("tachyon", "Svelgr the Devourer", "Svelgr the Devourer Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Tachyon Core Piece":
                case "Orange Overdrive":
                case "Orange Tachyon Grip":
                case "Orange Tachyon Trigger":
                case "Orange Powercell":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5083);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("tachyon", "Svelgr the Devourer", "Svelgr the Devourer Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Saeculum Gem":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5085);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("tachyon", "Svelgr the Devourer", "Svelgr Fang", isTemp: false);
                        Core.HuntMonster("portalwar", "Chronorysa", "Sands of Time", 6, isTemp: false);
                        Core.HuntMonster("portalmaze", "Time Wraith", "Wraith Wisp", 12, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34915", "Blue Tachyon Blade", "Mode: [select] only\nShould the bot buy \"Blue Tachyon Blade\" ?", false),
        new Option<bool>("34916", "Orange Tachyon Blade", "Mode: [select] only\nShould the bot buy \"Orange Tachyon Blade\" ?", false),
        new Option<bool>("34737", "Chrono Assassin", "Mode: [select] only\nShould the bot buy \"Chrono Assassin\" ?", false),
        new Option<bool>("35096", "Chrono Assassin Armor", "Mode: [select] only\nShould the bot buy \"Chrono Assassin Armor\" ?", false),
        new Option<bool>("34817", "Time Assassin", "Mode: [select] only\nShould the bot buy \"Time Assassin\" ?", false),
        new Option<bool>("34818", "Time Assassin Cowl", "Mode: [select] only\nShould the bot buy \"Time Assassin Cowl\" ?", false),
        new Option<bool>("34819", "Time Slicer", "Mode: [select] only\nShould the bot buy \"Time Slicer\" ?", false),
        new Option<bool>("34820", "Ruins of Time Cape", "Mode: [select] only\nShould the bot buy \"Ruins of Time Cape\" ?", false),
        new Option<bool>("35140", "Dual Tachyon Blades", "Mode: [select] only\nShould the bot buy \"Dual Tachyon Blades\" ?", false),
    };
}
