/*
name: Oddities Merge
description: This will get all or selected items on this merge shop.
tags: oddities-merge, friday the 13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class OdditiesMerge
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

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Cursed Doll Tassel", "Odd Coin", "Ectoplasmic Token" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!F13.Friday13thCheck("Oddities Merge"))
            return;

        F13.Oddities();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("oddities", 2134, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Cursed Doll Tassel":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(8667);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.KillMonster("oddities", "Enter", "Spawn", "*", "Chipped Wood", 7, log: false);
                        Core.KillMonster("oddities", "r6", "Left", "*", "Fuzz Tuff", 7, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("oddities", "Cursed Spirit", "Doll Eyes", 7, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Odd Coin":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8674);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("oddities", "r6", "Left", "*", "Frankensteined Teddy", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ectoplasmic Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8675);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("oddities", "Cursed Spirit", "Doll Eye", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70165", "Mysterious Johnson", "Mode: [select] only\nShould the bot buy \"Mysterious Johnson\" ?", false),
        new Option<bool>("70166", "Mysterious Johnson's Hat", "Mode: [select] only\nShould the bot buy \"Mysterious Johnson's Hat\" ?", false),
        new Option<bool>("70167", "Mysterious Johnson's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Mysterious Johnson's Hat + Locks\" ?", false),
        new Option<bool>("70168", "Magical Johnson", "Mode: [select] only\nShould the bot buy \"Magical Johnson\" ?", false),
        new Option<bool>("70169", "Magical Johnson's Hat", "Mode: [select] only\nShould the bot buy \"Magical Johnson's Hat\" ?", false),
        new Option<bool>("70170", "Magical Johnson's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Magical Johnson's Hat + Locks\" ?", false),
        new Option<bool>("70131", "Neo Necromantress", "Mode: [select] only\nShould the bot buy \"Neo Necromantress\" ?", false),
        new Option<bool>("70132", "Neo Necromantress' Braid", "Mode: [select] only\nShould the bot buy \"Neo Necromantress' Braid\" ?", false),
        new Option<bool>("70133", "Neo Necromantress' Fur Cloak", "Mode: [select] only\nShould the bot buy \"Neo Necromantress' Fur Cloak\" ?", false),
        new Option<bool>("70134", "Sally's Plushie", "Mode: [select] only\nShould the bot buy \"Sally's Plushie\" ?", false),
        new Option<bool>("70127", "Gothic Thief", "Mode: [select] only\nShould the bot buy \"Gothic Thief\" ?", false),
        new Option<bool>("70128", "Gothic Thief's Hair", "Mode: [select] only\nShould the bot buy \"Gothic Thief's Hair\" ?", false),
        new Option<bool>("70129", "Gothic Thief's Locks", "Mode: [select] only\nShould the bot buy \"Gothic Thief's Locks\" ?", false),
        new Option<bool>("70130", "Gothic Thief's Wrap", "Mode: [select] only\nShould the bot buy \"Gothic Thief's Wrap\" ?", false),
        new Option<bool>("57579", "Dread Spirit Hunter", "Mode: [select] only\nShould the bot buy \"Dread Spirit Hunter\" ?", false),
        new Option<bool>("57580", "Spirit Hunter Hat", "Mode: [select] only\nShould the bot buy \"Spirit Hunter Hat\" ?", false),
        new Option<bool>("57581", "Spirit Hunter Hat + Locks", "Mode: [select] only\nShould the bot buy \"Spirit Hunter Hat + Locks\" ?", false),
        new Option<bool>("57582", "Spirit Hunter Hood", "Mode: [select] only\nShould the bot buy \"Spirit Hunter Hood\" ?", false),
        new Option<bool>("57583", "Ancient Chains of Binding", "Mode: [select] only\nShould the bot buy \"Ancient Chains of Binding\" ?", false),
        new Option<bool>("57584", "Slasher of Wrath", "Mode: [select] only\nShould the bot buy \"Slasher of Wrath\" ?", false),
        new Option<bool>("57585", "Spirit Katana of Wrath", "Mode: [select] only\nShould the bot buy \"Spirit Katana of Wrath\" ?", false),
    };
}
