/*
name: SevenCirclesWarMerge
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SevenCirclesWarMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    public CoreLegion Legion = new();
    private CoreAdvanced Adv = new();
    public SevenCircles Circles = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "Indulgence", "Beast Soul", "Souls of Heresy", "Essence of Treachery", "Essence of Wrath", "Essence of Violence" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Circles.CirclesWar();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sevencircleswar", 1984, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Indulgence":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7978);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("sevencircles", "r2", "Left", "Limbo Guard", "Souls of Limbo", 25);
                        Core.KillMonster("sevencircles", "r3", "Left", "Luxuria Guard", "Essence of Luxuria", 1);
                        Core.KillMonster("sevencircles", "r5", "Left", "Gluttony Guard", "Essence of Gluttony", 1);
                        Core.KillMonster("sevencircles", "r7", "Left", "Avarice Guard", "Essence of Avarice", 1);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Beast Soul":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Adv.BestGear(RacialGearBoost.Undead);
                    Adv.SmartEnhance(Core.SoloClass);
                    Core.HuntMonster("sevencircleswar", "The Beast", req.Name, quant, isTemp: false, publicRoom: true);
                    break;

                case "Souls of Heresy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7983, 7980, 7981); // Blasphemy? Blasphe-you! ID:7983 | War Medals ID:7980 | Mega War Medals ID:7981
                    while (!Bot.ShouldExit && !Core.CheckInventory("Souls of Heresy", quant))
                        Core.KillMonster("sevencircleswar", "r7", "Left", "Heresy Guard", log: false);
                    Core.CancelRegisteredQuests();
                    break;

                case "Essence of Treachery":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7988);
                    while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Treachery", quant))
                        Core.KillMonster("sevencircleswar", "r13", "Left", "Treachery Guard", log: false);
                    Core.CancelRegisteredQuests();
                    break;

                case "Essence of Wrath":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7979);
                    while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Wrath", quant))
                        Core.KillMonster("sevencircleswar", "Enter", "Spawn", "Wrath Guard", "Wrath Guards Defeated", 12);
                    Core.CancelRegisteredQuests();
                    break;

                case "Essence of Violence":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7985);
                    while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Violence", quant))
                        Core.KillMonster("sevencircleswar", "r9", "Left", "Violence Guard", log: false);
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("59754", "Faces of Violence", "Mode: [select] only\nShould the bot buy \"Faces of Violence\" ?", false),
        new Option<bool>("59755", "Crown of Wrath", "Mode: [select] only\nShould the bot buy \"Crown of Wrath\" ?", false),
        new Option<bool>("60038", "Head of the Legion Beast", "Mode: [select] only\nShould the bot buy \"Head of the Legion Beast\" ?", false),
        new Option<bool>("59721", "Face of Treachery", "Mode: [select] only\nShould the bot buy \"Face of Treachery\" ?", false),
        new Option<bool>("60095", "Legion Viking", "Mode: [select] only\nShould the bot buy \"Legion Viking\" ?", false),
        new Option<bool>("60096", "Legion Viking Helm", "Mode: [select] only\nShould the bot buy \"Legion Viking Helm\" ?", false),
        new Option<bool>("60097", "Legion Viking Mask", "Mode: [select] only\nShould the bot buy \"Legion Viking Mask\" ?", false),
        new Option<bool>("60101", "Legion Viking Hatchet", "Mode: [select] only\nShould the bot buy \"Legion Viking Hatchet\" ?", false),
        new Option<bool>("60099", "Legion Viking Big Axe", "Mode: [select] only\nShould the bot buy \"Legion Viking Big Axe\" ?", false),
        new Option<bool>("60100", "Legion Viking Flail", "Mode: [select] only\nShould the bot buy \"Legion Viking Flail\" ?", false),
        new Option<bool>("60098", "Legion Viking Back Axe", "Mode: [select] only\nShould the bot buy \"Legion Viking Back Axe\" ?", false),
    };
}
