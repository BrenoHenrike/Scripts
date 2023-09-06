/*
name: Faction Banners Merge
description: This bot will farm the items belonging to the selected mode for the Faction Banners Merge [1717] in /snowmore
tags: faction, banners, merge, snowmore, alchemy, banner, i, left, ii, arcangrove, chaos, chronospan, crystallis, doomwood, dwarfhold, evil, good, hollowborn, horc, legion, lycan, nation, ravenloss, seraphic, seraph, troll, vampire, blight, kings, winter, throne
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FactionBannersMerge
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
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Blighted Deer's Hide", "Blighted Lion's Fang", "Blighted Wolf's Blood", "Blighted Dragon's Bone", "Northern Crown", "Winter Throne" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("snowmore", 1717, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Blighted Deer's Hide":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("snowmore", "Blighted Deer", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Blighted Lion's Fang":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("snowmore", "Blighted Lion", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Blighted Wolf's Blood":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("snowmore", "Blighted Wolf", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Blighted Dragon's Bone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("snowmore", "Blighted Dragon", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Northern Crown":
                case "Winter Throne":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("snowmore", "Jon S'Nooooooo", req.Name, quant, false, false); ;
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48413", "Alchemy Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Alchemy Banner I (Left)\" ?", false),
        new Option<bool>("48433", "Alchemy Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Alchemy Banner II (Left)\" ?", false),
        new Option<bool>("48414", "Arcangrove Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Arcangrove Banner I (Left)\" ?", false),
        new Option<bool>("48434", "Arcangrove Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Arcangrove Banner II (Left)\" ?", false),
        new Option<bool>("48415", "Chaos Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Chaos Banner I (Left)\" ?", false),
        new Option<bool>("48435", "Chaos Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Chaos Banner II (Left)\" ?", false),
        new Option<bool>("48416", "Chronospan Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Chronospan Banner I (Left)\" ?", false),
        new Option<bool>("48436", "Chronospan Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Chronospan Banner II (Left)\" ?", false),
        new Option<bool>("48417", "Crystallis Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Crystallis Banner I (Left)\" ?", false),
        new Option<bool>("48437", "Crystallis Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Crystallis Banner II (Left)\" ?", false),
        new Option<bool>("48418", "Doomwood Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Doomwood Banner I (Left)\" ?", false),
        new Option<bool>("48438", "Doomwood Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Doomwood Banner II (Left)\" ?", false),
        new Option<bool>("48419", "Dwarfhold Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Dwarfhold Banner I (Left)\" ?", false),
        new Option<bool>("48439", "Dwarfhold Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Dwarfhold Banner II (Left)\" ?", false),
        new Option<bool>("48420", "Evil Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Evil Banner I (Left)\" ?", false),
        new Option<bool>("48440", "Evil Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Evil Banner II (Left)\" ?", false),
        new Option<bool>("48421", "Good Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Good Banner I (Left)\" ?", false),
        new Option<bool>("48441", "Good Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Good Banner II (Left)\" ?", false),
        new Option<bool>("48422", "Hollowborn Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Hollowborn Banner I (Left)\" ?", false),
        new Option<bool>("48442", "Hollowborn Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Hollowborn Banner II (Left)\" ?", false),
        new Option<bool>("48423", "Horc Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Horc Banner I (Left)\" ?", false),
        new Option<bool>("48443", "Horc Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Horc Banner II (Left)\" ?", false),
        new Option<bool>("48424", "Legion Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Legion Banner I (Left)\" ?", false),
        new Option<bool>("48444", "Legion Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Legion Banner II (Left)\" ?", false),
        new Option<bool>("48425", "Lycan Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Lycan Banner I (Left)\" ?", false),
        new Option<bool>("48445", "Lycan Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Lycan Banner II (Left)\" ?", false),
        new Option<bool>("48426", "Nation Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Nation Banner I (Left)\" ?", false),
        new Option<bool>("48446", "Nation Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Nation Banner II (Left)\" ?", false),
        new Option<bool>("48427", "Ravenloss Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Ravenloss Banner I (Left)\" ?", false),
        new Option<bool>("48447", "Ravenloss Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Ravenloss Banner II (Left)\" ?", false),
        new Option<bool>("48428", "Seraphic Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Seraphic Banner I (Left)\" ?", false),
        new Option<bool>("48448", "Seraph Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Seraph Banner II (Left)\" ?", false),
        new Option<bool>("48429", "Troll Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Troll Banner I (Left)\" ?", false),
        new Option<bool>("48449", "Troll Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Troll Banner II (Left)\" ?", false),
        new Option<bool>("48430", "Vampire Banner I (Left)", "Mode: [select] only\nShould the bot buy \"Vampire Banner I (Left)\" ?", false),
        new Option<bool>("48450", "Vampire Banner II (Left)", "Mode: [select] only\nShould the bot buy \"Vampire Banner II (Left)\" ?", false),
        new Option<bool>("48432", "Blight King's Winter Throne", "Mode: [select] only\nShould the bot buy \"Blight King's Winter Throne\" ?", false),
    };
}
