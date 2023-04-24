/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FireInvasionMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreSoW SOW = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "ShadowFire Trophy" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SOW.Tyndarius();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("fireinvasion", 2031, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "ShadowFire Trophy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8192);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireinvasion", "Living Shadowflame", "ShadowFlame Tag", 15, log: false);
                        Core.HuntMonster("fireinvasion", "Shadefire Cavalry", "Corrupted Badge", 3, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("61694", "Ruby SpellBlade", "Mode: [select] only\nShould the bot buy \"Ruby SpellBlade\" ?", false),
        new Option<bool>("61695", "Ruby Hat + Bangs", "Mode: [select] only\nShould the bot buy \"Ruby Hat + Bangs\" ?", false),
        new Option<bool>("61696", "Ruby Hat + Hair", "Mode: [select] only\nShould the bot buy \"Ruby Hat + Hair\" ?", false),
        new Option<bool>("61697", "Ruby Spell Shroud", "Mode: [select] only\nShould the bot buy \"Ruby Spell Shroud\" ?", false),
        new Option<bool>("61698", "Burning Ruby Blade", "Mode: [select] only\nShould the bot buy \"Burning Ruby Blade\" ?", false),
        new Option<bool>("61699", "Dual Burning Ruby Blades", "Mode: [select] only\nShould the bot buy \"Dual Burning Ruby Blades\" ?", false),
        new Option<bool>("61700", "Burning Ruby BattleGear", "Mode: [select] only\nShould the bot buy \"Burning Ruby BattleGear\" ?", false),
        new Option<bool>("61701", "Burning Blade Pixie", "Mode: [select] only\nShould the bot buy \"Burning Blade Pixie\" ?", false),
        new Option<bool>("61686", "Sapphire SpellBlade", "Mode: [select] only\nShould the bot buy \"Sapphire SpellBlade\" ?", false),
        new Option<bool>("61687", "Sapphire Hat + Bangs", "Mode: [select] only\nShould the bot buy \"Sapphire Hat + Bangs\" ?", false),
        new Option<bool>("61688", "Sapphire Hat + Hair", "Mode: [select] only\nShould the bot buy \"Sapphire Hat + Hair\" ?", false),
        new Option<bool>("61689", "Burning Sapphire Blade", "Mode: [select] only\nShould the bot buy \"Burning Sapphire Blade\" ?", false),
        new Option<bool>("61690", "Dual Burning Sapphire Blades", "Mode: [select] only\nShould the bot buy \"Dual Burning Sapphire Blades\" ?", false),
        new Option<bool>("61691", "Burning Sapphire BattleGear", "Mode: [select] only\nShould the bot buy \"Burning Sapphire BattleGear\" ?", false),
        new Option<bool>("61692", "Sapphire Flame Pixie", "Mode: [select] only\nShould the bot buy \"Sapphire Flame Pixie\" ?", false),
        new Option<bool>("61693", "Sapphire Spell Shroud", "Mode: [select] only\nShould the bot buy \"Sapphire Spell Shroud\" ?", false),
    };
}
