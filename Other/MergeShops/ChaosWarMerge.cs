/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ChaosWarMerge
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
        Core.BankingBlackList.AddRange(new[] { "Chaos Eye", "Chaos Tentacle " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("chaoswar", 642, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Chaos Eye":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.KillMonster("chaoswar", "r2", "Spawn", "*", req.Name, quant, isTemp: false, log: false);
                    break;
                case "Chaos Tentacle":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.KillMonster("chaoswar", "r2", "Spawn", "*", req.Name, quant, isTemp: false, log: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("17839", "Chaos Destroyer", "Mode: [select] only\nShould the bot buy \"Chaos Destroyer\" ?", false),
        new Option<bool>("17840", "Chaos Destroyer Helmet", "Mode: [select] only\nShould the bot buy \"Chaos Destroyer Helmet\" ?", false),
        new Option<bool>("17841", "Chaos Destroyer Cloak", "Mode: [select] only\nShould the bot buy \"Chaos Destroyer Cloak\" ?", false),
        new Option<bool>("17559", "Chaoaxe", "Mode: [select] only\nShould the bot buy \"Chaoaxe\" ?", false),
        new Option<bool>("17842", "DESTROY CHAOS", "Mode: [select] only\nShould the bot buy \"DESTROY CHAOS\" ?", false),
        new Option<bool>("17551", "Maul of the Mauler", "Mode: [select] only\nShould the bot buy \"Maul of the Mauler\" ?", false),
        new Option<bool>("18630", "Ebon Chaos Longbow", "Mode: [select] only\nShould the bot buy \"Ebon Chaos Longbow\" ?", false),
        new Option<bool>("18629", "Ebon Chaos Shotgun", "Mode: [select] only\nShould the bot buy \"Ebon Chaos Shotgun\" ?", false),
        new Option<bool>("18632", "Ebon Chaos Katanas", "Mode: [select] only\nShould the bot buy \"Ebon Chaos Katanas\" ?", false),
        new Option<bool>("18633", "Ebon Chaos Revolvers", "Mode: [select] only\nShould the bot buy \"Ebon Chaos Revolvers\" ?", false),
        new Option<bool>("17932", "Chaorrupter Unlocked", "Mode: [select] only\nShould the bot buy \"Chaorrupter Unlocked\" ?", false),
        new Option<bool>("17873", "Chaorrupter Unlocked", "Mode: [select] only\nShould the bot buy \"Chaorrupter Unlocked\" ?", false),
    };
}
