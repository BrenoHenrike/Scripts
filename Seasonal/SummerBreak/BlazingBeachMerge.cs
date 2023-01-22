/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/SummerBreak/BlazingBeach.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BlazingBeachMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public BlazingBeachStory BBS = new();
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
        if (!Core.isSeasonalMapActive("blazingbeach"))
            return;

        BBS.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("blazingbeach", 2138, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case $"Mother Dragon’s Gift":
                    Core.RegisterQuests(8709);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("blazingbeach", "r5", "Right", "Red-Hot Raider", "Raider Repelled", 10, log: false);
                        Core.KillMonster("blazingbeach", "r2", "Right", "Scalding Shooter", "Sharpshooter Shooed", 8, log: false);
                        Core.KillMonster("blazingbeach", "r2", "Right", "Burning Bombadier", "Bomber Bye-Byed", 6, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70495", "Magma BlazeBeard", "Mode: [select] only\nShould the bot buy \"Magma BlazeBeard\" ?", false),
        new Option<bool>("70496", "Magma BlazeBeard Morph", "Mode: [select] only\nShould the bot buy \"Magma BlazeBeard Morph\" ?", false),
        new Option<bool>("70497", "Magma BlazeBeard Female Morph", "Mode: [select] only\nShould the bot buy \"Magma BlazeBeard Female Morph\" ?", false),
        new Option<bool>("70498", "Magma BlazeBeard Flames", "Mode: [select] only\nShould the bot buy \"Magma BlazeBeard Flames\" ?", false),
        new Option<bool>("70499", "Magma BlazeBeard Cutlass", "Mode: [select] only\nShould the bot buy \"Magma BlazeBeard Cutlass\" ?", false),
        new Option<bool>("70661", "Dual Magma BlazeBeard Cutlasses", "Mode: [select] only\nShould the bot buy \"Dual Magma BlazeBeard Cutlasses\" ?", false),
        new Option<bool>("68640", "Magmagilt Odachi", "Mode: [select] only\nShould the bot buy \"Magmagilt Odachi\" ?", false),
        new Option<bool>("68641", "Magmagilt Odachis", "Mode: [select] only\nShould the bot buy \"Magmagilt Odachis\" ?", false),
        new Option<bool>("68643", "Magmagilt Kama", "Mode: [select] only\nShould the bot buy \"Magmagilt Kama\" ?", false),
        new Option<bool>("68644", "Magmagilt Kamas", "Mode: [select] only\nShould the bot buy \"Magmagilt Kamas\" ?", false),
        new Option<bool>("68647", "Magmagilt Tabar", "Mode: [select] only\nShould the bot buy \"Magmagilt Tabar\" ?", false),
        new Option<bool>("68648", "Magmagilt Tabars", "Mode: [select] only\nShould the bot buy \"Magmagilt Tabars\" ?", false),
    };
}
