/*
name: Haku War Merge
description: This bot will farm the items belonging to the selected mode for the Haku War Merge [2416] in /hakuwar
tags: haku, war, merge, hakuwar, yokai, dragonslayer, noble, beard, cloak, quiver, luminous, mikazuki, kisshoten, naginata, seven, fang, dragonbane, tanto, enchanted
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HakuWarMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDOY DOY = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Village's Grace" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DOY.HakuWar();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hakuwar", 2416, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Village's Grace":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9601, 9602);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("hakuwar", "Enter", "Spawn", "*", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("83795", "Yokai Dragonslayer", "Mode: [select] only\nShould the bot buy \"Yokai Dragonslayer\" ?", false),
        new Option<bool>("83796", "Noble Yokai Dragonslayer Helm", "Mode: [select] only\nShould the bot buy \"Noble Yokai Dragonslayer Helm\" ?", false),
        new Option<bool>("83797", "Yokai Dragonslayer Helm", "Mode: [select] only\nShould the bot buy \"Yokai Dragonslayer Helm\" ?", false),
        new Option<bool>("83798", "Noble Yokai Dragonslayer Beard", "Mode: [select] only\nShould the bot buy \"Noble Yokai Dragonslayer Beard\" ?", false),
        new Option<bool>("83799", "Yokai Dragonslayer Beard", "Mode: [select] only\nShould the bot buy \"Yokai Dragonslayer Beard\" ?", false),
        new Option<bool>("83803", "Yokai Dragonslayer War Cloak", "Mode: [select] only\nShould the bot buy \"Yokai Dragonslayer War Cloak\" ?", false),
        new Option<bool>("83805", "Yokai Dragonslayer Quiver Cloak", "Mode: [select] only\nShould the bot buy \"Yokai Dragonslayer Quiver Cloak\" ?", false),
        new Option<bool>("83808", "Luminous Mikazuki", "Mode: [select] only\nShould the bot buy \"Luminous Mikazuki\" ?", false),
        new Option<bool>("83809", "Dual Luminous Mikazuki", "Mode: [select] only\nShould the bot buy \"Dual Luminous Mikazuki\" ?", false),
        new Option<bool>("83812", "Kisshoten Naginata", "Mode: [select] only\nShould the bot buy \"Kisshoten Naginata\" ?", false),
        new Option<bool>("83806", "Seven Fang Blade", "Mode: [select] only\nShould the bot buy \"Seven Fang Blade\" ?", false),
        new Option<bool>("83807", "Seven Fang Blades", "Mode: [select] only\nShould the bot buy \"Seven Fang Blades\" ?", false),
        new Option<bool>("83810", "Dragonbane Tanto", "Mode: [select] only\nShould the bot buy \"Dragonbane Tanto\" ?", false),
        new Option<bool>("83811", "Dual Dragonbane Tanto", "Mode: [select] only\nShould the bot buy \"Dual Dragonbane Tanto\" ?", false),
        new Option<bool>("83814", "Enchanted Yokai Dragonslayer", "Mode: [select] only\nShould the bot buy \"Enchanted Yokai Dragonslayer\" ?", false),
        new Option<bool>("83815", "Enchanted Noble Yokai Dragonslayer Helm", "Mode: [select] only\nShould the bot buy \"Enchanted Noble Yokai Dragonslayer Helm\" ?", false),
        new Option<bool>("83816", "Enchanted Yokai Dragonslayer Helm", "Mode: [select] only\nShould the bot buy \"Enchanted Yokai Dragonslayer Helm\" ?", false),
        new Option<bool>("83817", "Enchanted Noble Yokai Dragonslayer Beard", "Mode: [select] only\nShould the bot buy \"Enchanted Noble Yokai Dragonslayer Beard\" ?", false),
        new Option<bool>("83818", "Enchanted Yokai Dragonslayer Beard", "Mode: [select] only\nShould the bot buy \"Enchanted Yokai Dragonslayer Beard\" ?", false),
        new Option<bool>("83821", "Enchanted Yokai Dragonslayer War Cloak", "Mode: [select] only\nShould the bot buy \"Enchanted Yokai Dragonslayer War Cloak\" ?", false),
        new Option<bool>("83823", "Enchanted Yokai Dragonslayer Quiver Cloak", "Mode: [select] only\nShould the bot buy \"Enchanted Yokai Dragonslayer Quiver Cloak\" ?", false),
        new Option<bool>("83824", "Enchanted Seven Fang Blade", "Mode: [select] only\nShould the bot buy \"Enchanted Seven Fang Blade\" ?", false),
        new Option<bool>("83825", "Enchanted Seven Fang Blades", "Mode: [select] only\nShould the bot buy \"Enchanted Seven Fang Blades\" ?", false),
        new Option<bool>("83826", "Enchanted Luminous Mikazuki", "Mode: [select] only\nShould the bot buy \"Enchanted Luminous Mikazuki\" ?", false),
        new Option<bool>("83827", "Dual Enchanted Luminous Mikazuki", "Mode: [select] only\nShould the bot buy \"Dual Enchanted Luminous Mikazuki\" ?", false),
        new Option<bool>("83828", "Enchanted Dragonbane Tanto", "Mode: [select] only\nShould the bot buy \"Enchanted Dragonbane Tanto\" ?", false),
        new Option<bool>("83829", "Dual Enchanted Dragonbane Tanto", "Mode: [select] only\nShould the bot buy \"Dual Enchanted Dragonbane Tanto\" ?", false),
        new Option<bool>("83830", "Enchanted Kisshoten Naginata", "Mode: [select] only\nShould the bot buy \"Enchanted Kisshoten Naginata\" ?", false),
    };
}
