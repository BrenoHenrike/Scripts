/*
name: Fabyo's Spooky Merge
description: This will get all or selected items on this merge shop.
tags: fabyos-spooky-merge, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FabyosSpookyMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreFriday13th F13 = new();
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
        Core.BankingBlackList.AddRange(new[] { "Spooky Fabric Scrap", "Eerie Embellishment" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!F13.Friday13thCheck("Fabyos Spooky Merge"))
            return;

        F13.Oddities();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("oddities", 2135, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Spooky Fabric Scrap":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8676);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("oddities", "r3", "Left", "*", "Cursed Cloth Roll", 13, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Eerie Embellishment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8677);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("oddities", "r9", "Left", "*", "Freaky Fripperies", 13, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70191", "Goth Pirate Musician", "Mode: [select] only\nShould the bot buy \"Goth Pirate Musician\" ?", false),
        new Option<bool>("70192", "Goth Pirate TopHat + Beard", "Mode: [select] only\nShould the bot buy \"Goth Pirate TopHat + Beard\" ?", false),
        new Option<bool>("70193", "Goth Pirate TopHat + Locks", "Mode: [select] only\nShould the bot buy \"Goth Pirate TopHat + Locks\" ?", false),
        new Option<bool>("70194", "Unicorn Commander Musician", "Mode: [select] only\nShould the bot buy \"Unicorn Commander Musician\" ?", false),
        new Option<bool>("70195", "Unicorn Commander's TopHat", "Mode: [select] only\nShould the bot buy \"Unicorn Commander's TopHat\" ?", false),
        new Option<bool>("70196", "Unicorn Commander's TopHat + Locks", "Mode: [select] only\nShould the bot buy \"Unicorn Commander's TopHat + Locks\" ?", false),
        new Option<bool>("70197", "Gothic Decorator", "Mode: [select] only\nShould the bot buy \"Gothic Decorator\" ?", false),
        new Option<bool>("70198", "Gothic Decorator's Beard", "Mode: [select] only\nShould the bot buy \"Gothic Decorator's Beard\" ?", false),
        new Option<bool>("70199", "Gothic Decorator's Hair", "Mode: [select] only\nShould the bot buy \"Gothic Decorator's Hair\" ?", false),
        new Option<bool>("70200", "Bloodmoon Musician", "Mode: [select] only\nShould the bot buy \"Bloodmoon Musician\" ?", false),
        new Option<bool>("70201", "Gothic Musician's Beard", "Mode: [select] only\nShould the bot buy \"Gothic Musician's Beard\" ?", false),
    };
}
