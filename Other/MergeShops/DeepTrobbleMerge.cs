/*
name: Deep Trobble Merge
description: This bot will farm the items belonging to the selected mode for the Deep Trobble Merge [2338] in /pirates
tags: deep, trobble, merge, pirates, silver, sun, bandana, pistol, cutlass, enchanted, plunger, omocha, tsuika, no, higure
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DeepTrobbleMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreAstravia Astravia => new();
    private static CoreDarkon CDarkon = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Bounty Hunter Dubloon", "Deep Trobble Plunger", "Darkon's Receipt", "La's Gratitude", "Astravian Medal", "A Melody", "Suki's Prestige", "Ancient Remnant", "Mourning Flower", "Unfinished Musical Score" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("pirates", 2338, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Bounty Hunter Dubloon":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    // Here There Be Dragons
                    Core.RegisterQuests(9158);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("volcano", "Lava Golem", "Hammer of the Core", quant, isTemp: false);

                        //jump from aggro cell vv
                        Core.JumpWait();

                        if (!Core.CheckInventory("Nifarii Blade"))
                        {
                            Core.EnsureAccept(9804);
                            Core.HuntMonster("alliance", "Chaorrupted Evil Lieutenant", "Chaorruption Essence", 75);
                            Core.HuntMonster("alliance", "Chaorrupted Evil Lieutenant", "Chaorruption Vial");
                            Core.EnsureComplete(9804);
                            Bot.Wait.ForPickup("Nifarii Blade");
                        }
                        Core.HuntMonster("waterstorm", "Living Water", "Souls of Living Water", 20);
                        Core.HuntMonster("lair", "Purple Draconian", "Pieces of Iron", 15);
                        Core.HuntMonster("embersea", "Coal Creeper", "Coal", 15);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Deep Trobble Plunger":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    // Pressurized Weapon
                    Core.RegisterQuests(9157);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("brightoak", "Bright Treeant", "Chunk of Rubber", 10);
                        Core.HuntMonster("marsh", "Marsh Tree", "Broken Sticks");
                        Core.BuyItem("yulgar", 16, 16946, shopItemID: 10477);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Darkon's Receipt":
                    CDarkon.FarmReceipt(quant);
                    break;

                case "La's Gratitude":
                    CDarkon.LasGratitude(quant);
                    break;

                case "Astravian Medal":
                    CDarkon.AstravianMedal(quant);
                    break;

                case "A Melody":
                    CDarkon.AMelody(quant);
                    break;

                case "Suki's Prestige":
                    CDarkon.SukisPrestiege(quant);
                    break;

                case "Ancient Remnant":
                    CDarkon.AncientRemnant(quant);
                    break;

                case "Mourning Flower":
                    CDarkon.WheelofFortune(quant, 0);
                    break;

                case "Unfinished Musical Score":
                    CDarkon.UnfinishedMusicalScore(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("25531", "Silver Sun Bandana", "Mode: [select] only\nShould the bot buy \"Silver Sun Bandana\" ?", false),
        new Option<bool>("25588", "Silver Sun Pistol", "Mode: [select] only\nShould the bot buy \"Silver Sun Pistol\" ?", false),
        new Option<bool>("25088", "Silver Sun Cutlass", "Mode: [select] only\nShould the bot buy \"Silver Sun Cutlass\" ?", false),
        new Option<bool>("61875", "Enchanted Trobble Plunger", "Mode: [select] only\nShould the bot buy \"Enchanted Trobble Plunger\" ?", false),
        new Option<bool>("71897", "Omocha", "Mode: [select] only\nShould the bot buy \"Omocha\" ?", false),
        new Option<bool>("71898", "Tsuika no Omocha", "Mode: [select] only\nShould the bot buy \"Tsuika no Omocha\" ?", false),
        new Option<bool>("79817", "Higure", "Mode: [select] only\nShould the bot buy \"Higure\" ?", false),
    };
}
