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

public class AstraviaPastMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDarkon Darkon = new CoreDarkon();
    public CoreAstravia CoreAstravia = new();

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
        Adv.StartBuyAllMerge("astraviapast", 2126, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                case "Suki's Prestige":
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Darkon.SukisPrestiege(quant);
                    break;

                case "Prince Drago's Attire":
                case "Prince Drago's Hair":
                case "Prince Drago's Dark Attire":
                    Core.HuntMonster("astraviapast", "Forsaken Husk", req.Name, isTemp: false);
                    break;

                case "Suki's Casual Armor":
                case "Suki's Ponytail":
                    Core.HuntMonster("astraviapast", "Aurola", req.Name, isTemp: false);
                    break;

                case "Regulus' Hair":
                    Core.HuntMonster("astraviapast", "Regulus", req.Name, isTemp: false);
                    break;

                case "Titania's Hair":
                    Core.HuntMonster("astraviapast", "Titania", req.Name, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("69155", "Prince Drago's Royal Attire", "Mode: [select] only\nShould the bot buy \"Prince Drago's Royal Attire\" ?", false),
        new Option<bool>("69156", "Prince Drago's Royal Dark Attire", "Mode: [select] only\nShould the bot buy \"Prince Drago's Royal Dark Attire\" ?", false),
        new Option<bool>("69158", "Prince Drago's Morph", "Mode: [select] only\nShould the bot buy \"Prince Drago's Morph\" ?", false),
        new Option<bool>("69159", "Prince Drago's Scarred Morph", "Mode: [select] only\nShould the bot buy \"Prince Drago's Scarred Morph\" ?", false),
        new Option<bool>("69160", "Suki's Armor", "Mode: [select] only\nShould the bot buy \"Suki's Armor\" ?", false),
        new Option<bool>("69163", "Suki's Morph", "Mode: [select] only\nShould the bot buy \"Suki's Morph\" ?", false),
        new Option<bool>("69164", "Suki's Gauntlets", "Mode: [select] only\nShould the bot buy \"Suki's Gauntlets\" ?", false),
        new Option<bool>("69166", "Regulus' Morph", "Mode: [select] only\nShould the bot buy \"Regulus' Morph\" ?", false),
        new Option<bool>("69168", "Titania's Morph", "Mode: [select] only\nShould the bot buy \"Titania's Morph\" ?", false),
    };
}
