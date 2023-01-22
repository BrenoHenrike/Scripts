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

public class WorldCoreMerge
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
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("worldsoul", 1572, findIngredients, itemBlackList: new[] { "Core Guardian Bank" });

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

                case "Guardian Shard":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("worldsoul", "r4", "Left", "*", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43212", "Samurai Seraph", "Mode: [select] only\nShould the bot buy \"Samurai Seraph\" ?", false),
        new Option<bool>("43216", "Sheathed Seraph Katanas", "Mode: [select] only\nShould the bot buy \"Sheathed Seraph Katanas\" ?", false),
        new Option<bool>("43214", "Samurai Seraph Backblade", "Mode: [select] only\nShould the bot buy \"Samurai Seraph Backblade\" ?", false),
        new Option<bool>("43215", "Samurai Seraph's Katana", "Mode: [select] only\nShould the bot buy \"Samurai Seraph's Katana\" ?", false),
        new Option<bool>("43197", "Weaponized Dark Shard", "Mode: [select] only\nShould the bot buy \"Weaponized Dark Shard\" ?", false),
        new Option<bool>("43196", "Celestial Light Daggers", "Mode: [select] only\nShould the bot buy \"Celestial Light Daggers\" ?", false),
        new Option<bool>("43221", "Core Guardian Bank", "Mode: [select] only\nShould the bot buy \"Core Guardian Bank\" ?", false),
        new Option<bool>("43213", "Samurai Seraph's Hat + Mask", "Mode: [select] only\nShould the bot buy \"Samurai Seraph's Hat + Mask\" ?", false),
        new Option<bool>("43222", "Samurai Seraph's Hat", "Mode: [select] only\nShould the bot buy \"Samurai Seraph's Hat\" ?", false),
    };
}
