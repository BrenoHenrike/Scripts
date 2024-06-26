/*
name: SoulForge Merge
description: This bot will farm the items belonging to the selected mode for the SoulForge Merge [577] in /underworld
tags: soulforge, merge, underworld, classy, skullseeker, legion, beast, within, cane, death, bane, pride, horrid, cleaver, spine, shanks, ancient, undead, horns, breaker, warrior
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SoulForgeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreLegion Legion = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Legion.SoulForgeHammer();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("underworld", 577, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("16421", "Classy SkullSeeker", "Mode: [select] only\nShould the bot buy \"Classy SkullSeeker\" ?", false),
        new Option<bool>("16422", "Legion Beast Within", "Mode: [select] only\nShould the bot buy \"Legion Beast Within\" ?", false),
        new Option<bool>("16423", "SkullSeeker Cane", "Mode: [select] only\nShould the bot buy \"SkullSeeker Cane\" ?", false),
        new Option<bool>("16424", "Classy SkullSeeker Cane", "Mode: [select] only\nShould the bot buy \"Classy SkullSeeker Cane\" ?", false),
        new Option<bool>("16425", "Death Bane of the Legion", "Mode: [select] only\nShould the bot buy \"Death Bane of the Legion\" ?", false),
        new Option<bool>("16467", "Pride of the Legion", "Mode: [select] only\nShould the bot buy \"Pride of the Legion\" ?", false),
        new Option<bool>("16534", "Legion Horrid Cleaver", "Mode: [select] only\nShould the bot buy \"Legion Horrid Cleaver\" ?", false),
        new Option<bool>("16535", "Legion Spine Shanks", "Mode: [select] only\nShould the bot buy \"Legion Spine Shanks\" ?", false),
        new Option<bool>("17275", "Ancient Undead Horns", "Mode: [select] only\nShould the bot buy \"Ancient Undead Horns\" ?", false),
        new Option<bool>("17276", "Ancient Undead Breaker", "Mode: [select] only\nShould the bot buy \"Ancient Undead Breaker\" ?", false),
        new Option<bool>("17277", "Dual Ancient Legion Axe", "Mode: [select] only\nShould the bot buy \"Dual Ancient Legion Axe\" ?", false),
        new Option<bool>("17278", "Ancient Undead Warrior", "Mode: [select] only\nShould the bot buy \"Ancient Undead Warrior\" ?", false),
        new Option<bool>("17343", "Ancient Undead Helm", "Mode: [select] only\nShould the bot buy \"Ancient Undead Helm\" ?", false),
    };
}
