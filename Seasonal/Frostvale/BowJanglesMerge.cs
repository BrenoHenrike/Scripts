/*
name: Bow Jangles Merge
description: This will get all or selected items on this merge shop.
tags: bow-jangles-merge, seasonal, frostvale
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

public class BowJanglesMerge
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
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Gift Ribbons" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        FV.BowJangles();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("frostvale", 1946, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Gift Ribbons":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7829);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("goldenruins", "Golden Warrior", "Golden Warriors Trashed", 6);
                        Core.HuntMonster("goldenruins", "Maximillian Lionfang", "Lionfang Thrown Out");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57931", "GiftHunter", "Mode: [select] only\nShould the bot buy \"GiftHunter\" ?", false),
        new Option<bool>("57932", "GiftHunter's Hat", "Mode: [select] only\nShould the bot buy \"GiftHunter's Hat\" ?", false),
        new Option<bool>("57933", "GiftHunter's Hat + Scarf", "Mode: [select] only\nShould the bot buy \"GiftHunter's Hat + Scarf\" ?", false),
        new Option<bool>("57934", "GiftHunter's Beanie", "Mode: [select] only\nShould the bot buy \"GiftHunter's Beanie\" ?", false),
        new Option<bool>("57935", "GiftHunter's Cape", "Mode: [select] only\nShould the bot buy \"GiftHunter's Cape\" ?", false),
        new Option<bool>("57936", "GiftHunter's Sack of Gifts", "Mode: [select] only\nShould the bot buy \"GiftHunter's Sack of Gifts\" ?", false),
        new Option<bool>("58038", "Jolly Frostval Caster", "Mode: [select] only\nShould the bot buy \"Jolly Frostval Caster\" ?", false),
        new Option<bool>("58041", "Jolly Frostval Caster's Locks", "Mode: [select] only\nShould the bot buy \"Jolly Frostval Caster's Locks\" ?", false),
        new Option<bool>("58042", "Jolly Frostval Caster's Hair", "Mode: [select] only\nShould the bot buy \"Jolly Frostval Caster's Hair\" ?", false),
        new Option<bool>("58043", "Jolly Frostval Caster's Locks + Hat", "Mode: [select] only\nShould the bot buy \"Jolly Frostval Caster's Locks + Hat\" ?", false),
        new Option<bool>("58044", "Jolly Frostval Caster's Hat", "Mode: [select] only\nShould the bot buy \"Jolly Frostval Caster's Hat\" ?", false),
        new Option<bool>("58046", "Jolly Frostval Caster's Jester Cape", "Mode: [select] only\nShould the bot buy \"Jolly Frostval Caster's Jester Cape\" ?", false),
        new Option<bool>("58047", "The Edge of Giving", "Mode: [select] only\nShould the bot buy \"The Edge of Giving\" ?", false),
        new Option<bool>("58049", "Jolly Frostval Caster's Sword", "Mode: [select] only\nShould the bot buy \"Jolly Frostval Caster's Sword\" ?", false),
        new Option<bool>("58152", "Reversed Edge of Giving", "Mode: [select] only\nShould the bot buy \"Reversed Edge of Giving\" ?", false),
    };
}
