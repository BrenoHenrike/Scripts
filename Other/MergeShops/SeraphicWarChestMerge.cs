/*
name: Seraphic War Chest Merge
description: This bot will farm the items belonging to the selected mode for the Seraphic War Chest Merge [1573] in /seraphicwardage
tags: seraphic, war, chest, merge, seraphicwardage, ada, statue, laken, blood, legion, warrior, undead, seraph, seraphs, lakens, personal, paladin
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SeraphicWarChestMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private SeraphicWar_Story SW = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Blood Token", "Dark Token", "Seraphic Paladin Shield", "Seraphic Paladin Wings" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SW.SeraphicWar_Questline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("seraphicwardage", 1573, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Blood Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6246, 6247);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("seraphicwarlaken", "Enter", "Spawn", "*", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dark Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6248, 6249);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("seraphicwardage", "Enter", "Spawn", "*", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Seraphic Paladin Shield":
                case "Seraphic Paladin Wings":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("seraphicwardage", "Supercharged Laken", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43235", "Ada Statue", "Mode: [select] only\nShould the bot buy \"Ada Statue\" ?", false),
        new Option<bool>("43236", "Laken Statue", "Mode: [select] only\nShould the bot buy \"Laken Statue\" ?", false),
        new Option<bool>("43252", "Blood Legion Warrior", "Mode: [select] only\nShould the bot buy \"Blood Legion Warrior\" ?", false),
        new Option<bool>("43253", "Blood Legion Helmet", "Mode: [select] only\nShould the bot buy \"Blood Legion Helmet\" ?", false),
        new Option<bool>("43256", "Undead Seraph Warrior", "Mode: [select] only\nShould the bot buy \"Undead Seraph Warrior\" ?", false),
        new Option<bool>("43258", "Undead Seraph's Helm", "Mode: [select] only\nShould the bot buy \"Undead Seraph's Helm\" ?", false),
        new Option<bool>("43211", "Laken's Personal Armor", "Mode: [select] only\nShould the bot buy \"Laken's Personal Armor\" ?", false),
        new Option<bool>("43273", "Laken the Seraphic Paladin", "Mode: [select] only\nShould the bot buy \"Laken the Seraphic Paladin\" ?", false),
    };
}
