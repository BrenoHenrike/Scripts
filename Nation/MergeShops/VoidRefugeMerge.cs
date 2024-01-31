/*
name: Void Refuge Merge
description: This bot will farm the items belonging to the selected mode for the Void Refuge Merge [2408] in /voidrefuge
tags: void, refuge, merge, voidrefuge, envenomed, edge, nulgath, enchanted, carnage, evolved, crest, blood, spines, bloodletter, katana, katanas, bone, quaker, betrayal, soul, corroding, visionary, heart, render, golden, redeemer, poison, drinker, guillotine, fiend, carver, unstable
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\Nation\VoidRefuge.cs
//cs_include Scripts/Nation\CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class VoidRefugeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private VoidRefuge VR = new();
    private CoreNation Nation = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Venomous Fang Blade", "Unidentified 13", "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath", "Unidentified 23", "Totem of Nulgath", "Blood Gem of the Archfiend", "Evolved Carnage of Nulgath", "Fiendish Remains", "Voucher of Nulgath (non-mem)", "Evolved Carnage Helm", "Evolved Carnage Crest", "Blood Void Spines", "Blood Void Spikes", "Gem of Nulgath", "Bloodletter Katana", "Bloodletter Katanas", "1st Betrayal Blade of Nulgath", "2nd Betrayal Blade of Nulgath", "3rd Betrayal Blade of Nulgath", "4th Betrayal Blade of Nulgath", "5th Betrayal Blade of Nulgath", "6th Betrayal Blade of Nulgath", "7th Betrayal Blade of Nulgath", "8th Betrayal Blade of Nulgath", "Unmoulded Fiend Essence", "Gold Voucher 25k" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        VR.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("voidrefuge", 2408, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Venomous Fang Blade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("tercessuinotlim", "Ninja Spy", "Spy’s Info", isTemp: false, log: false);
                    Core.HuntMonster("citadel", "Inquisitor Captain", "Captain’s Info", isTemp: false, log: false);
                    Core.HuntMonster("lairattack", "Flame Dragon General", "Broken Fang Blade", isTemp: false, log: false);
                    Core.GetMapItem(12571, map: "museum");
                    break;

                case "Unidentified 13":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmUni13(quant);
                    break;

                case "Tainted Gem":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmTaintedGem(quant);
                    break;

                case "Dark Crystal Shard":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Diamond of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Unidentified 23":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.TheAssistant("Unidentified 23", quant);
                    break;

                case "Totem of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmTotemofNulgath(quant);
                    break;

                case "Blood Gem of the Archfiend":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmBloodGem(quant);
                    break;

                case "Evolved Carnage of Nulgath":
                case "Evolved Carnage Helm":
                case "Evolved Carnage Crest":
                case "Blood Void Spines":
                case "Blood Void Spikes":
                case "Bloodletter Katana":
                case "Bloodletter Katanas":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("voidrefuge", "Carnage", req.Name, quant, false, false);
                    break;

                case "Fiendish Remains":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9532);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("voidrefuge", "Paladin Ascendant", "Sussurating Helm", 3, log: false);
                        Core.HuntMonster("voidrefuge", "Nation Outrider", "Scarred Coin", 8, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("voidrefuge", "Carnage", "Carnage's Ichor", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmVoucher(false);
                    break;

                case "Gem of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmGemofNulgath(quant);
                    break;

                case "1st Betrayal Blade of Nulgath":
                case "2nd Betrayal Blade of Nulgath":
                case "3rd Betrayal Blade of Nulgath":
                case "4th Betrayal Blade of Nulgath":
                case "5th Betrayal Blade of Nulgath":
                case "6th Betrayal Blade of Nulgath":
                case "7th Betrayal Blade of Nulgath":
                case "8th Betrayal Blade of Nulgath":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.KisstheVoid(0, req.Name);
                    break;

                case "Unmoulded Fiend Essence":
                    Core.FarmingLogger(req.Name, quant);
                    Adv.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79256", "Envenomed Edge of Nulgath", "Mode: [select] only\nShould the bot buy \"Envenomed Edge of Nulgath\" ?", false),
        new Option<bool>("83090", "Enchanted Carnage Void of Nulgath", "Mode: [select] only\nShould the bot buy \"Enchanted Carnage Void of Nulgath\" ?", false),
        new Option<bool>("83091", "Enchanted Evolved Carnage Helm", "Mode: [select] only\nShould the bot buy \"Enchanted Evolved Carnage Helm\" ?", false),
        new Option<bool>("83092", "Enchanted Evolved Carnage Crest", "Mode: [select] only\nShould the bot buy \"Enchanted Evolved Carnage Crest\" ?", false),
        new Option<bool>("83093", "Enchanted Blood Void Spines", "Mode: [select] only\nShould the bot buy \"Enchanted Blood Void Spines\" ?", false),
        new Option<bool>("83094", "Enchanted Blood Void Spikes", "Mode: [select] only\nShould the bot buy \"Enchanted Blood Void Spikes\" ?", false),
        new Option<bool>("83095", "Enchanted Bloodletter Katana", "Mode: [select] only\nShould the bot buy \"Enchanted Bloodletter Katana\" ?", false),
        new Option<bool>("83096", "Enchanted Bloodletter Katanas", "Mode: [select] only\nShould the bot buy \"Enchanted Bloodletter Katanas\" ?", false),
        new Option<bool>("83141", "Bone Quaker Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Bone Quaker Betrayal Blade\" ?", false),
        new Option<bool>("83142", "Bone Quaker Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Bone Quaker Betrayal Blades\" ?", false),
        new Option<bool>("83143", "Soul Corroding Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Soul Corroding Betrayal Blade\" ?", false),
        new Option<bool>("83144", "Soul Corroding Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Soul Corroding Betrayal Blades\" ?", false),
        new Option<bool>("83145", "Visionary Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Visionary Betrayal Blade\" ?", false),
        new Option<bool>("83146", "Visionary Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Visionary Betrayal Blade\" ?", false),
        new Option<bool>("83147", "Heart Render Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Heart Render Betrayal Blade\" ?", false),
        new Option<bool>("83148", "Heart Render Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Heart Render Betrayal Blades\" ?", false),
        new Option<bool>("83149", "Golden Redeemer Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Golden Redeemer Betrayal Blade\" ?", false),
        new Option<bool>("83150", "Golden Redeemer Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Golden Redeemer Betrayal Blades\" ?", false),
        new Option<bool>("83151", "Poison Drinker Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Poison Drinker Betrayal Blade\" ?", false),
        new Option<bool>("83152", "Poison Drinker Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Poison Drinker Betrayal Blades\" ?", false),
        new Option<bool>("83153", "Guillotine Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Guillotine Betrayal Blade\" ?", false),
        new Option<bool>("83154", "Guillotine Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Guillotine Betrayal Blades\" ?", false),
        new Option<bool>("83155", "Fiend Carver Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Fiend Carver Betrayal Blade\" ?", false),
        new Option<bool>("83156", "Fiend Carver Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Fiend Carver Betrayal Blades\" ?", false),
        new Option<bool>("83139", "Unstable Betrayal Blade", "Mode: [select] only\nShould the bot buy \"Unstable Betrayal Blade\" ?", false),
        new Option<bool>("83140", "Unstable Betrayal Blades", "Mode: [select] only\nShould the bot buy \"Unstable Betrayal Blades\" ?", false),
        new Option<bool>("82878", "Void Betrayal Knight", "Mode: [select] only\nShould the bot buy \"Void Betrayal Knight\" ?", false),
        new Option<bool>("82879", "Void Betrayer Crest", "Mode: [select] only\nShould the bot buy \"Void Betrayer Crest\" ?", false),
        new Option<bool>("82880", "Void Betrayer Guard", "Mode: [select] only\nShould the bot buy \"Void Betrayer Guard\" ?", false),
        new Option<bool>("82881", "Void Betrayer Horns", "Mode: [select] only\nShould the bot buy \"Void Betrayer Horns\" ?", false),
        new Option<bool>("82882", "Void Betrayer Cloak", "Mode: [select] only\nShould the bot buy \"Void Betrayer Cloak\" ?", false),
        new Option<bool>("82883", "Void Betrayer Spined Cloak", "Mode: [select] only\nShould the bot buy \"Void Betrayer Spined Cloak\" ?", false),
        new Option<bool>("82885", "Void Betrayer Sword", "Mode: [select] only\nShould the bot buy \"Void Betrayer Sword\" ?", false),
        new Option<bool>("83323", "Void Betrayer Swords", "Mode: [select] only\nShould the bot buy \"Void Betrayer Swords\" ?", false),
        new Option<bool>("82886", "Void Betrayer Dagger", "Mode: [select] only\nShould the bot buy \"Void Betrayer Dagger\" ?", false),
        new Option<bool>("82887", "Void Betrayer Daggers", "Mode: [select] only\nShould the bot buy \"Void Betrayer Daggers\" ?", false),
        new Option<bool>("82888", "Void Betrayer Reaver", "Mode: [select] only\nShould the bot buy \"Void Betrayer Reaver\" ?", false),
        new Option<bool>("82889", "Void Betrayer Reavers", "Mode: [select] only\nShould the bot buy \"Void Betrayer Reavers\" ?", false),
    };
}
