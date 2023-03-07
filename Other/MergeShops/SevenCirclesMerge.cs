/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SevenCirclesMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreLegion Legion = new();
    public SevenCircles Circles = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Indulgence", "Legion Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Circles.Circles();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sevencircles", 1980, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Indulgence":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7978);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("sevencircles", "r2", "Left", "Limbo Guard", "Souls of Limbo", 25);
                        Core.KillMonster("sevencircles", "r3", "Left", "Luxuria Guard", "Essence of Luxuria", 1);
                        Core.KillMonster("sevencircles", "r5", "Left", "Gluttony Guard", "Essence of Gluttony", 1);
                        Core.KillMonster("sevencircles", "r7", "Left", "Avarice Guard", "Essence of Avarice", 1);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54176", "Underworld Nemesis", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis\" ?", false),
        new Option<bool>("54178", "Underworld Nemesis Horns + Locks", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Horns + Locks\" ?", false),
        new Option<bool>("54179", "Underworld Nemesis Enchanted Horns + Locks", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Enchanted Horns + Locks\" ?", false),
        new Option<bool>("54180", "Underworld Nemesis Dark Horns + Locks", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Dark Horns + Locks\" ?", false),
        new Option<bool>("54182", "Underworld Nemesis Horns", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Horns\" ?", false),
        new Option<bool>("54183", "Underworld Nemesis Enchanted Horns", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Enchanted Horns\" ?", false),
        new Option<bool>("54184", "Underworld Nemesis Dark Horns", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Dark Horns\" ?", false),
        new Option<bool>("54185", "Underworld Nemesis Dark Hood", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Dark Hood\" ?", false),
        new Option<bool>("54186", "Underworld Nemesis Hood + Horns", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Hood + Horns\" ?", false),
        new Option<bool>("54187", "One-Winged Underworld Nemesis", "Mode: [select] only\nShould the bot buy \"One-Winged Underworld Nemesis\" ?", false),
        new Option<bool>("54188", "Underworld Nemesis Wings", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Wings\" ?", false),
        new Option<bool>("54189", "Underworld Nemesis Claws", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Claws\" ?", false),
        new Option<bool>("54190", "Underworld Nemesis Kama", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Kama\" ?", false),
        new Option<bool>("54191", "Underworld Nemesis Scythe", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Scythe\" ?", false),
        new Option<bool>("59994", "Underworld Nemesis Kamas", "Mode: [select] only\nShould the bot buy \"Underworld Nemesis Kamas\" ?", false),
        new Option<bool>("59756", "Aspect of Luxuria", "Mode: [select] only\nShould the bot buy \"Aspect of Luxuria\" ?", false),
        new Option<bool>("59753", "Gluttony's Maw", "Mode: [select] only\nShould the bot buy \"Gluttony's Maw\" ?", false),
        new Option<bool>("59752", "Stare of Greed", "Mode: [select] only\nShould the bot buy \"Stare of Greed\" ?", false),
        new Option<bool>("59757", "Luxuria's Daggers", "Mode: [select] only\nShould the bot buy \"Luxuria's Daggers\" ?", false),
        new Option<bool>("59758", "Luxuria's Reversed Daggers", "Mode: [select] only\nShould the bot buy \"Luxuria's Reversed Daggers\" ?", false),
    };
}
