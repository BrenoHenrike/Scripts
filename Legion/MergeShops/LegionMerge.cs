/*
name: Legion Merge
description: This bot will farm the items belonging to the selected mode for the Legion Merge [1207] in /shadowblast
tags: legion, merge, shadowblast, executioner, evolved, soul, mender, soulstealer
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LegionMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
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
        Core.BankingBlackList.AddRange(new[] { "Emblem of Dage", "Diamond Token of Dage", "Legion Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowblast", 1207, findIngredients, buyOnlyThis, buyMode: buyMode, Group: "Last");

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

                case "Emblem of Dage":
                    Legion.EmblemofDage(quant);
                    break;

                case "Diamond Token of Dage":
                    Legion.DiamondTokenofDage(quant);
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("33153", "Evolved Soul Mender", "Mode: [select] only\nShould the bot buy \"Evolved Soul Mender\" ?", false),

        new Option<bool>("33163", "Legion Executioner", "Mode: [select] only\nShould the bot buy \"Legion Executioner\"\n[Emblemof Dage/Diamond Token] ?", false),
        new Option<bool>("33173", "Legion Soulstealer", "Mode: [select] only\nShould the bot buy \"Legion Soulstealer\"\n[Emblemof Dage/Diamond Token]  ?", false),
        new Option<bool>("33175", "Legion Soulstealer Hair", "Mode: [select] only\nShould the bot buy \"Legion Soulstealer Hair\"\n[Emblemof Dage/Diamond Token]  ?", false),
        new Option<bool>("33174", "Legion Soulstealer Locks", "Mode: [select] only\nShould the bot buy \"Legion Soulstealer Locks\"\n[Emblemof Dage/Diamond Token]  ?", false),
    };
}
