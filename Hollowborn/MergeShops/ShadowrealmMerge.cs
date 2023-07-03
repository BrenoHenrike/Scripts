/*
name: Shadowrealm Merge
description: This bot will farm the items belonging to the selected mode for the Shadowrealm Merge [1889] in /shadowrealm
tags: shadowrealm, merge, shadowrealm, hollowborn, thunderlord, visor, lightning, lightnings, vampire, flying, skulls, scythe, chained, reaper, guard, fiend, blind, reapers, , horns, ponytail, katana, kamas, kama, minion, sheathed, dragonslayer, dragonslayers, beard, scarf, vine, rogue, thorn, assassin, assassins, wreath, thorns, guardian, whip, agony, poison, claws, ranger, rangers, quiver, enchanted, bow, battlemage, sigil, hammer, hammers, armaments
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/AgeofRuin/CoreAOR.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Other/MergeShops/YulgarsUndineMerge.cs
//cs_include Scripts/Hollowborn/MergeShops/DawnFortressMerge.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs
//
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowrealmMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private YulgarsUndineMerge YUM = new();
    private DawnFortressMerge DFM = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Hollow Soul", "Bone Dust", "Death's Oversight", "Death's Scythe", "Unmoulded Fiend Essence", "Poisonous Rogue", "Nightshade Thorn Assasasin", "Elven Assassin's Locks", "Elven Assassin's Locks + Scarf", "Elven Assassin's Hair", "Elven Assassin’s Scarf", "Poisonous Thorn Wreath", "Nightshade Assassin Guardian", "Reversed Blade of Thorns", "Reversed Daggers of Thorns", "Envenomed Whip of Agony", "Envenomed Gauntlet", "Gold Voucher 25k", "Dawn Vindicator Archer", "Vindicator Archer's Hat + Locks", "Gilded Scout's Quiver", "Bright Bow of the Dawn", "Vindicator Scout's Bow", "Dawn Vindicator General", "Vindicator General's Hood", "Vindicator General's Hood + Locks", "Blessed Rune of Vindication", "Blessed Shield of Vindication", "Blessed Hammer of the Dawn", "Blessed Hammers of the Dawn", "Battlegear of Vindication" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowrealm", 1889, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hollow Soul":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7553, 7555);
                        Core.KillMonster("shadowrealm", "r2", "Left", "Gargrowl", "Darkseed", 8, log: false);
                        Core.KillMonster("shadowrealm", "r2", "Left", "Shadow Guardian", "Shadow Medallion", 5, log: false);
                        Core.EnsureComplete(7553);
                        Core.EnsureComplete(7555);
                    }
                    break;

                case "Bone Dust":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Farm.BattleUnderB(req.Name, quant);
                    break;

                case "Death's Oversight":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.KillMonster("shadowattack", "Boss", "Left", "Death", req.Name, quant, false);
                    break;

                case "Death's Scythe":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("shadowattack", "Death", req.Name, isTemp: false);
                    break;

                case "Unmoulded Fiend Essence":
                    Adv.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence", quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Elven Assassin's Locks + Scarf":
                case "Elven Assassin's Locks":
                case "Elven Assassin's Hair":
                case "Elven Assassin’s Scarf":
                case "Poisonous Thorn Wreath":
                case "Nightshade Assassin Guardian":
                case "Reversed Blade of Thorns":
                case "Reversed Daggers of Thorns":
                case "Envenomed Whip of Agony":
                case "Envenomed Gauntlet":
                case "Poisonous Rogue":
                case "Nightshade Thorn Assasasin":
                    YUM.BuyAllMerge(req.Name);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Gold Voucher 25k":
                    Adv.BuyItem("sunlightzone", 2288, 57304, quant, 7782);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Vindicator Archer's Hat + Locks":
                case "Dawn Vindicator Archer":
                case "Dawn Vindicator General":
                case "Bright Bow of the Dawn":
                case "Vindicator General's Hood":
                case "Vindicator General's Hood + Locks":
                case "Blessed Shield of Vindication":
                case "Blessed Hammers of the Dawn":
                case "Blessed Hammer of the Dawn":
                    DFM.BuyAllMerge(req.Name);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Gilded Scout's Quiver":
                case "Vindicator Scout's Bow":
                    Core.HuntMonster("neofortress", "Vindicator Recruit", req.Name, isTemp: false);

                    break;

                case "Blessed Rune of Vindication":
                case "Battlegear of Vindication":
                    Core.HuntMonster("neofortress", "Vindicator General", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54867", "Hollowborn Thunderlord", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord\" ?", false),
        new Option<bool>("54868", "Hollowborn Thunderlord Visor", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Visor\" ?", false),
        new Option<bool>("54869", "Hollowborn Thunderlord Visage", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Visage\" ?", false),
        new Option<bool>("54870", "Hollowborn Thunderlord Lightning Cape", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Lightning Cape\" ?", false),
        new Option<bool>("54871", "Hollowborn Thunderlord Lightning", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Lightning\" ?", false),
        new Option<bool>("54872", "Hollowborn Thunderlord Lightnings", "Mode: [select] only\nShould the bot buy \"Hollowborn Thunderlord Lightnings\" ?", false),
        new Option<bool>("54874", "Hollowborn Vampire", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire\" ?", false),
        new Option<bool>("54875", "Hollowborn Vampire Hair", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Hair\" ?", false),
        new Option<bool>("54876", "Hollowborn Vampire Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Mask\" ?", false),
        new Option<bool>("54877", "Hollowborn Vampire Flying Skulls", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Flying Skulls\" ?", false),
        new Option<bool>("54878", "Hollowborn Vampire Scythe", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Scythe\" ?", false),
        new Option<bool>("61766", "Hollowborn Chained Reaper", "Mode: [select] only\nShould the bot buy \"Hollowborn Chained Reaper\" ?", false),
        new Option<bool>("61767", "Hollowborn Chained Guard", "Mode: [select] only\nShould the bot buy \"Hollowborn Chained Guard\" ?", false),
        new Option<bool>("61768", "Hollowborn Fiend Reaper", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Reaper\" ?", false),
        new Option<bool>("61769", "Hollowborn Fiend Guard", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Guard\" ?", false),
        new Option<bool>("61770", "Hollowborn Fiend Hair", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Hair\" ?", false),
        new Option<bool>("61771", "Blind Hollowborn Fiend Hair", "Mode: [select] only\nShould the bot buy \"Blind Hollowborn Fiend Hair\" ?", false),
        new Option<bool>("61772", "Hollowborn Fiend Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Mask\" ?", false),
        new Option<bool>("61773", "Hollowborn Reaper's Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Hood\" ?", false),
        new Option<bool>("61774", "Hollowborn Reaper's Hood + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Hood + Locks\" ?", false),
        new Option<bool>("61775", "Hollowborn Reaper's Hooded Mask", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Hooded Mask\" ?", false),
        new Option<bool>("61776", "Hollowborn Fiend Reaper's Horns", "Mode: [select] only\nShould the bot buy \"Hollowborn Fiend Reaper's Horns\" ?", false),
        new Option<bool>("61777", "Hollowborn Reaper's Ponytail", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Ponytail\" ?", false),
        new Option<bool>("61778", "Blind Hollowborn Reaper's Ponytail", "Mode: [select] only\nShould the bot buy \"Blind Hollowborn Reaper's Ponytail\" ?", false),
        new Option<bool>("61779", "Hollowborn Reaper's Mask + Ponytail", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Mask + Ponytail\" ?", false),
        new Option<bool>("61780", "Hollowborn Reaper's Katana", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Katana\" ?", false),
        new Option<bool>("61781", "Hollowborn Reaper's Scythe", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Scythe\" ?", false),
        new Option<bool>("61782", "Hollowborn Reaper's Daggers", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Daggers\" ?", false),
        new Option<bool>("61783", "Hollowborn Reaper's Kamas", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Kamas\" ?", false),
        new Option<bool>("61784", "Hollowborn Reaper's Kama", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Kama\" ?", false),
        new Option<bool>("61785", "Hollowborn Reaper's Minion", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Minion\" ?", false),
        new Option<bool>("61786", "Hollowborn Reaper's Sheathed Katana", "Mode: [select] only\nShould the bot buy \"Hollowborn Reaper's Sheathed Katana\" ?", false),
        new Option<bool>("55355", "Hollowborn DragonSlayer", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer\" ?", false),
        new Option<bool>("55356", "Hollowborn DragonSlayer's Helm", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Helm\" ?", false),
        new Option<bool>("55357", "Hollowborn DragonSlayer's Beard", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Beard\" ?", false),
        new Option<bool>("55358", "Hollowborn DragonSlayer's Scarf", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Scarf\" ?", false),
        new Option<bool>("55359", "Hollowborn DragonSlayer's Cape", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Cape\" ?", false),
        new Option<bool>("55360", "Hollowborn DragonSlayer's Blade", "Mode: [select] only\nShould the bot buy \"Hollowborn DragonSlayer's Blade\" ?", false),
        new Option<bool>("78297", "Hollowborn Vine Rogue", "Mode: [select] only\nShould the bot buy \"Hollowborn Vine Rogue\" ?", false),
        new Option<bool>("78298", "Hollowborn Thorn Assassin", "Mode: [select] only\nShould the bot buy \"Hollowborn Thorn Assassin\" ?", false),
        new Option<bool>("78299", "Hollowborn Assassin's Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Assassin's Locks\" ?", false),
        new Option<bool>("78300", "Hollowborn Assassin's Locks + Scarf", "Mode: [select] only\nShould the bot buy \"Hollowborn Assassin's Locks + Scarf\" ?", false),
        new Option<bool>("78301", "Hollowborn Assassin's Hair", "Mode: [select] only\nShould the bot buy \"Hollowborn Assassin's Hair\" ?", false),
        new Option<bool>("78302", "Hollowborn Assassin's Hair + Scarf", "Mode: [select] only\nShould the bot buy \"Hollowborn Assassin's Hair + Scarf\" ?", false),
        new Option<bool>("78303", "Hollowborn Wreath of Thorns", "Mode: [select] only\nShould the bot buy \"Hollowborn Wreath of Thorns\" ?", false),
        new Option<bool>("78304", "Hollowborn Thorn Guardian", "Mode: [select] only\nShould the bot buy \"Hollowborn Thorn Guardian\" ?", false),
        new Option<bool>("78305", "Hollowborn Blade of Thorns", "Mode: [select] only\nShould the bot buy \"Hollowborn Blade of Thorns\" ?", false),
        new Option<bool>("78306", "Hollowborn Daggers of Thorns", "Mode: [select] only\nShould the bot buy \"Hollowborn Daggers of Thorns\" ?", false),
        new Option<bool>("78307", "Hollowborn Whip of Agony", "Mode: [select] only\nShould the bot buy \"Hollowborn Whip of Agony\" ?", false),
        new Option<bool>("78308", "Hollowborn Poison Claws", "Mode: [select] only\nShould the bot buy \"Hollowborn Poison Claws\" ?", false),
        new Option<bool>("67444", "Hollowborn Ranger", "Mode: [select] only\nShould the bot buy \"Hollowborn Ranger\" ?", false),
        new Option<bool>("67445", "Hollowborn Ranger's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Ranger's Hat + Locks\" ?", false),
        new Option<bool>("67446", "Hollowborn Ranger's Hat", "Mode: [select] only\nShould the bot buy \"Hollowborn Ranger's Hat\" ?", false),
        new Option<bool>("67447", "Hollowborn Ranger Quiver", "Mode: [select] only\nShould the bot buy \"Hollowborn Ranger Quiver\" ?", false),
        new Option<bool>("67448", "Enchanted Hollowborn Bow", "Mode: [select] only\nShould the bot buy \"Enchanted Hollowborn Bow\" ?", false),
        new Option<bool>("67449", "Hollowborn Ranger Bow", "Mode: [select] only\nShould the bot buy \"Hollowborn Ranger Bow\" ?", false),
        new Option<bool>("67450", "Hollowborn Battlemage", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage\" ?", false),
        new Option<bool>("67451", "Hollowborn Battlemage Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage Hood\" ?", false),
        new Option<bool>("67452", "Hollowborn Battlemage Hood + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage Hood + Locks\" ?", false),
        new Option<bool>("67453", "Hollowborn Battlemage Rune", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage Rune\" ?", false),
        new Option<bool>("67454", "Hollowborn Battlemage Sigil", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage Sigil\" ?", false),
        new Option<bool>("67455", "Hollowborn Battlemage Hammer", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage Hammer\" ?", false),
        new Option<bool>("67456", "Hollowborn Battlemage Hammers", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage Hammers\" ?", false),
        new Option<bool>("67457", "Hollowborn Battlemage Armaments", "Mode: [select] only\nShould the bot buy \"Hollowborn Battlemage Armaments\" ?", false),
    };
}
