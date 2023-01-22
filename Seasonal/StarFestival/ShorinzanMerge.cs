/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/StarFestival/StarFestival.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShorinzanMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public StarFestival SF = new();

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
        SF.StoryLine();
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
    };
}
