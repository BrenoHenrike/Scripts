/*
name: Awe Merge
description: This bot will farm the items belonging to the selected mode for the Awe Merge [632] in /museum
tags: awe, merge, museum, prime, spear, guardian, guardians, armored, dragon, pet, flameborn, iceborn, windborn, earthborn, stormborn, waterborn, lightborn, darkborn, baby, enchanted
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AweMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreAwe Awe = new();
    private ArmorOfAwe AoA = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Aura of Awe", "Blade of Awe", "Staff of Awe", "Spear of Awe", "Dagger of Awe", "Guardian Patent", "Guardian Dragon Pet", "Baby Red Dragon", "Armor of Awe", "Cape of Awe" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Farm.BladeofAweREP();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("museum", 632, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Aura of Awe":
                    if (!Bot.Player.IsMember)
                        break;

                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(2939);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dwakelcrashsite", "Mithril Man", "Evolution Of Awe", 13, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Blade of Awe":
                case "Spear of Awe":
                case "Dagger of Awe":
                case "Staff of Awe":
                case "Guardian Dragon Pet":
                    Adv.BuyItem("museum", 631, req.Name);
                    break;

                case "Guardian Patent":
                    if (Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intAQ") > 0)
                    {
                        Core.Logger("Active Aqw Guardian Acc Requiored for this Item.");
                        break;
                    }
                    else Adv.BuyItem("museum", 53, "Guardian Patent");
                    break;

                case "Baby Red Dragon":
                    Adv.BuyItem("AriaPet", 12, req.Name);
                    break;

                case "Armor of Awe":
                    AoA.GetArmor();
                    break;

                case "Cape of Awe":
                    Awe.GetAweRelic("Cape", 4178, 1, 1, "doomvault", "Binky");
                    Adv.BuyItem("museum", 1129, "Cape of Awe");
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("17586", "Prime Blade of Awe", "Mode: [select] only\nShould the bot buy \"Prime Blade of Awe\" ?", false),
        new Option<bool>("17608", "Prime Staff of Awe", "Mode: [select] only\nShould the bot buy \"Prime Staff of Awe\" ?", false),
        new Option<bool>("17609", "Prime Spear of Awe", "Mode: [select] only\nShould the bot buy \"Prime Spear of Awe\" ?", false),
        new Option<bool>("17610", "Prime Dagger of Awe", "Mode: [select] only\nShould the bot buy \"Prime Dagger of Awe\" ?", false),
        new Option<bool>("17587", "Guardian Blade of Awe", "Mode: [select] only\nShould the bot buy \"Guardian Blade of Awe\" ?", false),
        new Option<bool>("17588", "Guardian's Prime Blade of Awe", "Mode: [select] only\nShould the bot buy \"Guardian's Prime Blade of Awe\" ?", false),
        new Option<bool>("17803", "Armored Guardian Dragon Pet", "Mode: [select] only\nShould the bot buy \"Armored Guardian Dragon Pet\" ?", false),
        new Option<bool>("64231", "Flameborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Flameborn Blade of Awe\" ?", false),
        new Option<bool>("64232", "Iceborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Iceborn Blade of Awe\" ?", false),
        new Option<bool>("64233", "Windborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Windborn Blade of Awe\" ?", false),
        new Option<bool>("64234", "Earthborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Earthborn Blade of Awe\" ?", false),
        new Option<bool>("64235", "Stormborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Stormborn Blade of Awe\" ?", false),
        new Option<bool>("64236", "Waterborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Waterborn Blade of Awe\" ?", false),
        new Option<bool>("64237", "Lightborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Lightborn Blade of Awe\" ?", false),
        new Option<bool>("64238", "Darkborn Blade of Awe", "Mode: [select] only\nShould the bot buy \"Darkborn Blade of Awe\" ?", false),
        new Option<bool>("73246", "Baby Dragon of Awe", "Mode: [select] only\nShould the bot buy \"Baby Dragon of Awe\" ?", false),
        new Option<bool>("68891", "Enchanted Armor of Awe", "Mode: [select] only\nShould the bot buy \"Enchanted Armor of Awe\" ?", false),
        new Option<bool>("68892", "Enchanted Cape of Awe", "Mode: [select] only\nShould the bot buy \"Enchanted Cape of Awe\" ?", false),
    };
}
