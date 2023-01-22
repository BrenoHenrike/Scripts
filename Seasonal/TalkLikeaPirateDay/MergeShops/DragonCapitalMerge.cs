/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DragonCapitalStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DragonPirateStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DragonCapitalMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public DragonCapitalStory DCS = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dragon King’s Favor", "Regal Pirate Fleet", "Regal Pirate Leggings", "Regal Pirate's Hat", "Regal Pirate's Accessories", "Regal Pirate's Hat + Locks", "Regal Pirate's Accessories + Locks", "Regal Pirate's Wheel", "Regal Pirate's Rapier", "Regal Pirate's Accoutrements", "Formal Pirate Fleet", "Formal Pirate Leggings", "Regal Pirate's Cape + Wheel " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DCS.DragonCapital();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dragoncapital", 2053, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dragon King’s Favor":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8288);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dragoncapital", "Titan Leech", "Titan Leftovers Defeated", 6);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Regal Pirate Fleet":
                case "Regal Pirate Leggings":
                case "Regal Pirate's Hat":
                case "Regal Pirate's Accessories":
                case "Regal Pirate's Hat + Locks":
                case "Regal Pirate's Accessories + Locks":
                case "Regal Pirate's Rapier":
                case "Regal Pirate's Accoutrements":
                case "Formal Pirate Fleet":
                case "Formal Pirate Leggings":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dragoncapital", "Leviathanius", req.Name, isTemp: false);
                    break;
                case "Regal Pirate's Wheel":
                case "Regal Pirate's Cape + Wheel":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dragoncapital", "Empowered Scalebeard", req.Name, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("62726", "Diabolical Admiral", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral\" ?", false),
        new Option<bool>("62727", "Diabolical Officer", "Mode: [select] only\nShould the bot buy \"Diabolical Officer\" ?", false),
        new Option<bool>("62728", "Diabolical Admiral's Cap", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Cap\" ?", false),
        new Option<bool>("62729", "Diabolical Admiral's Cap + Locks", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Cap + Locks\" ?", false),
        new Option<bool>("62730", "Diabolical Admiral's Cap + Visor", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Cap + Visor\" ?", false),
        new Option<bool>("62731", "Diabolical Admiral's Cap Locks + Visor", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Cap Locks + Visor\" ?", false),
        new Option<bool>("62732", "Diabolical Admiral's Locks", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Locks\" ?", false),
        new Option<bool>("62733", "Diabolical Admiral's Locks + Visor", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Locks + Visor\" ?", false),
        new Option<bool>("62734", "Diabolical Admiral's Shag", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Shag\" ?", false),
        new Option<bool>("62735", "Diabolical Admiral's Shag + Visor", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Shag + Visor\" ?", false),
        new Option<bool>("62736", "Admiral's Eldritch Arms", "Mode: [select] only\nShould the bot buy \"Admiral's Eldritch Arms\" ?", false),
        new Option<bool>("62737", "Diabolical Admiral's Cloak", "Mode: [select] only\nShould the bot buy \"Diabolical Admiral's Cloak\" ?", false),
        new Option<bool>("62738", "Diabolical Cloak + Arms", "Mode: [select] only\nShould the bot buy \"Diabolical Cloak + Arms\" ?", false),
        new Option<bool>("62739", "Diabolical Cloak + Tide", "Mode: [select] only\nShould the bot buy \"Diabolical Cloak + Tide\" ?", false),
        new Option<bool>("62740", "Diabolical Tide", "Mode: [select] only\nShould the bot buy \"Diabolical Tide\" ?", false),
        new Option<bool>("62741", "Diabolical Telescope", "Mode: [select] only\nShould the bot buy \"Diabolical Telescope\" ?", false),
        new Option<bool>("62742", "Sheathed Diabolical Sword", "Mode: [select] only\nShould the bot buy \"Sheathed Diabolical Sword\" ?", false),
        new Option<bool>("62743", "Diabolical Flare Gun", "Mode: [select] only\nShould the bot buy \"Diabolical Flare Gun\" ?", false),
        new Option<bool>("62744", "Diabolical Cane Sword", "Mode: [select] only\nShould the bot buy \"Diabolical Cane Sword\" ?", false),
        new Option<bool>("62746", "Diabolical Sword", "Mode: [select] only\nShould the bot buy \"Diabolical Sword\" ?", false),
        new Option<bool>("62860", "Enchanted Regal Pirate", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate\" ?", false),
        new Option<bool>("62861", "Enchanted Regal Pirate Leggings", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate Leggings\" ?", false),
        new Option<bool>("62864", "Enchanted Regal Pirate Hat", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate Hat\" ?", false),
        new Option<bool>("62865", "Enchanted Regal Pirate Accessories", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate Accessories\" ?", false),
        new Option<bool>("62866", "Formal Pirate Eyepatch", "Mode: [select] only\nShould the bot buy \"Formal Pirate Eyepatch\" ?", false),
        new Option<bool>("62867", "Enchanted Formal Pirate Accessories", "Mode: [select] only\nShould the bot buy \"Enchanted Formal Pirate Accessories\" ?", false),
        new Option<bool>("62868", "Enchanted Regal Pirate Hat + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate Hat + Locks\" ?", false),
        new Option<bool>("62869", "Formal Pirate Eyepatch + Locks", "Mode: [select] only\nShould the bot buy \"Formal Pirate Eyepatch + Locks\" ?", false),
        new Option<bool>("63785", "Enchanted Formal Pirate Hat + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Formal Pirate Hat + Locks\" ?", false),
        new Option<bool>("63786", "Enchanted Formal Accessories + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Formal Accessories + Locks\" ?", false),
        new Option<bool>("63787", "Enchanted Formal Pirate Hat", "Mode: [select] only\nShould the bot buy \"Enchanted Formal Pirate Hat\" ?", false),
        new Option<bool>("63788", "Enchanted Regal Accessories + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Accessories + Locks\" ?", false),
        new Option<bool>("62870", "Formal Pirate Fleet Cape", "Mode: [select] only\nShould the bot buy \"Formal Pirate Fleet Cape\" ?", false),
        new Option<bool>("62871", "Enchanted Formal Pirate Cape + Wheel", "Mode: [select] only\nShould the bot buy \"Enchanted Formal Pirate Cape + Wheel\" ?", false),
        new Option<bool>("62872", "Enchanted Pirate's Ship Wheel", "Mode: [select] only\nShould the bot buy \"Enchanted Pirate's Ship Wheel\" ?", false),
        new Option<bool>("62873", "Enchanted Regal Pirate Rapier", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate Rapier\" ?", false),
        new Option<bool>("62874", "Enchanted Regal Pirate Accoutrements", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate Accoutrements\" ?", false),
        new Option<bool>("63781", "Enchanted Formal Pirate", "Mode: [select] only\nShould the bot buy \"Enchanted Formal Pirate\" ?", false),
        new Option<bool>("63782", "Enchanted Formal Pirate Leggings", "Mode: [select] only\nShould the bot buy \"Enchanted Formal Pirate Leggings\" ?", false),
        new Option<bool>("63783", "Enchanted Regal Pirate Cape + Wheel", "Mode: [select] only\nShould the bot buy \"Enchanted Regal Pirate Cape + Wheel\" ?", false),
    };
}
