/*
name: Shadowrealm
description: This bot will farm the items belonging to the selected mode for the Shadowrealm Merge [1889] in /shadowrealm
tags: shadowrealm, merge, shadowrealm, hollowborn, thunderlord, visor, lightning, lightnings, vampire, flying, skulls, scythe, chained, reaper, guard, fiend, blind, reapers, , horns, ponytail, katana, kamas, kama, minion, sheathed, dragonslayer, dragonslayers, beard, scarf
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowrealmMerge
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
        Core.BankingBlackList.AddRange(new[] { "Hollow Soul", "Bone Dust", "Death's Oversight", "Death's Scythe", "Unmoulded Fiend Essence" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowrealm", 1889, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hollow Soul":
                    Core.FarmingLogger($"{req.Name}", quant);
                    // Core.RegisterQuests(7553, 7555);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7553, 7555); //Get the Seeds 7553, Flex it! 7555
                        Core.KillMonster("shadowrealm", "r2", "Left", "Gargrowl", "Darkseed", 8, log: false);
                        Core.KillMonster("shadowrealm", "r2", "Left", "Shadow Guardian", "Shadow Medallion", 5, log: false);
                        Core.EnsureComplete(7553, 7555); //Get the Seeds 7553, Flex it! 7555
                    }
                    // Core.CancelRegisteredQuests();
                    break;

                case "Bone Dust":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Farm.BattleUnderB(req.Name, quant);
                    break;

                case "Death's Oversight":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.KillMonster("shadowattack", "Boss", "Left", "Death", req.Name, quant, false);
                    break;

                case "Death's Scythe":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("shadowattack", "Death", req.Name, isTemp: false);
                    break;

                case "Unmoulded Fiend Essence":
                    Adv.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence", quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54867", "Hollowborn Thunderlord", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord\" ?", false),
        new Option<bool>("54868", "Hollowborn Thunderlord Visor", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Visor\" ?", false),
        new Option<bool>("54869", "Hollowborn Thunderlord Visage", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Visage\" ?", false),
        new Option<bool>("54870", "Hollowborn Thunderlord Lightning Cape", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Lightning Cape\" ?", false),
        new Option<bool>("54871", "Hollowborn Thunderlord Lightning", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Lightning\" ?", false),
        new Option<bool>("54872", "Hollowborn Thunderlord Lightnings", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Lightnings\" ?", false),
        new Option<bool>("54874", "Hollowborn Vampire", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire\" ?", false),
        new Option<bool>("54875", "Hollowborn Vampire Hair", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Hair\" ?", false),
        new Option<bool>("54876", "Hollowborn Vampire Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Mask\" ?", false),
        new Option<bool>("54877", "Hollowborn Vampire Flying Skulls", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Flying Skulls\" ?", false),
        new Option<bool>("54878", "Hollowborn Vampire Scythe", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Scythe\" ?", false),
        new Option<bool>("61766", "Hollowborn Chained Reaper", "Mode: [select] only\nShould the bot buy \"Hollowborn Chained Reaper\" ?", false),
        new Option<bool>("61767", "Hollowborn Chained Guard", "Mode: [select] only\nShould the bot buy \"Hollowborn Chained Guard\" ?", false),
        new Option<bool>("61768", "Hollowborn Fiend Reaper", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Reaper\" ?", false),
        new Option<bool>("61769", "Hollowborn Fiend Guard", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Guard\" ?", false),
        new Option<bool>("61770", "Hollowborn Fiend Hair", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Hair\" ?", false),
        new Option<bool>("61771", "Blind Hollowborn Fiend Hair", "Mode: [select] only\nShould the bot buy \"Blind Hollowborn Fiend Hair\" ?", false),
        new Option<bool>("61772", "Hollowborn Fiend Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Mask\" ?", false),
        new Option<bool>("61773", "Hollowborn Reaper's Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Hood\" ?", false),
        new Option<bool>("61774", "Hollowborn Reaper's Hood + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Hood + Locks\" ?", false),
        new Option<bool>("61775", "Hollowborn Reaper's Hooded Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Hooded Mask\" ?", false),
        new Option<bool>("61776", "Hollowborn Fiend Reaper's Horns", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Reaper's Horns\" ?", false),
        new Option<bool>("61777", "Hollowborn Reaper's Ponytail", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Ponytail\" ?", false),
        new Option<bool>("61778", "Blind Hollowborn Reaper's Ponytail", "Mode: [select] only\nShould the bot buy \"Blind Hollowborn Reaper's Ponytail\" ?", false),
        new Option<bool>("61779", "Hollowborn Reaper's Mask + Ponytail", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Mask + Ponytail\" ?", false),
        new Option<bool>("61780", "Hollowborn Reaper's Katana", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Katana\" ?", false),
        new Option<bool>("61781", "Hollowborn Reaper's Scythe", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Scythe\" ?", false),
        new Option<bool>("61782", "Hollowborn Reaper's Daggers", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Daggers\" ?", false),
        new Option<bool>("61783", "Hollowborn Reaper's Kamas", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Kamas\" ?", false),
        new Option<bool>("61784", "Hollowborn Reaper's Kama", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Kama\" ?", false),
        new Option<bool>("61785", "Hollowborn Reaper's Minion", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Minion\" ?", false),
        new Option<bool>("61786", "Hollowborn Reaper's Sheathed Katana", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Sheathed Katana\" ?", false),
        new Option<bool>("55355", "Hollowborn DragonSlayer", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer\" ?", false),
        new Option<bool>("55356", "Hollowborn DragonSlayer's Helm", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Helm\" ?", false),
        new Option<bool>("55357", "Hollowborn DragonSlayer's Beard", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Beard\" ?", false),
        new Option<bool>("55358", "Hollowborn DragonSlayer's Scarf", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Scarf\" ?", false),
        new Option<bool>("55359", "Hollowborn DragonSlayer's Cape", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Cape\" ?", false),
        new Option<bool>("55360", "Hollowborn DragonSlayer's Blade", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Blade\" ?", false),
    };
}
