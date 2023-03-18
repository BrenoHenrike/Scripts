/*
name: Fortress Delve Merge
description: This bot will farm the required items from /fortressdelve, do the story, and buy items from the merge shop
tags: fortressdelve, merge, dage, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelve.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/FortressDelve.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FortressDelveMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private FortressDelve FD = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Rune of Radiance"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        FD.DoStory();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("fortressdelve", 2247, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Rune of Radiance":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9170);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("fortressdelve", "Enlightened Shadow", "Shadowscythe Bone Shard", 10, log: false);
                        Core.HuntMonster("fortressdelve", "Delirious Elemental", "Elemental Residue", 10, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("fortressdelve", "Astero", "Glass Wing", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76755", "Undead Parabellum Gauntlets", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Gauntlets\" ?", false),
        new Option<bool>("76754", "Undead Parabellum Gauntlet", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Gauntlet\" ?", false),
        new Option<bool>("76753", "Undead Parabellum Sword and Shield", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Sword and Shield\" ?", false),
        new Option<bool>("76752", "Undead Parabellum Hammer", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Hammer\" ?", false),
        new Option<bool>("76751", "Undead Parabellum Blades", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Blades\" ?", false),
        new Option<bool>("76750", "Undead Parabellum Blade", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Blade\" ?", false),
        new Option<bool>("76749", "Undead Parabellum Back Sword and Shield", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Back Sword and Shield\" ?", false),
        new Option<bool>("76748", "Undead Parabellum Back Sword", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Back Sword\" ?", false),
        new Option<bool>("76747", "Undead Parabellum Back Shield", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Back Shield\" ?", false),
        new Option<bool>("76746", "Undead Parabellum Back Hammer", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Back Hammer\" ?", false),
        new Option<bool>("76745", "Undead Parabellum Guard", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Guard\" ?", false),
        new Option<bool>("76744", "Undead Parabellum Helm", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Helm\" ?", false),
        new Option<bool>("76743", "Undead Parabellum Armet", "Mode: [select] only\nShould the bot buy \"Undead Parabellum Armet\" ?", false),
        new Option<bool>("76742", "Undead Parabellum", "Mode: [select] only\nShould the bot buy \"Undead Parabellum\" ?", false),
        new Option<bool>("76382", "Underworld Infiltrator Cowl", "Mode: [select] only\nShould the bot buy \"Underworld Infiltrator Cowl\" ?", false),
        new Option<bool>("76381", "Underworld Infiltrator Hood", "Mode: [select] only\nShould the bot buy \"Underworld Infiltrator Hood\" ?", false),
        new Option<bool>("76380", "Underworld Infiltrator", "Mode: [select] only\nShould the bot buy \"Underworld Infiltrator\" ?", false),
        new Option<bool>("76384", "Underworld Infiltrator Blade", "Mode: [select] only\nShould the bot buy \"Underworld Infiltrator Blade\" ?", false),
        new Option<bool>("76385", "Underworld Infiltrator Blades", "Mode: [select] only\nShould the bot buy \"Underworld Infiltrator Blades\" ?", false),
        new Option<bool>("77020", "Young Empress' Armor", "Mode: [select] only\nShould the bot buy \"Young Empress' Armor\" ?", false),
        new Option<bool>("76789", "Young Empress' Blade", "Mode: [select] only\nShould the bot buy \"Young Empress' Blade\" ?", false),
    };
}
