/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EternalChaosMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Reality Shard"});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("eternalchaos", 2089, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Reality Shard":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8456);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("eternalchaos", "Chaos Time Fairy", "Preserved Chaos Fairy Wing", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("64866", "Chaos Poncho", "Mode: [select] only\nShould the bot buy \"Chaos Poncho\" ?", false),
        new Option<bool>("64867", "Chaos Stetson", "Mode: [select] only\nShould the bot buy \"Chaos Stetson\" ?", false),
        new Option<bool>("64868", "Chaos Stetson + Locks", "Mode: [select] only\nShould the bot buy \"Chaos Stetson + Locks\" ?", false),
        new Option<bool>("64869", "Chaotic Hood", "Mode: [select] only\nShould the bot buy \"Chaotic Hood\" ?", false),
        new Option<bool>("64870", "Chaotic Hood + Bangs", "Mode: [select] only\nShould the bot buy \"Chaotic Hood + Bangs\" ?", false),
        new Option<bool>("64871", "Chaotic Mask", "Mode: [select] only\nShould the bot buy \"Chaotic Mask\" ?", false),
        new Option<bool>("64872", "Chaotic Mask + Bangs", "Mode: [select] only\nShould the bot buy \"Chaotic Mask + Bangs\" ?", false),
        new Option<bool>("64873", "Chaos Trench Coat", "Mode: [select] only\nShould the bot buy \"Chaos Trench Coat\" ?", false),
        new Option<bool>("64874", "Chaos Trench Coat + Sword", "Mode: [select] only\nShould the bot buy \"Chaos Trench Coat + Sword\" ?", false),
        new Option<bool>("64875", "Sheathed Chaos Sword", "Mode: [select] only\nShould the bot buy \"Sheathed Chaos Sword\" ?", false),
        new Option<bool>("64876", "Chaos Longsword", "Mode: [select] only\nShould the bot buy \"Chaos Longsword\" ?", false),
        new Option<bool>("64877", "Chaos Revolver", "Mode: [select] only\nShould the bot buy \"Chaos Revolver\" ?", false),
        new Option<bool>("64878", "Dual Chaos Revolvers", "Mode: [select] only\nShould the bot buy \"Dual Chaos Revolvers\" ?", false),
        new Option<bool>("64879", "Chaos Backstabber", "Mode: [select] only\nShould the bot buy \"Chaos Backstabber\" ?", false),
        new Option<bool>("64880", "Dual Chaos Backstabbers", "Mode: [select] only\nShould the bot buy \"Dual Chaos Backstabbers\" ?", false),
        new Option<bool>("64881", "Reversed Chaos Backstabber", "Mode: [select] only\nShould the bot buy \"Reversed Chaos Backstabber\" ?", false),
        new Option<bool>("64882", "Dual Reversed Chaos Backstabbers", "Mode: [select] only\nShould the bot buy \"Dual Reversed Chaos Backstabbers\" ?", false),
        new Option<bool>("64883", "Chaos Dagger Revolvers", "Mode: [select] only\nShould the bot buy \"Chaos Dagger Revolvers\" ?", false),
        new Option<bool>("67059", "TimeReaper", "Mode: [select] only\nShould the bot buy \"TimeReaper\" ?", false),
        new Option<bool>("67061", "TimeReaper Halo Hair", "Mode: [select] only\nShould the bot buy \"TimeReaper Halo Hair\" ?", false),
        new Option<bool>("67063", "TimeReaper Halo Locks", "Mode: [select] only\nShould the bot buy \"TimeReaper Halo Locks\" ?", false),
        new Option<bool>("67064", "TimeReaper Hat", "Mode: [select] only\nShould the bot buy \"TimeReaper Hat\" ?", false),
        new Option<bool>("67065", "TimeReaper Hat + Locks", "Mode: [select] only\nShould the bot buy \"TimeReaper Hat + Locks\" ?", false),
        new Option<bool>("67068", "TimeReaper Mantle", "Mode: [select] only\nShould the bot buy \"TimeReaper Mantle\" ?", false),
        new Option<bool>("67069", "TimeReaper Awakened Mantle", "Mode: [select] only\nShould the bot buy \"TimeReaper Awakened Mantle\" ?", false),
        new Option<bool>("67070", "TimeReaper Runes", "Mode: [select] only\nShould the bot buy \"TimeReaper Runes\" ?", false),
        new Option<bool>("67071", "TimeReaper Blade", "Mode: [select] only\nShould the bot buy \"TimeReaper Blade\" ?", false),
        new Option<bool>("67072", "Dual TimeReaper Blades", "Mode: [select] only\nShould the bot buy \"Dual TimeReaper Blades\" ?", false),
        new Option<bool>("67077", "TimeReaper Claw", "Mode: [select] only\nShould the bot buy \"TimeReaper Claw\" ?", false),
        new Option<bool>("67078", "TimeReaper Claws", "Mode: [select] only\nShould the bot buy \"TimeReaper Claws\" ?", false),
    };
}
