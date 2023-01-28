/*
name: Technocaster Merge
description: This will get all or selected items on this merge shop.
tags: technocaster-merge, black-friday, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TechnocasterMerge
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
        Core.BankingBlackList.AddRange(new[] { "Purified Energy Core", "Seraphic Steel Plate" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Core.AddDrop("Purified Energy Core", "Seraphic Steel Plate");
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("technospace", 1802, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Purified Energy Core":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7236);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("technospace", "Technocaster Rogue", "Energy Core", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Seraphic Steel Plate":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7235);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("technospace", "Technowolf", "Seraphic Steel", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("51392", "Seraphic Technocaster", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster\" ?", false),
        new Option<bool>("51393", "Seraphic Technocaster Locks", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Locks\" ?", false),
        new Option<bool>("51394", "Seraphic Technocaster Locks + Helm", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Locks + Helm\" ?", false),
        new Option<bool>("51395", "Seraphic Technocaster Locks + Scarf", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Locks + Scarf\" ?", false),
        new Option<bool>("51400", "Seraphic Technocaster Antenna", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Antenna\" ?", false),
        new Option<bool>("51399", "Seraphic Technocaster Collar", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Collar\" ?", false),
        new Option<bool>("51396", "Seraphic Technocaster Hair", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Hair\" ?", false),
        new Option<bool>("51397", "Seraphic Technocaster Helm", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Helm\" ?", false),
        new Option<bool>("51398", "Seraphic Technocaster Visor", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Visor\" ?", false),
        new Option<bool>("51401", "Seraphic Technocaster Scarf", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Scarf\" ?", false),
        new Option<bool>("51402", "Seraphic Technocaster Wing", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Wing\" ?", false),
        new Option<bool>("51403", "Legacy Of Sai", "Mode: [select] only\nShould the bot buy \"Legacy Of Sai\" ?", false),
        new Option<bool>("51458", "Technocaster House", "Mode: [select] only\nShould the bot buy \"Technocaster House\" ?", false),
        new Option<bool>("51538", "Seraphic Technocaster Locks + Visor", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Locks + Visor\" ?", false),
        new Option<bool>("51539", "Seraphic Technocaster Locks + Antenna", "Mode: [select] only\nShould the bot buy \"Seraphic Technocaster Locks + Antenna\" ?", false),
    };
}
