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

public class DwobosMerge
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
        Core.BankingBlackList.AddRange(new[] { "Dwobo Coin " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("crashruins", 1212, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dwobo Coin":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(Core.IsMember ? 4798 : 4797);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (!Core.IsMember)
                            Core.KillMonster("crashruins", "r2", "Left", "Unlucky Explorer", "Ancient Treasure", 10);
                        else Core.KillMonster("crashruins", "r2", "Left", "Unlucky Explorer", "Ancient Treasure", 8);

                        if (!Core.IsMember)
                            Core.KillMonster("crashruins", "r2", "Left", "Spacetime Anomaly", "Pieces of Future Tech", 7);
                        else Core.KillMonster("crashruins", "r2", "Left", "Spacetime Anomaly", "Pieces of Future Tech", 5);

                        Core.HuntMonster("crashruins", "Cluckmoo Idol", "Idol Heart");

                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("33440", "Dwakel Warrior", "Mode: [select] only\nShould the bot buy \"Dwakel Warrior\" ?", false),
        new Option<bool>("33441", "Dwakel Morph", "Mode: [select] only\nShould the bot buy \"Dwakel Morph\" ?", false),
        new Option<bool>("33327", "Magical Butter Stick of Power", "Mode: [select] only\nShould the bot buy \"Magical Butter Stick of Power\" ?", false),
        new Option<bool>("33442", "Orb of Alexander", "Mode: [select] only\nShould the bot buy \"Orb of Alexander\" ?", false),
        new Option<bool>("29424", "Nulglin", "Mode: [select] only\nShould the bot buy \"Nulglin\" ?", false),
        new Option<bool>("33439", "Galactic Treasure Hunter", "Mode: [select] only\nShould the bot buy \"Galactic Treasure Hunter\" ?", false),
        new Option<bool>("33433", "Dwobo Bank", "Mode: [select] only\nShould the bot buy \"Dwobo Bank\" ?", false),
        new Option<bool>("33453", "Gem of Nulgath Cape", "Mode: [select] only\nShould the bot buy \"Gem of Nulgath Cape\" ?", false),
        new Option<bool>("33451", "Gemstone of Nulgath", "Mode: [select] only\nShould the bot buy \"Gemstone of Nulgath\" ?", false),
        new Option<bool>("33461", "Rustbucket Armor", "Mode: [select] only\nShould the bot buy \"Rustbucket Armor\" ?", false),
    };
}
