/*
name: Gundahar's Stash Merge
description: This will get all or selected items on this merge shop.
tags: gundahars-stash-merge, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Seasonal\Frostvale\Frostvale.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GundaharsStashMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public Frostvale Frostvale = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Icy Pelt", "WinterWild Axe", "Old Moglin Teddy Mace" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("deerhunt"))
            return;

        Frostvale.DeerHunt();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("deerhunt", 2077, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Icy Pelt":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8433);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("deerhunt", "r2", "Left", "Deer?", "Wolf Warded", quant);
                        Core.KillMonster("deerhunt", "r3", "Left", "Scared Wolf", "Deer Deterred", quant);
                        Core.KillMonster("deerhunt", "r6", "Left", "Frightened Owl", "Owl Ousted", quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "WinterWild Axe":
                case "Old Moglin Teddy Mace":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("deerhunt", "Zweinichthirsch", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("49776", "WinterWild Warrior", "Mode: [select] only\nShould the bot buy \"WinterWild Warrior\" ?", false),
        new Option<bool>("49777", "WinterWild Locks", "Mode: [select] only\nShould the bot buy \"WinterWild Locks\" ?", false),
        new Option<bool>("49778", "WinterWild Beard", "Mode: [select] only\nShould the bot buy \"WinterWild Beard\" ?", false),
        new Option<bool>("49779", "WinterWild Hood", "Mode: [select] only\nShould the bot buy \"WinterWild Hood\" ?", false),
        new Option<bool>("49780", "WinterWild Hood + Beard", "Mode: [select] only\nShould the bot buy \"WinterWild Hood + Beard\" ?", false),
        new Option<bool>("49781", "WinterWild Warrior Back Axes", "Mode: [select] only\nShould the bot buy \"WinterWild Warrior Back Axes\" ?", false),
        new Option<bool>("49784", "Dual WinterWild Axes", "Mode: [select] only\nShould the bot buy \"Dual WinterWild Axes\" ?", false),
        new Option<bool>("49785", "WinterWild Dagger", "Mode: [select] only\nShould the bot buy \"WinterWild Dagger\" ?", false),
        new Option<bool>("66271", "Moglin Teddy Mace", "Mode: [select] only\nShould the bot buy \"Moglin Teddy Mace\" ?", false),
    };
}
