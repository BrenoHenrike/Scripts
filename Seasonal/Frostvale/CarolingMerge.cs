/*
name: Caroling Merge
description: This will get all or selected items on this merge shop.
tags: caroling-merge, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CarolingMerge
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
        Core.BankingBlackList.AddRange(new[] { "Red Ribbon", "Wrapping Paper", "Icy Fur", "Silver Tinsel" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("carolinn", 2197, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                case "Silver Tinsel":
                case "Red Ribbon":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        // Core.HuntMonster("caroling", "Frostval Tree", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                        Core.Join("caroling");
                        for (int i = 0; i <= 5; i++)
                        {
                            Bot.Kill.Monster("Frostval Tree");
                            Bot.Sleep(Core.ActionDelay);
                            Bot.Wait.ForPickup(req.Name);
                            if (i == 5)
                            {
                                Core.Join("carolinn-100000");
                                Bot.Sleep(1500);
                            }
                        }
                    }
                    break;

                case "Icy Fur":
                case "Wrapping Paper":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("carolinn", "Krumpet", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("75005", "Midwinter Cheermaker", "Mode: [select] only\nShould the bot buy \"Midwinter Cheermaker\" ?", false),
        new Option<bool>("75006", "Midwinter Cutie Hat", "Mode: [select] only\nShould the bot buy \"Midwinter Cutie Hat\" ?", false),
        new Option<bool>("75007", "Midwinter Cutie Hat and Locks", "Mode: [select] only\nShould the bot buy \"Midwinter Cutie Hat and Locks\" ?", false),
        new Option<bool>("75008", "Midwinter Antlered Hat", "Mode: [select] only\nShould the bot buy \"Midwinter Antlered Hat\" ?", false),
        new Option<bool>("75009", "Midwinter Antlered Hat and Locks", "Mode: [select] only\nShould the bot buy \"Midwinter Antlered Hat and Locks\" ?", false),
        new Option<bool>("75010", "Midwinter Beanie", "Mode: [select] only\nShould the bot buy \"Midwinter Beanie\" ?", false),
        new Option<bool>("75011", "Midwinter Beanie and Locks", "Mode: [select] only\nShould the bot buy \"Midwinter Beanie and Locks\" ?", false),
        new Option<bool>("75012", "Midwinter Snowflakes", "Mode: [select] only\nShould the bot buy \"Midwinter Snowflakes\" ?", false),
        new Option<bool>("75013", "Midwinter Cutie Cape", "Mode: [select] only\nShould the bot buy \"Midwinter Cutie Cape\" ?", false),
        new Option<bool>("75014", "Midwinter Snowy Nimbo", "Mode: [select] only\nShould the bot buy \"Midwinter Snowy Nimbo\" ?", false),
        new Option<bool>("75015", "Licorice Candy Cane", "Mode: [select] only\nShould the bot buy \"Licorice Candy Cane\" ?", false),
        new Option<bool>("75016", "Darkwood Carved Armaments", "Mode: [select] only\nShould the bot buy \"Darkwood Carved Armaments\" ?", false),
        new Option<bool>("73885", "Northlands Paladin", "Mode: [select] only\nShould the bot buy \"Northlands Paladin\" ?", false),
        new Option<bool>("73887", "Frozen Paladin's Helm", "Mode: [select] only\nShould the bot buy \"Frozen Paladin's Helm\" ?", false),
        new Option<bool>("73890", "Glacial Light of Destiny", "Mode: [select] only\nShould the bot buy \"Glacial Light of Destiny\" ?", false),
    };
}
