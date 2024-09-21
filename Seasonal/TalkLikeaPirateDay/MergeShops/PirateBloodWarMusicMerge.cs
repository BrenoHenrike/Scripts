/*
name: Pirate Blood War Music Merge
description: This bot will farm the items belonging to the selected mode for the Pirate Blood War Music Merge [2483] in /piratebloodhub
tags: pirate, blood, war, music, merge, piratebloodhub, goth, musician, invese, fancy, pigtails, beanie, cut, cat, morph, messenger, bag, backup, guitar, rocker
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class PirateBloodWarMusicMerge
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
        Core.BankingBlackList.AddRange(new[] { "Gilded Sheet Music" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("piratebloodhub", 2483, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Gilded Sheet Music":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9868, 9869);
                    Core.KillMonster("piratevampire", "r2", "Left", "*", req.Name, req.Quantity, req.Temp);
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("86062", "Goth Musician", "Mode: [select] only\nShould the bot buy \"Goth Musician\" ?", false),
        new Option<bool>("86063", "Invese Goth Musician", "Mode: [select] only\nShould the bot buy \"Invese Goth Musician\" ?", false),
        new Option<bool>("86070", "Fancy Goth Pigtails", "Mode: [select] only\nShould the bot buy \"Fancy Goth Pigtails\" ?", false),
        new Option<bool>("86071", "Goth Beanie Cut", "Mode: [select] only\nShould the bot buy \"Goth Beanie Cut\" ?", false),
        new Option<bool>("86072", "Goth Beanie Locks", "Mode: [select] only\nShould the bot buy \"Goth Beanie Locks\" ?", false),
        new Option<bool>("86073", "Goth Beanie Hair", "Mode: [select] only\nShould the bot buy \"Goth Beanie Hair\" ?", false),
        new Option<bool>("86077", "Goth Cat Beanie Cut", "Mode: [select] only\nShould the bot buy \"Goth Cat Beanie Cut\" ?", false),
        new Option<bool>("86078", "Goth Cat Beanie Locks", "Mode: [select] only\nShould the bot buy \"Goth Cat Beanie Locks\" ?", false),
        new Option<bool>("86081", "Fancy Goth Pigtails Visage", "Mode: [select] only\nShould the bot buy \"Fancy Goth Pigtails Visage\" ?", false),
        new Option<bool>("86082", "Goth Beanie Cut Morph", "Mode: [select] only\nShould the bot buy \"Goth Beanie Cut Morph\" ?", false),
        new Option<bool>("86083", "Goth Beanie Locks Visage", "Mode: [select] only\nShould the bot buy \"Goth Beanie Locks Visage\" ?", false),
        new Option<bool>("86084", "Goth Beanie Hair Visage", "Mode: [select] only\nShould the bot buy \"Goth Beanie Hair Visage\" ?", false),
        new Option<bool>("86088", "Goth Cat Beanie Cut Morph", "Mode: [select] only\nShould the bot buy \"Goth Cat Beanie Cut Morph\" ?", false),
        new Option<bool>("86089", "Goth Cat Beanie Locks Visage", "Mode: [select] only\nShould the bot buy \"Goth Cat Beanie Locks Visage\" ?", false),
        new Option<bool>("86090", "Goth Messenger Bag", "Mode: [select] only\nShould the bot buy \"Goth Messenger Bag\" ?", false),
        new Option<bool>("86092", "Backup Goth Guitar", "Mode: [select] only\nShould the bot buy \"Backup Goth Guitar\" ?", false),
        new Option<bool>("86097", "Goth Rocker Guitar", "Mode: [select] only\nShould the bot buy \"Goth Rocker Guitar\" ?", false),
    };
}
