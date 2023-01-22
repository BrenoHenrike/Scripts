/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LunaCoveMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

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
        if (!Core.isSeasonalMapActive("lunacove"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("lunacove", 32, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Moon Rock Fragments":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("lunacove", "r2", "Right", "*", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30397", "Beachin' Werewolf", "Mode: [select] only\nShould the bot buy \"Beachin' Werewolf\" ?", false),
        new Option<bool>("30398", "Were-diver Morph", "Mode: [select] only\nShould the bot buy \"Were-diver Morph\" ?", false),
        new Option<bool>("30399", "Snorkling Wolf", "Mode: [select] only\nShould the bot buy \"Snorkling Wolf\" ?", false),
        new Option<bool>("30400", "Wolf on the Beach Morph", "Mode: [select] only\nShould the bot buy \"Wolf on the Beach Morph\" ?", false),
        new Option<bool>("30391", "Spiralling Stars Hair", "Mode: [select] only\nShould the bot buy \"Spiralling Stars Hair\" ?", false),
        new Option<bool>("30392", "Spiralling Stars Locks", "Mode: [select] only\nShould the bot buy \"Spiralling Stars Locks\" ?", false),
        new Option<bool>("30402", "Crescent Moon Staff", "Mode: [select] only\nShould the bot buy \"Crescent Moon Staff\" ?", false),
    };
}
