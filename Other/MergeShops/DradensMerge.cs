/*
name: Dradens Merge
description: This bot will farm the items belonging to the selected mode for the Dradens Merge [1137] in /phoenixrise
tags: dradens, merge, phoenixrise, phoenix, tempest, general, firestorm, banner, fyreborn, mauler, corundum, carver, cavernous, horned
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DradensMerge
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
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Phoenix Gate Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("phoenixrise", 1137, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Phoenix Gate Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.IsMember)
                    {
                        Core.RegisterQuests(4214);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("phoenixrise", "Firestorm Tiger", "Tigerskin", 5, log: false);
                            Core.HuntMonster("phoenixrise", "Infernal Goblin", "Strips of Goblin Leather", 3, log: false);
                            Core.HuntMonster("phoenixrise", "Lava Troll", "Lava Globule", 4, log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    else
                    {
                        Core.FarmingLogger(req.Name, quant);
                        Core.EquipClass(ClassType.Solo);
                        Core.RegisterQuests(4215);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("phoenixrise", "Cinderclaw", "Minotiger Horn", log: false);
                            Core.HuntMonster("phoenixrise", "Gargrowl", "Stone Shard", log: false);
                            Core.HuntMonster("phoenixrise", "Pyrric Ursus", "Crystal Pommel", log: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("29516", "Blade of the Phoenix", "Mode: [select] only\nShould the bot buy \"Blade of the Phoenix\" ?", false),
        new Option<bool>("29294", "Tempest General Helm", "Mode: [select] only\nShould the bot buy \"Tempest General Helm\" ?", false),
        new Option<bool>("29295", "Firestorm General", "Mode: [select] only\nShould the bot buy \"Firestorm General\" ?", false),
        new Option<bool>("29494", "Phoenixrise Banner Cape", "Mode: [select] only\nShould the bot buy \"Phoenixrise Banner Cape\" ?", false),
        new Option<bool>("29488", "Fyreborn Mauler", "Mode: [select] only\nShould the bot buy \"Fyreborn Mauler\" ?", false),
        new Option<bool>("29489", "Corundum Carver Cape", "Mode: [select] only\nShould the bot buy \"Corundum Carver Cape\" ?", false),
        new Option<bool>("29490", "Cavernous Carver Axe", "Mode: [select] only\nShould the bot buy \"Cavernous Carver Axe\" ?", false),
        new Option<bool>("29491", "Fyreborn Horned Locks", "Mode: [select] only\nShould the bot buy \"Fyreborn Horned Locks\" ?", false),
        new Option<bool>("29492", "Fyreborn Horned Helm", "Mode: [select] only\nShould the bot buy \"Fyreborn Horned Helm\" ?", false),
    };
}
