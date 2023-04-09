/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class UnderworldTeamMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreLegion Legion = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Combat Trophy", "1v1 Legion PvP Trophy" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("legionpvp", 1695, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Legion Combat Trophy":
                    Legion.DagePvP(quant, 0, 0);
                    break;

                case "1v1 Legion PvP Trophy":
                    Core.Logger("Cannot Get Item, Requires Manual pvp.");
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        //Items the bot Cannot get dueu to 1v1 pvp trophy
        // new Option<bool>("47592", "Legion Spellcaster", "Mode: [select] only\nShould the bot buy \"Legion Spellcaster\" ?", false),
        // new Option<bool>("47596", "Legion SpellCaster's Morph Locks", "Mode: [select] only\nShould the bot buy \"Legion SpellCaster's Morph Locks\" ?", false),
        // new Option<bool>("47594", "Legion SpellCaster's Mask Locks", "Mode: [select] only\nShould the bot buy \"Legion SpellCaster's Mask Locks\" ?", false),
        // new Option<bool>("47598", "Legion SpellCaster's Hair", "Mode: [select] only\nShould the bot buy \"Legion SpellCaster's Hair\" ?", false),
        // new Option<bool>("47595", "Legion SpellCaster's Locks", "Mode: [select] only\nShould the bot buy \"Legion SpellCaster's Locks\" ?", false),
        // new Option<bool>("47597", "Legion SpellCaster's Skull Morph", "Mode: [select] only\nShould the bot buy \"Legion SpellCaster's Skull Morph\" ?", false),
        // new Option<bool>("47593", "Legion SpellCaster's Spikes", "Mode: [select] only\nShould the bot buy \"Legion SpellCaster's Spikes\" ?", false),
        // new Option<bool>("47599", "Immortal Skull Morph", "Mode: [select] only\nShould the bot buy \"Immortal Skull Morph\" ?", false),
        // new Option<bool>("47600", "Dark Flame Morph", "Mode: [select] only\nShould the bot buy \"Dark Flame Morph\" ?", false),

        //Itmes the bot can get without 1v1 trophie 
        new Option<bool>("47339", "Scarred Caster's Hair", "Mode: [select] only\nShould the bot buy \"Scarred Caster's Hair\" ?", false),
        new Option<bool>("47605", "Wings of the Underworld", "Mode: [select] only\nShould the bot buy \"Wings of the Underworld\" ?", false),
        new Option<bool>("47606", "Dark Runes of the Underworld", "Mode: [select] only\nShould the bot buy \"Dark Runes of the Underworld\" ?", false),
        new Option<bool>("47334", "Scarred Caster's Locks", "Mode: [select] only\nShould the bot buy \"Scarred Caster's Locks\" ?", false),
        new Option<bool>("47611", "Burning Skull Mace", "Mode: [select] only\nShould the bot buy \"Burning Skull Mace\" ?", false),
        new Option<bool>("47602", "Legion's Fiend Helm", "Mode: [select] only\nShould the bot buy \"Legion's Fiend Helm\" ?", false),
        new Option<bool>("47607", "SpellCaster's Dark Staff", "Mode: [select] only\nShould the bot buy \"SpellCaster's Dark Staff\" ?", false),
        new Option<bool>("47609", "SpellCaster's Dark Accoutrements", "Mode: [select] only\nShould the bot buy \"SpellCaster's Dark Accoutrements\" ?", false),
        new Option<bool>("47344", "Wrath of the Legion Blade", "Mode: [select] only\nShould the bot buy \"Wrath of the Legion Blade\" ?", false),

    };
}
