/*
name: Panopticon Gear Merge
description: This bot will farm the items belonging to the selected mode for the Panopticon Gear Merge [2439] in /trenchobserve
tags: panopticon, gear, merge, trenchobserve, teuthi, amp, rhizo, countenance, artillery, armament
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
//cs_include Scripts/Story\AgeOfRuin\CoreAOR.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class PanopticonGearMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Panopticon Gear Wreckage", "Panopticon Gear Linker" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.DeepWater(true);
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("trenchobserve", 2439, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Panopticon Gear Wreckage":
                case "Panopticon Gear Linker":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9730); //C:\The Depths are a Harsh Mistress (9730)
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("trenchobserve", "Sea Spirit", "Squishy Organic Thingy", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("85273", "Panopticon Teuthi &amp; Rhizo", "Mode: [select] only\nShould the bot buy \"Panopticon Teuthi &amp; Rhizo\" ?", false),
        new Option<bool>("85274", "Panopticon Rhizo &amp; Teuthi", "Mode: [select] only\nShould the bot buy \"Panopticon Rhizo &amp; Teuthi\" ?", false),
        new Option<bool>("85275", "Panopticon Rhizo Countenance", "Mode: [select] only\nShould the bot buy \"Panopticon Rhizo Countenance\" ?", false),
        new Option<bool>("85276", "Panopticon Teuthi Countenance", "Mode: [select] only\nShould the bot buy \"Panopticon Teuthi Countenance\" ?", false),
        new Option<bool>("85277", "Panopticon Rhizo Artillery", "Mode: [select] only\nShould the bot buy \"Panopticon Rhizo Artillery\" ?", false),
        new Option<bool>("85278", "Panopticon Teuthi Artillery", "Mode: [select] only\nShould the bot buy \"Panopticon Teuthi Artillery\" ?", false),
        new Option<bool>("85279", "Panopticon Rhizo Armament", "Mode: [select] only\nShould the bot buy \"Panopticon Rhizo Armament\" ?", false),
        new Option<bool>("85280", "Panopticon Teuthi Armament", "Mode: [select] only\nShould the bot buy \"Panopticon Teuthi Armament\" ?", false),
    };
}
