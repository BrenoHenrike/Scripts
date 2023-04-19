/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThunderFang.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class StormCacheMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private ThunderFang TF = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Tonitrus Gem" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        TF.StoryLine();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("thunderfang", 1103, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Tonitrus Gem":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4146);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("thunderfang", "Storm Draconian", "Storm Draconians Defeated", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("29781", "Tempestas Egg", "Mode: [select] only\nShould the bot buy \"Tempestas Egg\" ?", false),
        new Option<bool>("29732", "Thunderfang Battlemage", "Mode: [select] only\nShould the bot buy \"Thunderfang Battlemage\" ?", false),
        new Option<bool>("29733", "Electrostatic Hair", "Mode: [select] only\nShould the bot buy \"Electrostatic Hair\" ?", false),
        new Option<bool>("29734", "Electrostatic Locks", "Mode: [select] only\nShould the bot buy \"Electrostatic Locks\" ?", false),
        new Option<bool>("29735", "Cape of Lightning", "Mode: [select] only\nShould the bot buy \"Cape of Lightning\" ?", false),
        new Option<bool>("29782", "Lightning Dragon Pet", "Mode: [select] only\nShould the bot buy \"Lightning Dragon Pet\" ?", false),
    };
}
