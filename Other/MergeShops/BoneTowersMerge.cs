/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TowersMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreToD TOD = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        TOD.BoneTowerAll();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("towersilver", 1243, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                // Add how to get items here
                case "SilverSkull Amulet":
                    Core.RegisterQuests(5009);
                    Core.EquipClass(ClassType.Farm);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("towersilver", "Fallen DeathKnight", "Chef Ramskull's Apron");
                        Core.HuntMonster("towersilver", "Undead Knight", "Chef Ramskull's Hat");
                        Core.HuntMonster("towersilver", "Undead Warrior", "Chef Ramskull's Cookbook");
                        Core.HuntMonster("towersilver", "Ghoul", "Chef Ramskull's Spatula");
                        Core.HuntMonster("towersilver", "Undead Guard", "Chef Ramskull's Skillet");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "GoldSkull Amulet":
                    Core.RegisterQuests(5023);
                    Core.EquipClass(ClassType.Farm);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("towergold", "Book Maggot", "Book Pages", 10);
                        Core.HuntMonster("towergold", "Vampire Bat", "Batwing Leather");
                        Core.HuntMonster("towergold", "Skullspider", "Skullspider Silk", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bonecastle Amulet":
                    Core.RegisterQuests(4993);
                    Core.EquipClass(ClassType.Farm);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Green Rat", "Gamey Rat Meat", 3);
                        Core.HuntMonster("bonecastle", "Undead Waiter", "Waiter's Notepad");
                        Core.HuntMonster("bonecastle", "Turtle", "Turtle's Eggs", 6);
                        Core.HuntMonster("bonecastle", "Ghoul", "Ghoul \"Vinegar\"", 6);
                        Core.HuntMonster("bonecastle", "Grateful Undead", "Spices", 2);
                        Core.HuntMonster("bonecastle", "The Butcher", "Bag of Bone Flour");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "DeathKnight Lord Greaves":
                case "DeathKnight Lord Hauberk":
                case "DeathKnight Lord Chest Plate":
                case "DeathKnight Lord Gauntlets":
                case "DeathKnight Lord Boots":
                    Bot.Drops.Add("DeathKnight Lord Gauntlets", "DeathKnight Lord Greaves", "DeathKnight Lord Chest Plate", "DeathKnight Lord Hauberk", "DeathKnight Lord Boots");
                    Core.EquipClass(ClassType.Solo);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Vaden", req.Name, isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Golden DeathKnight Lord Gauntlets":
                case "Golden DeathKnight Lord Chest Plate":
                case "Golden DeathKnight Lord Hauberk":
                case "Golden DeathKnight Lord Boots":
                case "Golden DeathKnight Lord Greaves":
                    Bot.Drops.Add("Golden DeathKnight Lord Gauntlets", "Golden DeathKnight Lord Greaves", "Golden DeathKnight Lord Chest Plate", "Golden DeathKnight Lord Hauberk", "Golden DeathKnight Lord Boots");
                    Core.EquipClass(ClassType.Farm);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towergold", "Yurrod the Gold", req.Name, isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Silver DeathKnight Lord Greaves":
                case "Silver DeathKnight Lord Chest Plate":
                case "Silver DeathKnight Lord Hauberk":
                case "Silver DeathKnight Lord Boots":
                case "Silver DeathKnight Lord Gauntlets":
                    Bot.Drops.Add("Silver DeathKnight Lord Gauntlets", "Silver DeathKnight Lord Greaves", "Silver DeathKnight Lord Chest Plate", "Silver DeathKnight Lord Hauberk", "Silver DeathKnight Lord Boots");
                    Core.EquipClass(ClassType.Farm);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towersilver", "Flester the Silver", req.Name, isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34717", "DeathKnight Lord", "Mode: [select] only\nShould the bot buy \"DeathKnight Lord\" ?", false),
        new Option<bool>("34726", "DeathKnight's Blade", "Mode: [select] only\nShould the bot buy \"DeathKnight's Blade\" ?", false),
        new Option<bool>("34729", "DeathKnight Helm", "Mode: [select] only\nShould the bot buy \"DeathKnight Helm\" ?", false),
        new Option<bool>("34724", "Silver DeathKnight Lord", "Mode: [select] only\nShould the bot buy \"Silver DeathKnight Lord\" ?", false),
        new Option<bool>("34727", "Silver DeathKnight's Blade", "Mode: [select] only\nShould the bot buy \"Silver DeathKnight's Blade\" ?", false),
        new Option<bool>("34730", "Silver DeathKnight Helm", "Mode: [select] only\nShould the bot buy \"Silver DeathKnight Helm\" ?", false),
        new Option<bool>("34725", "Golden DeathKnight Lord", "Mode: [select] only\nShould the bot buy \"Golden DeathKnight Lord\" ?", false),
        new Option<bool>("34728", "Golden DeathKnight's Blade", "Mode: [select] only\nShould the bot buy \"Golden DeathKnight's Blade\" ?", false),
        new Option<bool>("34731", "Golden DeathKnight Helm", "Mode: [select] only\nShould the bot buy \"Golden DeathKnight Helm\" ?", false),
        new Option<bool>("34744", "DeathKnight Lord Cape", "Mode: [select] only\nShould the bot buy \"DeathKnight Lord Cape\" ?", false),
    };
}
