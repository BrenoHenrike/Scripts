/*
name: Lavarock Merge
description: This bot will farm the items belonging to the selected mode for the Lavarock Merge [2292] in /lavarockbay
tags: lavarock, merge, lavarockbay, fiery, fissure, scythe, molten, lava, orb, orbs, guardian, frilled, horned, sheathed, kalayos, oath, oaths
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LavarockMerge
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
        Core.BankingBlackList.AddRange(new[] { "Volcanic Fragment" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Core.Logger("This boss requires a strong solo class or army. Do not proceed if you can't beat the boss.");
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("lavarockbay", 2292, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Volcanic Fragment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.KillMonster("lavarockbay", "r2", "Left", "*", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("13772", "Fiery Fissure Scythe", "Mode: [select] only\nShould the bot buy \"Fiery Fissure Scythe\" ?", false),
        new Option<bool>("78100", "Molten Lava Orb", "Mode: [select] only\nShould the bot buy \"Molten Lava Orb\" ?", false),
        new Option<bool>("78101", "Molten Lava Orbs", "Mode: [select] only\nShould the bot buy \"Molten Lava Orbs\" ?", false),
        new Option<bool>("78102", "Lavarock Guardian", "Mode: [select] only\nShould the bot buy \"Lavarock Guardian\" ?", false),
        new Option<bool>("78103", "Lavarock Guardian Frilled Helm", "Mode: [select] only\nShould the bot buy \"Lavarock Guardian Frilled Helm\" ?", false),
        new Option<bool>("78104", "Lavarock Guardian Horned Helm", "Mode: [select] only\nShould the bot buy \"Lavarock Guardian Horned Helm\" ?", false),
        new Option<bool>("78105", "Lavarock Guardian Sheathed Blade", "Mode: [select] only\nShould the bot buy \"Lavarock Guardian Sheathed Blade\" ?", false),
        new Option<bool>("78107", "Kalayo's Oath", "Mode: [select] only\nShould the bot buy \"Kalayo's Oath\" ?", false),
        new Option<bool>("78110", "Kalayo's Oaths", "Mode: [select] only\nShould the bot buy \"Kalayo's Oaths\" ?", false),
    };
}
