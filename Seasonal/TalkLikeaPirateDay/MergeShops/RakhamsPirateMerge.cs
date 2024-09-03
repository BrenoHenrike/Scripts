/*
name: Rakham's Pirate Merge [Member]
description: This bot will farm the items belonging to the selected mode for the Rakhams Pirate Merge [350] in /pirates
tags: talk-like-a-pirate-day, seasonal, tlapd, rakham, pirate, merge, pirates, blunderbuss, boom, renegade, looter, handcannon, high, seas, captains, cutlass, bandana, looting, tricorn, terror, mermaids, hairpoon, calais, looters, raiment, bladed
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Pirates[Member].cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class RakhamsPirateMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Pirates Pir = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Pack Of Spices", "Gold Ingot", "Bolt Of Silk" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.IsMember)
            return;

        Pir.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("pirates", 350, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Pack Of Spices":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(1550);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("pirates", "Capt. Beard");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Gold Ingot":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(1548);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("pirates", "Undead Pirate");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bolt Of Silk":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(1549);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("pirates", "Undead Pirate");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("10535", "Blunderbuss of Boom", "Mode: [select] only\nShould the bot buy \"Blunderbuss of Boom\" ?", false),
        new Option<bool>("10523", "Pirate Renegade", "Mode: [select] only\nShould the bot buy \"Pirate Renegade\" ?", false),
        new Option<bool>("10524", "Looter", "Mode: [select] only\nShould the bot buy \"Looter\" ?", false),
        new Option<bool>("10534", "HandCannon of the High Seas", "Mode: [select] only\nShould the bot buy \"HandCannon of the High Seas\" ?", false),
        new Option<bool>("10537", "Captain's Cutlass of Boom", "Mode: [select] only\nShould the bot buy \"Captain's Cutlass of Boom\" ?", false),
        new Option<bool>("10539", "Bandana of Looting", "Mode: [select] only\nShould the bot buy \"Bandana of Looting\" ?", false),
        new Option<bool>("10540", "Pirate's Tricorn of Terror", "Mode: [select] only\nShould the bot buy \"Pirate's Tricorn of Terror\" ?", false),
        new Option<bool>("10542", "Mermaid's Hairpoon", "Mode: [select] only\nShould the bot buy \"Mermaid's Hairpoon\" ?", false),
        new Option<bool>("10543", "Cutlass Calais", "Mode: [select] only\nShould the bot buy \"Cutlass Calais\" ?", false),
        new Option<bool>("10545", "Looter's Raiment", "Mode: [select] only\nShould the bot buy \"Looter's Raiment\" ?", false),
        new Option<bool>("10536", "Bladed Blunderbuss of Boom", "Mode: [select] only\nShould the bot buy \"Bladed Blunderbuss of Boom\" ?", false),
    };
}
