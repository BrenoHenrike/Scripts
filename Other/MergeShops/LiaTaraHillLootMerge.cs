/*
name: LiaTaraHill Loot Merge
description: This bot will farm the items belonging to the selected mode for the LiaTaraHill Loot Merge [2460] in /liatarahill
tags: liatarahill, loot, merge, liatarahill, gold, voucher, k, skye, cailleach, plasma, orbs, warden, south, wardens, ornate, obelisk, shattered
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LiaTaraHillLootMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Salvaged Skye Armament", "Golden Catalyst", "Plasma Orb", "Drained Skye Obelisk" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.LiaTaraHill();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("liatarahill", 2460, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Salvaged Skye Armament":
                    Core.FarmingLogger(req.Name, quant);
                    Core.KillMonster("castleeblana", "r2", "Left", "Skye Warrior", req.Name, req.Quantity, req.Temp, false);
                    break;

                case "Golden Catalyst":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9815);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("liatarahill", "Undead Garde", "Garde's Brooch", 9, log: false);
                        Core.HuntMonster("liatarahill", "Garde Wraith", "Ghost Blossoms", 9, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("liatarahill", "Warden Illaria", "Illaria's Amulet", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Plasma Orb":
                case "Drained Skye Obelisk":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("liatarahill", "Warden Illaria", req.Name, quant, req.Temp, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("86350", "Skye Cailleach", "Mode: [select] only\nShould the bot buy \"Skye Cailleach\" ?", false),
        new Option<bool>("86351", "Skye Cailleach Hood", "Mode: [select] only\nShould the bot buy \"Skye Cailleach Hood\" ?", false),
        new Option<bool>("86558", "Plasma Orbs", "Mode: [select] only\nShould the bot buy \"Plasma Orbs\" ?", false),
        new Option<bool>("86352", "Skye Warden of the South", "Mode: [select] only\nShould the bot buy \"Skye Warden of the South\" ?", false),
        new Option<bool>("86353", "Skye Warden's Ornate Mask", "Mode: [select] only\nShould the bot buy \"Skye Warden's Ornate Mask\" ?", false),
        new Option<bool>("87186", "Skye Obelisk", "Mode: [select] only\nShould the bot buy \"Skye Obelisk\" ?", false),
        new Option<bool>("87185", "Shattered Skye Obelisk", "Mode: [select] only\nShould the bot buy \"Shattered Skye Obelisk\" ?", false),
    };
}
