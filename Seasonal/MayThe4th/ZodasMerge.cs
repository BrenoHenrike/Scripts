/*
name: Zoda's Merge
description: This bot will farm the items belonging to the selected mode for the Zoda's Merge [2133] in /zorbaspalace
tags: zodas, merge, zorbaspalace, augmented, light, rebellion, dark, fifth, chaos, imperium, aulorian, opened, , spear, mace, pistol, pistols, golden, armaments, lorosian, hunter, hunters, saber, sabers, blaster, handgun, handguns, armblade, armblades, set, pangalactic, double
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\MayThe4th\Zoda.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ZodasMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Zoda Z = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Light Blade of the Rebellion", "Zorblatt’s Pizza Slice", "Dark Blade of the Fifth", "Chaos Blade of the Imperium" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Z.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("zorbaspalace", 2133, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Light Blade of the Rebellion":
                    Core.FarmingLogger(req.Name, quant);
                    Farm.GoodREP();
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8648);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Z.AssembledSword();
                        Core.HuntMonster("greed", "Goregold", "Goregold Resisted", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Zorblatt’s Pizza Slice":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8651);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("zorbaspit", "Zorblatt", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dark Blade of the Fifth":
                    Core.FarmingLogger(req.Name, quant);
                    Farm.EvilREP();
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8649);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Z.AssembledSword();
                        Core.HuntMonster("murdermoon", "Fifth Sepulchure", "Fifth Sepulchure Defeated", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Chaos Blade of the Imperium":
                    Core.FarmingLogger(req.Name, quant);
                    Farm.ChaosREP();
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8650);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Z.AssembledSword();
                        Core.HuntMonster("ledgermayne", "Ledgermayne", "Ledgermayne Defeated", isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70076", "Augmented Light Blade of the Rebellion", "Mode: [select] only\nShould the bot buy \"Augmented Light Blade of the Rebellion\" ?", false),
        new Option<bool>("70074", "Augmented Dark Blade of the Fifth", "Mode: [select] only\nShould the bot buy \"Augmented Dark Blade of the Fifth\" ?", false),
        new Option<bool>("70078", "Augmented Chaos Blade of the Imperium", "Mode: [select] only\nShould the bot buy \"Augmented Chaos Blade of the Imperium\" ?", false),
        new Option<bool>("69863", "Aulorian", "Mode: [select] only\nShould the bot buy \"Aulorian\" ?", false),
        new Option<bool>("69864", "Aulorian Helm", "Mode: [select] only\nShould the bot buy \"Aulorian Helm\" ?", false),
        new Option<bool>("69865", "Aulorian Opened Helm", "Mode: [select] only\nShould the bot buy \"Aulorian Opened Helm\" ?", false),
        new Option<bool>("69866", "Aulorian Sword + Cape", "Mode: [select] only\nShould the bot buy \"Aulorian Sword + Cape\" ?", false),
        new Option<bool>("69867", "Aulorian Cape", "Mode: [select] only\nShould the bot buy \"Aulorian Cape\" ?", false),
        new Option<bool>("69868", "Aulorian Blade", "Mode: [select] only\nShould the bot buy \"Aulorian Blade\" ?", false),
        new Option<bool>("69869", "Aulorian Spear", "Mode: [select] only\nShould the bot buy \"Aulorian Spear\" ?", false),
        new Option<bool>("69870", "Aulorian Mace", "Mode: [select] only\nShould the bot buy \"Aulorian Mace\" ?", false),
        new Option<bool>("69871", "Aulorian Pistol", "Mode: [select] only\nShould the bot buy \"Aulorian Pistol\" ?", false),
        new Option<bool>("69872", "Aulorian Pistols", "Mode: [select] only\nShould the bot buy \"Aulorian Pistols\" ?", false),
        new Option<bool>("69873", "Aulorian Golden Gun", "Mode: [select] only\nShould the bot buy \"Aulorian Golden Gun\" ?", false),
        new Option<bool>("69874", "Aulorian Golden Guns", "Mode: [select] only\nShould the bot buy \"Aulorian Golden Guns\" ?", false),
        new Option<bool>("69875", "Aulorian Guns", "Mode: [select] only\nShould the bot buy \"Aulorian Guns\" ?", false),
        new Option<bool>("69876", "Aulorian Armaments", "Mode: [select] only\nShould the bot buy \"Aulorian Armaments\" ?", false),
        new Option<bool>("69885", "Lorosian Hunter", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter\" ?", false),
        new Option<bool>("69886", "Lorosian Hunter Helm", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Helm\" ?", false),
        new Option<bool>("69887", "Lorosian Hunter Hat + Mask", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Hat + Mask\" ?", false),
        new Option<bool>("69888", "Lorosian Hunter's Saber Cape", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter's Saber Cape\" ?", false),
        new Option<bool>("69889", "Lorosian Hunter Saber", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Saber\" ?", false),
        new Option<bool>("69890", "Lorosian Hunter Sabers", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Sabers\" ?", false),
        new Option<bool>("69891", "Lorosian Hunter Blaster", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Blaster\" ?", false),
        new Option<bool>("69892", "Lorosian Hunter Handgun", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Handgun\" ?", false),
        new Option<bool>("69893", "Lorosian Hunter Handguns", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Handguns\" ?", false),
        new Option<bool>("69894", "Lorosian Hunter ArmBlade", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter ArmBlade\" ?", false),
        new Option<bool>("69895", "Lorosian Hunter ArmBlades", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter ArmBlades\" ?", false),
        new Option<bool>("69896", "Lorosian Hunter Gun Set", "Mode: [select] only\nShould the bot buy \"Lorosian Hunter Gun Set\" ?", false),
        new Option<bool>("70044", "Pan-Galactic Double Saber", "Mode: [select] only\nShould the bot buy \"Pan-Galactic Double Saber\" ?", false),
        new Option<bool>("70045", "Pan-Galactic Double Sabers", "Mode: [select] only\nShould the bot buy \"Pan-Galactic Double Sabers\" ?", false),
        new Option<bool>("70046", "Pan-Galactic Saber", "Mode: [select] only\nShould the bot buy \"Pan-Galactic Saber\" ?", false),
        new Option<bool>("70047", "Pan-Galactic Sabers", "Mode: [select] only\nShould the bot buy \"Pan-Galactic Sabers\" ?", false),
    };
}
