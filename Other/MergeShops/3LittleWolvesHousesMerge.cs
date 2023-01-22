/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ThreeLittleWolvesHousesMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Building Material", "Foundation Material", "Decor Material", "Dragonrune Blueprint", "Mana Golem's Core", "Arcangrove Blueprint", "Falcontower Blueprint", "Citadel Caverns Blueprint", "Citadel Blueprint", "Seraphic Blueprint", "Hachiko Blueprint", "Clubhouse Blueprint " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("buyhouse", 1729, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Building Material":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6915);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("farm", "Treeant", "Wooden Planks", 5);
                        Core.HuntMonster("bloodtusk", "Rhison", "Glue");
                        Core.HuntMonster("crashsite", "ProtoSartorium", "Nails", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Foundation Material":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6916);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("river", "Zardman Fisher", "River Stones", 5);
                        Core.HuntMonster("dwarfprison", "Balboa", "Boulder", 3);
                        Core.HuntMonster("dragonplane", "Earth Elemental", "Marble");
                        Core.HuntMonster("gilead", "Fire Elemental", "Flames", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Decor Material":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6917);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("farm", "Scarecrow", "Fabric", 5);
                        Core.HuntMonster("goose", "Can of Paint", "Paint", 5);
                        Core.HuntMonster("undergroundlabb", "Window", "Glass", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dragonrune Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Farm.ElementalMasterREP();
                        Core.BuyItem("dragonrune", 690, 48758);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Mana Golem's Core":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("elemental", "Mana Golem", "Mana Golem's Core", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Arcangrove Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Farm.ArcangroveREP();
                        Core.BuyItem("arcangrove", 214, 48759);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Falcontower Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("falconreach", "Dragon Drakath", "Falcontower Blueprint", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Citadel Caverns Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("citadel", "Belrot the Fiend", "Citadel Caverns Blueprint", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Citadel Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("citadel", 44, 48761);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Seraphic Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("seraph", 1133, 48762);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Hachiko Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("dragonkoiz", 95, 48763);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Clubhouse Blueprint":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("clubhouse", "Riddlelord's Golem", "Clubhouse Blueprint", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48516", "Dragonrune House", "Mode: [select] only\nShould the bot buy \"Dragonrune House\" ?", false),
        new Option<bool>("48517", "Dragonrune Hall", "Mode: [select] only\nShould the bot buy \"Dragonrune Hall\" ?", false),
        new Option<bool>("48518", "Arcangrove Tower House", "Mode: [select] only\nShould the bot buy \"Arcangrove Tower House\" ?", false),
        new Option<bool>("48765", "Tower of Magic House", "Mode: [select] only\nShould the bot buy \"Tower of Magic House\" ?", false),
        new Option<bool>("48766", "Falcontower House", "Mode: [select] only\nShould the bot buy \"Falcontower House\" ?", false),
        new Option<bool>("48767", "Citadel Caverns House", "Mode: [select] only\nShould the bot buy \"Citadel Caverns House\" ?", false),
        new Option<bool>("48771", "Citadel House", "Mode: [select] only\nShould the bot buy \"Citadel House\" ?", false),
        new Option<bool>("48768", "Seraphic Fortress", "Mode: [select] only\nShould the bot buy \"Seraphic Fortress\" ?", false),
        new Option<bool>("48769", "Hachiko Hotel", "Mode: [select] only\nShould the bot buy \"Hachiko Hotel\" ?", false),
        new Option<bool>("48770", "Clubhouse", "Mode: [select] only\nShould the bot buy \"Clubhouse\" ?", false),
    };
}
