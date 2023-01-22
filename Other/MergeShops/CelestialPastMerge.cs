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

public class CelestialPastMerge
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
        Core.BankingBlackList.AddRange(new[] { "Celestial Quintessence " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("celestialpast", 1909, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Celestial Quintessence":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("CelestialPast", "Blessed Bear", req.Name, quant, isTemp: false);
                        Core.HuntMonster("CelestialPast", "Blessed Deer", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("56082", "Celestial Summoner", "Mode: [select] only\nShould the bot buy \"Celestial Summoner\" ?", false),
        new Option<bool>("56083", "Celestial Summoner Hair", "Mode: [select] only\nShould the bot buy \"Celestial Summoner Hair\" ?", false),
        new Option<bool>("56084", "Celestial Summoner Helm", "Mode: [select] only\nShould the bot buy \"Celestial Summoner Helm\" ?", false),
        new Option<bool>("56085", "Celestial Summoner Hood", "Mode: [select] only\nShould the bot buy \"Celestial Summoner Hood\" ?", false),
        new Option<bool>("56086", "Celestial Summoner Rune Cape", "Mode: [select] only\nShould the bot buy \"Celestial Summoner Rune Cape\" ?", false),
        new Option<bool>("56153", "Celestial Summoner Rune", "Mode: [select] only\nShould the bot buy \"Celestial Summoner Rune\" ?", false),
        new Option<bool>("56154", "Celestial Summoner Cape", "Mode: [select] only\nShould the bot buy \"Celestial Summoner Cape\" ?", false),
    };
}
