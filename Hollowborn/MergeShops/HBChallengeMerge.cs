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

public class HBChallengeMerge
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
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hbchallenge", 1887, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hollowborn Adept":
                case "Hollowborn Locks":
                case "Hollowborn Shag":
                case "Hollowborn Blades":
                case "Hollowborn Cleaver":
                case "Hollowborn Executioner's Axe":
                    Core.HuntMonster("hbchallenge", "Shadow Rider", req.Name, isTemp: false);
                    break;

                case "Hollowborn Spirit":
                case "Hollowborn Spite":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7548);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("hbchallenge", "Enter", "Spawn", "Hollowborn Tamer", "Hollowborn Tamer Defeated", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54655", "Hollowborn Executioner", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner\" ?", false),
        new Option<bool>("54657", "Hollowborn Executioner's Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner's Hood\" ?", false),
        new Option<bool>("54660", "Hollowborn Gas Mask + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Gas Mask + Locks\" ?", false),
        new Option<bool>("54661", "Hollowborn Gas Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Gas Mask\" ?", false),
        new Option<bool>("54663", "Hollowborn Scarves", "Mode: [select] only\nShould the bot buy \"Hollowborn Scarves\" ?", false),
        new Option<bool>("54664", "Hollowborn Executioner's Bite", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner's Bite\" ?", false),
        new Option<bool>("54955", "Dual Hollowborn Cleavers", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Cleavers\" ?", false),
        new Option<bool>("54973", "Hollowborn Executioner's Bite + Axe", "Mode: [select] only\nShould the bot buy \"Hollowborn Executioner's Bite + Axe\" ?", false),
    };
}
