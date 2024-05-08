/*
name: Queens Battle Merge
description: This bot will farm the items belonging to the selected mode for the Queens Battle Merge [2061] in /queenbattle
tags: queens, battle, merge, queenbattle, st, chaoslord, prime, chaoslords, tattered, cloak, twisted, inversion, good, ponytail, morph, evil, rd, primes, monstrous, control, th, runed, laser, katana, katanas, ruined, wings, twintails
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/OrbHunt.cs
//cs_include Scripts/Story\QueenofMonsters\Extra\QueenBattle.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class QueensBattleMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private QueenBattle QB = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Severed Tentacle", "1st Hero of Balance", "1st Hero of Balance Hood", "1st Hero of Balance Cloak", "Good Hero of Balance", "Good Hero of Balance Morph", "Evil Hero of Balance", "Evil Hero of Balance Morph", "3rd Hero of Balance", "3rd Hero of Balance Locks", "3rd Hero of Balance Scarf", "3rd Hero of Balance Daggers", "3rd Hero of Balance Dirk", "4th Hero of Balance", "4th Hero of Balance Cloak", "5th Hero of Balance", "5th Hero of Balance Morph", "5th Hero of Balance Wings", "7th Hero of Balance", "7th Hero of Balance Morph" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        QB.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("queenbattle", 2061, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Severed Tentacle":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8362);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("queenbattle", "Proto Chaos Champion", "Proto Chaos Champion Redefeated", log: false);
                        Core.HuntMonster("queenbattle", "Queen of Monsters", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "1st Hero of Balance":
                case "1st Hero of Balance Hood":
                case "1st Hero of Balance Cloak":
                case "Good Hero of Balance":
                case "Good Hero of Balance Morph":
                case "Evil Hero of Balance":
                case "Evil Hero of Balance Morph":
                case "3rd Hero of Balance":
                case "3rd Hero of Balance Locks":
                case "3rd Hero of Balance Scarf":
                case "3rd Hero of Balance Daggers":
                case "3rd Hero of Balance Dirk":
                case "4th Hero of Balance":
                case "4th Hero of Balance Cloak":
                case "5th Hero of Balance":
                case "5th Hero of Balance Morph":
                case "5th Hero of Balance Wings":
                case "7th Hero of Balance":
                case "7th Hero of Balance Morph":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("queenbattle", "Queen of Monsters", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("64802", "1st ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"1st ChaosLord Prime\" ?", false),
        new Option<bool>("64803", "1st ChaosLord Prime Hood", "Mode: [select] only\nShould the bot buy \"1st ChaosLord Prime Hood\" ?", false),
        new Option<bool>("64804", "1st ChaosLord's Tattered Cloak", "Mode: [select] only\nShould the bot buy \"1st ChaosLord's Tattered Cloak\" ?", false),
        new Option<bool>("64805", "Twisted Staff of Inversion", "Mode: [select] only\nShould the bot buy \"Twisted Staff of Inversion\" ?", false),
        new Option<bool>("64806", "Good ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"Good ChaosLord Prime\" ?", false),
        new Option<bool>("64807", "Good ChaosLord's Ponytail", "Mode: [select] only\nShould the bot buy \"Good ChaosLord's Ponytail\" ?", false),
        new Option<bool>("64808", "Good ChaosLord's Morph", "Mode: [select] only\nShould the bot buy \"Good ChaosLord's Morph\" ?", false),
        new Option<bool>("64809", "Evil ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"Evil ChaosLord Prime\" ?", false),
        new Option<bool>("64810", "Evil ChaosLord's Locks", "Mode: [select] only\nShould the bot buy \"Evil ChaosLord's Locks\" ?", false),
        new Option<bool>("64811", "Evil ChaosLord's Morph", "Mode: [select] only\nShould the bot buy \"Evil ChaosLord's Morph\" ?", false),
        new Option<bool>("64812", "3rd ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"3rd ChaosLord Prime\" ?", false),
        new Option<bool>("64813", "3rd ChaosLord Prime's Locks", "Mode: [select] only\nShould the bot buy \"3rd ChaosLord Prime's Locks\" ?", false),
        new Option<bool>("64814", "3rd ChaosLord's Tattered Cloak", "Mode: [select] only\nShould the bot buy \"3rd ChaosLord's Tattered Cloak\" ?", false),
        new Option<bool>("64815", "Monstrous Blade of Control", "Mode: [select] only\nShould the bot buy \"Monstrous Blade of Control\" ?", false),
        new Option<bool>("64816", "4th ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"4th ChaosLord Prime\" ?", false),
        new Option<bool>("64817", "4th ChaosLord Prime's Helmet", "Mode: [select] only\nShould the bot buy \"4th ChaosLord Prime's Helmet\" ?", false),
        new Option<bool>("64818", "4th ChaosLord's Runed Cloak", "Mode: [select] only\nShould the bot buy \"4th ChaosLord's Runed Cloak\" ?", false),
        new Option<bool>("64819", "4th ChaosLord's Laser Katana", "Mode: [select] only\nShould the bot buy \"4th ChaosLord's Laser Katana\" ?", false),
        new Option<bool>("64820", "4th ChaosLord's Dual Katanas", "Mode: [select] only\nShould the bot buy \"4th ChaosLord's Dual Katanas\" ?", false),
        new Option<bool>("64821", "5th ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"5th ChaosLord Prime\" ?", false),
        new Option<bool>("64822", "5th ChaosLord's Morph", "Mode: [select] only\nShould the bot buy \"5th ChaosLord's Morph\" ?", false),
        new Option<bool>("64823", "5th ChaosLord's Ruined Wings", "Mode: [select] only\nShould the bot buy \"5th ChaosLord's Ruined Wings\" ?", false),
        new Option<bool>("64824", "6th ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"6th ChaosLord Prime\" ?", false),
        new Option<bool>("64825", "6th ChaosLord's Twintails", "Mode: [select] only\nShould the bot buy \"6th ChaosLord's Twintails\" ?", false),
        new Option<bool>("64826", "7th ChaosLord Prime", "Mode: [select] only\nShould the bot buy \"7th ChaosLord Prime\" ?", false),
        new Option<bool>("64827", "7th ChaosLord's Morph", "Mode: [select] only\nShould the bot buy \"7th ChaosLord's Morph\" ?", false),
    };
}
