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
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
//cs_include Scripts/Other/ShadowflameWarMedal.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CrownsreachDefenseMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoW SoW = new();
    public CoreSoC SoC = new();
    public ShadowflameWarMedal SWM = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "ShadowFlame War Medal " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SoW.ShadowWar();
        SoC.DualPlane();
        Adv.StartBuyAllMerge("chaosamulet", 1914, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "ShadowFlame War Medal":
                    Core.FarmingLogger(req.Name, quant);
                    SWM.Medals(quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54290", "ShadowFlame Warlock", "Mode: [select] only\nShould the bot buy \"ShadowFlame Warlock\" ?", false),
        new Option<bool>("54292", "ShadowFlame Warlock's Hat", "Mode: [select] only\nShould the bot buy \"ShadowFlame Warlock's Hat\" ?", false),
        new Option<bool>("54293", "ShadowFlame Warlock's Hat + Locks", "Mode: [select] only\nShould the bot buy \"ShadowFlame Warlock's Hat + Locks\" ?", false),
        new Option<bool>("54294", "ShadowFlame Warlock's Topper", "Mode: [select] only\nShould the bot buy \"ShadowFlame Warlock's Topper\" ?", false),
        new Option<bool>("54295", "ShadowFlame Warlock's Topper + Locks", "Mode: [select] only\nShould the bot buy \"ShadowFlame Warlock's Topper + Locks\" ?", false),
        new Option<bool>("54300", "ShadowFlame Warlock's Scythe", "Mode: [select] only\nShould the bot buy \"ShadowFlame Warlock's Scythe\" ?", false),
        new Option<bool>("54259", "ShadowFlame Battle Staff", "Mode: [select] only\nShould the bot buy \"ShadowFlame Battle Staff\" ?", false),
        new Option<bool>("54261", "ShadowFlame Battle Spear", "Mode: [select] only\nShould the bot buy \"ShadowFlame Battle Spear\" ?", false),
        new Option<bool>("54302", "Shadow Warlock Commander", "Mode: [select] only\nShould the bot buy \"Shadow Warlock Commander\" ?", false),
        new Option<bool>("54303", "Shadow Commander's Hat", "Mode: [select] only\nShould the bot buy \"Shadow Commander's Hat\" ?", false),
        new Option<bool>("54304", "Shadow Commander's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Shadow Commander's Hat + Locks\" ?", false),
        new Option<bool>("54305", "Shadow Commander's Skull Hat", "Mode: [select] only\nShould the bot buy \"Shadow Commander's Skull Hat\" ?", false),
        new Option<bool>("54306", "Shadow Commander's Topper", "Mode: [select] only\nShould the bot buy \"Shadow Commander's Topper\" ?", false),
        new Option<bool>("54307", "Shadow Commander's Topper + Locks", "Mode: [select] only\nShould the bot buy \"Shadow Commander's Topper + Locks\" ?", false),
        new Option<bool>("54308", "Shadow Commander's Skull Topper", "Mode: [select] only\nShould the bot buy \"Shadow Commander's Skull Topper\" ?", false),
    };
}
