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

public class AshfallCampMerge
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
        Core.BankingBlackList.AddRange(new[] { "Iron Ore", "Iron Ingot", "Bile Stone", "Molten Lava", "Fabric", "Blue Dye", "Red Dye", "Dragon Scale", "Defender Badge", "Flame Claws", "Flame Heart", "Sulphur Ore", "Venom Sac", "Venom Fangs", "Crystal Eye", "Glass Horns", "Storm Heart", "Melted Glass", "Copper Wire" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ashfallcamp", 1422, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Fabric":
                case "Blue Dye":
                case "Red Dye":
                case "Green Dye":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5898);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //QuarterMaster’s Supplies 5898
                        Core.HuntMonster("AshfallCamp", "Lava Dragoblin", "Supply Chest", 8, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dragon Scales":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(5893);
                    while (!Bot.ShouldExit && !Core.CheckInventory(40375, quant))
                    {
                        //Blackrawk Magebane 5893
                        Core.HuntMonster("AshfallCamp", "Blackrawk", "Blackrawk Defeated", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Defender Badge":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("AshfallCamp", "Blackrawk|Infernus|Smoldur", req.Name, quant, false);
                    break;

                case "Flame Claws":
                case "Flame Heart":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("AshfallCamp", "Smoldur", req.Name, quant, false);
                    break;

                case "Sulphur Ore":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5899);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("AshfallCamp", "Sulphur Dracolich", "Sulphur Crystal", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Iron Ore":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    int i = 1;
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.ID, quant))
                    {
                        Core.EnsureAccept(5900);
                        Core.HuntMonster("AshfallCamp", "Draconian Guard", "Iron Lump", 5, log: false);
                        Core.HuntMonster("AshfallCamp", "Draconian Guard", "Bile Drops", 3, log: false);
                        Core.EnsureComplete(5900, req.ID);
                        Core.Logger($"Quest completed x{i++} times: [5900] \"Ingots and Outguts\"");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Bile Stone":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    int b = 1;
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.ID, quant))
                    {
                        Core.EnsureAccept(5900);
                        Core.HuntMonster("AshfallCamp", "Draconian Guard", "Iron Lump", 5, log: false);
                        Core.HuntMonster("AshfallCamp", "Draconian Guard", "Bile Drops", 3, log: false);
                        Core.EnsureComplete(5900, req.ID);
                        Core.Logger($"Quest completed x{b++} times: [5900] \"Ingots and Outguts\"");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Molten Lava":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5897);
                    while (!Bot.ShouldExit && !Core.CheckInventory(40352, quant))
                    {
                        Core.HuntMonster("AshfallCamp", "Lava Rock", "Lava Glob", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Venom Sac":
                case "Venom Fangs":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("AshfallCamp", "Infernus", req.Name, quant, false);
                    break;

                case "Crystal Eye":
                case "Glass Horns":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("AshfallCamp", "Blackrawk", req.Name, quant, false);
                    break;

                case "Storm Heart":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("Pride", "Valsarian", req.Name, quant, false);
                    break;

                case "Melted Glass":
                case "Copper Wire":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Pride", "Cellar Guard|Drakel Guard|Elite Guard", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("40401", "DragonSlayer Locks", "Mode: [select] only\nShould the bot buy \"DragonSlayer Locks\" ?", false),
        new Option<bool>("40402", "DragonSlayer Cut", "Mode: [select] only\nShould the bot buy \"DragonSlayer Cut\" ?", false),
        new Option<bool>("40374", "DragonSlayer Guard", "Mode: [select] only\nShould the bot buy \"DragonSlayer Guard\" ?", false),
        new Option<bool>("40409", "Flame Dragon Armor", "Mode: [select] only\nShould the bot buy \"Flame Dragon Armor\" ?", false),
        new Option<bool>("40410", "Flame Dragon Helm", "Mode: [select] only\nShould the bot buy \"Flame Dragon Helm\" ?", false),
        new Option<bool>("40411", "Flame Dragon Cape", "Mode: [select] only\nShould the bot buy \"Flame Dragon Cape\" ?", false),
        new Option<bool>("40412", "Flame Dragon Polearm", "Mode: [select] only\nShould the bot buy \"Flame Dragon Polearm\" ?", false),
        new Option<bool>("40413", "Green Dragon Armor", "Mode: [select] only\nShould the bot buy \"Green Dragon Armor\" ?", false),
        new Option<bool>("40414", "Green Dragon Helm", "Mode: [select] only\nShould the bot buy \"Green Dragon Helm\" ?", false),
        new Option<bool>("40415", "Green Dragon Cape", "Mode: [select] only\nShould the bot buy \"Green Dragon Cape\" ?", false),
        new Option<bool>("40416", "Green Dragon Polearm", "Mode: [select] only\nShould the bot buy \"Green Dragon Polearm\" ?", false),
        new Option<bool>("40405", "Ice Dragon Armor", "Mode: [select] only\nShould the bot buy \"Ice Dragon Armor\" ?", false),
        new Option<bool>("40406", "Ice Dragon Helm", "Mode: [select] only\nShould the bot buy \"Ice Dragon Helm\" ?", false),
        new Option<bool>("40407", "Ice Dragon Cape", "Mode: [select] only\nShould the bot buy \"Ice Dragon Cape\" ?", false),
        new Option<bool>("40408", "Ice Dragon Polearm", "Mode: [select] only\nShould the bot buy \"Ice Dragon Polearm\" ?", false),
        new Option<bool>("40553", "Lightning Orb Mace", "Mode: [select] only\nShould the bot buy \"Lightning Orb Mace\" ?", false),
        new Option<bool>("40552", "Storm Drakel Warrior", "Mode: [select] only\nShould the bot buy \"Storm Drakel Warrior\" ?", false),
        new Option<bool>("40565", "Stormpowered Staff", "Mode: [select] only\nShould the bot buy \"Stormpowered Staff\" ?", false),
        new Option<bool>("40563", "Copperwire Daggers", "Mode: [select] only\nShould the bot buy \"Copperwire Daggers\" ?", false),
        new Option<bool>("40564", "Dwakelcharged Slicer", "Mode: [select] only\nShould the bot buy \"Dwakelcharged Slicer\" ?", false),
    };
}
