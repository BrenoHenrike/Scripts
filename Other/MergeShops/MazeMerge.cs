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

public class MazeMerge
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
        Core.BankingBlackList.AddRange(new[] { "Mehensi Fang " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("whitehole", 1273, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Mehensi Fang":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("whitehole", "Mehensi Serpent", req.Name, quant, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("35571", "Black Hole Spear", "Mode: [select] only\nShould the bot buy \"Black Hole Spear\" ?", false),
        new Option<bool>("35572", "Black Hole Sword", "Mode: [select] only\nShould the bot buy \"Black Hole Sword\" ?", false),
        new Option<bool>("35574", "Colossal Black Hole Sword", "Mode: [select] only\nShould the bot buy \"Colossal Black Hole Sword\" ?", false),
        new Option<bool>("35499", "Dark Flaming Fists", "Mode: [select] only\nShould the bot buy \"Dark Flaming Fists\" ?", false),
        new Option<bool>("35579", "Void Wanderer", "Mode: [select] only\nShould the bot buy \"Void Wanderer\" ?", false),
        new Option<bool>("35576", "Dark Matter Cape", "Mode: [select] only\nShould the bot buy \"Dark Matter Cape\" ?", false),
    };
}
