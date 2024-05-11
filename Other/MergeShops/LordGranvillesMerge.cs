/*
name: Lord Granvilles Merge
description: This bot will farm the items belonging to the selected mode for the Lord Granvilles Merge [2442] in /balemorale
tags: lord, granvilles, merge, balemorale, gold, voucher, k, defender, defenders, sheathed, noble
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LordGranvillesMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Balemorale Crest" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("balemorale", 2442, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Balemorale Crest":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("balemorale", "r2", "Left", "*", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("20702", "Defender Of Balemorale", "Mode: [select] only\nShould the bot buy \"Defender Of Balemorale\" ?", false),
        new Option<bool>("20703", "Defender Of Balemorale Helm", "Mode: [select] only\nShould the bot buy \"Defender Of Balemorale Helm\" ?", false),
        new Option<bool>("20704", "Defenders of Balemorale Spikes", "Mode: [select] only\nShould the bot buy \"Defenders of Balemorale Spikes\" ?", false),
        new Option<bool>("20705", "Sheathed Balemorale Defender Blades", "Mode: [select] only\nShould the bot buy \"Sheathed Balemorale Defender Blades\" ?", false),
        new Option<bool>("20706", "Noble Defender Of Balemorale Blade", "Mode: [select] only\nShould the bot buy \"Noble Defender Of Balemorale Blade\" ?", false),
    };
}
