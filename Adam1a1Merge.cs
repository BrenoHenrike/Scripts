using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
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

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Diabolical Ectomancer", "Fresh Ectoplasm", "IOU Slip", "EctoBlade", "Ectoplasmic Chains", "Bongo Cart Pet", "Reho's Golden Sword Hilt"});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
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
                        Core.HuntMonster("banished", "r14", "Left", "*", "Desterrat Moya", "Infected Tentacle", 1, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fresh Ectoplasm":
                case "IOU Slip":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8009);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("djinn", "Lamia", "Tasty Poison", 10, log: false);
                        Core.HuntMonster("vendorbooths", "r10", "Left", "*", "Caffeine Imp", "Coffee Beans", 10, log: false);
                        Core.HuntMonster("charredpath", "r7", "Right", "*", "Toxic Wisteria", "Necessary Antidote", 1, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "EctoBlade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shadowfallwar", "garden1", "bottom", "*", "Skeletal Fire Mage", "Ectoblade", 1, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fresh Ectoplasm":
                case "IOU Slip":
                case "Ectoplasmic Chains":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8010);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("doomwood", "r2", "Right", "*", "Doomwood Ectomancer", "Refined Ectoplasm", 10, log: false);
                        Core.HuntMonster("ectocave", "Boss", "Left", "*", "Ektorax", "Ektorax's Ectoplasm", 1, log: false);
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
                        Core.HuntMonster("extinction", "r1", "Left", "Control Panel", "Iron II.0" 4, log: false);
                        Core.HuntMonster("doomwood", "r5", "Bottom", "Doomwood Treeant", "Wood", 10, log: false);
                        Core.HuntMonster("crashsite", "Dwakel Blaster", "Big Iron Bolts", 10, log: false);
                        Core.findIngredients("portalmaze", "r3", "Left", "Piece of Cake", 1, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
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
