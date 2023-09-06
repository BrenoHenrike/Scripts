/*
name: Starsinc Merge
description: This bot will farm the items belonging to the selected mode for the Starsinc Merge [678] in /starsinc
tags: starsinc, merge, starsinc, finals, base, skull, horned, galactic, stars, primes, exo, wings, legendary, prime, dominus, visor
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class StarsincMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public StarSinc Star = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Star Scrap", "Brimstone Scrap", "Star Fragment", "Taker and Giver Stone", "Prime's Respect" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Star.StarSincQuests();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("starsinc", 678, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Star Scrap Metal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4289);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Undead Space Marine|Undead Space Warrior", "Golden Spork of Justice");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Brimstone Scrap":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("starsinc", "Infernal Imp", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Star Fragment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4413);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("starsinc", "Living Star", "Living Star Defeated", 30, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Taker and Giver Stone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(4414);
                        Farm.BattleUnderB("Bone Dust", 15);
                        Farm.BludrutBrawlBoss(quant: 5);
                        Core.HuntMonster("starsinc", "Living Star", "Living Star Essence", 100, false);
                        Core.EnsureComplete(4414);

                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Prime's Respect":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(4415);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("starsinc", "Empowered Prime", "Empowered Primed Defeated", 10, false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30563", "Final's Base Armor", "Mode: [select] only\nShould the bot buy \"Final's Base Armor\" ?", false),
        new Option<bool>("30565", "Final's Skull Helm", "Mode: [select] only\nShould the bot buy \"Final's Skull Helm\" ?", false),
        new Option<bool>("30564", "Final's Armor", "Mode: [select] only\nShould the bot buy \"Final's Armor\" ?", false),
        new Option<bool>("30566", "Final's Horned Skull", "Mode: [select] only\nShould the bot buy \"Final's Horned Skull\" ?", false),
        new Option<bool>("30466", "Final's Blade", "Mode: [select] only\nShould the bot buy \"Final's Blade\" ?", false),
        new Option<bool>("30637", "Galactic Blade of Stars", "Mode: [select] only\nShould the bot buy \"Galactic Blade of Stars\" ?", false),
        new Option<bool>("30649", "Prime's Exo Wings", "Mode: [select] only\nShould the bot buy \"Prime's Exo Wings\" ?", false),
        new Option<bool>("30470", "Prime's Legendary Wings", "Mode: [select] only\nShould the bot buy \"Prime's Legendary Wings\" ?", false),
        new Option<bool>("30656", "Prime Dominus", "Mode: [select] only\nShould the bot buy \"Prime Dominus\" ?", false),
        new Option<bool>("30657", "Legendary Prime Dominus", "Mode: [select] only\nShould the bot buy \"Legendary Prime Dominus\" ?", false),
        new Option<bool>("30658", "Legendary Prime Dominus Visor", "Mode: [select] only\nShould the bot buy \"Legendary Prime Dominus Visor\" ?", false),
        new Option<bool>("30659", "Prime Dominus Visor", "Mode: [select] only\nShould the bot buy \"Prime Dominus Visor\" ?", false),
    };
}
