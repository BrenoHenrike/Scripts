/*
name: Harvest Day Parade Merge
description: This will get all or selected items on this merge shop.
tags: harvest-day-parade-merge, seasonal, harvest-day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HarvestDayParadeMerge
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
        Core.BankingBlackList.AddRange(new[] { "Pink Balloon Scrap", "Green Balloon Scrap", "Red Balloon Scrap " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("float"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("float", 234, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Pink Balloon Scrap":
                case "Green Balloon Scrap":
                case "Red Balloon Scrap":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("float", "Beleen Balloon", req.Name, quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("32295", "Pilgrim Warrior", "Mode: [select] only\nShould the bot buy \"Pilgrim Warrior\" ?", false),
        new Option<bool>("32296", "Pilgrim Wizard", "Mode: [select] only\nShould the bot buy \"Pilgrim Wizard\" ?", false),
        new Option<bool>("32292", "Settler's Greatsword", "Mode: [select] only\nShould the bot buy \"Settler's Greatsword\" ?", false),
        new Option<bool>("32293", "Settler's Longsword", "Mode: [select] only\nShould the bot buy \"Settler's Longsword\" ?", false),
        new Option<bool>("32299", "Turdraken Trophy Sword", "Mode: [select] only\nShould the bot buy \"Turdraken Trophy Sword\" ?", false),
        new Option<bool>("32300", "Shield and Spear o' Plenty", "Mode: [select] only\nShould the bot buy \"Shield and Spear o' Plenty\" ?", false),
        new Option<bool>("32301", "Turdraken Scale Sword", "Mode: [select] only\nShould the bot buy \"Turdraken Scale Sword\" ?", false),
        new Option<bool>("32302", "Fallen Leaf Sword", "Mode: [select] only\nShould the bot buy \"Fallen Leaf Sword\" ?", false),
        new Option<bool>("32382", "Staff of Thanks", "Mode: [select] only\nShould the bot buy \"Staff of Thanks\" ?", false),
        new Option<bool>("32297", "Harvest Sword and Shield", "Mode: [select] only\nShould the bot buy \"Harvest Sword and Shield\" ?", false),
    };
}
