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

public class ExtinctionGamesMerge
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
        Core.BankingBlackList.AddRange(new[] { "Burningjay Feather", "Coal", "Fabric Scraps", "Iron II.0", "A Kitten? " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("extinction", 1025, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Burningjay Feather":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extinction", "Lard", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Coal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extinction", "Cyworg", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fabric Scraps":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extinction", "Lard", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Iron II.0":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extinction", "Cyworg", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "A Kitten?":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extinction", "SN.O.W.", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("26741", "MockingSlay Pet", "Mode: [select] only\nShould the bot buy \"MockingSlay Pet\" ?", false),
        new Option<bool>("26738", "MockingHawk Hair", "Mode: [select] only\nShould the bot buy \"MockingHawk Hair\" ?", false),
        new Option<bool>("26803", "MockingSlayer Bow", "Mode: [select] only\nShould the bot buy \"MockingSlayer Bow\" ?", false),
        new Option<bool>("26733", "MockingSlayer Quiver", "Mode: [select] only\nShould the bot buy \"MockingSlayer Quiver\" ?", false),
        new Option<bool>("26731", "Kat Mess Braid", "Mode: [select] only\nShould the bot buy \"Kat Mess Braid\" ?", false),
        new Option<bool>("26138", "Extinction Survivor Locks", "Mode: [select] only\nShould the bot buy \"Extinction Survivor Locks\" ?", false),
        new Option<bool>("26137", "Kale's Pithy Helm", "Mode: [select] only\nShould the bot buy \"Kale's Pithy Helm\" ?", false),
        new Option<bool>("26136", "Meeta's Machete", "Mode: [select] only\nShould the bot buy \"Meeta's Machete\" ?", false),
        new Option<bool>("26135", "Double HAMM Pack", "Mode: [select] only\nShould the bot buy \"Double HAMM Pack\" ?", false),
        new Option<bool>("26134", "Exctinction Survivor", "Mode: [select] only\nShould the bot buy \"Exctinction Survivor\" ?", false),
    };
}
