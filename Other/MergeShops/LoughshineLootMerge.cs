/*
name: Loughshine Loot Merge
description: This bot will farm the items belonging to the selected mode for the Loughshine Loot Merge [2452] in /loughshine
tags: loughshine, loot, merge, loughshine, gold, voucher, k, skye, executor, backhanded, speirling, warden, west, wardens, cowled, guardian
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\AgeOfRuin\CoreAOR.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LoughshineLootMerge
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
        Core.BankingBlackList.AddRange(new[] { "Salvaged Skye Armament", "Speirling Dagger", "Speirling Daggers", "Solid Gold Alloy", "Skye Executor Hooded Locks", "Skye Executor's Cloak" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.Loughshine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("loughshine", 2452, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Salvaged Skye Armament":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("castleeblana", "Skye Warrior", req.Name, quant, req.Temp, false);
                    break;

                case "Speirling Dagger":
                case "Speirling Daggers":
                case "Skye Executor Hooded Locks":
                case "Skye Executor's Cloak":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("loughshine", "Skye Executor", req.Name, quant, req.Temp, false);
                    break;

                case "Solid Gold Alloy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9765);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("loughshine", "Scorched Elder Yew", "Yew Root", 100, log: false);
                        Core.HuntMonster("loughshine", "Energy Elemental", "Ion Particles", 60, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("loughshine", "Warden Iseul", "Gold Pendant", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("86037", "Skye Executor", "Mode: [select] only\nShould the bot buy \"Skye Executor\" ?", false),
        new Option<bool>("86038", "Skye Executor Hooded Visage", "Mode: [select] only\nShould the bot buy \"Skye Executor Hooded Visage\" ?", false),
        new Option<bool>("86039", "Skye Executor Hood", "Mode: [select] only\nShould the bot buy \"Skye Executor Hood\" ?", false),
        new Option<bool>("86044", "Backhanded Speirling Dagger", "Mode: [select] only\nShould the bot buy \"Backhanded Speirling Dagger\" ?", false),
        new Option<bool>("86045", "Backhanded Speirling Daggers", "Mode: [select] only\nShould the bot buy \"Backhanded Speirling Daggers\" ?", false),
        new Option<bool>("86046", "Skye Warden of the West", "Mode: [select] only\nShould the bot buy \"Skye Warden of the West\" ?", false),
        new Option<bool>("86047", "Skye Warden's Cowled Visage", "Mode: [select] only\nShould the bot buy \"Skye Warden's Cowled Visage\" ?", false),
        new Option<bool>("86048", "Skye Warden's Cowled Mask", "Mode: [select] only\nShould the bot buy \"Skye Warden's Cowled Mask\" ?", false),
        new Option<bool>("86049", "Skye Warden's Cowled Locks", "Mode: [select] only\nShould the bot buy \"Skye Warden's Cowled Locks\" ?", false),
        new Option<bool>("86050", "Skye Warden of the West Cape", "Mode: [select] only\nShould the bot buy \"Skye Warden of the West Cape\" ?", false),
        new Option<bool>("86051", "Speirling Guardian Dagger", "Mode: [select] only\nShould the bot buy \"Speirling Guardian Dagger\" ?", false),
        new Option<bool>("86052", "Speirling Guardian Daggers", "Mode: [select] only\nShould the bot buy \"Speirling Guardian Daggers\" ?", false),
        new Option<bool>("86053", "Backhanded Speirling Guardian Dagger", "Mode: [select] only\nShould the bot buy \"Backhanded Speirling Guardian Dagger\" ?", false),
        new Option<bool>("86054", "Backhanded Speirling Guardian Daggers", "Mode: [select] only\nShould the bot buy \"Backhanded Speirling Guardian Daggers\" ?", false),
    };
}
