/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EaglesReachArmoryMerge
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
        Core.BankingBlackList.AddRange(new[] { "Algid Token", "Frost Token", "Icy Token", "Rime Token", "Gelid Token", "Glacial Token "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battlegrounda", 1062, findIngredients);

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

                case "Algid Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battlegrounda", "r2", "Left", "*", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Frost Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battlegroundb", "r2", "Left", "*", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Icy Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battlegroundc", "r2", "Left", "*", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Rime Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battlegroundd", "r2", "Left", "*", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Gelid Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battlegrounde", "r2", "Left", "*", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Glacial Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battlegroundf", "r2", "Left", "*", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("27724", "Arena Warrior", "Mode: [select] only\nShould the bot buy \"Arena Warrior\" ?", false),
        new Option<bool>("27728", "Ice-Ten Mask", "Mode: [select] only\nShould the bot buy \"Ice-Ten Mask\" ?", false),
        new Option<bool>("27727", "Ice-Ten Mask and Locks", "Mode: [select] only\nShould the bot buy \"Ice-Ten Mask and Locks\" ?", false),
        new Option<bool>("26951", "Desolate Duel Axes", "Mode: [select] only\nShould the bot buy \"Desolate Duel Axes\" ?", false),
        new Option<bool>("26947", "Desolate ShadowHood Locks", "Mode: [select] only\nShould the bot buy \"Desolate ShadowHood Locks\" ?", false),
        new Option<bool>("26948", "Desolate ShadowHood", "Mode: [select] only\nShould the bot buy \"Desolate ShadowHood\" ?", false),
        new Option<bool>("25201", "Scarred Sword and Shield", "Mode: [select] only\nShould the bot buy \"Scarred Sword and Shield\" ?", false),
        new Option<bool>("25200", "Golden Sword and Shield", "Mode: [select] only\nShould the bot buy \"Golden Sword and Shield\" ?", false),
        new Option<bool>("26944", "Desolate Rogue", "Mode: [select] only\nShould the bot buy \"Desolate Rogue\" ?", false),
        new Option<bool>("26945", "Hood of the Desolate Locks", "Mode: [select] only\nShould the bot buy \"Hood of the Desolate Locks\" ?", false),
        new Option<bool>("26946", "Hood of the Desolate", "Mode: [select] only\nShould the bot buy \"Hood of the Desolate\" ?", false),
        new Option<bool>("26950", "Desolate Mask", "Mode: [select] only\nShould the bot buy \"Desolate Mask\" ?", false),
        new Option<bool>("26952", "Fallen Cape", "Mode: [select] only\nShould the bot buy \"Fallen Cape\" ?", false),
        new Option<bool>("25764", "Blade of Ancient Evil", "Mode: [select] only\nShould the bot buy \"Blade of Ancient Evil\" ?", false),
        new Option<bool>("25765", "Sheathed Blade of Evil", "Mode: [select] only\nShould the bot buy \"Sheathed Blade of Evil\" ?", false),
        new Option<bool>("25766", "Ancient Crossed Katana", "Mode: [select] only\nShould the bot buy \"Ancient Crossed Katana\" ?", false),
        new Option<bool>("25767", "Mask of Ancient Evil", "Mode: [select] only\nShould the bot buy \"Mask of Ancient Evil\" ?", false),
        new Option<bool>("25768", "Locks of Ancient Evil", "Mode: [select] only\nShould the bot buy \"Locks of Ancient Evil\" ?", false),
        new Option<bool>("25762", "Ancient Evil Commander", "Mode: [select] only\nShould the bot buy \"Ancient Evil Commander\" ?", false),
        new Option<bool>("25189", "Hyaline Spear", "Mode: [select] only\nShould the bot buy \"Hyaline Spear\" ?", false),
        new Option<bool>("25190", "Sheer Polearm", "Mode: [select] only\nShould the bot buy \"Sheer Polearm\" ?", false),
        new Option<bool>("25191", "Translucent Axe", "Mode: [select] only\nShould the bot buy \"Translucent Axe\" ?", false),
        new Option<bool>("27729", "Golden Staff Of Healing", "Mode: [select] only\nShould the bot buy \"Golden Staff Of Healing\" ?", false),
        new Option<bool>("27731", "Bronze Barbarian", "Mode: [select] only\nShould the bot buy \"Bronze Barbarian\" ?", false),
        new Option<bool>("27725", "Gladiator Warrior", "Mode: [select] only\nShould the bot buy \"Gladiator Warrior\" ?", false),
        new Option<bool>("27730", "Gilded Gladiator", "Mode: [select] only\nShould the bot buy \"Gilded Gladiator\" ?", false),
    };
}
