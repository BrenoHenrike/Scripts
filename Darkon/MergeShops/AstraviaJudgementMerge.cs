/*
name:  Astravia Judgement Merge
description:  Astravia Judgement Merge
tags: astravia judgement, merge, mergeshop
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AstraviaJudgeMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDarkon Darkon = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("astraviajudge", 2065, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                // Add how to get items here
                case "A Melody":
                    Darkon.AMelody(quant);
                    break;

                case "Re's Party Attire":
                    Core.HuntMonster("astraviajudgement", "La", req.Name, quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("65302", "Mi's Attire", "Mode: [select] only\nShould the bot buy \"Mi's Attire\" ?", false),
        new Option<bool>("65303", "Mi's Sleeveless Attire", "Mode: [select] only\nShould the bot buy \"Mi's Sleeveless Attire\" ?", false),
        new Option<bool>("65304", "Mi's Hair", "Mode: [select] only\nShould the bot buy \"Mi's Hair\" ?", false),
        new Option<bool>("65305", "Mi's Antennae", "Mode: [select] only\nShould the bot buy \"Mi's Antennae\" ?", false),
        new Option<bool>("65306", "Mi's Morph", "Mode: [select] only\nShould the bot buy \"Mi's Morph\" ?", false),
        new Option<bool>("66332", "Re's Party Arms", "Mode: [select] only\nShould the bot buy \"Re's Party Arms\" ?", false),
        new Option<bool>("66334", "Astravian Officer's White Hat", "Mode: [select] only\nShould the bot buy \"Astravian Officer's White Hat\" ?", false),
        new Option<bool>("66335", "Astravian Officer's White Hat + Locks", "Mode: [select] only\nShould the bot buy \"Astravian Officer's White Hat + Locks\" ?", false),
    };
}

