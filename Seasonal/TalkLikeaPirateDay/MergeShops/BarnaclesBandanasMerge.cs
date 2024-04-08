/*
name: Barnacle's Bandanas Merge
description: This bot will farm the items belonging to the selected mode for the Barnacles Bandanas Merge [984] in /pirates
tags: tlapd,talk-like-a-pirate-day,seasonal,barnacle, bandanas, merge, pirates, chaos, bandana, undead, legion, rotting, naval, icy, galactic, doom, chrono, bright, blazing, toxic
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

public class BarnaclesBandanasMerge
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
        Core.BankingBlackList.AddRange(new[] { "Scrap of Cloth" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("pirates", 984, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Scrap of Cloth":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        foreach (int mon in new[] { 3, 7, 15, 10 })
                        {
                            Monster? M = Bot.Monsters.MapMonsters.FirstOrDefault(x => x != null && x.MapID == mon);
                            if (M == null)
                                continue;

                            if (Bot.Map.Name != "tlapd")
                                Core.Join("tlapd");
                            if (Bot.Player.Cell != M!.Cell)
                                Core.Jump(M.Cell);

                            if (M != null && M.HP >= 0)
                                Bot.Hunt.Monster(M.MapID);

                            if (Core.CheckInventory(req.Name, quant))
                                break;
                        }
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("25492", "Chaos Bandana", "Mode: [select] only\nShould the bot buy \"Chaos Bandana\" ?", false),
        new Option<bool>("25493", "Undead Legion Bandana", "Mode: [select] only\nShould the bot buy \"Undead Legion Bandana\" ?", false),
        new Option<bool>("25494", "Rotting Bandana", "Mode: [select] only\nShould the bot buy \"Rotting Bandana\" ?", false),
        new Option<bool>("25495", "Naval Bandana", "Mode: [select] only\nShould the bot buy \"Naval Bandana\" ?", false),
        new Option<bool>("25496", "Legion Bandana", "Mode: [select] only\nShould the bot buy \"Legion Bandana\" ?", false),
        new Option<bool>("25497", "Icy Bandana", "Mode: [select] only\nShould the bot buy \"Icy Bandana\" ?", false),
        new Option<bool>("25498", "Galactic Bandana", "Mode: [select] only\nShould the bot buy \"Galactic Bandana\" ?", false),
        new Option<bool>("25499", "Doom Bandana", "Mode: [select] only\nShould the bot buy \"Doom Bandana\" ?", false),
        new Option<bool>("25500", "Chrono Bandana", "Mode: [select] only\nShould the bot buy \"Chrono Bandana\" ?", false),
        new Option<bool>("25501", "Bright Bandana", "Mode: [select] only\nShould the bot buy \"Bright Bandana\" ?", false),
        new Option<bool>("25502", "Blazing Bandana", "Mode: [select] only\nShould the bot buy \"Blazing Bandana\" ?", false),
        new Option<bool>("56385", "Toxic Bandana", "Mode: [select] only\nShould the bot buy \"Toxic Bandana\" ?", false),
    };
}
