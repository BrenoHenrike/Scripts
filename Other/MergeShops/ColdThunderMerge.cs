/*
name: ColdThunder Merge
description: This bot will farm the items belonging to the selected mode for the ColdThunder Merge [2467] in /coldthunder
tags: coldthunder, merge, coldthunder, thundersnow, demigod, morph, ruler, demigods, divine, scathanna, eyes, shadow, veil, skyes, wailing, greatsword, storm, scythe, hand, winter, hands, skye, emissary, queen, ionas, royal, attire, electrifying, summer, zilla, shades, visor, tote, bag, chibi, greatswords
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ColdThunderMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR CoreAOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Skye's Lightning", "Electrifying Zilla Tail", "Electrifying Zilla Bag" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        CoreAOR.ColdThunder();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("coldthunder", 2467, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Skye's Lightning":
                    Core.FarmingLogger(req.Name, quant);
                    // 9834 | Eilean a' Cheò
                    Core.RegisterQuests(9834);
                    CoreAOR.ColdThunderBoss(req.Name, req.Quantity, req.Temp);
                    Core.CancelRegisteredQuests();
                    break;

                case "Electrifying Zilla Tail":
                case "Electrifying Zilla Bag":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("castlegaheris", "Energy Elemental", req.Name, req.Quantity, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("87430", "Thundersnow Demigod", "Mode: [select] only\nShould the bot buy \"Thundersnow Demigod\" ?", false),
        new Option<bool>("87431", "Thundersnow Demigod Morph", "Mode: [select] only\nShould the bot buy \"Thundersnow Demigod Morph\" ?", false),
        new Option<bool>("87432", "Thundersnow Demigod Visage", "Mode: [select] only\nShould the bot buy \"Thundersnow Demigod Visage\" ?", false),
        new Option<bool>("87433", "Thundersnow Ruler Morph", "Mode: [select] only\nShould the bot buy \"Thundersnow Ruler Morph\" ?", false),
        new Option<bool>("87434", "Thundersnow Ruler Visage", "Mode: [select] only\nShould the bot buy \"Thundersnow Ruler Visage\" ?", false),
        new Option<bool>("87435", "Thundersnow Ruler Hair", "Mode: [select] only\nShould the bot buy \"Thundersnow Ruler Hair\" ?", false),
        new Option<bool>("87436", "Thundersnow Ruler Locks", "Mode: [select] only\nShould the bot buy \"Thundersnow Ruler Locks\" ?", false),
        new Option<bool>("87437", "Demigod's Divine Scathanna", "Mode: [select] only\nShould the bot buy \"Demigod's Divine Scathanna\" ?", false),
        new Option<bool>("87438", "Divine Eyes of Scathanna", "Mode: [select] only\nShould the bot buy \"Divine Eyes of Scathanna\" ?", false),
        new Option<bool>("87439", "Demigod's Shadow Veil", "Mode: [select] only\nShould the bot buy \"Demigod's Shadow Veil\" ?", false),
        new Option<bool>("87449", "Demigod's Scathanna", "Mode: [select] only\nShould the bot buy \"Demigod's Scathanna\" ?", false),
        new Option<bool>("87440", "Skye's Wailing Greatsword", "Mode: [select] only\nShould the bot buy \"Skye's Wailing Greatsword\" ?", false),
        new Option<bool>("87442", "Divine Storm Scythe", "Mode: [select] only\nShould the bot buy \"Divine Storm Scythe\" ?", false),
        new Option<bool>("87443", "Hand of Winter", "Mode: [select] only\nShould the bot buy \"Hand of Winter\" ?", false),
        new Option<bool>("87444", "Hands of Winter", "Mode: [select] only\nShould the bot buy \"Hands of Winter\" ?", false),
        new Option<bool>("87424", "Skye Emissary", "Mode: [select] only\nShould the bot buy \"Skye Emissary\" ?", false),
        new Option<bool>("87425", "Skye Emissary Morph", "Mode: [select] only\nShould the bot buy \"Skye Emissary Morph\" ?", false),
        new Option<bool>("87426", "Skye Emissary Hair", "Mode: [select] only\nShould the bot buy \"Skye Emissary Hair\" ?", false),
        new Option<bool>("87427", "Queen Iona's Royal Attire", "Mode: [select] only\nShould the bot buy \"Queen Iona's Royal Attire\" ?", false),
        new Option<bool>("87428", "Queen Iona's Visage", "Mode: [select] only\nShould the bot buy \"Queen Iona's Visage\" ?", false),
        new Option<bool>("87429", "Queen Iona's Locks", "Mode: [select] only\nShould the bot buy \"Queen Iona's Locks\" ?", false),
        new Option<bool>("87113", "Electrifying Summer Zilla", "Mode: [select] only\nShould the bot buy \"Electrifying Summer Zilla\" ?", false),
        new Option<bool>("87114", "Electrifying Zilla Shades Visage", "Mode: [select] only\nShould the bot buy \"Electrifying Zilla Shades Visage\" ?", false),
        new Option<bool>("87115", "Electrifying Summer Zilla Visage", "Mode: [select] only\nShould the bot buy \"Electrifying Summer Zilla Visage\" ?", false),
        new Option<bool>("87116", "Electrifying Zilla Visor Visage", "Mode: [select] only\nShould the bot buy \"Electrifying Zilla Visor Visage\" ?", false),
        new Option<bool>("87119", "Electrifying Zilla Tote Bag", "Mode: [select] only\nShould the bot buy \"Electrifying Zilla Tote Bag\" ?", false),
        new Option<bool>("87120", "Electrifying Zilla Summer Chibi", "Mode: [select] only\nShould the bot buy \"Electrifying Zilla Summer Chibi\" ?", false),
        new Option<bool>("87441", "Skye's Wailing Greatswords", "Mode: [select] only\nShould the bot buy \"Skye's Wailing Greatswords\" ?", false),
    };
}
