/*
name: DagesBirthdayMerge
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DagesBirthdayMerge
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
        Core.BankingBlackList.AddRange(new[] { "Shard of Armor", "Helm Piece", "Leg Pieces", "Arm Pieces", "Weapon Shard", "Cape Piece", "Death's Scythe" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("undervoid"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("undervoid", 839, findIngredients, buyOnlyThis, buyMode: buyMode);

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


                case "Shard of Armor":
                case "Helm Piece":
                case "Leg Pieces":
                case "Arm Pieces":
                    //3408 requires you to join the legion (1200acs) added a method for non-legions
                    if (Core.isCompletedBefore(793))
                    {
                        Core.FarmingLogger(req.Name, quant);
                        Core.EquipClass(ClassType.Farm);
                        Core.RegisterQuests(3408);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.KillMonster("underworld", "r8", "left", "*", "Dread Head", 20, log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    else
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("undervoid", "Conquest", req.Name, quant, false);
                    }
                    break;

                case "Weapon Shard":
                case "Cape Piece":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("undervoid", "Conquest", req.Name, quant, false);
                    break;

                    // case "Death's Scythe":
                    //     Core.FarmingLogger(req.Name, quant);
                    //     Core.EquipClass(ClassType.Farm);
                    //     Core.RegisterQuests(0000);
                    //     while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    //     {
                    //         Core.Logger("This item is not setup yet");
                    //         Bot.Wait.ForPickup(req.Name);
                    //     }
                    //     Core.CancelRegisteredQuests();
                    //     break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("23071", "Spirit of War", "Mode: [select] only\nShould the bot buy \"Spirit of War\" ?", false),
        new Option<bool>("23072", "Spirit of Famine", "Mode: [select] only\nShould the bot buy \"Spirit of Famine\" ?", false),
        new Option<bool>("23073", "Spirit of Conquest", "Mode: [select] only\nShould the bot buy \"Spirit of Conquest\" ?", false),
        new Option<bool>("23074", "Spirit of Death", "Mode: [select] only\nShould the bot buy \"Spirit of Death\" ?", false),
        new Option<bool>("23102", "Death's Kamas", "Mode: [select] only\nShould the bot buy \"Death's Kamas\" ?", false),
        new Option<bool>("23098", "Conquest Bow", "Mode: [select] only\nShould the bot buy \"Conquest Bow\" ?", false),
        new Option<bool>("23094", "Famine's Gilded Horns", "Mode: [select] only\nShould the bot buy \"Famine's Gilded Horns\" ?", false),
        new Option<bool>("23085", "Conquest's Embellished Cloak", "Mode: [select] only\nShould the bot buy \"Conquest's Embellished Cloak\" ?", false),
        new Option<bool>("23076", "Conquest's Hood and Crown", "Mode: [select] only\nShould the bot buy \"Conquest's Hood and Crown\" ?", false),
        new Option<bool>("23089", "Conquest's Cloak", "Mode: [select] only\nShould the bot buy \"Conquest's Cloak\" ?", false),
        new Option<bool>("23099", "Hidden Face of Conquest", "Mode: [select] only\nShould the bot buy \"Hidden Face of Conquest\" ?", false),
        new Option<bool>("23101", "Conquest Polearm", "Mode: [select] only\nShould the bot buy \"Conquest Polearm\" ?", false),
        new Option<bool>("23110", "War's Cape", "Mode: [select] only\nShould the bot buy \"War's Cape\" ?", false),
        new Option<bool>("23066", "Obsidian Underguard", "Mode: [select] only\nShould the bot buy \"Obsidian Underguard\" ?", false),
        new Option<bool>("23117", "War's Helm", "Mode: [select] only\nShould the bot buy \"War's Helm\" ?", false),
        //Requires 300acs
        // new Option<bool>("23122", "Death's Grimmer Scythe", "Mode: [select] only\nShould the bot buy \"Death's Grimmer Scythe\" ?", false),
    };
}
