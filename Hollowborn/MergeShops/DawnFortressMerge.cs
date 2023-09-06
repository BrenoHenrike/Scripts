/*
name: Dawn Fortress Merge
description: This bot will farm the items belonging to the selected mode for the Dawn Fortress Merge [2300] in /neofortress
tags: dawn, fortress, merge, neofortress, vindicator, archer, scout, archers, , scouts, bright, bow, general, captain, generals, captains, blessed, shield, vindication, hammer, hammers, battlegear, hollowborn, ranger, rangers, quiver, enchanted, battlemage, sigil, armaments
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DawnFortressMerge
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
        Core.BankingBlackList.AddRange(new[] { "Grace Orb", "Vindicator Badge", "Vindicator Soldier's Hair", "Vindicator Scout's Bow", "Blessed Sigil of Vindication", "Hammer of Vindication", "Hammers of Vindication", "Hollow Soul", "Bone Dust", "Gilded Scout's Quiver", "Blessed Rune of Vindication", "Battlegear of Vindication" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("neofortress", 2300, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Grace Orb":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9291);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("neofortress", "Vindicator Recruit", "Grace Extracted", 20, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Vindicator Badge":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8299);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("trygve", "r2", "Left", "Blood Eagle", "Eagle Heart", 8);
                        Core.KillMonster("trygve", "r4", "Left", "Rune Boar", "Boar Heart", 8);
                        Core.HuntMonster("trygve", "Gramiel", "Vindicator Seal");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Vindicator Soldier's Hair":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("neofortress", "Vindicator Soldier", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Vindicator Scout's Bow":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("neofortress", "Vindicator Recruit", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Blessed Sigil of Vindication":
                case "Hammer of Vindication":
                case "Hammers of Vindication":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("neofortress", "Vindicator General", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

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
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Bone Dust":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Farm.BattleUnderB(req.Name, quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;


                case "Gilded Scout's Quiver":
                    Core.HuntMonster("neofortress", "Vindicator Recruit", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
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
        new Option<bool>("78532", "Dawn Vindicator Archer", "Mode: [select] only\nShould the bot buy \"Dawn Vindicator Archer\" ?", false),
        new Option<bool>("78533", "Vindicator Scout", "Mode: [select] only\nShould the bot buy \"Vindicator Scout\" ?", false),
        new Option<bool>("78534", "Vindicator Archer's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Vindicator Archer's Hat + Locks\" ?", false),
        new Option<bool>("78536", "Vindicator Archer's Hat", "Mode: [select] only\nShould the bot buy \"Vindicator Archer's Hat\" ?", false),
        new Option<bool>("78538", "Vindicator Scout's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Vindicator Scout's Hat + Locks\" ?", false),
        new Option<bool>("78539", "Vindicator Scout's Hat", "Mode: [select] only\nShould the bot buy \"Vindicator Scout's Hat\" ?", false),
        new Option<bool>("78541", "Bright Bow of the Dawn", "Mode: [select] only\nShould the bot buy \"Bright Bow of the Dawn\" ?", false),
        new Option<bool>("78545", "Dawn Vindicator General", "Mode: [select] only\nShould the bot buy \"Dawn Vindicator General\" ?", false),
        new Option<bool>("78546", "Vindicator Captain", "Mode: [select] only\nShould the bot buy \"Vindicator Captain\" ?", false),
        new Option<bool>("78547", "Vindicator General's Hood", "Mode: [select] only\nShould the bot buy \"Vindicator General's Hood\" ?", false),
        new Option<bool>("78548", "Vindicator General's Hood + Locks", "Mode: [select] only\nShould the bot buy \"Vindicator General's Hood + Locks\" ?", false),
        new Option<bool>("78549", "Vindicator Captain's Hood", "Mode: [select] only\nShould the bot buy \"Vindicator Captain's Hood\" ?", false),
        new Option<bool>("78550", "Vindicator Captain's Hood + Locks", "Mode: [select] only\nShould the bot buy \"Vindicator Captain's Hood + Locks\" ?", false),
        new Option<bool>("78552", "Blessed Shield of Vindication", "Mode: [select] only\nShould the bot buy \"Blessed Shield of Vindication\" ?", false),
        new Option<bool>("78554", "Blessed Hammer of the Dawn", "Mode: [select] only\nShould the bot buy \"Blessed Hammer of the Dawn\" ?", false),
        new Option<bool>("78555", "Blessed Hammers of the Dawn", "Mode: [select] only\nShould the bot buy \"Blessed Hammers of the Dawn\" ?", false),
        new Option<bool>("78556", "Blessed Battlegear of the Dawn", "Mode: [select] only\nShould the bot buy \"Blessed Battlegear of the Dawn\" ?", false),
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
