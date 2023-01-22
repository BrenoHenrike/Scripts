/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HollowbornLabMerge
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
        Core.BankingBlackList.AddRange(new[] { "Hollowborn Residue " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hbchallenge", 2191, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hollowborn Residue":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8996); //Hazardous Hybrid 8996
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("hbchallenge", "r5", "Left", "Chaoroot Compound", "Inert Charoot", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("74265", "Hollowborn Creator", "Mode: [select] only\nShould the bot buy \"Hollowborn Creator\" ?", false),
        new Option<bool>("74266", "Hollowborn Creator Morph", "Mode: [select] only\nShould the bot buy \"Hollowborn Creator Morph\" ?", false),
        new Option<bool>("74267", "Module 005 Morph", "Mode: [select] only\nShould the bot buy \"Module 005 Morph\" ?", false),
        new Option<bool>("74268", "Module 005 Tanks", "Mode: [select] only\nShould the bot buy \"Module 005 Tanks\" ?", false),
        new Option<bool>("74270", "Module 005 Gauntlet", "Mode: [select] only\nShould the bot buy \"Module 005 Gauntlet\" ?", false),
        new Option<bool>("69019", "Hollowborn Alchemist", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist\" ?", false),
        new Option<bool>("69020", "Hollowborn Alchemist's Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Hood\" ?", false),
        new Option<bool>("69021", "Hollowborn Alchemist's Hood + Goggles", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Hood + Goggles\" ?", false),
        new Option<bool>("69022", "Hollowborn Alchemist's Hooded Facemask", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Hooded Facemask\" ?", false),
        new Option<bool>("69023", "Hollowborn Alchemist's Protective Gear", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Protective Gear\" ?", false),
        new Option<bool>("69024", "Hollowborn Alchemist's Hair", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Hair\" ?", false),
        new Option<bool>("69025", "Hollowborn Alchemist's Morph", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Morph\" ?", false),
        new Option<bool>("69026", "Hollowborn Alchemist's Facemask", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Facemask\" ?", false),
        new Option<bool>("69027", "Hollowborn Alchemist's Gasmask", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Gasmask\" ?", false),
        new Option<bool>("69028", "Hollowborn Alchemist's Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Locks\" ?", false),
        new Option<bool>("69029", "Hollowborn Alchemist's Morph + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Morph + Locks\" ?", false),
        new Option<bool>("69030", "Hollowborn Alchemist's Facemask + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Facemask + Locks\" ?", false),
        new Option<bool>("69031", "Hollowborn Alchemist's Gasmask + Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Gasmask + Locks\" ?", false),
        new Option<bool>("69032", "Hollowborn Alchemist's Blade", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Blade\" ?", false),
        new Option<bool>("69033", "Hollowborn Alchemist's Blades", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Blades\" ?", false),
        new Option<bool>("69034", "Hollowborn Alchemist's Shrunken Head", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's Shrunken Head\" ?", false),
        new Option<bool>("69035", "Hollowborn Alchemist's BattleGear", "Mode: [select] only\nShould the bot buy \"Hollowborn Alchemist's BattleGear\" ?", false),
    };
}
