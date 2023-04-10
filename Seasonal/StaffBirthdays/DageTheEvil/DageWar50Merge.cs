/*
name: DageWar50Merge
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DageWar50Merge
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
        Core.BankingBlackList.AddRange(new[] { "Cyber Skull", "UnDeath Core" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("futurewardage", 1377, findIngredients, buyOnlyThis, buyMode: buyMode);

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
               
                case "Cyber Skull":
                case "UnDeath Core":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("futurewardage", "SF3017 Paragonator", req.Name, quant, log: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("38435", "Eternal PainSaw", "Mode: [select] only\nShould the bot buy \"Eternal PainSaw\" ?", false),
        new Option<bool>("38806", "Dual Eternal PainSaws", "Mode: [select] only\nShould the bot buy \"Dual Eternal PainSaws\" ?", false),
        new Option<bool>("38808", "Enchanted Eternal PainSaw", "Mode: [select] only\nShould the bot buy \"Enchanted Eternal PainSaw\" ?", false),
        new Option<bool>("38809", "Enchanted Eternal PainSaws", "Mode: [select] only\nShould the bot buy \"Enchanted Eternal PainSaws\" ?", false),
    };
}
