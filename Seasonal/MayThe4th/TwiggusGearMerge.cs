/*
name: Twiggus Gear Merge
description: This bot will farm the items belonging to the selected mode for the Twiggus Gear Merge [2272] in /murdermoon
tags: twiggus, gear, merge, murdermoon, astravian, enforcer, cap, flapped, enforce, cloaked, complete, halo, mace, sickle, sickles, backhand
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TwiggusGearMergeMerge
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
        Core.BankingBlackList.AddRange(new[] { "Fwog Egg", "Astravian Enforcer Crescent Halo" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("murdermoon", 2272, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Fwog Egg":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9223);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("murdermoon", "Tempest Soldier", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Astravian Enforcer Crescent Halo":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("murdermoon", "Fifth Sepulchure", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77676", "Astravian Enforcer", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer\" ?", false),
        new Option<bool>("77678", "Astravian Enforcer Hair", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Hair\" ?", false),
        new Option<bool>("77679", "Astravian Enforcer Locks", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Locks\" ?", false),
        new Option<bool>("77680", "Astravian Enforcer Cap", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Cap\" ?", false),
        new Option<bool>("77681", "Astravian Enforcer Cap and Locks", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Cap and Locks\" ?", false),
        new Option<bool>("77682", "Astravian Enforcer Flapped Cap", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Flapped Cap\" ?", false),
        new Option<bool>("77683", "Astravian Enforce Flapped Cap and Locks", "Mode: [select] only\nShould the bot buy \"Astravian Enforce Flapped Cap and Locks\" ?", false),
        new Option<bool>("77684", "Astravian Enforcer Helm", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Helm\" ?", false),
        new Option<bool>("77677", "Astravian Cloaked Enforcer", "Mode: [select] only\nShould the bot buy \"Astravian Cloaked Enforcer\" ?", false),
        new Option<bool>("77685", "Astravian Enforcer Cape", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Cape\" ?", false),
        new Option<bool>("77687", "Astravian Enforcer Complete Halo", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Complete Halo\" ?", false),
        new Option<bool>("77690", "Astravian Enforcer Mace", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Mace\" ?", false),
        new Option<bool>("77691", "Astravian Enforcer Sickle", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Sickle\" ?", false),
        new Option<bool>("77692", "Astravian Enforcer Sickles", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Sickles\" ?", false),
        new Option<bool>("77693", "Astravian Enforcer Backhand Sickle", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Backhand Sickle\" ?", false),
        new Option<bool>("77694", "Astravian Enforcer Backhand Sickles", "Mode: [select] only\nShould the bot buy \"Astravian Enforcer Backhand Sickles\" ?", false),
    };
}
