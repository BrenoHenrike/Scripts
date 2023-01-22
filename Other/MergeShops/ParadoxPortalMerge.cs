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

public class ParadoxPortalMerge
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
        Core.BankingBlackList.AddRange(new[] { "Paradox Core", "Time Heart", "Paradox Gem " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("portalmaze", 1248, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Paradox Core":
                case "Time Heart":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("portalmazec", "Vorefax", req.Name, quant, isTemp: false);
                    break;


                case "Paradox Gem":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dragontown", "The Neverborn", req.Name, quant, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34829", "Jurassic Monarch Wings", "Mode: [select] only\nShould the bot buy \"Jurassic Monarch Wings\" ?", false),
        new Option<bool>("34905", "Green Death of Time Scythe", "Mode: [select] only\nShould the bot buy \"Green Death of Time Scythe\" ?", false),
        new Option<bool>("34927", "Jurassic Butterfly", "Mode: [select] only\nShould the bot buy \"Jurassic Butterfly\" ?", false),
        new Option<bool>("34924", "Valen and Lynaria Armor", "Mode: [select] only\nShould the bot buy \"Valen and Lynaria Armor\" ?", false),
        new Option<bool>("34917", "Blue Portal Cape", "Mode: [select] only\nShould the bot buy \"Blue Portal Cape\" ?", false),
        new Option<bool>("34918", "Orange Portal Cape", "Mode: [select] only\nShould the bot buy \"Orange Portal Cape\" ?", false),
    };
}
