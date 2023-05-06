/*
name: Mawa's Stand Merge
description: This bot will farm the items belonging to the selected mode for the Mawa's Stand Merge [1874] in /zorbaspalace
tags: mawas, stand, merge, zorbaspalace, galactic, shadow, trooper, twicket, twig, fortuna, twillek, starcrasher, , scarf, backblades, lightblade, lightblades, parodian, guard, guards, sheathed
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MawasStandMerge
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
        Core.BankingBlackList.AddRange(new[] { "Furry Egg", "Woopee" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("zorbaspalace", 1874, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Furry Egg":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("zorbaspalace", "Lem-or", req.Name, quant, false, false);
                    break;

                case "Woopee":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("zorbaspalace", "Thwompcat", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54390", "Galactic Shadow Trooper", "Mode: [select] only\nShould the bot buy \"Galactic Shadow Trooper\" ?", false),
        new Option<bool>("54394", "Galactic Shadow Trooper Helm", "Mode: [select] only\nShould the bot buy \"Galactic Shadow Trooper Helm\" ?", false),
        new Option<bool>("54447", "Twicket", "Mode: [select] only\nShould the bot buy \"Twicket\" ?", false),
        new Option<bool>("54448", "Twig Fortuna", "Mode: [select] only\nShould the bot buy \"Twig Fortuna\" ?", false),
        new Option<bool>("54449", "Twill-ek", "Mode: [select] only\nShould the bot buy \"Twill-ek\" ?", false),
        new Option<bool>("54440", "StarCrasher", "Mode: [select] only\nShould the bot buy \"StarCrasher\" ?", false),
        new Option<bool>("54441", "StarCrasher Helm", "Mode: [select] only\nShould the bot buy \"StarCrasher Helm\" ?", false),
        new Option<bool>("54442", "StarCrasher Helm + Scarf", "Mode: [select] only\nShould the bot buy \"StarCrasher Helm + Scarf\" ?", false),
        new Option<bool>("54443", "StarCrasher Hood", "Mode: [select] only\nShould the bot buy \"StarCrasher Hood\" ?", false),
        new Option<bool>("54444", "StarCrasher Backblades", "Mode: [select] only\nShould the bot buy \"StarCrasher Backblades\" ?", false),
        new Option<bool>("54445", "StarCrasher Lightblade", "Mode: [select] only\nShould the bot buy \"StarCrasher Lightblade\" ?", false),
        new Option<bool>("54446", "StarCrasher Lightblades", "Mode: [select] only\nShould the bot buy \"StarCrasher Lightblades\" ?", false),
        new Option<bool>("54455", "Parodian Guard", "Mode: [select] only\nShould the bot buy \"Parodian Guard\" ?", false),
        new Option<bool>("54456", "Parodian Guard Helm", "Mode: [select] only\nShould the bot buy \"Parodian Guard Helm\" ?", false),
        new Option<bool>("54457", "Parodian Guard's Sheathed Daggers", "Mode: [select] only\nShould the bot buy \"Parodian Guard's Sheathed Daggers\" ?", false),
        new Option<bool>("54458", "Parodian Guard's Sheathed Polearm", "Mode: [select] only\nShould the bot buy \"Parodian Guard's Sheathed Polearm\" ?", false),
        new Option<bool>("54459", "Parodian Guard's Polearm", "Mode: [select] only\nShould the bot buy \"Parodian Guard's Polearm\" ?", false),
        new Option<bool>("54460", "Parodian Guard's Daggers", "Mode: [select] only\nShould the bot buy \"Parodian Guard's Daggers\" ?", false),
    };
}
