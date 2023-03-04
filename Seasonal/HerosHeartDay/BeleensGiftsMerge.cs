/*
name: Beleen's Gifts
description: Farms the materials needed for Beleen's Gifts Merge in canalshore.
tags: beleen's gifts, merge, love, canalshore, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BeleensGiftsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Beleen's Gratitude", "Townspeople's Affection", "Astice's Claw" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("canalshore", 1833, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Beleen's Gratitude":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (Core.IsMember)
                    {
                        Core.RegisterQuests(7355);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("canalshore", "Fishwing", "Fishwing Defeated", 6, log: false);
                            Core.HuntMonster("canalshore", "MerSiren", "MerSiren Defeated", 8, log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    else
                    {
                        Core.RegisterQuests(7351);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("canalshore", "Water Elemental", "Water Elemental Defeated", 5, log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Townspeople's Affection":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (Core.IsMember)
                    {
                        Core.RegisterQuests(7355);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("canalshore", "Fishwing", "Fishwing Defeated", 6, log: false);
                            Core.HuntMonster("canalshore", "MerSiren", "MerSiren Defeated", 8, log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    else
                    {
                        Core.RegisterQuests(7350);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("canalshore", "Trapped Snack", "Civilian Rescued", 4, log: false);
                            Core.HuntMonster("canalshore", "MerSiren", "MerSiren Defeated", 5, log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Astice's Claw":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    if (Core.IsMember)
                    {
                        Core.RegisterQuests(7356);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("canalshore", "Astice", "Broken Claw", log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    else
                    {
                        Core.RegisterQuests(7352);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("canalshore", "Astice", "Astice Defeated", log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("53228", "Leviathan Warrior", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior\" ?", false),
        new Option<bool>("53229", "Leviathan Warrior's Helm", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior's Helm\" ?", false),
        new Option<bool>("53230", "Leviathan Warrior's Helm + Locks", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior's Helm + Locks\" ?", false),
        new Option<bool>("53231", "Leviathan Warrior's Crown", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior's Crown\" ?", false),
        new Option<bool>("53232", "Leviathan Warrior Morph", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior Morph\" ?", false),
        new Option<bool>("53233", "Leviathan Warrior Morph + Locks", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior Morph + Locks\" ?", false),
        new Option<bool>("53234", "Leviathan Warrior's Back Trident", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior's Back Trident\" ?", false),
        new Option<bool>("53235", "Leviathan Warrior's Trident", "Mode: [select] only\nShould the bot buy \"Leviathan Warrior's Trident\" ?", false),
        new Option<bool>("53236", "Dual Leviathan Warrior Tridents", "Mode: [select] only\nShould the bot buy \"Dual Leviathan Warrior Tridents\" ?", false),
    };
}
