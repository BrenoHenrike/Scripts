/*
name: Molten Forge Merge
description: This bot will farm the items belonging to the selected mode for the Molten Forge Merge [1428] in /battleundere
tags: molten, forge, merge, battleundere, lavamorphosis, lavamorph, eyes, visor, wings, stinger, venom, banes
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MoltenForgeMerge
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
        Core.BankingBlackList.AddRange(new[] { "Molten Core", "Crystamorphosis", "Crystamorph Helm", "Crystamorph Eyes", "Crystamorph Visor", "Crystamorph Wings", "Crystamorph Stinger", "Crystamorph Venom", "Crystamorph Dual Banes" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battleundere", 1428, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Molten Core":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("battleundere", "Lava Guard", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Crystamorphosis":
                case "Crystamorph Helm":
                case "Crystamorph Eyes":
                case "Crystamorph Visor":
                case "Crystamorph Wings":
                case "Crystamorph Stinger":
                case "Crystamorph Venom":
                case "Crystamorph Dual Banes":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battleundere", "r9", "left", "*", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("40605", "Lavamorphosis", "Mode: [select] only\nShould the bot buy \"Lavamorphosis\" ?", false),
        new Option<bool>("40607", "Lavamorph Helm", "Mode: [select] only\nShould the bot buy \"Lavamorph Helm\" ?", false),
        new Option<bool>("40608", "Lavamorph Eyes", "Mode: [select] only\nShould the bot buy \"Lavamorph Eyes\" ?", false),
        new Option<bool>("40606", "Lavamorph Visor", "Mode: [select] only\nShould the bot buy \"Lavamorph Visor\" ?", false),
        new Option<bool>("40612", "Lavamorph Wings", "Mode: [select] only\nShould the bot buy \"Lavamorph Wings\" ?", false),
        new Option<bool>("40609", "Lavamorph Stinger", "Mode: [select] only\nShould the bot buy \"Lavamorph Stinger\" ?", false),
        new Option<bool>("40611", "Lavamorph Venom", "Mode: [select] only\nShould the bot buy \"Lavamorph Venom\" ?", false),
        new Option<bool>("40610", "Lavamorph Dual Banes", "Mode: [select] only\nShould the bot buy \"Lavamorph Dual Banes\" ?", false),
    };
}
