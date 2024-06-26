/*
name: Yokai Treasure Merge
description: This bot will farm the items belonging to the selected mode for the Yokai Treasure Merge [2339] in /yokaitreasure
tags: yokai, treasure, merge, yokaitreasure, tengu, admiral, daitengu, bearded, feather, cap, morph, typhoon, cutlasses, dragonslayer, commanders, pistol, pistols, boomstick, boomsticks, shinobi, pirate, master, stealthy, sea, masked, moonlit, steel, rapiers, iron, flight, doomtech, moglin, minion, battlepet
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YokaiTreasureMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDOY DOY = new();
    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Wuji Steel", "Mercury Phial", "Tengu Typhoon Cutlass", "Stealthy Sea Hair", "Stealthy Sea Locks", "Stealthy Sea Patch Hair", "Stealthy Sea Patch Locks", "Moonlit Steel Rapier", "Iron Flight Cutlass" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DOY.YokaiTreasure();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("yokaitreasure", 2339, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Wuji Steel":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9406);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("yokaitreasure", "Admiral Zheng", "Shapeshifting Pearl", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("yokaitreasure", "Needle Mouth", "Condemned Brand", 4, log: false);
                        Core.HuntMonster("yokaitreasure", "Imperial Warrior", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Mercury Phial":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("yokaitreasure", "Needle Mouth", req.Name, quant, false, false);
                    break;

                case "Tengu Typhoon Cutlass":
                case "Moonlit Steel Rapier":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("yokaitreasure", "Admiral Zheng", req.Name, quant, false, false);
                    break;

                case "Stealthy Sea Hair":
                case "Stealthy Sea Locks":
                case "Stealthy Sea Patch Hair":
                case "Stealthy Sea Patch Locks":
                case "Iron Flight Cutlass":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("yokaitreasure", "Imperial Warrior", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79818", "Tengu Admiral", "Mode: [select] only\nShould the bot buy \"Tengu Admiral\" ?", false),
        new Option<bool>("79819", "Daitengu Admiral", "Mode: [select] only\nShould the bot buy \"Daitengu Admiral\" ?", false),
        new Option<bool>("79821", "Bearded Tengu Feather Cap", "Mode: [select] only\nShould the bot buy \"Bearded Tengu Feather Cap\" ?", false),
        new Option<bool>("79823", "Daitengu Admiral Morph", "Mode: [select] only\nShould the bot buy \"Daitengu Admiral Morph\" ?", false),
        new Option<bool>("79826", "Tengu Typhoon Cutlasses", "Mode: [select] only\nShould the bot buy \"Tengu Typhoon Cutlasses\" ?", false),
        new Option<bool>("79935", "DragonSlayer Commander's Pistol", "Mode: [select] only\nShould the bot buy \"DragonSlayer Commander's Pistol\" ?", false),
        new Option<bool>("79936", "DragonSlayer Commander's Pistols", "Mode: [select] only\nShould the bot buy \"DragonSlayer Commander's Pistols\" ?", false),
        new Option<bool>("79937", "DragonSlayer Commander's Boomstick", "Mode: [select] only\nShould the bot buy \"DragonSlayer Commander's Boomstick\" ?", false),
        new Option<bool>("79938", "DragonSlayer Commander's Boomsticks", "Mode: [select] only\nShould the bot buy \"DragonSlayer Commander's Boomsticks\" ?", false),
        new Option<bool>("80185", "Shinobi Pirate Master", "Mode: [select] only\nShould the bot buy \"Shinobi Pirate Master\" ?", false),
        new Option<bool>("80188", "Stealthy Sea Morph", "Mode: [select] only\nShould the bot buy \"Stealthy Sea Morph\" ?", false),
        new Option<bool>("80189", "Stealthy Sea Visage", "Mode: [select] only\nShould the bot buy \"Stealthy Sea Visage\" ?", false),
        new Option<bool>("80192", "Shinobi Pirate Cap Hair", "Mode: [select] only\nShould the bot buy \"Shinobi Pirate Cap Hair\" ?", false),
        new Option<bool>("80193", "Shinobi Pirate Cap Locks", "Mode: [select] only\nShould the bot buy \"Shinobi Pirate Cap Locks\" ?", false),
        new Option<bool>("80194", "Masked Shinobi Pirate Cap Hair", "Mode: [select] only\nShould the bot buy \"Masked Shinobi Pirate Cap Hair\" ?", false),
        new Option<bool>("80195", "Masked Shinobi Pirate Cap Locks", "Mode: [select] only\nShould the bot buy \"Masked Shinobi Pirate Cap Locks\" ?", false),
        new Option<bool>("80199", "Moonlit Steel Rapiers", "Mode: [select] only\nShould the bot buy \"Moonlit Steel Rapiers\" ?", false),
        new Option<bool>("80201", "Iron Flight Cutlasses", "Mode: [select] only\nShould the bot buy \"Iron Flight Cutlasses\" ?", false),
        new Option<bool>("79504", "DoomTech Moglin Minion", "Mode: [select] only\nShould the bot buy \"DoomTech Moglin Minion\" ?", false),
        new Option<bool>("79505", "DoomTech Moglin Minion BattlePet", "Mode: [select] only\nShould the bot buy \"DoomTech Moglin Minion BattlePet\" ?", false),
    };
}
