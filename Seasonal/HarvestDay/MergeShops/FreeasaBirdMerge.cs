/*
name: Free as a Bird Merge
description: This bot will farm the items belonging to the selected mode for the Free as a Bird Merge [2361] in /birdswithharms
tags: free, as, a, bird, merge, birdswithharms, gryphonclaw, knights, scythe, wings, guard, crested, knight, warriors, warrior
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FreeasaBirdMerge
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
        Core.BankingBlackList.AddRange(new[] { "Stymphalian's Bronze Feather" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("birdswithharms", 2361, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Stymphalian's Bronze Feather":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    // Muck and Feather 9462
                    Core.RegisterQuests(9462);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonsterMapID("birdswithharms", 33);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73547", "GryphonClaw Knight's Scythe", "Mode: [select] only\nShould the bot buy \"GryphonClaw Knight's Scythe\" ?", false),
        new Option<bool>("73546", "GryphonClaw Knight's Blades", "Mode: [select] only\nShould the bot buy \"GryphonClaw Knight's Blades\" ?", false),
        new Option<bool>("73545", "GryphonClaw Knight's Blade", "Mode: [select] only\nShould the bot buy \"GryphonClaw Knight's Blade\" ?", false),
        new Option<bool>("73543", "GryphonClaw Knight's Wings", "Mode: [select] only\nShould the bot buy \"GryphonClaw Knight's Wings\" ?", false),
        new Option<bool>("73541", "GryphonClaw Knight's Guard Helm", "Mode: [select] only\nShould the bot buy \"GryphonClaw Knight's Guard Helm\" ?", false),
        new Option<bool>("73540", "GryphonClaw Knight's Crested Helm", "Mode: [select] only\nShould the bot buy \"GryphonClaw Knight's Crested Helm\" ?", false),
        new Option<bool>("73539", "GryphonClaw Knight", "Mode: [select] only\nShould the bot buy \"GryphonClaw Knight\" ?", false),
        new Option<bool>("73538", "GryphonClaw Warrior's Blades", "Mode: [select] only\nShould the bot buy \"GryphonClaw Warrior's Blades\" ?", false),
        new Option<bool>("73537", "GryphonClaw Warrior's Blade", "Mode: [select] only\nShould the bot buy \"GryphonClaw Warrior's Blade\" ?", false),
        new Option<bool>("73536", "GryphonClaw Warrior's Wings", "Mode: [select] only\nShould the bot buy \"GryphonClaw Warrior's Wings\" ?", false),
        new Option<bool>("73535", "GryphonClaw Guard Helm", "Mode: [select] only\nShould the bot buy \"GryphonClaw Guard Helm\" ?", false),
        new Option<bool>("73534", "GryphonClaw Warrior's Crested Helm", "Mode: [select] only\nShould the bot buy \"GryphonClaw Warrior's Crested Helm\" ?", false),
        new Option<bool>("73533", "GryphonClaw Warrior", "Mode: [select] only\nShould the bot buy \"GryphonClaw Warrior\" ?", false),
    };
}
