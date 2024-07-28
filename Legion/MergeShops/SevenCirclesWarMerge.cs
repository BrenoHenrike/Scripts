/*
name: SevenCirclesWarMerge
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Legion/MergeShops/SevenCirclesMerge.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SevenCirclesWarMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    public CoreLegion Legion = new();
    private CoreAdvanced Adv = new();
    public SevenCircles Circles = new();
    public SevenCirclesMerge SevenCirclesMerge = new();
    private static CoreAdvanced sAdv = new();
    private HeadoftheLegionBeast HeadoftheLegionBeast = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;


    public string[] HeadLegionBeast =
        {
        "Penance",
        "Essence of Wrath",
        "Essence of Violence",
        "Essence of Treachery",
        "Souls of Heresy",
        "Indulgence",
        "Beast Soul",
        "Helms of the Seven Circles",
        "Faces of Violence",
        "Crown of Wrath",
        "Stare of Greed",
        "Gluttony's Maw",
        "Aspect of Luxuria",
        "Face of Treachery"
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "Indulgence", "Beast Soul", "Souls of Heresy", "Essence of Treachery", "Essence of Wrath", "Essence of Violence" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Circles.CirclesWar();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sevencircleswar", 1984, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Indulgence":
                    HeadoftheLegionBeast.Indulgence(req.Quantity);
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Beast Soul":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    //Adv.BestGear(RacialGearBoost.Undead);
                    Adv.SmartEnhance(Core.SoloClass);
                    Core.HuntMonster("sevencircleswar", "The Beast", req.Name, quant, isTemp: false, publicRoom: true);
                    break;

                case "Souls of Heresy":
                    HeadoftheLegionBeast.SoulsHeresy(req.Quantity);
                    break;

                case "Penance":
                    HeadoftheLegionBeast.Penance(req.Quantity);
                    break;


                case "Essence of Treachery":
                    HeadoftheLegionBeast.EssenceTreachery(req.Quantity);
                    break;

                case "Essence of Wrath":
                    HeadoftheLegionBeast.EssenceWrath(req.Quantity);
                    break;

                case "Essence of Violence":
                    HeadoftheLegionBeast.EssenceViolence(req.Quantity);
                    break;

                //these come from circles not war:
                case "Stare of Greed":
                case "Gluttony's Maw":
                case "Aspect of Luxuria":
                    SevenCirclesMerge.BuyAllMerge(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("59754", "Faces of Violence", "Mode: [select] only\nShould the bot buy \"Faces of Violence\" ?", false),
        new Option<bool>("59755", "Crown of Wrath", "Mode: [select] only\nShould the bot buy \"Crown of Wrath\" ?", false),
        new Option<bool>("60038", "Head of the Legion Beast", "Mode: [select] only\nShould the bot buy \"Head of the Legion Beast\" ?", false),
        new Option<bool>("59721", "Face of Treachery", "Mode: [select] only\nShould the bot buy \"Face of Treachery\" ?", false),
        new Option<bool>("60095", "Legion Viking", "Mode: [select] only\nShould the bot buy \"Legion Viking\" ?", false),
        new Option<bool>("60096", "Legion Viking Helm", "Mode: [select] only\nShould the bot buy \"Legion Viking Helm\" ?", false),
        new Option<bool>("60097", "Legion Viking Mask", "Mode: [select] only\nShould the bot buy \"Legion Viking Mask\" ?", false),
        new Option<bool>("60101", "Legion Viking Hatchet", "Mode: [select] only\nShould the bot buy \"Legion Viking Hatchet\" ?", false),
        new Option<bool>("60099", "Legion Viking Big Axe", "Mode: [select] only\nShould the bot buy \"Legion Viking Big Axe\" ?", false),
        new Option<bool>("60100", "Legion Viking Flail", "Mode: [select] only\nShould the bot buy \"Legion Viking Flail\" ?", false),
        new Option<bool>("60098", "Legion Viking Back Axe", "Mode: [select] only\nShould the bot buy \"Legion Viking Back Axe\" ?", false),
    };
}
