/*
name: Wubbles Merge Shop
description: This script farms the items from Wubbles Merge Shop.
tags: seasonal, love, wubbles, merge, heros heart day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HerosHeartDay/Wubbles.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class WubblesMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Wubbles Wub = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Wub Charm", "Furry Heart", "Chocolate Tail" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Wub.CompleteStory();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("wubbles", 1691, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Wub Charm":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("wubblevania", "Charmed Alina", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Chocolate Tail":
                case "Furry Heart":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("wubblevania", "Mr. Wubbles", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("46840", "Cupid of Love", "Mode: [select] only\nShould the bot buy \"Cupid of Love\" ?", false),
        new Option<bool>("46841", "Cupid of Love Halo", "Mode: [select] only\nShould the bot buy \"Cupid of Love Halo\" ?", false),
        new Option<bool>("46842", "Cupid of Love Wings", "Mode: [select] only\nShould the bot buy \"Cupid of Love Wings\" ?", false),
        new Option<bool>("46843", "Cupid of Love Bow + Arrow", "Mode: [select] only\nShould the bot buy \"Cupid of Love Bow + Arrow\" ?", false),
        new Option<bool>("46844", "Cupid of Love Bow", "Mode: [select] only\nShould the bot buy \"Cupid of Love Bow\" ?", false),
        new Option<bool>("46831", "Cupid of Gold", "Mode: [select] only\nShould the bot buy \"Cupid of Gold\" ?", false),
        new Option<bool>("46832", "Cupid of Gold Crown", "Mode: [select] only\nShould the bot buy \"Cupid of Gold Crown\" ?", false),
        new Option<bool>("46833", "Cupid of Gold Wings", "Mode: [select] only\nShould the bot buy \"Cupid of Gold Wings\" ?", false),
        new Option<bool>("46834", "Cupid of Gold Bow", "Mode: [select] only\nShould the bot buy \"Cupid of Gold Bow\" ?", false),
        new Option<bool>("46845", "Cupid of Gold Bow + Arrow", "Mode: [select] only\nShould the bot buy \"Cupid of Gold Bow + Arrow\" ?", false),
    };
}
