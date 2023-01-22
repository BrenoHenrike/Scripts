/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Shinkansen.cs
//cs_include Scripts/Story/Eden.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GachaponMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public Eden Eden = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Second Chance Coin " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Eden.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("onsen", 1926, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Second Chance Coin":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    //I heard you like Gacha 7781
                    Core.RegisterQuests(7781);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Adv.BuyItem("onsen", 1926, "Gachapon Coin");
                        Core.HuntMonster("yokaigrave", "Skello Kitty", "Skello Kitty Bone");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57239", "Crystallis Jinbei", "Mode: [select] only\nShould the bot buy \"Crystallis Jinbei\" ?", false),
        new Option<bool>("57240", "Crystallis Yukata", "Mode: [select] only\nShould the bot buy \"Crystallis Yukata\" ?", false),
        new Option<bool>("57241", "Crystallis Yukata + Haori", "Mode: [select] only\nShould the bot buy \"Crystallis Yukata + Haori\" ?", false),
        new Option<bool>("57242", "Cool Crystallis Yukata", "Mode: [select] only\nShould the bot buy \"Cool Crystallis Yukata\" ?", false),
        new Option<bool>("57243", "Dark Crystallis Jinbei", "Mode: [select] only\nShould the bot buy \"Dark Crystallis Jinbei\" ?", false),
        new Option<bool>("57244", "Onsen Yukata", "Mode: [select] only\nShould the bot buy \"Onsen Yukata\" ?", false),
        new Option<bool>("57245", "Onsen Yukata + Haori", "Mode: [select] only\nShould the bot buy \"Onsen Yukata + Haori\" ?", false),
        new Option<bool>("57246", "Cool Onsen Yukata", "Mode: [select] only\nShould the bot buy \"Cool Onsen Yukata\" ?", false),
    };
}
