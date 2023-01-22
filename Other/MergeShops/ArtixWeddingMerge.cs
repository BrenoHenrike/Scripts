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

public class ArtixWeddingMerge
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
        Core.BankingBlackList.AddRange(new[] { "Love Token " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battlewedding", 788, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Love Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("battlewedding", "Platinum Mech Dragon", "Love Token");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("21402", "Artix's Wedding Party", "Mode: [select] only\nShould the bot buy \"Artix's Wedding Party\" ?", false),
        new Option<bool>("21411", "Bellhop Minion", "Mode: [select] only\nShould the bot buy \"Bellhop Minion\" ?", false),
        new Option<bool>("21499", "Ebil Ninja", "Mode: [select] only\nShould the bot buy \"Ebil Ninja\" ?", false),
        new Option<bool>("21487", "Legendary Wedding Usher", "Mode: [select] only\nShould the bot buy \"Legendary Wedding Usher\" ?", false),
        new Option<bool>("21474", "Red Wedding Guest", "Mode: [select] only\nShould the bot buy \"Red Wedding Guest\" ?", false),
        new Option<bool>("21472", "Silver Wedding Guest", "Mode: [select] only\nShould the bot buy \"Silver Wedding Guest\" ?", false),
        new Option<bool>("21512", "Ebil Ninja Hood", "Mode: [select] only\nShould the bot buy \"Ebil Ninja Hood\" ?", false),
        new Option<bool>("21492", "Wedding Usher Helm", "Mode: [select] only\nShould the bot buy \"Wedding Usher Helm\" ?", false),
        new Option<bool>("21484", "Red Bouquet-on-Your-Back", "Mode: [select] only\nShould the bot buy \"Red Bouquet-on-Your-Back\" ?", false),
        new Option<bool>("21483", "Silver Bouquet-on-Your-Back", "Mode: [select] only\nShould the bot buy \"Silver Bouquet-on-Your-Back\" ?", false),
        new Option<bool>("21624", "Crimson ZARDIS Key", "Mode: [select] only\nShould the bot buy \"Crimson ZARDIS Key\" ?", false),
        new Option<bool>("21598", "ZARDIS Key", "Mode: [select] only\nShould the bot buy \"ZARDIS Key\" ?", false),
        new Option<bool>("21488", "Bullhorn Of Love", "Mode: [select] only\nShould the bot buy \"Bullhorn Of Love\" ?", false),
        new Option<bool>("21480", "Red Bouquet", "Mode: [select] only\nShould the bot buy \"Red Bouquet\" ?", false),
        new Option<bool>("21479", "Silver Bouquet", "Mode: [select] only\nShould the bot buy \"Silver Bouquet\" ?", false),
        new Option<bool>("21476", "Red Rose", "Mode: [select] only\nShould the bot buy \"Red Rose\" ?", false),
        new Option<bool>("21475", "Silver Rose", "Mode: [select] only\nShould the bot buy \"Silver Rose\" ?", false),
        new Option<bool>("21640", "Black Thorn Blade", "Mode: [select] only\nShould the bot buy \"Black Thorn Blade\" ?", false),
        new Option<bool>("21643", "Pink Wedding Guest", "Mode: [select] only\nShould the bot buy \"Pink Wedding Guest\" ?", false),
    };
}
