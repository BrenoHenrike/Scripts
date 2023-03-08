/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LuckdragonMerge
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
        Core.BankingBlackList.AddRange(new[] { "Golden Coupon" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("luckdragon"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("luckdragon", 1233, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Golden Coupon":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonsterMapID("luckdragon", 2, req.Name, quant, isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("38875", "Lucky Looter", "Mode: [select] only\nShould the bot buy \"Lucky Looter\" ?", false),
        new Option<bool>("38876", "Sneaky Lucky Looter Hair", "Mode: [select] only\nShould the bot buy \"Sneaky Lucky Looter Hair\" ?", false),
        new Option<bool>("38877", "Sneaky Lucky Looter Locks", "Mode: [select] only\nShould the bot buy \"Sneaky Lucky Looter Locks\" ?", false),
        new Option<bool>("38878", "Lucky Boy Hair", "Mode: [select] only\nShould the bot buy \"Lucky Boy Hair\" ?", false),
        new Option<bool>("38879", "Lucky Girl Locks", "Mode: [select] only\nShould the bot buy \"Lucky Girl Locks\" ?", false),
        new Option<bool>("38880", "Gold Sack", "Mode: [select] only\nShould the bot buy \"Gold Sack\" ?", false),
        new Option<bool>("38881", "Gold Sack on your Back", "Mode: [select] only\nShould the bot buy \"Gold Sack on your Back\" ?", false),
    };
}
