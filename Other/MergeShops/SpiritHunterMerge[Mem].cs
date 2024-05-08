/*
name: SpiritHunter Merge
description: This bot will farm the items belonging to the selected mode for the SpiritHunter Merge [375] in /bludrut
tags: spirithunter, merge, bludrut, katana, halberd, spirithunters, reaper, back, scythe
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story\Bludrut.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SpiritHunterMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Bludrut Blud = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Spirit Ward Sigil", "Screamwave", "Ectoamber" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.IsMember)
        {
            Core.Logger("This script requires you to be a member", messageBox: true, stopBot: true);
            return;
        }
        Blud.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("bludrut", 375, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Spirit Ward Sigil":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(1695); // Spirit Ward Sigil (1695)
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bludrut4", "Groglurk", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Screamwave":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(1694); // Screamwave (1694)
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bludrut3", "Siren", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ectoamber":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(1693);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bludrut", "Rattlebones", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("11155", "SpiritHunter Armor", "Mode: [select] only\nShould the bot buy \"SpiritHunter Armor\" ?", false),
        new Option<bool>("11156", "SpiritHunter Mask", "Mode: [select] only\nShould the bot buy \"SpiritHunter Mask\" ?", false),
        new Option<bool>("11157", "SpiritHunter Blades", "Mode: [select] only\nShould the bot buy \"SpiritHunter Blades\" ?", false),
        new Option<bool>("11158", "SpiritHunter Katana", "Mode: [select] only\nShould the bot buy \"SpiritHunter Katana\" ?", false),
        new Option<bool>("11159", "SpiritHunter Halberd", "Mode: [select] only\nShould the bot buy \"SpiritHunter Halberd\" ?", false),
        new Option<bool>("11160", "SpiritHunter's Reaper", "Mode: [select] only\nShould the bot buy \"SpiritHunter's Reaper\" ?", false),
        new Option<bool>("11161", "SpiritHunter Back Scythe", "Mode: [select] only\nShould the bot buy \"SpiritHunter Back Scythe\" ?", false),
    };
}
