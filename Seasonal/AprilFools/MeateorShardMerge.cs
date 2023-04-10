/*
name: Meateor Shard Merge
description: This script farms all the materials needed for Meateor Shard Merge in meateortown.
tags: meateor, meateortown, shard, seasonal, april fools, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MeateorShardMerge
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
        Core.BankingBlackList.AddRange(new[] { "Meateor Shard", "Cutie Cow Pet", "ChickenCow Teeth", "Silver Savior of Battleon", "Illustrious Savior of Battleon", "Armored Defender of Battleon", "Armored Victor of Battleon", "Silver Defender's Helm", "Silver Savior's Visor", "Silver Savior's Magical Wrap", "Illustrious Defender Pet", "Illustrious Savior's Blade", "Illustrious Savior's Blades", "Silver Victor's Hammer", "Silver Victor's Hammers" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("meateortown", 2128, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Meateor Shard":
                case "Cutie Cow Pet":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("meateortown", "Giant ChickenCow", req.Name, quant, false, false);
                    break;

                case "ChickenCow Teeth":
                case "Silver Savior of Battleon":
                case "Illustrious Savior of Battleon":
                case "Armored Defender of Battleon":
                case "Armored Victor of Battleon":
                case "Silver Defender's Helm":
                case "Silver Savior's Visor":
                case "Silver Savior's Magical Wrap":
                case "Illustrious Defender Pet":
                case "Illustrious Savior's Blade":
                case "Illustrious Savior's Blades":
                case "Silver Victor's Hammer":
                case "Silver Victor's Hammers":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.isCompletedBefore(8612))
                    {
                        Core.EnsureAccept(8612);
                        Core.HuntMonster("meateortown", "Giant ChickenCow", "ChickenCow Tamed");
                        Core.EnsureComplete(8612);
                    }
                    Core.RegisterQuests(8613);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("meateortown", "Spicy ChickenCow");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("69334", "ChickenCow BeastMaster", "Mode: [select] only\nShould the bot buy \"ChickenCow BeastMaster\" ?", false),
        new Option<bool>("69335", "ChickenCowl", "Mode: [select] only\nShould the bot buy \"ChickenCowl\" ?", false),
        new Option<bool>("69336", "ChickenCowl + Bangs", "Mode: [select] only\nShould the bot buy \"ChickenCowl + Bangs\" ?", false),
        new Option<bool>("68209", "Pocky Manslayers", "Mode: [select] only\nShould the bot buy \"Pocky Manslayers\" ?", false),
        new Option<bool>("68212", "Burgers of Destiny", "Mode: [select] only\nShould the bot buy \"Burgers of Destiny\" ?", false),
        new Option<bool>("69226", "Overboard Blade of Boxgath", "Mode: [select] only\nShould the bot buy \"Overboard Blade of Boxgath\" ?", false),
        new Option<bool>("69303", "Egg-cellent Trobble Pet", "Mode: [select] only\nShould the bot buy \"Egg-cellent Trobble Pet\" ?", false),
        new Option<bool>("69227", "Caladboard", "Mode: [select] only\nShould the bot buy \"Caladboard\" ?", false),
        new Option<bool>("69224", "Blinding Box of Destiny", "Mode: [select] only\nShould the bot buy \"Blinding Box of Destiny\" ?", false),
        new Option<bool>("69400", "UnFowled BeastMaster", "Mode: [select] only\nShould the bot buy \"UnFowled BeastMaster\" ?", false),
        new Option<bool>("69498", "Sweetie Cow Pet", "Mode: [select] only\nShould the bot buy \"Sweetie Cow Pet\" ?", false),
        new Option<bool>("69341", "Golden Savior of Battleon", "Mode: [select] only\nShould the bot buy \"Golden Savior of Battleon\" ?", false),
        new Option<bool>("69342", "Glorious Savior of Battleon", "Mode: [select] only\nShould the bot buy \"Glorious Savior of Battleon\" ?", false),
        new Option<bool>("69343", "Gilded Defender of Battleon", "Mode: [select] only\nShould the bot buy \"Gilded Defender of Battleon\" ?", false),
        new Option<bool>("69344", "Gilded Victor of Battleon", "Mode: [select] only\nShould the bot buy \"Gilded Victor of Battleon\" ?", false),
        new Option<bool>("69345", "Golden Defender's Helm", "Mode: [select] only\nShould the bot buy \"Golden Defender's Helm\" ?", false),
        new Option<bool>("69346", "Golden Savior's Visor", "Mode: [select] only\nShould the bot buy \"Golden Savior's Visor\" ?", false),
        new Option<bool>("69347", "Battleon Savior's Magical Wrap", "Mode: [select] only\nShould the bot buy \"Battleon Savior's Magical Wrap\" ?", false),
        new Option<bool>("69348", "Glorious Defender Twilly Pet", "Mode: [select] only\nShould the bot buy \"Glorious Defender Twilly Pet\" ?", false),
        new Option<bool>("69349", "Glorious Savior's Blade", "Mode: [select] only\nShould the bot buy \"Glorious Savior's Blade\" ?", false),
        new Option<bool>("69350", "Glorious Savior's Blades", "Mode: [select] only\nShould the bot buy \"Glorious Savior's Blades\" ?", false),
        new Option<bool>("69351", "Golden Victor's Hammer", "Mode: [select] only\nShould the bot buy \"Golden Victor's Hammer\" ?", false),
        new Option<bool>("69352", "Golden Victor's Hammers", "Mode: [select] only\nShould the bot buy \"Golden Victor's Hammers\" ?", false),
    };
}
