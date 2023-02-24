/*
name: Love Locked Merge
description: This script farms the items from Love Locked Merge Shop.
tags: seasonal, love, merge, love locked merge, heros heart day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LoveLockedMerge
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
        Core.BankingBlackList.AddRange(new[] { "Love Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("lovelockdown", 1217, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Love Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4814);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("lovelockdown", "Sweeter Fish", "Sweeter Fish Swished", 6, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("38455", "TwillyBall", "Mode: [select] only\nShould the bot buy \"TwillyBall\" ?", false),
        new Option<bool>("38457", "Book Swarm Pet", "Mode: [select] only\nShould the bot buy \"Book Swarm Pet\" ?", false),
        new Option<bool>("38453", "Love Harvester", "Mode: [select] only\nShould the bot buy \"Love Harvester\" ?", false),
        new Option<bool>("38459", "Pa-Love-Din", "Mode: [select] only\nShould the bot buy \"Pa-Love-Din\" ?", false),
        new Option<bool>("38460", "Pa-Love-Din Helm", "Mode: [select] only\nShould the bot buy \"Pa-Love-Din Helm\" ?", false),
        new Option<bool>("38468", "Gilded Hourglass of Love", "Mode: [select] only\nShould the bot buy \"Gilded Hourglass of Love\" ?", false),
        new Option<bool>("38456", "TwigBall", "Mode: [select] only\nShould the bot buy \"TwigBall\" ?", false),
    };
}
