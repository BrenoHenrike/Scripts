/*
name: Dance Guru Merge
description: This script farms all the materials for Dance Guru Merge.
tags: dance guru, merge, seasonal, carnaval
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Carnaval/DanceGuru.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DanceGuruMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private DanceGuru DG = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Spirit Beads", "Pássaro Rosa Cape", "Pano Azul" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DG.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("danceguru", 1976, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Spirit Beads":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7957);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("danceguru", "Crow", "Bead Shards", 20, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Pássaro Rosa Cape":
                case "Pano Azul":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("danceguru", "Carnaval Harpy", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("59646", "Cabelo Bonita", "Mode: [select] only\nShould the bot buy \"Cabelo Bonita\" ?", false),
        new Option<bool>("59645", "Cabelo de Festa", "Mode: [select] only\nShould the bot buy \"Cabelo de Festa\" ?", false),
        new Option<bool>("59653", "Pássaro Rosa Outfit", "Mode: [select] only\nShould the bot buy \"Pássaro Rosa Outfit\" ?", false),
        new Option<bool>("59655", "Pássaro Rosa Headpiece Morph", "Mode: [select] only\nShould the bot buy \"Pássaro Rosa Headpiece Morph\" ?", false),
        new Option<bool>("59657", "Pássaro Rosa Headdress Morph", "Mode: [select] only\nShould the bot buy \"Pássaro Rosa Headdress Morph\" ?", false),
        new Option<bool>("59660", "Pássaro Rosa Wrap", "Mode: [select] only\nShould the bot buy \"Pássaro Rosa Wrap\" ?", false),
        new Option<bool>("59662", "Pássaro Rosa Dual Fan", "Mode: [select] only\nShould the bot buy \"Pássaro Rosa Dual Fan\" ?", false),
        new Option<bool>("59705", "Evil Abada Outfit", "Mode: [select] only\nShould the bot buy \"Evil Abada Outfit\" ?", false),
        new Option<bool>("59706", "Good Abada Outfit", "Mode: [select] only\nShould the bot buy \"Good Abada Outfit\" ?", false),
        new Option<bool>("59704", "Chaos Abada Outfit", "Mode: [select] only\nShould the bot buy \"Chaos Abada Outfit\" ?", false),
        new Option<bool>("59707", "Hollowborn Abada Outfit", "Mode: [select] only\nShould the bot buy \"Hollowborn Abada Outfit\" ?", false),
    };
}
