/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/SolarNewYear/WaterWar.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class WaterWarMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreStory Story = new CoreStory();
    private WaterWar WW = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Water Drop", "Solar Badge" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        WW.StoryLine();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("waterwar", 1711, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Water Drop":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6814, 6816);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("WaterWar", "Solar Elemental");
                    Core.CancelRegisteredQuests();
                    break;

                case "Solar Badge":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("WaterWar", "Aloe", req.Name, quant, false);

                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48097", "Yaksha and Kinnaree", "Mode: [select] only\nShould the bot buy \"Yaksha and Kinnaree\" ?", false),
        new Option<bool>("48101", "Yaksha Hair", "Mode: [select] only\nShould the bot buy \"Yaksha Hair\" ?", false),
        new Option<bool>("48102", "Yaksha Mask", "Mode: [select] only\nShould the bot buy \"Yaksha Mask\" ?", false),
        new Option<bool>("48107", "Kinnaree Locks", "Mode: [select] only\nShould the bot buy \"Kinnaree Locks\" ?", false),
        new Option<bool>("48100", "Yaksa Tapod", "Mode: [select] only\nShould the bot buy \"Yaksa Tapod\" ?", false),
        new Option<bool>("48099", "Kinnaree Kris", "Mode: [select] only\nShould the bot buy \"Kinnaree Kris\" ?", false),
        new Option<bool>("48098", "Kinnaree Wings", "Mode: [select] only\nShould the bot buy \"Kinnaree Wings\" ?", false),
        new Option<bool>("48104", "Water War Hair", "Mode: [select] only\nShould the bot buy \"Water War Hair\" ?", false),
        new Option<bool>("48103", "Water War Locks", "Mode: [select] only\nShould the bot buy \"Water War Locks\" ?", false),
        new Option<bool>("48112", "Mud Spurter", "Mode: [select] only\nShould the bot buy \"Mud Spurter\" ?", false),
        new Option<bool>("48110", "Super Squirter", "Mode: [select] only\nShould the bot buy \"Super Squirter\" ?", false),
    };
}
