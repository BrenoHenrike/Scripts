/*
name: leZard Man Merge
description: This bot will farm the items belonging to the selected mode for the leZard Man Merge [2428] in /battleon
tags: lezard, man, merge, battleon, void, stareater, blackout, stareaters, morph, ravenous, grin, flare, tusk, tusks, tail, tentacles, total, solar, eclipse, chibi, buddy, collapse, knights, glasses, , adventurers, double, puffs
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class leZardManMerge
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
        Core.BankingBlackList.AddRange(new[] { "Cosmic Dust", "Cosmic Aura" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battleon", 2428, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Cosmic Dust":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9678);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.KillMonster("starsinc", "r2", "Left", "Living Star", "Star Dust", 30);
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

                case "Cosmic Aura":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9679);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                    Core.EquipClass(ClassType.Solo);
                        Core.HuntMonsterMapID("whitehole", 49, "Vortex Essence", 12);
                    Core.EquipClass(ClassType.Farm);
                        Core.KillMonster("dreadspace", "r16", "Left", "Trobble", "Star Scrap Metal", 10);
                        Core.HuntMonster("blackholesun", "Black Light Elemental", "Black Light Aura", 7);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("83480", "Void Star-Eater", "Mode: [select] only\nShould the bot buy \"Void Star-Eater\" ?", false),
        new Option<bool>("85305", "BlackOut Armor", "Mode: [select] only\nShould the bot buy \"BlackOut Armor\" ?", false),
        new Option<bool>("83481", "Star-Eater's Morph", "Mode: [select] only\nShould the bot buy \"Star-Eater's Morph\" ?", false),
        new Option<bool>("83482", "Star-Eater's Ravenous Grin Morph", "Mode: [select] only\nShould the bot buy \"Star-Eater's Ravenous Grin Morph\" ?", false),
        new Option<bool>("85306", "BlackOut Morph", "Mode: [select] only\nShould the bot buy \"BlackOut Morph\" ?", false),
        new Option<bool>("83488", "Star-Eater's Void Flare", "Mode: [select] only\nShould the bot buy \"Star-Eater's Void Flare\" ?", false),
        new Option<bool>("83489", "Star-Eater's Tusk", "Mode: [select] only\nShould the bot buy \"Star-Eater's Tusk\" ?", false),
        new Option<bool>("83490", "Star-Eater's Tusks", "Mode: [select] only\nShould the bot buy \"Star-Eater's Tusks\" ?", false),
        new Option<bool>("83483", "Star-Eater's Tail", "Mode: [select] only\nShould the bot buy \"Star-Eater's Tail\" ?", false),
        new Option<bool>("83484", "Star-Eater's Tentacles", "Mode: [select] only\nShould the bot buy \"Star-Eater's Tentacles\" ?", false),
        new Option<bool>("83485", "Star-Eater's Tail and Tentacles", "Mode: [select] only\nShould the bot buy \"Star-Eater's Tail and Tentacles\" ?", false),
        new Option<bool>("85307", "Total Solar Eclipse", "Mode: [select] only\nShould the bot buy \"Total Solar Eclipse\" ?", false),
        new Option<bool>("83487", "Star-Eater's Chibi Buddy", "Mode: [select] only\nShould the bot buy \"Star-Eater's Chibi Buddy\" ?", false),
        new Option<bool>("83486", "Star-Eater's Void Collapse", "Mode: [select] only\nShould the bot buy \"Star-Eater's Void Collapse\" ?", false),
        new Option<bool>("85321", "Knight's Total Eclipse Solar Glasses", "Mode: [select] only\nShould the bot buy \"Knight's Total Eclipse Solar Glasses\" ?", false),
        new Option<bool>("85320", "Knight's Total Eclipse Solar Glasses + Locks", "Mode: [select] only\nShould the bot buy \"Knight's Total Eclipse Solar Glasses + Locks\" ?", false),
        new Option<bool>("85319", "Adventurer's Total Eclipse Solar Glasses", "Mode: [select] only\nShould the bot buy \"Adventurer's Total Eclipse Solar Glasses\" ?", false),
        new Option<bool>("85322", "Total Eclipse Solar Glasses + Double Puffs", "Mode: [select] only\nShould the bot buy \"Total Eclipse Solar Glasses + Double Puffs\" ?", false),
    };
}
