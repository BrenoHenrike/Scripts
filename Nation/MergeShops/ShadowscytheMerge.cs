/*
name: Shadowscythe Merge
description: This bot will farm the items belonging to the selected mode for the Shadowscythe Merge [1208] in /shadowblast
tags: shadowscythe, merge, shadowblast, doomfire, empowered, flaming, exalted
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/Nation/ShadowBlastArena.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowscytheMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private ShadowBlastArena SBA = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Diamond Token of Gravelyn", "Emblem of Gravelyn" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SBA.Doall();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowblast", 1208, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Diamond Token of Gravelyn":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(4737);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (!Core.CheckInventory("Defeated Makai", 25))
                        {
                            Core.EquipClass(ClassType.Farm);
                            Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Defeated Makai", 25, false);
                            Core.JumpWait();
                            Core.Join("aqlesson");
                        }
                        Core.EquipClass(ClassType.Solo);
                        Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Eye", publicRoom: true);
                        Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle", publicRoom: true);

                        //More then one item of the same name as drop btoh temp and non-temp.
                        while (!Bot.ShouldExit && !Core.CheckInventory(33257))
                            Core.KillMonster("dflesson", "r12", "Right", "Fluffy the Dracolich", log: false, publicRoom: true);

                        Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang");
                        Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade", publicRoom: true);
                        Bot.Drops.Pickup("Legion Token", "Diamond Token of Dage");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Emblem of Gravelyn":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4750);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shadowblast", "Carnage", "Shadow Seal", isTemp: false);
                        Core.HuntMonster("shadowblast", "Legion Fenrir", "Gem of Superiority", isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("33273", "DOOMFire Blade", "Mode: [select] only\nShould the bot buy \"DOOMFire Blade\" ?", false),
        new Option<bool>("33274", "Empowered DOOMFire Blade", "Mode: [select] only\nShould the bot buy \"Empowered DOOMFire Blade\" ?", false),
        new Option<bool>("33275", "Flaming DOOMFire Blade", "Mode: [select] only\nShould the bot buy \"Flaming DOOMFire Blade\" ?", false),
        new Option<bool>("33276", "Exalted DOOMFire Blade", "Mode: [select] only\nShould the bot buy \"Exalted DOOMFire Blade\" ?", false),
        new Option<bool>("33209", "DOOMFire Cape", "Mode: [select] only\nShould the bot buy \"DOOMFire Cape\" ?", false),
    };
}
