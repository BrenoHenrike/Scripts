/*
name: Daibh's Merge
description: Farms the materials needed for Daibh's Merge in luck.
tags: luck, seasonal, daibh's merge, shamrock, fair, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DaibhsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreLegion Leg = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Golden Ticket", "Makai Token", "Legion Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("luck", 1565, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Golden Ticket":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Join("luck");
                        Core.SendPackets("%xt%zm%getMapItem%10173%101%");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Makai Token":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Join("luck");
                        Core.SendPackets("%xt%zm%getMapItem%10173%5679%");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Legion Token":
                    Leg.FarmLegionToken(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43097", "Dark Void Monk", "Mode: [select] only\nShould the bot buy \"Dark Void Monk\" ?", false),
        new Option<bool>("43180", "Dark Void Knight", "Mode: [select] only\nShould the bot buy \"Dark Void Knight\" ?", false),
        new Option<bool>("43135", "Legion Fan", "Mode: [select] only\nShould the bot buy \"Legion Fan\" ?", false),
        new Option<bool>("43138", "Dual Legion Fans", "Mode: [select] only\nShould the bot buy \"Dual Legion Fans\" ?", false),
        new Option<bool>("43193", "Void Monk's Flame", "Mode: [select] only\nShould the bot buy \"Void Monk's Flame\" ?", false),
        new Option<bool>("43194", "Void Knight's Blade", "Mode: [select] only\nShould the bot buy \"Void Knight's Blade\" ?", false),
        new Option<bool>("47764", "Dual Lucky Caladbolgs", "Mode: [select] only\nShould the bot buy \"Dual Lucky Caladbolgs\" ?", false),
        new Option<bool>("47765", "Lucky Paragon Pet", "Mode: [select] only\nShould the bot buy \"Lucky Paragon Pet\" ?", false),
        new Option<bool>("47802", "Lucky Paragon Plate", "Mode: [select] only\nShould the bot buy \"Lucky Paragon Plate\" ?", false),
        new Option<bool>("47803", "Lucky Paragon Helm", "Mode: [select] only\nShould the bot buy \"Lucky Paragon Helm\" ?", false),
        new Option<bool>("47804", "Lucky Paragon Cape", "Mode: [select] only\nShould the bot buy \"Lucky Paragon Cape\" ?", false),
    };
}
