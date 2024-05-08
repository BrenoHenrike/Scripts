/*
name: Deimos Den Rewards Merge
description: This bot will farm the items belonging to the selected mode for the Deimos Den Rewards Merge [2418] in /legionarena
tags: deimos, den, rewards, merge, legionarena, soul, eater, underworld, master, combat, cursed, grudge, nagamaki, apocryphal, ankle, shackle, shackles, asphodel, hunters, bow, devastator, claw, claws, arena, provocator, horned, retiarius, trident, arrow
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion\CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DeimosDenRewardsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreLegion Legion = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Bone Sigil", "Legion Token", "Deimos' Chain", "Cursed Grudge Nagamaki", "Deimos' Fang", "Death Badge" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("deimos", 2418, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Bone Sigil":
                    Legion.BoneSigil(quant);
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Deimos' Chain":
                case "Cursed Grudge Nagamaki":
                case "Deimos' Fang":
                    Core.FarmingLogger(req.Name, quant);
                    Core.Logger($"{req.Name} requires you to kill Devastator Deimos which skua can't do, use army.");
                    break;

                case "Death Badge":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("legionarena", "Legion Fiend Rider", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("5006", "Soul Eater", "Mode: [select] only\nShould the bot buy \"Soul Eater\" ?", false),
        new Option<bool>("70028", "Underworld Master Rune", "Mode: [select] only\nShould the bot buy \"Underworld Master Rune\" ?", false),
        new Option<bool>("70029", "Underworld Combat Rune", "Mode: [select] only\nShould the bot buy \"Underworld Combat Rune\" ?", false),
        new Option<bool>("70031", "Cursed Dual Grudge Nagamaki", "Mode: [select] only\nShould the bot buy \"Cursed Dual Grudge Nagamaki\" ?", false),
        new Option<bool>("73639", "Apocryphal Underworld Dagger", "Mode: [select] only\nShould the bot buy \"Apocryphal Underworld Dagger\" ?", false),
        new Option<bool>("73640", "Apocryphal Underworld Daggers", "Mode: [select] only\nShould the bot buy \"Apocryphal Underworld Daggers\" ?", false),
        new Option<bool>("84195", "Underworld Ankle Shackle", "Mode: [select] only\nShould the bot buy \"Underworld Ankle Shackle\" ?", false),
        new Option<bool>("84196", "Underworld Ankle Shackles", "Mode: [select] only\nShould the bot buy \"Underworld Ankle Shackles\" ?", false),
        new Option<bool>("84197", "Asphodel Hunter's Bow", "Mode: [select] only\nShould the bot buy \"Asphodel Hunter's Bow\" ?", false),
        new Option<bool>("84309", "Devastator Deimos Claw", "Mode: [select] only\nShould the bot buy \"Devastator Deimos Claw\" ?", false),
        new Option<bool>("84310", "Devastator Deimos Claws", "Mode: [select] only\nShould the bot buy \"Devastator Deimos Claws\" ?", false),
        new Option<bool>("84374", "Underworld Arena Provocator", "Mode: [select] only\nShould the bot buy \"Underworld Arena Provocator\" ?", false),
        new Option<bool>("84375", "Horned Underworld Provocator Helm", "Mode: [select] only\nShould the bot buy \"Horned Underworld Provocator Helm\" ?", false),
        new Option<bool>("84376", "Underworld Provocator Helm", "Mode: [select] only\nShould the bot buy \"Underworld Provocator Helm\" ?", false),
        new Option<bool>("84377", "Underworld Provocator Cape", "Mode: [select] only\nShould the bot buy \"Underworld Provocator Cape\" ?", false),
        new Option<bool>("84378", "Underworld Retiarius Trident", "Mode: [select] only\nShould the bot buy \"Underworld Retiarius Trident\" ?", false),
        new Option<bool>("84388", "Asphodel Hunter's Bow And Arrow", "Mode: [select] only\nShould the bot buy \"Asphodel Hunter's Bow And Arrow\" ?", false),
    };
}
