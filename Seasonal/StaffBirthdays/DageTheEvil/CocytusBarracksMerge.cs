/*
name: Cocytus Barracks Merge
description: This bot will farm the items belonging to the selected mode for the Cocytus Barracks Merge [2421] in /cocytusbarracks
tags: cocytus, barracks, merge, cocytusbarracks, penitent, underworld, keeper, tower, bramble, blame, sullied, bangle, shield, acheron, usurper, lord, horns, halo, regalia, phantima, maiden, woe, defiler, ancients, defilers, shielded, ivory, overdriven, evocator, morph
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
//cs_include Scripts/Legion/CoreLegion.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CocytusBarracksMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDageBirthday CDB = new();
    private CoreLegion Legion = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Coin For the Dead", "Legion Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        CDB.CocytusBarracks();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("cocytusbarracks", 2421, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Coin For the Dead":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9633);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("cocytusbarracks", "Maleagant", "Aestiua Shard", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("cocytusbarracks", "Cerberus Pup", "Phlegethon Tag", 8, log: false);
                        Core.HuntMonster("cocytusbarracks", "Mourner", "Lethe Wreath", 8, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("84628", "Penitent Underworld Keeper", "Mode: [select] only\nShould the bot buy \"Penitent Underworld Keeper\" ?", false),
        new Option<bool>("84629", "Penitent Keeper Helm", "Mode: [select] only\nShould the bot buy \"Penitent Keeper Helm\" ?", false),
        new Option<bool>("84630", "Penitent Keeper Locks", "Mode: [select] only\nShould the bot buy \"Penitent Keeper Locks\" ?", false),
        new Option<bool>("84633", "Penitent Keeper Tower Helm", "Mode: [select] only\nShould the bot buy \"Penitent Keeper Tower Helm\" ?", false),
        new Option<bool>("84642", "Bramble Blame Blade", "Mode: [select] only\nShould the bot buy \"Bramble Blame Blade\" ?", false),
        new Option<bool>("84643", "Bramble Blame Blades", "Mode: [select] only\nShould the bot buy \"Bramble Blame Blades\" ?", false),
        new Option<bool>("84651", "Sullied Blame Blade and Bangle", "Mode: [select] only\nShould the bot buy \"Sullied Blame Blade and Bangle\" ?", false),
        new Option<bool>("84652", "Sullied Blame Blade and Shield", "Mode: [select] only\nShould the bot buy \"Sullied Blame Blade and Shield\" ?", false),
        new Option<bool>("84653", "Bramble Blame Blade and Shield", "Mode: [select] only\nShould the bot buy \"Bramble Blame Blade and Shield\" ?", false),
        new Option<bool>("84654", "Acheron Usurper Lord", "Mode: [select] only\nShould the bot buy \"Acheron Usurper Lord\" ?", false),
        new Option<bool>("84657", "Usurper Lord Horns", "Mode: [select] only\nShould the bot buy \"Usurper Lord Horns\" ?", false),
        new Option<bool>("84660", "Acheron Usurper Lord Halo Cape", "Mode: [select] only\nShould the bot buy \"Acheron Usurper Lord Halo Cape\" ?", false),
        new Option<bool>("84661", "Acheron Usurper Lord Regalia", "Mode: [select] only\nShould the bot buy \"Acheron Usurper Lord Regalia\" ?", false),
        new Option<bool>("84663", "Phantima, Maiden of Woe", "Mode: [select] only\nShould the bot buy \"Phantima, Maiden of Woe\" ?", false),
        new Option<bool>("84664", "Defiler of the Ancients", "Mode: [select] only\nShould the bot buy \"Defiler of the Ancients\" ?", false),
        new Option<bool>("84665", "Defilers of the Ancients", "Mode: [select] only\nShould the bot buy \"Defilers of the Ancients\" ?", false),
        new Option<bool>("84672", "Shielded Defiler of the Ancients", "Mode: [select] only\nShould the bot buy \"Shielded Defiler of the Ancients\" ?", false),
        new Option<bool>("84673", "Ivory Acheron Usurper Lord", "Mode: [select] only\nShould the bot buy \"Ivory Acheron Usurper Lord\" ?", false),
        new Option<bool>("84691", "Penitent Ivory Underworld Keeper", "Mode: [select] only\nShould the bot buy \"Penitent Ivory Underworld Keeper\" ?", false),
        new Option<bool>("84729", "Overdriven Evocator", "Mode: [select] only\nShould the bot buy \"Overdriven Evocator\" ?", false),
        new Option<bool>("84732", "Overdriven Evocator Hooded Morph", "Mode: [select] only\nShould the bot buy \"Overdriven Evocator Hooded Morph\" ?", false),
        new Option<bool>("84733", "Overdriven Evocator Hooded Visage", "Mode: [select] only\nShould the bot buy \"Overdriven Evocator Hooded Visage\" ?", false),
        new Option<bool>("84693", "Penitent Ivory Keeper Locks", "Mode: [select] only\nShould the bot buy \"Penitent Ivory Keeper Locks\" ?", false),
        new Option<bool>("84696", "Penitent Ivory Keeper Tower Helm", "Mode: [select] only\nShould the bot buy \"Penitent Ivory Keeper Tower Helm\" ?", false),
        new Option<bool>("84705", "Bramble Ivory Blame Blade", "Mode: [select] only\nShould the bot buy \"Bramble Ivory Blame Blade\" ?", false),
        new Option<bool>("84706", "Bramble Ivory Blame Blades", "Mode: [select] only\nShould the bot buy \"Bramble Ivory Blame Blades\" ?", false),
        new Option<bool>("84714", "Ivory Blame Blade and Bangle", "Mode: [select] only\nShould the bot buy \"Ivory Blame Blade and Bangle\" ?", false),
        new Option<bool>("84715", "Ivory Blame Blade and Shield", "Mode: [select] only\nShould the bot buy \"Ivory Blame Blade and Shield\" ?", false),
        new Option<bool>("84716", "Bramble Ivory Blame Blade and Shield", "Mode: [select] only\nShould the bot buy \"Bramble Ivory Blame Blade and Shield\" ?", false),
        new Option<bool>("84679", "Ivory Usurper Lord Halo Cape", "Mode: [select] only\nShould the bot buy \"Ivory Usurper Lord Halo Cape\" ?", false),
        new Option<bool>("84688", "Shielded Ivory Defiler of the Ancients", "Mode: [select] only\nShould the bot buy \"Shielded Ivory Defiler of the Ancients\" ?", false),
        new Option<bool>("84680", "Ivory Defiler of the Ancients", "Mode: [select] only\nShould the bot buy \"Ivory Defiler of the Ancients\" ?", false),
        new Option<bool>("84681", "Ivory Defilers of the Ancients", "Mode: [select] only\nShould the bot buy \"Ivory Defilers of the Ancients\" ?", false),
        new Option<bool>("84676", "Ivory Usurper Lord Horns", "Mode: [select] only\nShould the bot buy \"Ivory Usurper Lord Horns\" ?", false),
    };
}
