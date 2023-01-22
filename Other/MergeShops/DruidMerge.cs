/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DruidMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
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
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("arcangrove", 2003, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Druid Fabric":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(800, 801);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("arcangrove", "Gorillaphant", "Gorillaphant Tusk", 6);
                        Core.HuntMonster("arcangrove", "Seed Spitter", "Spool of Arcane Thread", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("60882", "Druid of the Lake", "Mode: [select] only\nShould the bot buy \"Druid of the Lake\" ?", false),
        new Option<bool>("60883", "Lake Druid's Locks", "Mode: [select] only\nShould the bot buy \"Lake Druid's Locks\" ?", false),
        new Option<bool>("60884", "Lake Druid's Hair", "Mode: [select] only\nShould the bot buy \"Lake Druid's Hair\" ?", false),
        new Option<bool>("60885", "Lake Druid's Locks + Lily", "Mode: [select] only\nShould the bot buy \"Lake Druid's Locks + Lily\" ?", false),
        new Option<bool>("60886", "Lake Druid's Hair + Lily", "Mode: [select] only\nShould the bot buy \"Lake Druid's Hair + Lily\" ?", false),
        new Option<bool>("60887", "Lake Druid's Flora + Fauna Cape", "Mode: [select] only\nShould the bot buy \"Lake Druid's Flora + Fauna Cape\" ?", false),
        new Option<bool>("60888", "Lake Druid's Fauna Cape", "Mode: [select] only\nShould the bot buy \"Lake Druid's Fauna Cape\" ?", false),
        new Option<bool>("60889", "Lake Druid's Flora Cape", "Mode: [select] only\nShould the bot buy \"Lake Druid's Flora Cape\" ?", false),
        new Option<bool>("60890", "Lake Druid's Harp", "Mode: [select] only\nShould the bot buy \"Lake Druid's Harp\" ?", false),
        new Option<bool>("60891", "Lake Druid's Lamp", "Mode: [select] only\nShould the bot buy \"Lake Druid's Lamp\" ?", false),
        new Option<bool>("60900", "Druid of the Grove", "Mode: [select] only\nShould the bot buy \"Druid of the Grove\" ?", false),
        new Option<bool>("60901", "Grove Druid's Locks + Crown", "Mode: [select] only\nShould the bot buy \"Grove Druid's Locks + Crown\" ?", false),
        new Option<bool>("60902", "Grove Druid's Hair + Crown", "Mode: [select] only\nShould the bot buy \"Grove Druid's Hair + Crown\" ?", false),
        new Option<bool>("60903", "Grove Druid's Horned Locks", "Mode: [select] only\nShould the bot buy \"Grove Druid's Horned Locks\" ?", false),
        new Option<bool>("60904", "Grove Druid's Horned Hair", "Mode: [select] only\nShould the bot buy \"Grove Druid's Horned Hair\" ?", false),
        new Option<bool>("60905", "Grove Druid's Locks", "Mode: [select] only\nShould the bot buy \"Grove Druid's Locks\" ?", false),
        new Option<bool>("60906", "Grove Druid's Hair", "Mode: [select] only\nShould the bot buy \"Grove Druid's Hair\" ?", false),
        new Option<bool>("60907", "Grove Druid's Horns + Fauna Cape", "Mode: [select] only\nShould the bot buy \"Grove Druid's Horns + Fauna Cape\" ?", false),
        new Option<bool>("60908", "Grove Druid's Fauna Cape", "Mode: [select] only\nShould the bot buy \"Grove Druid's Fauna Cape\" ?", false),
        new Option<bool>("60909", "Grove Druid's Horns Cape", "Mode: [select] only\nShould the bot buy \"Grove Druid's Horns Cape\" ?", false),
        new Option<bool>("60910", "Grove Druid's Dagger", "Mode: [select] only\nShould the bot buy \"Grove Druid's Dagger\" ?", false),
        new Option<bool>("60911", "Grove Druid's Staff", "Mode: [select] only\nShould the bot buy \"Grove Druid's Staff\" ?", false),
    };
}
