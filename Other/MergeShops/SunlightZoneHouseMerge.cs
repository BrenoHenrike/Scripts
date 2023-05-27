/*
name: Sunlight Zone House Merge
description: This bot will farm the items belonging to the selected mode for the Sunlight Zone House Merge [2289] in /sunlightzone
tags: sunlight, zone, house, merge, sunlightzone, stern, song, guest, taras, temporary, rest, disgruntled, mi
*/
//cs_include Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SunlightZoneHouseMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Sun Zone Chit", "Undine Visitor Badge"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
         AOR.SunlightZone();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sunlightzone", 2289, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Sun Zone Chit":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9252);
                        Core.HuntMonster("sunlightzone", "Marine Snow", "Marine Sample", log:false);
                        Core.HuntMonster("sunlightzone", "Infernal Illusion", "Infernal Sample", 10, log:false);
                        Core.HuntMonster("sunlightzone", "Seraphic Illusion", "Seraphic Sample", 10, log:false);
                    Core.CancelRegisteredQuests();
                    break;

                case "Undine Visitor Badge":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("sunlightzone", "Spectral Jellyfish", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77955", "Stern Song House Guest", "Mode: [select] only\nShould the bot buy \"Stern Song House Guest\" ?", false),
        new Option<bool>("77993", "Tara's Temporary Rest Guest", "Mode: [select] only\nShould the bot buy \"Tara's Temporary Rest Guest\" ?", false),
        new Option<bool>("77996", "Disgruntled Mi Guest", "Mode: [select] only\nShould the bot buy \"Disgruntled Mi Guest\" ?", false),
        new Option<bool>("77997", "Disgruntled Song Guest", "Mode: [select] only\nShould the bot buy \"Disgruntled Song Guest\" ?", false),
    };
}
