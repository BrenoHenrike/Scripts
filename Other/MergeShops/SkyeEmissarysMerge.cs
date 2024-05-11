/*
name: Skye Emissarys Merge
description: This bot will farm the items belonging to the selected mode for the Skye Emissarys Merge [2441] in /balemorale
tags: skye, emissarys, merge, balemorale, gold, voucher, k, halcyon, virtue, scepter, coronation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SkyeEmissarysMerge
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
        Core.BankingBlackList.AddRange(new[] { "Pristine Deepsea Pearl", "Deepdark Pearl" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("balemorale", 2441, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Pristine Deepsea Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9718);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("ashray", "Enter", "Spawn", "*", "Deepsea Pearls", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Deepdark Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9715);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("midnightzone", "Shadow Viscera", "Viscera Sample", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("67387", "Halcyon Virtue Blade", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Blade\" ?", false),
        new Option<bool>("67388", "Halcyon Virtue Blades", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Blades\" ?", false),
        new Option<bool>("85944", "Halcyon Skye Blade", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Blade\" ?", false),
        new Option<bool>("85945", "Halcyon Skye Blades", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Blades\" ?", false),
        new Option<bool>("85942", "Halcyon Virtue Scepter", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Scepter\" ?", false),
        new Option<bool>("85943", "Halcyon Skye Scepter", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Scepter\" ?", false),
        new Option<bool>("85948", "Halcyon Virtue Axe", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Axe\" ?", false),
        new Option<bool>("85949", "Halcyon Virtue Axes", "Mode: [select] only\nShould the bot buy \"Halcyon Virtue Axes\" ?", false),
        new Option<bool>("67397", "Halcyon Skye Axe", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Axe\" ?", false),
        new Option<bool>("67398", "Halcyon Skye Axes", "Mode: [select] only\nShould the bot buy \"Halcyon Skye Axes\" ?", false),
        new Option<bool>("85950", "Halcyon Coronation Axe", "Mode: [select] only\nShould the bot buy \"Halcyon Coronation Axe\" ?", false),
        new Option<bool>("85951", "Halcyon Coronation Axes", "Mode: [select] only\nShould the bot buy \"Halcyon Coronation Axes\" ?", false),
    };
}
