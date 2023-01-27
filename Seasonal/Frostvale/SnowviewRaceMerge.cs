/*
name: Snowview Race Merge
description: This will get all or selected items on this merge shop.
tags: snowview-race-merge, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SnowviewRaceMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public Frostvale FV = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Turkey Leg?" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        FV.SnowviewRace();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("snowviewrace", 2196, findIngredients);

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

                case "Turkey Leg?":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9027);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("snowviewrace", "Aurora Vaderix", "Aurora Wing", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("snowviewrace", "Bandit Fletcher", "Bandit Leader Bounty", 3, log: false);
                        Core.HuntMonster("snowviewrace", "Juvenile Vaderix", "Vaderix Drumstick", 7, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("74639", "Frigid Festive Wear", "Mode: [select] only\nShould the bot buy \"Frigid Festive Wear\" ?", false),
        new Option<bool>("74640", "Frigid Hair", "Mode: [select] only\nShould the bot buy \"Frigid Hair\" ?", false),
        new Option<bool>("74641", "Frigid Locks", "Mode: [select] only\nShould the bot buy \"Frigid Locks\" ?", false),
        new Option<bool>("74642", "Frigid Hair Morph", "Mode: [select] only\nShould the bot buy \"Frigid Hair Morph\" ?", false),
        new Option<bool>("74643", "Frigid Locks Morph", "Mode: [select] only\nShould the bot buy \"Frigid Locks Morph\" ?", false),
        new Option<bool>("74644", "Frigid Paperboy Cap", "Mode: [select] only\nShould the bot buy \"Frigid Paperboy Cap\" ?", false),
        new Option<bool>("74645", "Frigid Papergirl Cap", "Mode: [select] only\nShould the bot buy \"Frigid Papergirl Cap\" ?", false),
        new Option<bool>("74646", "Frigid Ice Halation", "Mode: [select] only\nShould the bot buy \"Frigid Ice Halation\" ?", false),
        new Option<bool>("74647", "Frigid Ice Staff", "Mode: [select] only\nShould the bot buy \"Frigid Ice Staff\" ?", false),
        new Option<bool>("74648", "Frigid Bell Flail", "Mode: [select] only\nShould the bot buy \"Frigid Bell Flail\" ?", false),
    };
}
