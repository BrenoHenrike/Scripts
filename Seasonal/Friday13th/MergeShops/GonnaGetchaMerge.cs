/*
name: Gonna Getcha Merge
description: This will get all or selected items on this merge shop.
tags: gonna-getcha-merge, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GonnaGetchaMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private CoreFriday13th F13 = new();
    private static CoreAdvanced sAdv = new();

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
        F13.Gonnagetcha();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("gonnagetcha", 1581, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "GetchaDolla":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6269);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("gonnagetcha", "Vengeful Ghost", "Ghost Gone", 2, log: false);
                        Core.HuntMonster("gonnagetcha", "Shrade Cultist", "Cultist Cleared", 6, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43346", "BoneSaw Judge", "Mode: [select] only\nShould the bot buy \"BoneSaw Judge\" ?", false),
        new Option<bool>("43348", "Hood of Judgement", "Mode: [select] only\nShould the bot buy \"Hood of Judgement\" ?", false),
        new Option<bool>("43351", "BoneSaw's Saw", "Mode: [select] only\nShould the bot buy \"BoneSaw's Saw\" ?", false),
        new Option<bool>("43347", "BoneSaw's Stare", "Mode: [select] only\nShould the bot buy \"BoneSaw's Stare\" ?", false),
        new Option<bool>("43352", "Bobby The Puppet", "Mode: [select] only\nShould the bot buy \"Bobby The Puppet\" ?", false),
        new Option<bool>("43349", "Bobby's Head", "Mode: [select] only\nShould the bot buy \"Bobby's Head\" ?", false),
        new Option<bool>("43350", "Bobby's Hair", "Mode: [select] only\nShould the bot buy \"Bobby's Hair\" ?", false),
        new Option<bool>("43356", "Bobby's Bike", "Mode: [select] only\nShould the bot buy \"Bobby's Bike\" ?", false),
        new Option<bool>("43354", "Bobby on the Back", "Mode: [select] only\nShould the bot buy \"Bobby on the Back\" ?", false),
        new Option<bool>("43358", "Black Knight Flag", "Mode: [select] only\nShould the bot buy \"Black Knight Flag\" ?", false),
        new Option<bool>("43357", "Dark Makai Flag", "Mode: [select] only\nShould the bot buy \"Dark Makai Flag\" ?", false),
        new Option<bool>("43355", "Bobby On Bike", "Mode: [select] only\nShould the bot buy \"Bobby On Bike\" ?", false),
        new Option<bool>("43353", "Bobby's Trike Mace", "Mode: [select] only\nShould the bot buy \"Bobby's Trike Mace\" ?", false),
    };
}
