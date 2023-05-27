/*
name: Seaview Souvenirs Merge
description: This bot will farm the items belonging to the selected mode for the Seaview Souvenirs Merge [2273] in /ashray
tags: seaview, souvenirs, merge, ashray, villager, goldtrimmed, tricorn, lass, gemme, numari, bandana, stingray, leather, sheathed, inn, mug, mugs, pyromancer, merchant, mariner, mariners, blazing, parrot, pal, hook
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
//cs_include Scripts/Story\AgeOfRuin\CoreAOR.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SeaviewSouvenirsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Treasure Chest", "Sur-gion Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.AshrayVillage();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ashray", 2273, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Treasure Chest":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("finalbattle", "r2", "Left", "*", req.Name, quant, false, false);
                    break;

                case "Sur-gion Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9235);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ashray", "Kitefin Shark Bait", "Shark Fin", 7, log: false);
                        Core.HuntMonster("ashray", "Ashray Fisherman", "Ashray Blood Sample", 7, log: false);
                        Core.HuntMonster("ashray", "Seafoam Elemental", "Seafoam Bubbles", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("72843", "Ashray Villager", "Mode: [select] only\nShould the bot buy \"Ashray Villager\" ?", false),
        new Option<bool>("72844", "Gold-trimmed Tricorn", "Mode: [select] only\nShould the bot buy \"Gold-trimmed Tricorn\" ?", false),
        new Option<bool>("72845", "Ashray Lass' Locks", "Mode: [select] only\nShould the bot buy \"Ashray Lass' Locks\" ?", false),
        new Option<bool>("72846", "Gemme Numari Bandana", "Mode: [select] only\nShould the bot buy \"Gemme Numari Bandana\" ?", false),
        new Option<bool>("72847", "Gemme Numari Bandana and Locks", "Mode: [select] only\nShould the bot buy \"Gemme Numari Bandana and Locks\" ?", false),
        new Option<bool>("72848", "Stingray Leather Sheathed Blade", "Mode: [select] only\nShould the bot buy \"Stingray Leather Sheathed Blade\" ?", false),
        new Option<bool>("72849", "Seaview Inn Mug", "Mode: [select] only\nShould the bot buy \"Seaview Inn Mug\" ?", false),
        new Option<bool>("72850", "Seaview Inn Mugs", "Mode: [select] only\nShould the bot buy \"Seaview Inn Mugs\" ?", false),
        new Option<bool>("72851", "Ashray Pyromancer", "Mode: [select] only\nShould the bot buy \"Ashray Pyromancer\" ?", false),
        new Option<bool>("72852", "Merchant Mariner", "Mode: [select] only\nShould the bot buy \"Merchant Mariner\" ?", false),
        new Option<bool>("72853", "Ashray Mariner's Hair", "Mode: [select] only\nShould the bot buy \"Ashray Mariner's Hair\" ?", false),
        new Option<bool>("72854", "Ashray Mariner's Locks", "Mode: [select] only\nShould the bot buy \"Ashray Mariner's Locks\" ?", false),
        new Option<bool>("72855", "Mariner's Blazing Parrot Pal", "Mode: [select] only\nShould the bot buy \"Mariner's Blazing Parrot Pal\" ?", false),
        new Option<bool>("72856", "Ashray Mariner's Hook", "Mode: [select] only\nShould the bot buy \"Ashray Mariner's Hook\" ?", false),
    };
}
