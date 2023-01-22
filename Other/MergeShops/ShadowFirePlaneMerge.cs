/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowFirePlaneMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoW SoW = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Shade Spark " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowfireplane", 2008, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Shade Spark":
                    SoW.Tyndarius();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8145);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Catching Fire 8145
                        Core.HuntMonster("shadowfireplane", "Living Shadowflame", "Shadefire Essence", 20);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("61388", "Voidborn Warlock", "Mode: [select] only\nShould the bot buy \"Voidborn Warlock\" ?", false),
        new Option<bool>("61389", "Voidborn Warlock's Hood", "Mode: [select] only\nShould the bot buy \"Voidborn Warlock's Hood\" ?", false),
        new Option<bool>("61390", "Voidborn Warlock's Guard", "Mode: [select] only\nShould the bot buy \"Voidborn Warlock's Guard\" ?", false),
        new Option<bool>("61391", "Voidborn Morph Helm", "Mode: [select] only\nShould the bot buy \"Voidborn Morph Helm\" ?", false),
        new Option<bool>("61392", "Angry Voidborn Morph Helm", "Mode: [select] only\nShould the bot buy \"Angry Voidborn Morph Helm\" ?", false),
        new Option<bool>("61393", "Happy Voidborn Morph Helm", "Mode: [select] only\nShould the bot buy \"Happy Voidborn Morph Helm\" ?", false),
        new Option<bool>("61394", "Hungry Voidborn Morph Helm", "Mode: [select] only\nShould the bot buy \"Hungry Voidborn Morph Helm\" ?", false),
        new Option<bool>("61395", "Voidborn Guardian Beast", "Mode: [select] only\nShould the bot buy \"Voidborn Guardian Beast\" ?", false),
        new Option<bool>("61396", "Voidborn Warlock Chakrams", "Mode: [select] only\nShould the bot buy \"Voidborn Warlock Chakrams\" ?", false),
        new Option<bool>("61397", "Voidborn Warlock Staff", "Mode: [select] only\nShould the bot buy \"Voidborn Warlock Staff\" ?", false),
        new Option<bool>("61807", "Voidborn Warlock Chakrams", "Mode: [select] only\nShould the bot buy \"Voidborn Warlock Chakrams\" ?", false),
        new Option<bool>("61332", "Flameburst Dragunblade", "Mode: [select] only\nShould the bot buy \"Flameburst Dragunblade\" ?", false),
    };
}
