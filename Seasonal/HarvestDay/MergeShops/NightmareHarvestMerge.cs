/*
name: Nightmare Harvest Merge
description: This will get all or selected items on this merge shop.
tags: nightmare-harvest-merge, seasonal, harvest-day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NightmareHarvestMerge
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
        Core.BankingBlackList.AddRange(new[] { "Earplug " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("gothicdream"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("gothicdream", 1939, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Earplug":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7808);
                    // Quiet Down, Will Ya? 7808
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("memetnightmare", "Fire Cyclone", "Cyclones Subdued", 8);
                        Core.HuntMonster("memetnightmare", "Burning Ember", "Embers Smothered", 8);
                        Core.HuntMonster("memetnightmare", "Cannibal Mermaid", "Mermaids Dispersed", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57588", "Teen Slasher", "Mode: [select] only\nShould the bot buy \"Teen Slasher\" ?", false),
        new Option<bool>("57589", "Teen Slasher's Hair", "Mode: [select] only\nShould the bot buy \"Teen Slasher's Hair\" ?", false),
        new Option<bool>("57590", "Teen Slasher's Side Mask", "Mode: [select] only\nShould the bot buy \"Teen Slasher's Side Mask\" ?", false),
        new Option<bool>("57591", "Teen Slasher's Mask", "Mode: [select] only\nShould the bot buy \"Teen Slasher's Mask\" ?", false),
        new Option<bool>("57592", "Teen Slasher's Locks", "Mode: [select] only\nShould the bot buy \"Teen Slasher's Locks\" ?", false),
        new Option<bool>("57593", "Teen Slasher's Side Mask + Locks", "Mode: [select] only\nShould the bot buy \"Teen Slasher's Side Mask + Locks\" ?", false),
        new Option<bool>("57594", "Teen Slasher's Mask + Locks", "Mode: [select] only\nShould the bot buy \"Teen Slasher's Mask + Locks\" ?", false),
        new Option<bool>("57595", "Teen Slasher's Knife", "Mode: [select] only\nShould the bot buy \"Teen Slasher's Knife\" ?", false),
        new Option<bool>("57640", "Dual Slasher's Knives", "Mode: [select] only\nShould the bot buy \"Dual Slasher's Knives\" ?", false),
        new Option<bool>("57596", "Unlucky Hoodie", "Mode: [select] only\nShould the bot buy \"Unlucky Hoodie\" ?", false),
        new Option<bool>("57597", "Unlucky Hoodie Hair", "Mode: [select] only\nShould the bot buy \"Unlucky Hoodie Hair\" ?", false),
        new Option<bool>("57598", "Unlucky Hoodie Locks", "Mode: [select] only\nShould the bot buy \"Unlucky Hoodie Locks\" ?", false),
        new Option<bool>("57651", "Doomba", "Mode: [select] only\nShould the bot buy \"Doomba\" ?", false),
        new Option<bool>("57652", "Terrifier", "Mode: [select] only\nShould the bot buy \"Terrifier\" ?", false),
        new Option<bool>("57653", "Terrifier's Tiny Hat", "Mode: [select] only\nShould the bot buy \"Terrifier's Tiny Hat\" ?", false),
        new Option<bool>("57654", "Terrifier's Tiny Hat (Female)", "Mode: [select] only\nShould the bot buy \"Terrifier's Tiny Hat (Female)\" ?", false),
        new Option<bool>("57655", "Terrifier's Morph", "Mode: [select] only\nShould the bot buy \"Terrifier's Morph\" ?", false),
        new Option<bool>("57553", "Spooky Skelly Suit", "Mode: [select] only\nShould the bot buy \"Spooky Skelly Suit\" ?", false),
        new Option<bool>("57554", "Spooky Skull Morph + Locks", "Mode: [select] only\nShould the bot buy \"Spooky Skull Morph + Locks\" ?", false),
        new Option<bool>("57555", "Spooky Skull Morph", "Mode: [select] only\nShould the bot buy \"Spooky Skull Morph\" ?", false),
        new Option<bool>("57556", "Spooky Batty Wings", "Mode: [select] only\nShould the bot buy \"Spooky Batty Wings\" ?", false),
    };
}
