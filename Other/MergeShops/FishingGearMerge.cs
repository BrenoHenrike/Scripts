/*
name: Fishing Gear Merge
description: This bot will farm all/selected/allAC items or materials for the Fishing Gear Merge shop in /greenguardwest
tags: fishing, chips, gear, greenguard, west, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FishingGearMerge
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
        Core.BankingBlackList.AddRange(new[] { "Fishin' Chips" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("greenguardwest", 363, findIngredients, buyOnlyThis, buyMode: buyMode);
        Core.TrashCan("Fishing Bait", "Fishing Dynamite");

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

                case "Fishin' Chips":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);

                    if (Farm.FactionRank("Fishing") <= 1)
                    {
                        Core.EnsureAccept(1682);
                        Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);
                        Core.EnsureComplete(1682);
                    }

                    bool legendDailyDone = Core.IsMember ? Bot.Quests.IsDailyComplete(1684) : true;
                    bool nonLegendDailyDone = Bot.Quests.IsDailyComplete(1683);
                    if (!legendDailyDone)
                        Core.EnsureAccept(1684);
                    if (!nonLegendDailyDone)
                        Core.EnsureComplete(1683);

                    Core.RegisterQuests(1682, 1614, 1615);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (Farm.FactionRank("Fishing") < 2)
                        {
                            Core.KillMonster("greenguardwest", "West3", "Right", "Frogzard", "Fishing Bait", 20, false, false);
                            Core.Join("fishing");

                            while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Core.CheckInventory("Fishing Bait"))
                            {
                                Bot.Send.Packet("%xt%zm%FishCast%1%Net%30%");
                                Bot.Sleep(10000);
                            }
                        }
                        else
                        {
                            Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Fishing Dynamite", 20, false, false);
                            Core.Join("fishing");

                            while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Core.CheckInventory("Fishing Dynamite"))
                            {
                                Bot.Send.Packet($"%xt%zm%FishCast%1%Dynamite%30%");
                                Bot.Sleep(3500);
                                Core.SendPackets("%xt%zm%getFish%1%false%");
                            }
                        }

                        while (!Bot.ShouldExit && Bot.TempInv.Contains("Fish Caught", 30))
                        {
                            Core.HuntMonster("swordhaven", "Slime", "Slime Sauce", 1, log: false);
                            Bot.Sleep(Core.ActionDelay);
                        }
                        while (!Bot.ShouldExit && Bot.TempInv.Contains("Endangered Fish", 5))
                        {
                            Core.HuntMonster("nexus", "Frogzard", "Greenguard Seal", 1, log: false);
                            Bot.Sleep(Core.ActionDelay);
                        }

                        if (!legendDailyDone && Bot.Quests.CanCompleteFullCheck(1684))
                        {
                            Core.EnsureComplete(1684);
                            legendDailyDone = true;
                        }
                        if (!nonLegendDailyDone && Bot.Quests.CanCompleteFullCheck(1683))
                        {
                            Core.EnsureComplete(1683);
                            nonLegendDailyDone = true;
                        }
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("9916", "The Super Pole", "Mode: [select] only\nShould the bot buy \"The Super Pole\" ?", false),
        new Option<bool>("10903", "Crabcake Hat", "Mode: [select] only\nShould the bot buy \"Crabcake Hat\" ?", false),
        new Option<bool>("10904", "Fishing Hat", "Mode: [select] only\nShould the bot buy \"Fishing Hat\" ?", false),
        new Option<bool>("10905", "Yaga Hatfish Hat", "Mode: [select] only\nShould the bot buy \"Yaga Hatfish Hat\" ?", false),
        new Option<bool>("10906", "Fishing Rod Backstrap", "Mode: [select] only\nShould the bot buy \"Fishing Rod Backstrap\" ?", false),
        new Option<bool>("10907", "Shrimp Axe", "Mode: [select] only\nShould the bot buy \"Shrimp Axe\" ?", false),
        new Option<bool>("10908", "Fishin' Hooks", "Mode: [select] only\nShould the bot buy \"Fishin' Hooks\" ?", false),
        new Option<bool>("10909", "Fishy Sword &amp; Shield", "Mode: [select] only\nShould the bot buy \"Fishy Sword &amp; Shield\" ?", false),
        new Option<bool>("10910", "Old Boot", "Mode: [select] only\nShould the bot buy \"Old Boot\" ?", false),
        new Option<bool>("10911", "Tackle Box", "Mode: [select] only\nShould the bot buy \"Tackle Box\" ?", false),
        new Option<bool>("10912", "Fishin' Rod", "Mode: [select] only\nShould the bot buy \"Fishin' Rod\" ?", false),
        new Option<bool>("11087", "Fish Suit", "Mode: [select] only\nShould the bot buy \"Fish Suit\" ?", false),
        new Option<bool>("11089", "Fish Tail", "Mode: [select] only\nShould the bot buy \"Fish Tail\" ?", false),
        new Option<bool>("11090", "Floppy Fish", "Mode: [select] only\nShould the bot buy \"Floppy Fish\" ?", false),
        new Option<bool>("11091", "Mystery Floppy Fish", "Mode: [select] only\nShould the bot buy \"Mystery Floppy Fish\" ?", false),
        new Option<bool>("11092", "Gaping Fish Head", "Mode: [select] only\nShould the bot buy \"Gaping Fish Head\" ?", false),
        new Option<bool>("11093", "Hillbilly Overalls", "Mode: [select] only\nShould the bot buy \"Hillbilly Overalls\" ?", false),
        new Option<bool>("11097", "Fisherman's Hat", "Mode: [select] only\nShould the bot buy \"Fisherman's Hat\" ?", false),
        new Option<bool>("11098", "Fisherwoman's Hat", "Mode: [select] only\nShould the bot buy \"Fisherwoman's Hat\" ?", false),
        new Option<bool>("11102", "Xtreme Fishing Jersey", "Mode: [select] only\nShould the bot buy \"Xtreme Fishing Jersey\" ?", false),
        new Option<bool>("11133", "Fisher Cape", "Mode: [select] only\nShould the bot buy \"Fisher Cape\" ?", false),
        new Option<bool>("11135", "Fishin' for Food", "Mode: [select] only\nShould the bot buy \"Fishin' for Food\" ?", false),
        new Option<bool>("50724", "Lure of Order", "Mode: [select] only\nShould the bot buy \"Lure of Order\" ?", false),
    };
}
