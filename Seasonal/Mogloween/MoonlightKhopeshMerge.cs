/*
name: Moonlight Khopesh Merge
description: This bot will farm the items belonging to the selected mode for the Moonlight Khopesh Merge [1175] in /bot.map.name
tags: moonlight, khopesh, merge, bot.map.name, moonlit, great, lunar, stellar, celestial
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/CruxShip.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MoonlightKhopeshMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CruxShip CruxShip = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Khonsu Seal" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        CruxShip.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge(Bot.Map.Name, 1175, findIngredients, buyOnlyThis, buyMode: buyMode);
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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Khonsu Seal":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("cruxship", "Apephryx", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("31983", "Moonlit Khopesh", "Mode: [select] only\nShould the bot buy \"Moonlit Khopesh\" ?", false),
        new Option<bool>("31984", "Great Moonlit Khopesh", "Mode: [select] only\nShould the bot buy \"Great Moonlit Khopesh\" ?", false),
        new Option<bool>("31985", "Lunar Khopesh", "Mode: [select] only\nShould the bot buy \"Lunar Khopesh\" ?", false),
        new Option<bool>("31986", "Stellar Khopesh", "Mode: [select] only\nShould the bot buy \"Stellar Khopesh\" ?", false),
        new Option<bool>("31987", "Celestial Khopesh", "Mode: [select] only\nShould the bot buy \"Celestial Khopesh\" ?", false),
    };
}
