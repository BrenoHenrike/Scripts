/*
name: Shorinzan Merge
description: This bot will farm the items belonging to the selected mode for the Shorinzan Merge [2148] in /starfest
tags: shorinzan, merge, starfest, starry, samurai, samurais, backswords, aurelian, aurous, hope, fallen, stars, dark, heart, hearts, blackhole, suns
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShorinzanMerge
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
        Core.BankingBlackList.AddRange(new[] { "Deepest Desire", "Hidden Hope", "Simple Wish", "Fallen Star Shard", "Hashihime's Heart" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("yokairiver"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("starfest", 2148, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Deepest Desire":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8753);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Tercessuinotlim", "Tainted Elemental", "Tainted Essence Collected", 10);
                        Core.HuntMonster("Tercessuinotlim", "Dark Makai", "Makai Essence Collected", 20);
                        Core.HuntMonster("necrodungeon", "SlimeSkull", "Necropolis Soul Collected", 15);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("necrodungeon", "5 Headed Dracolich", "Dracolich Soul Collected", 15);
                        Core.HuntMonster("necrodungeon", "Doom Overlord", "Doom Power Catalyst", 2);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Hidden Hope":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8751);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster($"battleunderb", "Enter", "Spawn", "*", "Bundle O’ Bones", 30);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonsterMapID($"Odokuro", 1, "Odokuro’s Occipital");
                        Core.HuntMonster($"bonecastle", "Vaden", "Vaden’s Other Arm");
                        Core.HuntMonster($"vordredboss", "Vordred", "Vordred’s Skull(s)", 3);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Simple Wish":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8748);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (!Core.CheckInventory("Twilly Twig"))
                        {
                            Core.AddDrop("Twilly Twig");
                            Core.EnsureAccept(11);
                            Core.HuntMonster("farm", "Treeant", "Treeant Branch");
                            Core.EnsureComplete(11);
                            Bot.Wait.ForPickup("Twilly Twig");
                        }
                        Core.HuntMonster("brightoak", "Bright Treeant", "Brightest Branch", 6);
                        Core.HuntMonster("farm", "Treeant", "Treant Leaf");
                        Core.HuntMonster("guardiantree", "Blossoming Treeant", "Beautiful Blossom", 6);
                        Core.HuntMonster("NibbleOn", "Mean Old Treeant", "Bitter Bark", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fallen Star Shard":
                    Core.EquipClass(ClassType.Solo);
                    Adv.BestGear(RacialGearBoost.Elemental);
                    Core.HuntMonster("starfest", "Fallen Star", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Hashihime's Heart":
                    Core.EquipClass(ClassType.Solo);
                    Adv.BestGear(RacialGearBoost.Chaos);
                    Core.HuntMonster("yokairiver", "Uji No Hashihime", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("71290", "Starry Samurai", "Mode: [select] only\nShould the bot buy \"Starry Samurai\" ?", false),
        new Option<bool>("71291", "Starry Samurai's Mask", "Mode: [select] only\nShould the bot buy \"Starry Samurai's Mask\" ?", false),
        new Option<bool>("71292", "Starry Samurai's BackSwords", "Mode: [select] only\nShould the bot buy \"Starry Samurai's BackSwords\" ?", false),
        new Option<bool>("71293", "Starry Samurai's Sword", "Mode: [select] only\nShould the bot buy \"Starry Samurai's Sword\" ?", false),
        new Option<bool>("71294", "Starry Samurai's Swords", "Mode: [select] only\nShould the bot buy \"Starry Samurai's Swords\" ?", false),
        new Option<bool>("69473", "Aurelian Sword", "Mode: [select] only\nShould the bot buy \"Aurelian Sword\" ?", false),
        new Option<bool>("69474", "Aurelian Swords", "Mode: [select] only\nShould the bot buy \"Aurelian Swords\" ?", false),
        new Option<bool>("69479", "Aurous Staff of Hope", "Mode: [select] only\nShould the bot buy \"Aurous Staff of Hope\" ?", false),
        new Option<bool>("78064", "Fallen Star's Dark Heart", "Mode: [select] only\nShould the bot buy \"Fallen Star's Dark Heart\" ?", false),
        new Option<bool>("78065", "Fallen Star's Dark Hearts", "Mode: [select] only\nShould the bot buy \"Fallen Star's Dark Hearts\" ?", false),
        new Option<bool>("78074", "Blackhole Sun's Heart", "Mode: [select] only\nShould the bot buy \"Blackhole Sun's Heart\" ?", false),
        new Option<bool>("78075", "Blackhole Sun's Hearts", "Mode: [select] only\nShould the bot buy \"Blackhole Sun's Hearts\" ?", false),
        new Option<bool>("78801", "Blackhole Sun's Heart", "Mode: [select] only\nShould the bot buy \"Blackhole Sun's Heart\" ?", false),
    };
}
