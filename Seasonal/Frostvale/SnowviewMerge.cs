/*
name: Snowview Merge
description: This will get all or selected items on this merge shop.
tags: snowview-merge, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Seasonal\Frostvale\Frostvale.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SnowviewMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public Frostvale Frostvale = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Fire Starting Kit " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Frostvale.Snowview();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("snowview", 2195, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Fire Starting Kit":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9016);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("snowview", "Vaderix", "Alien Mandible", log: false);
                        Core.HuntMonster("snowview", "Tundra Steed", "Horse Hair", 7, log: false);
                        Core.HuntMonster("snowview", "Mountain Owl", "Tinder Feathers", 7, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("74595", "Festive Sled Racer", "Mode: [select] only\nShould the bot buy \"Festive Sled Racer\" ?", false),
        new Option<bool>("74596", "Festive Sled Racer Hat", "Mode: [select] only\nShould the bot buy \"Festive Sled Racer Hat\" ?", false),
        new Option<bool>("74597", "Festive Sled Racer Champion Crown", "Mode: [select] only\nShould the bot buy \"Festive Sled Racer Champion Crown\" ?", false),
        new Option<bool>("74598", "Festive Sled Racer Locks and Hat", "Mode: [select] only\nShould the bot buy \"Festive Sled Racer Locks and Hat\" ?", false),
        new Option<bool>("74599", "Festive Sled Racer Locks and Crown", "Mode: [select] only\nShould the bot buy \"Festive Sled Racer Locks and Crown\" ?", false),
        new Option<bool>("74600", "Festive Enamel Snowflakes", "Mode: [select] only\nShould the bot buy \"Festive Enamel Snowflakes\" ?", false),
        new Option<bool>("74602", "Festive Sled Racer Cape", "Mode: [select] only\nShould the bot buy \"Festive Sled Racer Cape\" ?", false),
        new Option<bool>("74603", "Festive Sled Racer Cape and Enamel Snowflakes", "Mode: [select] only\nShould the bot buy \"Festive Sled Racer Cape and Enamel Snowflakes\" ?", false),
        new Option<bool>("74604", "Golden Snowflake Sickle", "Mode: [select] only\nShould the bot buy \"Golden Snowflake Sickle\" ?", false),
        new Option<bool>("74605", "Golden Snowflake Sickles", "Mode: [select] only\nShould the bot buy \"Golden Snowflake Sickles\" ?", false),
        new Option<bool>("74606", "Snowflake Sickle", "Mode: [select] only\nShould the bot buy \"Snowflake Sickle\" ?", false),
        new Option<bool>("74607", "Snowflake Sickles", "Mode: [select] only\nShould the bot buy \"Snowflake Sickles\" ?", false),
        new Option<bool>("74608", "Festive Golden Snowflake Bow", "Mode: [select] only\nShould the bot buy \"Festive Golden Snowflake Bow\" ?", false),
        new Option<bool>("74609", "Mix n' Match Sickles", "Mode: [select] only\nShould the bot buy \"Mix n' Match Sickles\" ?", false),
    };
}
