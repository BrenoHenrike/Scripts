/*
name: Felixs Gilded Gear Merge
description: This bot will farm the items belonging to the selected mode for the Felixs Gilded Gear Merge [2446] in /castleeblana
tags: felixs, gilded, gear, merge, castleeblana, gold, voucher, k, skye, warrior, warriors, cloak, speirling, warden, east, gaheris
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FelixsGildedGearMerge
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
        Core.BankingBlackList.AddRange(new[] { "Salvaged Skye Armament", "Liquid Gold Solution" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("castleeblana", 2446, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Salvaged Skye Armament":
                    Core.FarmingLogger(req.Name, quant);
                    Core.KillMonster("castleeblana", "r2", "Left", "Skye Warrior", req.Name, req.Quantity, req.Temp, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Liquid Gold Solution":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(9742);
                        Core.EquipClass(ClassType.Farm);
                        Core.KillMonster("castleeblana", "r6", "Left", "*", "Gorta's Soul", 12, log: false);
                        Core.KillMonster("castleeblana", "r5", "Left", "*", "Raven's Bauble", 12, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.KillMonster("castleeblana", "r10", "Left", "Warden Indradeep", "Rainfall Inscription", log: false);
                        Core.EnsureComplete(9742);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("85901", "Skye Warrior", "Mode: [select] only\nShould the bot buy \"Skye Warrior\" ?", false),
        new Option<bool>("85902", "Skye Warrior Mask", "Mode: [select] only\nShould the bot buy \"Skye Warrior Mask\" ?", false),
        new Option<bool>("85903", "Skye Warrior's Cloak", "Mode: [select] only\nShould the bot buy \"Skye Warrior's Cloak\" ?", false),
        new Option<bool>("85904", "Speirling Blade", "Mode: [select] only\nShould the bot buy \"Speirling Blade\" ?", false),
        new Option<bool>("85905", "Speirling Blades", "Mode: [select] only\nShould the bot buy \"Speirling Blades\" ?", false),
        new Option<bool>("85906", "Skye Warden of the East", "Mode: [select] only\nShould the bot buy \"Skye Warden of the East\" ?", false),
        new Option<bool>("85907", "Skye Warden of the East Mask", "Mode: [select] only\nShould the bot buy \"Skye Warden of the East Mask\" ?", false),
        new Option<bool>("85908", "Skye Warden of the East Cape", "Mode: [select] only\nShould the bot buy \"Skye Warden of the East Cape\" ?", false),
        new Option<bool>("85909", "Speirling Gaheris Blade", "Mode: [select] only\nShould the bot buy \"Speirling Gaheris Blade\" ?", false),
        new Option<bool>("85910", "Speirling Gaheris Blades", "Mode: [select] only\nShould the bot buy \"Speirling Gaheris Blades\" ?", false),
        new Option<bool>("86152", "Skye Warrior Hood", "Mode: [select] only\nShould the bot buy \"Skye Warrior Hood\" ?", false),
        new Option<bool>("86153", "Skye Warden Hood", "Mode: [select] only\nShould the bot buy \"Skye Warden Hood\" ?", false),
    };
}
