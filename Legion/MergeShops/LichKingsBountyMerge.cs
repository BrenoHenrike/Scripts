/*
name: Lich Kings Bounty Merge
description: This bot will farm the items belonging to the selected mode for the Lich Kings Bounty Merge [1658] in /frozenlair
tags: lich, kings, bounty, merge, frozenlair, legion, lords, scythe, masked, lord, , frozen, legionnaires, mini
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LichKingsBountyMerge
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
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "Ice Spike", "Sapphire Orb", "Ice Splinter", "Necrotic Orb" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("frozenlair", 1658, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ice Spike":
                case "Ice Splinter":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("frozenlair", "Frozen Legionnaire", req.Name, quant, false, false);
                    break;

                case "Sapphire Orb":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("frozenlair", "Legion Lich Lord", req.Name, quant, false, false);
                    break;

                case "Necrotic Orb":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("frozenlair", "Lich Lord", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("45846", "Legion Lich Lord's Staff", "Mode: [select] only\nShould the bot buy \"Legion Lich Lord's Staff\" ?", false),
        new Option<bool>("45845", "Legion Lich Lord's Scythe", "Mode: [select] only\nShould the bot buy \"Legion Lich Lord's Scythe\" ?", false),
        new Option<bool>("45844", "Legion Lich Lord's Runes", "Mode: [select] only\nShould the bot buy \"Legion Lich Lord's Runes\" ?", false),
        new Option<bool>("45843", "Legion Lich Lord's Helm", "Mode: [select] only\nShould the bot buy \"Legion Lich Lord's Helm\" ?", false),
        new Option<bool>("45842", "Legion Lich Lord's Masked Helm", "Mode: [select] only\nShould the bot buy \"Legion Lich Lord's Masked Helm\" ?", false),
        new Option<bool>("45841", "Legion Lich Lord + Staff", "Mode: [select] only\nShould the bot buy \"Legion Lich Lord + Staff\" ?", false),
        new Option<bool>("45840", "Lich Lord's Staff", "Mode: [select] only\nShould the bot buy \"Lich Lord's Staff\" ?", false),
        new Option<bool>("45839", "Lich Lord's Scythe", "Mode: [select] only\nShould the bot buy \"Lich Lord's Scythe\" ?", false),
        new Option<bool>("45838", "Lich Lord's Runes", "Mode: [select] only\nShould the bot buy \"Lich Lord's Runes\" ?", false),
        new Option<bool>("45837", "Lich Lord's Masked Helm", "Mode: [select] only\nShould the bot buy \"Lich Lord's Masked Helm\" ?", false),
        new Option<bool>("46056", "Lich Lord's Helm", "Mode: [select] only\nShould the bot buy \"Lich Lord's Helm\" ?", false),
        new Option<bool>("45836", "Lich Lord", "Mode: [select] only\nShould the bot buy \"Lich Lord\" ?", false),
        new Option<bool>("46057", "Frozen Legionnaire's Blade", "Mode: [select] only\nShould the bot buy \"Frozen Legionnaire's Blade\" ?", false),
        new Option<bool>("45652", "Legion Lich Lord", "Mode: [select] only\nShould the bot buy \"Legion Lich Lord\" ?", false),
        new Option<bool>("46071", "Mini Lich Lord", "Mode: [select] only\nShould the bot buy \"Mini Lich Lord\" ?", false),
        new Option<bool>("46079", "Mini Legion Lich Lord", "Mode: [select] only\nShould the bot buy \"Mini Legion Lich Lord\" ?", false),
    };
}
