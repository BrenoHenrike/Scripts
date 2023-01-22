/*
name: null
description: null
tags: null
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

public class FirstObservatoryMerge
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
        Adv.StartBuyAllMerge("firstobservatory", 2130, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ancient Remnant":
                    Darkon.AncientRemnant(quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("69703", "Aurola's Armor", "Mode: [select] only\nShould the bot buy \"Aurola's Armor\" ?", false),
        new Option<bool>("69704", "Aurola's Coat", "Mode: [select] only\nShould the bot buy \"Aurola's Coat\" ?", false),
        new Option<bool>("69705", "Princess Suki's Armor", "Mode: [select] only\nShould the bot buy \"Princess Suki's Armor\" ?", false),
        new Option<bool>("69706", "Princess Suki's Cut", "Mode: [select] only\nShould the bot buy \"Princess Suki's Cut\" ?", false),
        new Option<bool>("69707", "Princess Suki's Hair", "Mode: [select] only\nShould the bot buy \"Princess Suki's Hair\" ?", false),
        new Option<bool>("69708", "Princess Suki's Morph", "Mode: [select] only\nShould the bot buy \"Princess Suki's Morph\" ?", false),
        new Option<bool>("69709", "Princess Suki's Gauntlets", "Mode: [select] only\nShould the bot buy \"Princess Suki's Gauntlets\" ?", false),
    };
}
