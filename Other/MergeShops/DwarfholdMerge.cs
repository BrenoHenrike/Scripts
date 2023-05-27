/*
name: Dwarfhold Merge
description: This bot will farm the items belonging to the selected mode for the Dwarfhold Merge [2286] in /dwarfhold
tags: dwarfhold, merge, dwarfhold, armored, prospector, prospectors, braided, beard, spectacles, spectacled, practical, rucksack, geometric, silver, gilded, goggles, versatile, honored
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\LordsofChaos\Core13LoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DwarfholdMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Core13LoC LoC = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dwarven Metal", "Dwarven Alloy" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        LoC.Vath();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dwarfhold", 2286, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dwarven Metal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9237);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dwarfhold", "Chaos Drow", "Broken Drow Blade", 5, log: false);
                        Core.HuntMonster("dwarfhold", "Chaotic Draconian", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dwarven Alloy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9238);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("mooncursedlair", "Shard of Moonlight", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77578", "Armored Dwarfhold Prospector", "Mode: [select] only\nShould the bot buy \"Armored Dwarfhold Prospector\" ?", false),
        new Option<bool>("77579", "Dwarfhold Prospector's Braided Beard", "Mode: [select] only\nShould the bot buy \"Dwarfhold Prospector's Braided Beard\" ?", false),
        new Option<bool>("77580", "Dwarfhold Prospector's Spectacles", "Mode: [select] only\nShould the bot buy \"Dwarfhold Prospector's Spectacles\" ?", false),
        new Option<bool>("77581", "Dwarfhold Prospector's Spectacled Locks", "Mode: [select] only\nShould the bot buy \"Dwarfhold Prospector's Spectacled Locks\" ?", false),
        new Option<bool>("77582", "Practical Dwarfhold Rucksack", "Mode: [select] only\nShould the bot buy \"Practical Dwarfhold Rucksack\" ?", false),
        new Option<bool>("77583", "Dwarfhold Geometric Silver Axe", "Mode: [select] only\nShould the bot buy \"Dwarfhold Geometric Silver Axe\" ?", false),
        new Option<bool>("77948", "Dwarfhold Geometric Silver Axes", "Mode: [select] only\nShould the bot buy \"Dwarfhold Geometric Silver Axes\" ?", false),
        new Option<bool>("77572", "Gilded Dwarfhold Prospector", "Mode: [select] only\nShould the bot buy \"Gilded Dwarfhold Prospector\" ?", false),
        new Option<bool>("77573", "Gilded Prospector's Braided Beard", "Mode: [select] only\nShould the bot buy \"Gilded Prospector's Braided Beard\" ?", false),
        new Option<bool>("77574", "Gilded Prospector's Goggles", "Mode: [select] only\nShould the bot buy \"Gilded Prospector's Goggles\" ?", false),
        new Option<bool>("77575", "Gilded Prospector's Goggles and Locks", "Mode: [select] only\nShould the bot buy \"Gilded Prospector's Goggles and Locks\" ?", false),
        new Option<bool>("77576", "Versatile Dwarfhold Rucksack", "Mode: [select] only\nShould the bot buy \"Versatile Dwarfhold Rucksack\" ?", false),
        new Option<bool>("77577", "Honored Gilded Axe", "Mode: [select] only\nShould the bot buy \"Honored Gilded Axe\" ?", false),
        new Option<bool>("77949", "Honored Gilded Axes", "Mode: [select] only\nShould the bot buy \"Honored Gilded Axes\" ?", false),
    };
}
