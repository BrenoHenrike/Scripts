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

public class BidoBirthdayMerge
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
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("akiba", 1735, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Concentrated Mana":
                case "Bits of Cloth":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(6979);
                        Core.HuntMonster("prison", "Piggy Drake", "Broken Piggy Bank");
                        Core.EnsureComplete(6979);
                    }
                    Core.Join("akiba", "r1", "Right", false);
                    break;

                case "Green Scrap":
                case "Scrap Metal":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(6978);
                        Core.HuntMonster("newbie", "Slime", "Hidden Giftbox", 5);
                        Core.HuntMonster("noobshire", "Kittarian Mouse Eater", "Decorated Box", 5);
                        Core.EnsureComplete(6978);
                    }
                    Core.Join("akiba", "r1", "Right", false);
                    break;

                case "Bido's Appreciation":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(6980);
                        Core.HuntMonster("thevoid", "Xyfrag", "Piece of Xyfrag Perfectly Slushied");
                        Core.HuntMonster("ashfallcamp", "Smoldur", "Smoldur's Shedded Scales", 4);
                        Core.EnsureComplete(6980);
                    }
                    Core.Join("akiba", "r1", "Right", false);
                    break;

                case "Unknown Alloy":
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Adv.BuyItem("alchemyacademy", 2114, "Gold Voucher 100k", quant);
                        Adv.BuyItem("alchemyacademy", 2114, req.Name, quant);
                    }
                    Core.Join("akiba", "r1", "Right", false);
                    break;

                case "Monster Trophy":
                    Core.HuntMonster("towerofdoom", "Dread Klunk", req.Name, quant, false);
                    Core.Join("akiba", "r1", "Right", false);
                    break;

                case "Alteon the Imbalanced":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("brightfortress", "Imbalanced Alteon", req.Name, quant, false);
                    Core.Join("akiba", "r1", "Right", false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("49049", "Bido's Fireflies", "Mode: [select] only\nShould the bot buy \"Bido's Fireflies\" ?", false),
        new Option<bool>("49046", "Toxic Alchemist Hair", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist Hair\" ?", false),
        new Option<bool>("49047", "Toxic Alchemist Locks", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist Locks\" ?", false),
        new Option<bool>("49070", "Toxic Runes", "Mode: [select] only\nShould the bot buy \"Toxic Runes\" ?", false),
        new Option<bool>("49048", "Toxic Alchemist Wings", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist Wings\" ?", false),
        new Option<bool>("49068", "Alchemical Onyx Daggers", "Mode: [select] only\nShould the bot buy \"Alchemical Onyx Daggers\" ?", false),
        new Option<bool>("49067", "Alchemical Onyx Reversed Daggers", "Mode: [select] only\nShould the bot buy \"Alchemical Onyx Reversed Daggers\" ?", false),
        new Option<bool>("49064", "Alchemical Onyx Blade", "Mode: [select] only\nShould the bot buy \"Alchemical Onyx Blade\" ?", false),
        new Option<bool>("49065", "Alchemical Onyx Katana", "Mode: [select] only\nShould the bot buy \"Alchemical Onyx Katana\" ?", false),
        new Option<bool>("49092", "Dual Alchemical Onyx Katanas", "Mode: [select] only\nShould the bot buy \"Dual Alchemical Onyx Katanas\" ?", false),
        new Option<bool>("49069", "Alchemical Onyx Staff", "Mode: [select] only\nShould the bot buy \"Alchemical Onyx Staff\" ?", false),
        new Option<bool>("49071", "Toxic Aura Backblades", "Mode: [select] only\nShould the bot buy \"Toxic Aura Backblades\" ?", false),
        new Option<bool>("49081", "Seal of Balance", "Mode: [select] only\nShould the bot buy \"Seal of Balance\" ?", false),
        new Option<bool>("49061", "Toxic Aura Blade", "Mode: [select] only\nShould the bot buy \"Toxic Aura Blade\" ?", false),
        new Option<bool>("49062", "Alchemical Aura Blade", "Mode: [select] only\nShould the bot buy \"Alchemical Aura Blade\" ?", false),
        new Option<bool>("49063", "Amber Aura Blade", "Mode: [select] only\nShould the bot buy \"Amber Aura Blade\" ?", false),
        new Option<bool>("49060", "Toxic Bloodriver", "Mode: [select] only\nShould the bot buy \"Toxic Bloodriver\" ?", false),
        new Option<bool>("49044", "Toxic Alchemists Adornments", "Mode: [select] only\nShould the bot buy \"Toxic Alchemists Adornments\" ?", false),
        new Option<bool>("49045", "Toxic Rune Fists", "Mode: [select] only\nShould the bot buy \"Toxic Rune Fists\" ?", false),
        new Option<bool>("55694", "Toxic Diviner Hair", "Mode: [select] only\nShould the bot buy \"Toxic Diviner Hair\" ?", false),
        new Option<bool>("55695", "Toxic Diviner Rune", "Mode: [select] only\nShould the bot buy \"Toxic Diviner Rune\" ?", false),
        new Option<bool>("55696", "Toxic Diviner Blade", "Mode: [select] only\nShould the bot buy \"Toxic Diviner Blade\" ?", false),
        new Option<bool>("55697", "Toxic Diviner Spear", "Mode: [select] only\nShould the bot buy \"Toxic Diviner Spear\" ?", false),
        new Option<bool>("71065", "Toxic Alchemist Rogue", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist Rogue\" ?", false),
        new Option<bool>("71066", "Toxic Alchemist's Bandana", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Bandana\" ?", false),
        new Option<bool>("71067", "Toxic Alchemist's Bandana + Locks", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Bandana + Locks\" ?", false),
        new Option<bool>("71068", "Toxic Alchemist's Scarf", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Scarf\" ?", false),
        new Option<bool>("71069", "Toxic Alchemist's Scarf + Locks", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Scarf + Locks\" ?", false),
        new Option<bool>("71070", "Pixel On Your Back", "Mode: [select] only\nShould the bot buy \"Pixel On Your Back\" ?", false),
        new Option<bool>("71071", "Toxic Alchemist's Venom Blade", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Venom Blade\" ?", false),
        new Option<bool>("71072", "Toxic Alchemist's Venom Blades", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Venom Blades\" ?", false),
        new Option<bool>("71073", "Toxic Alchemist's Noxious Blade", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Noxious Blade\" ?", false),
        new Option<bool>("71074", "Toxic Alchemist's Noxious Blades", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Noxious Blades\" ?", false),
        new Option<bool>("71075", "Toxic Alchemist's Royal Sword", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Royal Sword\" ?", false),
        new Option<bool>("71076", "Toxic Alchemist's Venom Lance", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Venom Lance\" ?", false),
        new Option<bool>("71077", "Toxic Alchemist's Noxious Lance", "Mode: [select] only\nShould the bot buy \"Toxic Alchemist's Noxious Lance\" ?", false),
    };
}
