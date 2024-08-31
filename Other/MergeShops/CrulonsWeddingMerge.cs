/*
name: Crulons Wedding Merge
description: This bot will farm the items belonging to the selected mode for the Crulons Wedding Merge [2472] in /crulonwed
tags: crulons, wedding, merge, crulonwed, sandsea, ceremonial, attire, morph, amanis, veil, emerald, eons, royal, guest, enchanted, desert, bandana, luxorlight, wings, spear, bow
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Crulonwed.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CrulonsWeddingMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private crulonwedding crulonwedding = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Flame Incantation", "Silver Ward", "Almoravid's Bracer", "Honored Sandsea Guest", "Desert Bandana", "Luminous Emblem", "Luminous Soul Blade", "Luminous Soul Spear", "Luminous Soul Bow" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        #region Useable Monsters
        string[] UseableMonsters = new[]
        {
            "King Almoravid", // UseableMonsters[0]
            "Jaan al Nair",
            "Silver Elemental",
        };
        #endregion Useable Monsters

        crulonwedding.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("crulonwed", 2472, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Flame Incantation":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9848);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("djinnguard", UseableMonsters[1], "Jaan's Flames");

                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Silver Ward":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9849);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("towerofmirrors", UseableMonsters[2], "Silver Tincture", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Almoravid's Bracer":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9850);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("crulonwed", UseableMonsters[0], "Silver Tincture", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Luminous Soul Bow":
                case "Luminous Soul Spear":
                case "Luminous Emblem":
                case "Luminous Soul Blade":
                case "Desert Bandana":
                case "Honored Sandsea Guest":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("crulonwed", UseableMonsters[0], req.Name, req.Quantity, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("87682", "Sandsea Ceremonial Attire", "Mode: [select] only\nShould the bot buy \"Sandsea Ceremonial Attire\" ?", false),
        new Option<bool>("87683", "Crulon's Wedding Morph", "Mode: [select] only\nShould the bot buy \"Crulon's Wedding Morph\" ?", false),
        new Option<bool>("87684", "Amani's Wedding Visage", "Mode: [select] only\nShould the bot buy \"Amani's Wedding Visage\" ?", false),
        new Option<bool>("87685", "Crulon's Wedding Helm", "Mode: [select] only\nShould the bot buy \"Crulon's Wedding Helm\" ?", false),
        new Option<bool>("87686", "Amani's Wedding Veil", "Mode: [select] only\nShould the bot buy \"Amani's Wedding Veil\" ?", false),
        new Option<bool>("87687", "Crulon's Wedding Mask", "Mode: [select] only\nShould the bot buy \"Crulon's Wedding Mask\" ?", false),
        new Option<bool>("87688", "Emerald Eons Blade", "Mode: [select] only\nShould the bot buy \"Emerald Eons Blade\" ?", false),
        new Option<bool>("87689", "Emerald Eons Blades", "Mode: [select] only\nShould the bot buy \"Emerald Eons Blades\" ?", false),
        new Option<bool>("68243", "Royal Sandsea Guest", "Mode: [select] only\nShould the bot buy \"Royal Sandsea Guest\" ?", false),
        new Option<bool>("68244", "Enchanted Desert Bandana", "Mode: [select] only\nShould the bot buy \"Enchanted Desert Bandana\" ?", false),
        new Option<bool>("68245", "Luxorlight Helm", "Mode: [select] only\nShould the bot buy \"Luxorlight Helm\" ?", false),
        new Option<bool>("68246", "Luxorlight Wings", "Mode: [select] only\nShould the bot buy \"Luxorlight Wings\" ?", false),
        new Option<bool>("68248", "Luxorlight Blade", "Mode: [select] only\nShould the bot buy \"Luxorlight Blade\" ?", false),
        new Option<bool>("68249", "Luxorlight Spear", "Mode: [select] only\nShould the bot buy \"Luxorlight Spear\" ?", false),
        new Option<bool>("68250", "Luxorlight Bow", "Mode: [select] only\nShould the bot buy \"Luxorlight Bow\" ?", false),
    };
}
