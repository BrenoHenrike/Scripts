/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs
//cs_include Scripts/Legion/Various/LetitBurn(SoulEssence).cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class VulcarsMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreLegion Legion = new CoreLegion();
    public AnotherOneBitesTheDust SSand = new();
    public LetItBurn LetItBurn = new();


    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Soul Sand", "Legion Token", "Soul Essence", "Legion Undead Visor " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("underworld", 1985, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Soul Sand":
                    SSand.SoulSand(quant);
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Soul Essence":
                    LetItBurn.SoulEssence(quant);
                    break;

                case "Legion Undead Visor":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(7992);
                        Core.HuntMonster("dagefortress", "Grrrberus", "Grrberus' Flame");
                        SSand.SoulSand(3);
                        Core.EnsureCompleteChoose(7992, new[] { req.Name });
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("59961", "Undead Spawn Minion", "Mode: [select] only\nShould the bot buy \"Undead Spawn Minion\" ?", false),
        new Option<bool>("59962", "Undead Minion Visor", "Mode: [select] only\nShould the bot buy \"Undead Minion Visor\" ?", false),
        new Option<bool>("59968", "Legion Soul Devourer", "Mode: [select] only\nShould the bot buy \"Legion Soul Devourer\" ?", false),
        new Option<bool>("59982", "Legion Bonfire", "Mode: [select] only\nShould the bot buy \"Legion Bonfire\" ?", false),
        new Option<bool>("59959", "Legion Forge Spawn", "Mode: [select] only\nShould the bot buy \"Legion Forge Spawn\" ?", false),
        new Option<bool>("59964", "Legion Forge Visor", "Mode: [select] only\nShould the bot buy \"Legion Forge Visor\" ?", false),
        new Option<bool>("59966", "Legion Infinite Flames", "Mode: [select] only\nShould the bot buy \"Legion Infinite Flames\" ?", false),
        new Option<bool>("59965", "Legion Spawn Runes", "Mode: [select] only\nShould the bot buy \"Legion Spawn Runes\" ?", false),
        new Option<bool>("59967", "Legion Bonfire Altar", "Mode: [select] only\nShould the bot buy \"Legion Bonfire Altar\" ?", false),
        new Option<bool>("60059", "Dual Legion Soul Devourers", "Mode: [select] only\nShould the bot buy \"Dual Legion Soul Devourers\" ?", false),
    };
}
