/*
name: Undine Commissary Merge
description: This bot will farm the items belonging to the selected mode for the Undine Commissary Merge [2288] in /sunlightzone
tags: undine, commissary, merge, sunlightzone, fas, casual, ensemble, mis, doctor, defence, director, researcher, clean, bob, cut, horn, songs, high, ponytail, glasses
*/
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/AgeofRuin/CoreAOR.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class UndineCommissaryMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();


    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Undine Base Scrip", "Sun Zone Chit" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.SunlightZone();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sunlightzone", 2288, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Undine Base Scrip":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("sunlightzone", "Marine Snow", req.Name, quant, false, false);
                    break;

                case "Sun Zone Chit":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9252);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("sunlightzone", "Marine Snow", "Marine Sample", log: false);
                        Core.HuntMonster("sunlightzone", "Infernal Illusion", "Infernal Sample", 10, log: false);
                        Core.HuntMonster("sunlightzone", "Seraphic Illusion", "Seraphic Sample", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77957", "Fa's Casual Ensemble", "Mode: [select] only\nShould the bot buy \"Fa's Casual Ensemble\" ?", false),
        new Option<bool>("77960", "Mi's Casual Ensemble", "Mode: [select] only\nShould the bot buy \"Mi's Casual Ensemble\" ?", false),
        new Option<bool>("77963", "Undine Doctor", "Mode: [select] only\nShould the bot buy \"Undine Doctor\" ?", false),
        new Option<bool>("77964", "Undine Defence Director", "Mode: [select] only\nShould the bot buy \"Undine Defence Director\" ?", false),
        new Option<bool>("77965", "Undine Researcher", "Mode: [select] only\nShould the bot buy \"Undine Researcher\" ?", false),
        new Option<bool>("77958", "Fa's Clean Bob Cut", "Mode: [select] only\nShould the bot buy \"Fa's Clean Bob Cut\" ?", false),
        new Option<bool>("77959", "Fa's Clean Bob Cut Horn", "Mode: [select] only\nShould the bot buy \"Fa's Clean Bob Cut Horn\" ?", false),
        new Option<bool>("77961", "Song's High Ponytail Glasses", "Mode: [select] only\nShould the bot buy \"Song's High Ponytail Glasses\" ?", false),
        new Option<bool>("77962", "Song's High Ponytail", "Mode: [select] only\nShould the bot buy \"Song's High Ponytail\" ?", false),
    };
}
