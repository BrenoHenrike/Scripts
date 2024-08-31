/*
name: Neotower Merge
description: This bot will farm the items belonging to the selected mode for the Neotower Merge [2474] in /neotower
tags: neotower, merge, neotower, vindicator, assassin, priest, dawn, vindication, grace, texts, beast, tamer, claws
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NeotowerMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreHollowbornStory HB = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Vindicator Crest", "Vindicator Assassin Dirk", "Dawn Vindication Tome", "Dawn Vindication Spellbooks", "Dawn Vindication Grimoires" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        HB.NeoTower();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("neotower", 2474, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                /* MID's & there names:
                12 - Vindicator Assassin
                17 - Vindicator BeastTamer
                28 - Vindicator Priest
                */

                case "Vindicator Crest":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9865);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonsterMapID("neotower", 12, "Vindicated Blades");
                        Core.HuntMonsterMapID("neotower", 17, "Vindicated Chain");
                        Core.HuntMonsterMapID("neotower", 28, "Vindicated Scripture");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Vindicator Assassin Dirk":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonsterMapID("neotower", 12, req.Name, 1, req.Temp);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Dawn Vindication Grimoires":
                case "Dawn Vindication Spellbooks":
                case "Dawn Vindication Tome":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("neotower", "r10", "left", 28, "Vindicated Scripture");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("87953", "Vindicator Assassin", "Mode: [select] only\nShould the bot buy \"Vindicator Assassin\" ?", false),
        new Option<bool>("87954", "Vindicator Assassin Mask", "Mode: [select] only\nShould the bot buy \"Vindicator Assassin Mask\" ?", false),
        new Option<bool>("87957", "Vindicator Assassin Daggers", "Mode: [select] only\nShould the bot buy \"Vindicator Assassin Daggers\" ?", false),
        new Option<bool>("87958", "Vindicator Priest", "Mode: [select] only\nShould the bot buy \"Vindicator Priest\" ?", false),
        new Option<bool>("87959", "Vindicator Priest Mask", "Mode: [select] only\nShould the bot buy \"Vindicator Priest Mask\" ?", false),
        new Option<bool>("87961", "Dawn Vindication Grace Texts", "Mode: [select] only\nShould the bot buy \"Dawn Vindication Grace Texts\" ?", false),
        new Option<bool>("87962", "Vindicator Priest Staff", "Mode: [select] only\nShould the bot buy \"Vindicator Priest Staff\" ?", false),
        new Option<bool>("87964", "Vindicator Beast Tamer", "Mode: [select] only\nShould the bot buy \"Vindicator Beast Tamer\" ?", false),
        new Option<bool>("87965", "Vindicator Beast Tamer Mask", "Mode: [select] only\nShould the bot buy \"Vindicator Beast Tamer Mask\" ?", false),
        new Option<bool>("87966", "Vindicator Beast Tamer Hood", "Mode: [select] only\nShould the bot buy \"Vindicator Beast Tamer Hood\" ?", false),
        new Option<bool>("87967", "Vindicator Beast Tamer Claws", "Mode: [select] only\nShould the bot buy \"Vindicator Beast Tamer Claws\" ?", false),
    };
}
