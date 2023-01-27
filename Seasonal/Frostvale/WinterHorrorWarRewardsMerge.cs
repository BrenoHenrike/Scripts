/*
name: Winter Horror War Rewards Merge
description: This will get all or selected items on this merge shop.
tags: winter-horror-war-rewards-merge, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class WinterHorrorWarRewardsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public Frostvale FV = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Grief Medal" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        FV.Howardshill();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("winterhorror", 1953, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Grief Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Adv.BestGear(GearBoost.Elemental);
                    Core.RegisterQuests(7856, 7857);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("winterhorror", "Chillybones|FrostBite", "Monster Gem", 5);
                        Core.HuntMonster("winterhorror", "Chillybones|FrostBite", "Mega Monster Gem", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57917", "Polar Bear Hoodie", "Mode: [select] only\nShould the bot buy \"Polar Bear Hoodie\" ?", false),
        new Option<bool>("57918", "Polar Bear Hood", "Mode: [select] only\nShould the bot buy \"Polar Bear Hood\" ?", false),
        new Option<bool>("57919", "Polar Bear Hood + Locks", "Mode: [select] only\nShould the bot buy \"Polar Bear Hood + Locks\" ?", false),
        new Option<bool>("58116", "Enchanted Hoodie Outfit", "Mode: [select] only\nShould the bot buy \"Enchanted Hoodie Outfit\" ?", false),
        new Option<bool>("58117", "Enchanted Hood + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Hood + Locks\" ?", false),
        new Option<bool>("58118", "Enchanted Hood", "Mode: [select] only\nShould the bot buy \"Enchanted Hood\" ?", false),
        new Option<bool>("58119", "Enchanted Winter Hat + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Winter Hat + Locks\" ?", false),
        new Option<bool>("58120", "Enchanted Winter Hat", "Mode: [select] only\nShould the bot buy \"Enchanted Winter Hat\" ?", false),
        new Option<bool>("58549", "Icy Frostlorn Scythe", "Mode: [select] only\nShould the bot buy \"Icy Frostlorn Scythe\" ?", false),
        new Option<bool>("58545", "Icy Frostlorn Axe", "Mode: [select] only\nShould the bot buy \"Icy Frostlorn Axe\" ?", false),
        new Option<bool>("58547", "Icy Frostlorn Sickle", "Mode: [select] only\nShould the bot buy \"Icy Frostlorn Sickle\" ?", false),
    };
}
