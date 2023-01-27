/*
name: Shadow Dragon Shinobi Merge
description: This will get all or selected items on this merge shop.
tags: shadow-dragon-shinobi-merge, black-friday, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowDragonShinobiMerge
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
        Core.BankingBlackList.AddRange(new[] { "Dragon Shinobi Token " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        if (!Core.isSeasonalMapActive("blackfridaywar"))
        {
            Core.Logger("This merge shop is seasonal only.");
            return;
        }

        Adv.StartBuyAllMerge("blackfridaywar", 756, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dragon Shinobi Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    if (Bot.Quests.IsAvailable(7924))
                    {
                        Core.RegisterQuests(7924);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("shadowfortress", "1st Head Of Orochi", "Perfect Orochi Scales", 10, false, false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    else
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.RegisterQuests(7815);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("blackfridaywar", "Deal Bot 2.0", "EbilCorp Bots Battled", 20, log: false);
                            Core.HuntMonsterMapID("blackfridaywar", 3, "EbilCorp Shoppers Saved", 20, log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }

                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57723", "Shadow Dragon Shinobi", "Mode: [select] only\nShould the bot buy \"Shadow Dragon Shinobi\" ?", false),
        new Option<bool>("57719", "Shadow Dragon Shinobi Hood", "Mode: [select] only\nShould the bot buy \"Shadow Dragon Shinobi Hood\" ?", false),
        new Option<bool>("57720", "Shadow Dragon Shinobi Guard", "Mode: [select] only\nShould the bot buy \"Shadow Dragon Shinobi Guard\" ?", false),
        new Option<bool>("57722", "Fury of the Shadow Dragon", "Mode: [select] only\nShould the bot buy \"Fury of the Shadow Dragon\" ?", false),
        new Option<bool>("57721", "Wrath of the Shadow Dragon", "Mode: [select] only\nShould the bot buy \"Wrath of the Shadow Dragon\" ?", false),
        new Option<bool>("57718", "Shadow Dragon Shinobi Armor", "Mode: [select] only\nShould the bot buy \"Shadow Dragon Shinobi Armor\" ?", false),
    };
}
