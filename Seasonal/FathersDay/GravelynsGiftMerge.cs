/*
name: Gravelyns Gift Merge
description: This bot will farm the items belonging to the selected mode for the Gravelyns Gift Merge [2299] in /nursery
tags: gravelyns, gift, merge, nursery, doom, harried, legacy, furious, furred, shoulder, dark
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/FathersDay/HoratioQuests.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GravelynsGiftMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private HoratioQuests HQ = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Doom Essence" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        HQ.Horatio();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("nursery", 2299, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Doom Essence":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6948);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("nursery", "Flesh Golem", "Treasure Found", 10, true, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76226", "Gravelyn's Doom Gift", "Mode: [select] only\nShould the bot buy \"Gravelyn's Doom Gift\" ?", false),
        new Option<bool>("76229", "Harried Doom Spikes", "Mode: [select] only\nShould the bot buy \"Harried Doom Spikes\" ?", false),
        new Option<bool>("76228", "Legacy of Doom Helm", "Mode: [select] only\nShould the bot buy \"Legacy of Doom Helm\" ?", false),
        new Option<bool>("76227", "Furious Legacy of Doom Helm", "Mode: [select] only\nShould the bot buy \"Furious Legacy of Doom Helm\" ?", false),
        new Option<bool>("76230", "Furred Shoulder Cape", "Mode: [select] only\nShould the bot buy \"Furred Shoulder Cape\" ?", false),
        new Option<bool>("76231", "Fur-ious Cape of Doom", "Mode: [select] only\nShould the bot buy \"Fur-ious Cape of Doom\" ?", false),
        new Option<bool>("76233", "Dark Legacy Doom Blade", "Mode: [select] only\nShould the bot buy \"Dark Legacy Doom Blade\" ?", false),
        new Option<bool>("76234", "Furious Legacy Doom Blade", "Mode: [select] only\nShould the bot buy \"Furious Legacy Doom Blade\" ?", false),
    };
}
