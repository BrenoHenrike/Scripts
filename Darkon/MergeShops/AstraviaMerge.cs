/*
name:  Astravia Merge
description:  Astravia Merge
tags: astravia, merge, mergeshop
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

public class AstraviaMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
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
        Adv.StartBuyAllMerge("astravia", 1987, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "La's Gratitude":
                    Darkon.LasGratitude(quant);
                    break;

                case "The Moon's Head":
                case "The Moon's Cloak":
                    Core.HuntMonster("astravia", "The Moon", req.Name, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("60172", "The Moon's Amalgamation", "Mode: [select] only\nShould the bot buy \"The Moon's Amalgamation\" ?", false),
        new Option<bool>("60175", "The Moon's Hallowed Cloak", "Mode: [select] only\nShould the bot buy \"The Moon's Hallowed Cloak\" ?", false),
        new Option<bool>("60156", "Astravian Officer", "Mode: [select] only\nShould the bot buy \"Astravian Officer\" ?", false),
        new Option<bool>("60166", "Astravian Officer's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Astravian Officer's Hat + Locks\" ?", false),
        new Option<bool>("60165", "Astravian Officer's Hat", "Mode: [select] only\nShould the bot buy \"Astravian Officer's Hat\" ?", false),
        new Option<bool>("60161", "Carlton's Hair", "Mode: [select] only\nShould the bot buy \"Carlton's Hair\" ?", false),
        new Option<bool>("60164", "Talis's Hair", "Mode: [select] only\nShould the bot buy \"Talis's Hair\" ?", false),
        new Option<bool>("60162", "Kasper's Hair", "Mode: [select] only\nShould the bot buy \"Kasper's Hair\" ?", false),
        new Option<bool>("60163", "Rosa's Hair", "Mode: [select] only\nShould the bot buy \"Rosa's Hair\" ?", false),
        new Option<bool>("58078", "Astravian Mercenary's Dagger", "Mode: [select] only\nShould the bot buy \"Astravian Mercenary's Dagger\" ?", false),
        new Option<bool>("58082", "Astravia Mercenary's Boomstick", "Mode: [select] only\nShould the bot buy \"Astravia Mercenary's Boomstick\" ?", false),
    };
}
