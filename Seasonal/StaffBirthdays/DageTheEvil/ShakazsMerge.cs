/*
name: Shakazs Merge
description: This bot will farm the required items from /siegefortress, do the story, and buy items from the merge shop
tags: siegefortress, merge, dage, staff birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Seasonal\StaffBirthdays\Nulgath\TempleDelve.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShakazsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private TempleDelve TD = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Rune of Doom", "Abyssal Seer Hair", "Abyssal Frost Sedge Hat", "Abyssal Frost Samurai Spirit" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        TD.Storyline();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("siegefortress", 2246, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Rune of Doom":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9144);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("siegefortress", "Shadow Traitor", "Traitorous Specimen", 8, log: false);
                        Core.HuntMonster("siegefortress", "Enslaved Elemental", "Elemental Rune", 8, log: false);
                        Core.HuntMonster("siegefortress", "Enslaved Astero", "Colossal Light Rune", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Abyssal Seer Hair":
                case "Abyssal Frost Sedge Hat":
                case "Abyssal Frost Samurai Spirit":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Siege Fortress", "Dage The Evil", req.Name, isTemp: false, log: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76356", "ShadowScythe BladeMaster", "Mode: [select] only\nShould the bot buy \"ShadowScythe BladeMaster\" ?", false),
        new Option<bool>("76357", "ShadowScythe BladeMaster Hair", "Mode: [select] only\nShould the bot buy \"ShadowScythe BladeMaster Hair\" ?", false),
        new Option<bool>("76358", "ShadowScythe BladeMaster Short Locks", "Mode: [select] only\nShould the bot buy \"ShadowScythe BladeMaster Short Locks\" ?", false),
        new Option<bool>("76359", "ShadowScythe BladeMaster Short Hair", "Mode: [select] only\nShould the bot buy \"ShadowScythe BladeMaster Short Hair\" ?", false),
        new Option<bool>("76360", "ShadowScythe BladeMaster Locks", "Mode: [select] only\nShould the bot buy \"ShadowScythe BladeMaster Locks\" ?", false),
        new Option<bool>("76361", "ShadowScythe Flag", "Mode: [select] only\nShould the bot buy \"ShadowScythe Flag\" ?", false),
        new Option<bool>("76362", "Necrotic Katana of Doom", "Mode: [select] only\nShould the bot buy \"Necrotic Katana of Doom\" ?", false),
        new Option<bool>("76363", "Necrotic Katanas of Doom", "Mode: [select] only\nShould the bot buy \"Necrotic Katanas of Doom\" ?", false),
        new Option<bool>("76364", "Sheathed Necrotic Katana of Doom", "Mode: [select] only\nShould the bot buy \"Sheathed Necrotic Katana of Doom\" ?", false),
        new Option<bool>("76365", "Necrotic Naginata of Doom", "Mode: [select] only\nShould the bot buy \"Necrotic Naginata of Doom\" ?", false),
        new Option<bool>("76714", "Blind Abyssal Seer Hair", "Mode: [select] only\nShould the bot buy \"Blind Abyssal Seer Hair\" ?", false),
        new Option<bool>("76715", "Masked Abyssal Seer Visage", "Mode: [select] only\nShould the bot buy \"Masked Abyssal Seer Visage\" ?", false),
        new Option<bool>("76717", "Abyssal Frost Masked Sedge Hat", "Mode: [select] only\nShould the bot buy \"Abyssal Frost Masked Sedge Hat\" ?", false),
        new Option<bool>("76720", "Enchanted Abyssal Frost Samurai", "Mode: [select] only\nShould the bot buy \"Enchanted Abyssal Frost Samurai\" ?", false),
    };
}
