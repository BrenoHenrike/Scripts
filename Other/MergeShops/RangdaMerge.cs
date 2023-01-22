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

public class RangdaMerge
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
        Adv.StartBuyAllMerge("rangda", 1901, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Rangda’s Mask":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    Bot.Quests.UpdateQuest(7622);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("rangda", "Rangda", req.Name, quant, false);
                    break;

                case "Abhorrent Remnant":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("rangda", "Leyak", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("55784", "Gatotkaca", "Mode: [select] only\nShould the bot buy \"Gatotkaca\" ?", false),
        new Option<bool>("55785", "Gatot Crown and Locks", "Mode: [select] only\nShould the bot buy \"Gatot Crown and Locks\" ?", false),
        new Option<bool>("55786", "Gatot Crown", "Mode: [select] only\nShould the bot buy \"Gatot Crown\" ?", false),
        new Option<bool>("55787", "Bearded Gatot Crown", "Mode: [select] only\nShould the bot buy \"Bearded Gatot Crown\" ?", false),
        new Option<bool>("55795", "Gatot Sheath and Keris", "Mode: [select] only\nShould the bot buy \"Gatot Sheath and Keris\" ?", false),
        new Option<bool>("55791", "Arjuna's Bow", "Mode: [select] only\nShould the bot buy \"Arjuna's Bow\" ?", false),
        new Option<bool>("55794", "Gatot Mace", "Mode: [select] only\nShould the bot buy \"Gatot Mace\" ?", false),
        new Option<bool>("55790", "Gatot Wings", "Mode: [select] only\nShould the bot buy \"Gatot Wings\" ?", false),
    };
}
