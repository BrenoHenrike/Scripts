/*
name: Fire War Merge
description: This bot will farm the items belonging to the selected mode for the Fire War Merge [1587] in /firewar
tags: fire, war, merge, firewar, ignited, flame, guardian, guardians, wrap, runed, lance, , shield, accoutrements, dragonslayer, twilly, bank
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FireWarMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private DragonFableOrigins DFO = new();


    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Flame Guardian", "Dragon Flame", "Dragon Eye", "Flame Guardian Helm", "Flame Guardian's Wrap", "Flame Guardian's Blade", "Flame Guardian's Lance", "Flame Guardian's Blade + Shield", "Flame Guardian's Accoutrements" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DFO.GreatFireWar();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("firewar", 1587, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Flame Guardian":
                case "Flame Guardian Helm":
                case "Flame Guardian's Wrap":
                case "Flame Guardian's Blade":
                case "Flame Guardian's Lance":
                case "Flame Guardian's Blade + Shield":
                    Core.FarmingLogger(req.Name, quant);
                    Adv.BuyItem("firewar", 1586, req.Name);
                    break;

                case "Dragon Flame":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(6300);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("firewar", "Fire Dragon", "Fire Dragon Slain", 3);
                        Core.KillMonster("firewar", "r8", "Left", "Inferno Dragon", "Inferno Dragon Slain", 2);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dragon Eye":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("firewar", "Uriax", "Dragon Eye", quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Flame Guardian's Accoutrements":
                    Core.FarmingLogger(req.Name, quant);
                    if (Core.IsMember)
                        Adv.BuyItem("firewar", 1586, "Flame Guardian's Accoutrements");
                    else
                        Core.Logger("Membership is required.");

                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43617", "Ignited Flame Guardian", "Mode: [select] only\nShould the bot buy \"Ignited Flame Guardian\" ?", false),
        new Option<bool>("43618", "Ignited Guardian's Helm", "Mode: [select] only\nShould the bot buy \"Ignited Guardian's Helm\" ?", false),
        new Option<bool>("43644", "Ignited Guardian's Wrap", "Mode: [select] only\nShould the bot buy \"Ignited Guardian's Wrap\" ?", false),
        new Option<bool>("43619", "Ignited Guardian's Runed Wrap", "Mode: [select] only\nShould the bot buy \"Ignited Guardian's Runed Wrap\" ?", false),
        new Option<bool>("43647", "Ignited Guardian's Rune", "Mode: [select] only\nShould the bot buy \"Ignited Guardian's Rune\" ?", false),
        new Option<bool>("43620", "Ignited Guardian's Blade", "Mode: [select] only\nShould the bot buy \"Ignited Guardian's Blade\" ?", false),
        new Option<bool>("43621", "Ignited Guardian Lance", "Mode: [select] only\nShould the bot buy \"Ignited Guardian Lance\" ?", false),
        new Option<bool>("43623", "Ignited Guardian's Blade + Shield", "Mode: [select] only\nShould the bot buy \"Ignited Guardian's Blade + Shield\" ?", false),
        new Option<bool>("43643", "Ignited Guardian's Accoutrements", "Mode: [select] only\nShould the bot buy \"Ignited Guardian's Accoutrements\" ?", false),
        new Option<bool>("43649", "DragonSlayer Twilly Bank", "Mode: [select] only\nShould the bot buy \"DragonSlayer Twilly Bank\" ?", false),
    };
}
