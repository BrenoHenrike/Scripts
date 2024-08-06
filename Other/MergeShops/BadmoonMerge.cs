/*
name: Badmoon Merge
description: This bot will farm the items belonging to the selected mode for the Badmoon Merge [2470] in /badmoon
tags: badmoon, merge, badmoon, twisted, hunter, darkovian, captains, masked, hunters, arm, arms, sanguine, dussack, dussacks
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/GiantTaleStory.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Story/ShadowSlayerK.cs
//cs_include Scripts/Other\Various\ShadowslayerSummoningRitual.cs

//cs_include Scripts/Other\Various\ShadowslayerSummoningRitual2.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BadmoonMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private ShadowslayerSummoningRitual2 ssr2 = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Lunate Sigil", "Darkovian Hunter", "Darkovia Hunter's Cowl", "Iron Dussack"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("badmoon", 2470, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Lunate Sigil":
                    ssr2.LunateSigil(req.Quantity);
                    break;

                case "Darkovia Hunter's Cowl":
                case "Iron Dussack":
                    Core.EquipClass(ClassType.Farm);
                    Core.AddDrop(req.ID);
                    Core.KillMonster("badmoon", "r5", "left", "hunter", req.Name, req.Quantity, req.Temp);
                    break;

                    
                case "Darkovian Hunter":
                    Core.EquipClass(ClassType.Farm);
                    Core.AddDrop(req.ID);
                    Core.HuntMonster("badmoon", "Twisted Hunter", req.Name, req.Quantity, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("87583", "Twisted Hunter", "Mode: [select] only\nShould the bot buy \"Twisted Hunter\" ?", false),
        new Option<bool>("87586", "Darkovian Hunter Captain's Hat", "Mode: [select] only\nShould the bot buy \"Darkovian Hunter Captain's Hat\" ?", false),
        new Option<bool>("87585", "Darkovian Hunter Captain's Masked Hat", "Mode: [select] only\nShould the bot buy \"Darkovian Hunter Captain's Masked Hat\" ?", false),
        new Option<bool>("87590", "Twisted Hunter's Arm", "Mode: [select] only\nShould the bot buy \"Twisted Hunter's Arm\" ?", false),
        new Option<bool>("87591", "Twisted Hunter's Arms", "Mode: [select] only\nShould the bot buy \"Twisted Hunter's Arms\" ?", false),
        new Option<bool>("87588", "Sanguine Dussack", "Mode: [select] only\nShould the bot buy \"Sanguine Dussack\" ?", false),
        new Option<bool>("87589", "Sanguine Dussacks", "Mode: [select] only\nShould the bot buy \"Sanguine Dussacks\" ?", false),
    };
}
