/*
name: Ninth Queens Treasure Merge
description: This bot will farm the items belonging to the selected mode for the Ninth Queens Treasure Merge [2666] in /meresankhchambers
tags: ninth, queens, treasure, merge, meresankhchambers, warden, serket, scorpion, priestess, veil, pontiffs, cloak, pontiff, morph, headdress, hand, hands, saccara, lapis, sabre, shield, gem, selkis, eye, golden
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NinthQueensTreasureMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private static CoreFarms Farm { get => _Farm ??= new CoreFarms(); set => _Farm = value; }
    private static CoreFarms _Farm;
    private static CoreAdvanced Adv { get => _Adv ??= new CoreAdvanced(); set => _Adv = value; }
    private static CoreAdvanced _Adv;
    private static CoreAdvanced sAdv { get => _sAdv ??= new CoreAdvanced(); set => _sAdv = value; }
    private static CoreAdvanced _sAdv;

    public bool DontPreconfigure = true;
    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;

    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[]
        {
            "Meresankh's Forbidden Gem",
            "Scorpion Pontiff Headdress",
            "Scorpion Pontiff Hair",
            "Scorpion Priestess Veil",
            "Scorpion Priestess Locks",
            "Eye of Serket",
            "Saccara Lapis Sabres",
            "Saccara Lapis Sabre"
        });

        Core.AddDrop(new[]
        {
            "Meresankh's Forbidden Gem",
            "Scorpion Pontiff Headdress",
            "Scorpion Pontiff Hair",
            "Scorpion Priestess Veil",
            "Scorpion Priestess Locks",
            "Eye of Serket",
            "Saccara Lapis Sabres",
            "Saccara Lapis Sabre"
        });

        Core.SetOptions();
        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Adv.StartBuyAllMerge("meresankhchambers", 2666, findIngredients, buyOnlyThis, buyMode: buyMode);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;

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

                case "Gold Voucher 100k":
                    Farm.Voucher(req.Name, quant);
                    break;

                case "Meresankh's Forbidden Gem":
                    if (req.Upgrade && !Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires membership to farm, skipping.");
                        return;
                    }

                    int questID = Core.IsMember ? 10545 : 10544;

                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(questID);

                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("meresankhchambers", "Queen Meresankh", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }

                    Core.CancelRegisteredQuests();
                    break;

                case "Scorpion Pontiff Headdress":
                case "Scorpion Pontiff Hair":
                case "Scorpion Priestess Veil":
                case "Scorpion Priestess Locks":
                case "Eye of Serket":
                case "Saccara Lapis Sabres":
                case "Saccara Lapis Sabre":
                    if (req.Upgrade && !Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires membership to farm, skipping.");
                        return;
                    }

                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);

                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("meresankhchambers", "Queen Meresankh", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("98378", "Warden of Serket", "Mode: [select] only\nShould the bot buy \"Warden of Serket\" ?", false),
        new Option<bool>("98379", "Scorpion Priestess Visage", "Mode: [select] only\nShould the bot buy \"Scorpion Priestess Visage\" ?", false),
        new Option<bool>("98380", "Scorpion Priestess Veil Visage", "Mode: [select] only\nShould the bot buy \"Scorpion Priestess Veil Visage\" ?", false),
        new Option<bool>("98394", "Scorpion Pontiff's Cloak", "Mode: [select] only\nShould the bot buy \"Scorpion Pontiff's Cloak\" ?", false),
        new Option<bool>("98386", "Scorpion Pontiff Morph", "Mode: [select] only\nShould the bot buy \"Scorpion Pontiff Morph\" ?", false),
        new Option<bool>("98388", "Scorpion Pontiff Headdress Morph", "Mode: [select] only\nShould the bot buy \"Scorpion Pontiff Headdress Morph\" ?", false),
        new Option<bool>("98401", "Hand of Serket", "Mode: [select] only\nShould the bot buy \"Hand of Serket\" ?", false),
        new Option<bool>("98402", "Hands of Serket", "Mode: [select] only\nShould the bot buy \"Hands of Serket\" ?", false),
        new Option<bool>("98404", "Saccara Lapis Sabre and Shield", "Mode: [select] only\nShould the bot buy \"Saccara Lapis Sabre and Shield\" ?", false),
        new Option<bool>("98405", "Gem Axe of Selkis", "Mode: [select] only\nShould the bot buy \"Gem Axe of Selkis\" ?", false),
        new Option<bool>("98395", "Eye of Serket Cloak", "Mode: [select] only\nShould the bot buy \"Eye of Serket Cloak\" ?", false),
        new Option<bool>("98397", "Golden Selkis", "Mode: [select] only\nShould the bot buy \"Golden Selkis\" ?", false),
    };
}
