/*
name: Adam1a1 Merge
description: Merge shop for Adam1a1 in battleunderb
tags: merge, mergeshop
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/Banished.cs
//cs_include Scripts/Story/Adam1a1Quests.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class Adam1a1Merge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public Banished Banished = new();
    public Adam1a1Quest Adam1a1Quest = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Diabolical Ectomancer", "Fresh Ectoplasm", "IOU Slip", "EctoBlade", "Ectoplasmic Chains", "Bongo Cart Pet", "Reho's Golden Sword Hilt" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Banished.HikarisQuests();
        Adam1a1Quest.Storyline();
        Farm.BladeofAweREP(10, false);
        Adv.StartBuyAllMerge("battleunderb", 1990, findIngredients, buyOnlyThis, buyMode: buyMode);
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

                case "Diabolical Ectomancer":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7880);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("banished", "Desterrat Moya", "Infected Tentacle");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fresh Ectoplasm":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8009);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("vendorbooths", "Caffeine Imp", "Coffee Beans", 10);
                        Core.HuntMonster("djinn", "Lamia", "Tasty Poison", 10);
                        Core.HuntMonster("charredpath", "Toxic Wisteria", "Necessary Antidote");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "IOU Slip":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8009);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("vendorbooths", "Caffeine Imp", "Coffee Beans", 10);
                        Core.HuntMonster("djinn", "Lamia", "Tasty Poison", 10);
                        Core.HuntMonster("charredpath", "Toxic Wisteria", "Necessary Antidote");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "EctoBlade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shadowfallwar", "Skeletal Fire Mage", "EctoBlade", isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ectoplasmic Chains":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8010);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("temple", "Doomwood Ectomancer", "Refined Ectoplasm", 10);
                        Core.HuntMonster("ectocave", "Ektorax", "Ektorax's Ectoplasm");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bongo Cart Pet":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.AddDrop("Love Token");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(8013);
                        // Red Rose
                        if (!Core.CheckInventory("Red Rose", toInv: false))
                        {
                            while (!Bot.ShouldExit && !Core.CheckInventory(21567, 10))
                                Core.HuntMonster("battlewedding", "Silver Knight", isTemp: false);
                            Adv.BuyItem("battlewedding", 788, "Red Rose");
                        }
                        // Bangin' Bongo Cat Hair
                        if (!Core.CheckInventory("Bangin' Bongo Cat Hair"))
                            Adv.BuyItem("battleontown", 907, "Bangin' Bongo Cat Hair");
                        // Pink Rose
                        if (!Core.CheckInventory("Pink Rose"))
                        {
                            Core.RegisterQuests(1489);
                            while (!Bot.ShouldExit && !Core.CheckInventory("Magenta Dye", 35))
                            {
                                //Flowers for the Pink Gal 1489
                                Core.HuntMonster("Sandsea", "Cactus Creeper", "Fandango Flower", 5);
                                Core.KillMonster("wanders", "r2", "Down", "Lotus Spider", "Lotus Flower", 4);
                            }

                            Adv.BuyItem("tower", 347, "Pink Rose");
                            while (!Bot.ShouldExit && !Core.CheckInventory(21567, 10))
                                Core.HuntMonster("battlewedding", "Silver Knight", isTemp: false);
                            Adv.BuyItem("battlewedding", 788, "Red Rose");
                        }

                        // Stray Ectoplasm
                        if (!Core.CheckInventory("Stray Ectoplasm"))
                        {
                            while (!Bot.ShouldExit && !Core.CheckInventory("Fresh Ectoplasm", 15))
                            {
                                Core.EnsureAccept(8009);
                                Core.HuntMonster("vendorbooths", "Caffeine Imp", "Coffee Beans", 10);
                                Core.HuntMonster("djinn", "Lamia", "Tasty Poison", 10);
                                Core.HuntMonster("charredpath", "Toxic Wisteria", "Necessary Antidote");
                                Core.EnsureComplete(8009);
                                Bot.Wait.ForPickup("Fresh Ectoplasm");
                            }
                            Adv.BuyItem("battleunderb", 1990, "Stray Ectoplasm");
                        }
                        Core.EnsureComplete(8013);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Reho's Golden Sword Hilt":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8011);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("extinction", "Slimed Drone", "Iron II.0", 4, isTemp: false);
                        Core.HuntMonster("doomwood", "Doomwood Treeant", "Wood", 10);
                        Core.HuntMonster("crashsite", "Dwakel Blaster", "Big Iron Bolts", 10);
                        Core.HuntMonster("portalmaze", "Time Wraith", "Piece of Cake", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Awethur's Accoutrements":
                    Core.BuyItem("museum", 631, "Awethur's Accoutrements");
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("60473", "Ecto Enforcer", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer\" ?", false),
        new Option<bool>("60481", "Ecto Enforcer Axe", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Axe\" ?", false),
        new Option<bool>("60480", "Ecto Enforcer Staff", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Staff\" ?", false),
        new Option<bool>("60479", "Ecto Enforcer Sword", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Sword\" ?", false),
        new Option<bool>("60474", "Ecto Enforcer Helm Jedna", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Helm Jedna\" ?", false),
        new Option<bool>("60475", "Ecto Enforcer Helm Dva", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Helm Dva\" ?", false),
        new Option<bool>("60476", "Ecto Enforcer Helm Tri", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Helm Tri\" ?", false),
        new Option<bool>("60477", "Ecto Enforcer Cape", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Cape\" ?", false),
        new Option<bool>("60478", "Ecto Enforcer Cape + Weapons", "Mode: [select] only\nShould the bot buy \"Ecto Enforcer Cape + Weapons\" ?", false),
        new Option<bool>("60492", "Ecto Blade of Insanity", "Mode: [select] only\nShould the bot buy \"Ecto Blade of Insanity\" ?", false),
        new Option<bool>("60500", "Battle Bongo Cart Pet", "Mode: [select] only\nShould the bot buy \"Battle Bongo Cart Pet\" ?", false),
        new Option<bool>("60491", "Reho's Sword", "Mode: [select] only\nShould the bot buy \"Reho's Sword\" ?", false),
        new Option<bool>("60490", "Reho's Golden Sword", "Mode: [select] only\nShould the bot buy \"Reho's Golden Sword\" ?", false),
    };
}
