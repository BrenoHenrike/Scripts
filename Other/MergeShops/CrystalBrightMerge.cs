/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/BrightCrystalStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CrystalBrightMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreStory Story = new();
    private static CoreAdvanced sAdv = new();
    private BrightCrystalStory BrightCrystal = new();
    

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Carnival Ticket"});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dreamforest", 1240, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Carnival Ticket":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreamforest", "*", "Carnival Ticket", 300);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34535", "Carnival Worker", "Mode: [select] only\nShould the bot buy \"Carnival Worker\" ?", false),
        new Option<bool>("34536", "Carnival Worker Hat", "Mode: [select] only\nShould the bot buy \"Carnival Worker Hat\" ?", false),
        new Option<bool>("34537", "Carnival Worker Locks", "Mode: [select] only\nShould the bot buy \"Carnival Worker Locks\" ?", false),
        new Option<bool>("34568", "Miranda and William Outfits", "Mode: [select] only\nShould the bot buy \"Miranda and William Outfits\" ?", false),
        new Option<bool>("34569", "Miranda's Hair", "Mode: [select] only\nShould the bot buy \"Miranda's Hair\" ?", false),
        new Option<bool>("34570", "William's Hair", "Mode: [select] only\nShould the bot buy \"William's Hair\" ?", false),
    };
}
