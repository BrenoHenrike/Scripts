/*
name: Pirate Blood War Merge
description: This bot will farm the items belonging to the selected mode for the Pirate Blood War Merge [2482] in /piratebloodhub
tags: pirate, blood, war, merge, piratebloodhub, enchanted, isles, corsair, corsairs, accoutrements, battle, morph, patch, skull, pet, cutlass, cutlasses, siren, poacher, beard, eyepatch, vampiric, bloodthirst, whip, backup, golden, anchor, gold, chain, hook, hooks
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class PirateBloodWarMerge
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
        Core.BankingBlackList.AddRange(new[] { "Blood Testament Trophy" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("piratebloodhub", 2482, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Blood Testament Trophy":
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
        new Option<bool>("88172", "Enchanted Blood Isles Corsair", "Mode: [select] only\nShould the bot buy \"Enchanted Blood Isles Corsair\" ?", false),
        new Option<bool>("88173", "Enchanted Corsair's Accoutrements", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Accoutrements\" ?", false),
        new Option<bool>("88174", "Enchanted Corsair's Battle Morph", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Battle Morph\" ?", false),
        new Option<bool>("88175", "Enchanted Corsair's Patch", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Patch\" ?", false),
        new Option<bool>("88176", "Enchanted Corsair's Hat", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Hat\" ?", false),
        new Option<bool>("88180", "Blood Isles Skull Pet", "Mode: [select] only\nShould the bot buy \"Blood Isles Skull Pet\" ?", false),
        new Option<bool>("88181", "Enchanted Blood Isles Cutlass", "Mode: [select] only\nShould the bot buy \"Enchanted Blood Isles Cutlass\" ?", false),
        new Option<bool>("88182", "Enchanted Blood Isles Cutlasses", "Mode: [select] only\nShould the bot buy \"Enchanted Blood Isles Cutlasses\" ?", false),
        new Option<bool>("88269", "Siren Poacher Beard", "Mode: [select] only\nShould the bot buy \"Siren Poacher Beard\" ?", false),
        new Option<bool>("80988", "Siren Poacher Patch", "Mode: [select] only\nShould the bot buy \"Siren Poacher Patch\" ?", false),
        new Option<bool>("80989", "Siren Poacher Eyepatch and Locks", "Mode: [select] only\nShould the bot buy \"Siren Poacher Eyepatch and Locks\" ?", false),
        new Option<bool>("88039", "Vampiric Bloodthirst Whip", "Mode: [select] only\nShould the bot buy \"Vampiric Bloodthirst Whip\" ?", false),
        new Option<bool>("88194", "Blood Isles Backup Guns", "Mode: [select] only\nShould the bot buy \"Blood Isles Backup Guns\" ?", false),
        new Option<bool>("88195", "Golden Anchor", "Mode: [select] only\nShould the bot buy \"Golden Anchor\" ?", false),
        new Option<bool>("88214", "Gold Chain and Hook", "Mode: [select] only\nShould the bot buy \"Gold Chain and Hook\" ?", false),
        new Option<bool>("88215", "Gold Chain and Hooks", "Mode: [select] only\nShould the bot buy \"Gold Chain and Hooks\" ?", false),
    };
}
