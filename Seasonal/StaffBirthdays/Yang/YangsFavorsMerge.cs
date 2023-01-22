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

public class YangsFavorsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Yang’s Favor" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("tercessuinotlim", 2217, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Yang’s Favor":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9035);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("poisonforest", "Traitor Knight", "Traitor’s Medal", 15);
                        Core.HuntMonster("poisonforest", "Xavier Lionfang", "Xavier’s Medal", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70003", "BattleMaster Tharr", "Mode: [select] only\nShould the bot buy \"BattleMaster Tharr\" ?", false),
        new Option<bool>("70004", "Tharr's Helm + Beard", "Mode: [select] only\nShould the bot buy \"Tharr's Helm + Beard\" ?", false),
        new Option<bool>("70005", "Tharr's Helm", "Mode: [select] only\nShould the bot buy \"Tharr's Helm\" ?", false),
        new Option<bool>("70006", "Tharr's Wrap", "Mode: [select] only\nShould the bot buy \"Tharr's Wrap\" ?", false),
        new Option<bool>("70007", "Tharr's Double-Axe", "Mode: [select] only\nShould the bot buy \"Tharr's Double-Axe\" ?", false),
        new Option<bool>("70008", "Tharr's Axe and Shield", "Mode: [select] only\nShould the bot buy \"Tharr's Axe and Shield\" ?", false),
    };
}
