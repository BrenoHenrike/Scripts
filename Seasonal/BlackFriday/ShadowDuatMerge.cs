/*
name: ShadowDuat Merge
description: This bot will farm the items belonging to the selected mode for the ShadowDuat Merge [2493] in /shadowduat
tags: shadowduat, merge, shadowduat, ouroboros, warlock, shag, crown, morph, crowned, unbroken, dream, warlocks, altar, life, rebirth, forbidden, ouro, scythe, umbra, metamorphosis, tome, fang, umbral, apophis, lectors, doomed, knight, loyal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowDuatMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "A Memory", "Metamorphosis Maw's Knight", "Metamorphosis Maw's Loyal Knight", "Metamophosis Maw's Knight Hair", "Metamophosis Maw's Knight Morph" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowduat", 2493, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "A Memory":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9938, 9939);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("shadowduat", "r2", "Left", "*", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Metamorphosis Maw's Knight":
                case "Metamorphosis Maw's Loyal Knight":
                case "Metamophosis Maw's Knight Hair":
                case "Metamophosis Maw's Knight Morph":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("shadowduat", "DoomKnight Dryden", req.Name, quant, req.Temp, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("87828", "Ouroboros Warlock", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock\" ?", false),
        new Option<bool>("87829", "Ouroboros Warlock Hair", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock Hair\" ?", false),
        new Option<bool>("87830", "Ouroboros Warlock Shag", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock Shag\" ?", false),
        new Option<bool>("87831", "Ouroboros Warlock Crown Morph", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock Crown Morph\" ?", false),
        new Option<bool>("87832", "Ouroboros Crown", "Mode: [select] only\nShould the bot buy \"Ouroboros Crown\" ?", false),
        new Option<bool>("87833", "Ouroboros Warlock Hood", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock Hood\" ?", false),
        new Option<bool>("87834", "Ouroboros Warlock Crowned Hood", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock Crowned Hood\" ?", false),
        new Option<bool>("87835", "Ouroboros Warlock Crowned Hood Morph", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock Crowned Hood Morph\" ?", false),
        new Option<bool>("87836", "Ouroboros' Unbroken Dream", "Mode: [select] only\nShould the bot buy \"Ouroboros' Unbroken Dream\" ?", false),
        new Option<bool>("87837", "Ouroboros Warlock's Altar", "Mode: [select] only\nShould the bot buy \"Ouroboros Warlock's Altar\" ?", false),
        new Option<bool>("87838", "Life and Rebirth Staff", "Mode: [select] only\nShould the bot buy \"Life and Rebirth Staff\" ?", false),
        new Option<bool>("87839", "Forbidden Ouro Scythe", "Mode: [select] only\nShould the bot buy \"Forbidden Ouro Scythe\" ?", false),
        new Option<bool>("87840", "Umbra Metamorphosis Tome", "Mode: [select] only\nShould the bot buy \"Umbra Metamorphosis Tome\" ?", false),
        new Option<bool>("87841", "Forbidden Metamorphosis Fang", "Mode: [select] only\nShould the bot buy \"Forbidden Metamorphosis Fang\" ?", false),
        new Option<bool>("87842", "Umbral Metamorphosis Scythe and Tome", "Mode: [select] only\nShould the bot buy \"Umbral Metamorphosis Scythe and Tome\" ?", false),
        new Option<bool>("87843", "Apophis Lector's Staff and Tome", "Mode: [select] only\nShould the bot buy \"Apophis Lector's Staff and Tome\" ?", false),
        new Option<bool>("89116", "Doomed Metamorphosis Knight", "Mode: [select] only\nShould the bot buy \"Doomed Metamorphosis Knight\" ?", false),
        new Option<bool>("89117", "Doomed Metamorphosis' Loyal Knight", "Mode: [select] only\nShould the bot buy \"Doomed Metamorphosis' Loyal Knight\" ?", false),
        new Option<bool>("89118", "Doomed Metamorphosis Knight Hair", "Mode: [select] only\nShould the bot buy \"Doomed Metamorphosis Knight Hair\" ?", false),
        new Option<bool>("89119", "Doomed Metamorphosis Knight Morph", "Mode: [select] only\nShould the bot buy \"Doomed Metamorphosis Knight Morph\" ?", false),
    };
}
