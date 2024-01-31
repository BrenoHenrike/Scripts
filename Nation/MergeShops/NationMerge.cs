/*
name: Nation Merge
description: This bot will farm the items belonging to the selected mode for the Nation Merge [1206] in /shadowblast
tags: nation, merge, shadowblast, polish, pet, soulstealer, horned, void, executioner
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation\NationLoyaltyRewarded.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NationMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreNation Nation = new();
    private NationLoyaltyRewarded NLR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Diamond Badge of Nulgath", "Emblem of Nulgath", "Diamond of Nulgath" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowblast", 1206, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Diamond Badge of Nulgath":
                    NLR.FarmQuest(new string[] { req.Name }, quant);
                    break;

                case "Emblem of Nulgath":
                    Nation.EmblemofNulgath(quant);
                    break;

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

            }
        }
    }

    
    public List<IOption> Select = new()
    {
        new Option<bool>("33172", "Polish Pet", "Mode: [select] only\nShould the bot buy \"Polish Pet\" ?", false),
        new Option<bool>("33176", "Nation Soulstealer", "Mode: [select] only\nShould the bot buy \"Nation Soulstealer\" ?", false),
        new Option<bool>("33177", "Nation SoulStealer Hood", "Mode: [select] only\nShould the bot buy \"Nation SoulStealer Hood\" ?", false),
        new Option<bool>("33178", "Nation SoulStealer Horned Hood", "Mode: [select] only\nShould the bot buy \"Nation SoulStealer Horned Hood\" ?", false),
        new Option<bool>("33162", "Void Executioner", "Mode: [select] only\nShould the bot buy \"Void Executioner\" ?", false),
    };
}
