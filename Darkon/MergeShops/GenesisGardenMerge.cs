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

public class GenesisGardenMerge
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
        Adv.StartBuyAllMerge("genesisgarden", 2136, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Mourning Flower":
                    Darkon.MourningFlower(quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70354", "Prince Darkon's Armor", "Mode: [select] only\nShould the bot buy \"Prince Darkon's Armor\" ?", false),
        new Option<bool>("70355", "Prince Darkon's Casual Armor", "Mode: [select] only\nShould the bot buy \"Prince Darkon's Casual Armor\" ?", false),
        new Option<bool>("70356", "Prince Darkon's Scarred Hair", "Mode: [select] only\nShould the bot buy \"Prince Darkon's Scarred Hair\" ?", false),
        new Option<bool>("70357", "Prince Darkon's Scarred Morph", "Mode: [select] only\nShould the bot buy \"Prince Darkon's Scarred Morph\" ?", false),
        new Option<bool>("70358", "So's Student Uniform", "Mode: [select] only\nShould the bot buy \"So's Student Uniform\" ?", false),
        new Option<bool>("70359", "So's Student Hair", "Mode: [select] only\nShould the bot buy \"So's Student Hair\" ?", false),
        new Option<bool>("70360", "So's Student Morph", "Mode: [select] only\nShould the bot buy \"So's Student Morph\" ?", false),
        new Option<bool>("70361", "Jus Divinum Major", "Mode: [select] only\nShould the bot buy \"Jus Divinum Major\" ?", false),
        new Option<bool>("70362", "Jus Divinum Major Helmet", "Mode: [select] only\nShould the bot buy \"Jus Divinum Major Helmet\" ?", false),
        new Option<bool>("70363", "Jus Divinum Major Cape", "Mode: [select] only\nShould the bot buy \"Jus Divinum Major Cape\" ?", false),
    };
}
