/*
name: Dark Birthday Token Merge
description: This bot will farm materials in /darkbirthday and buy items from the merge shop
tags: legion, merge, staff, birthday, dage, evil, seasonal, event, darkbirthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DarkBirthdayTokenMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "Obsidian Rock" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("darkbirthday"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("darkbirthday", 1697, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Obsidian Rock":
                    Legion.ObsidianRock(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("28635", "Legion DoomKnight Armor", "Mode: [select] only\nShould the bot buy \"Legion DoomKnight Armor\" ?", false),
        new Option<bool>("28636", "Legion DoomKnight Helm", "Mode: [select] only\nShould the bot buy \"Legion DoomKnight Helm\" ?", false),
        new Option<bool>("28637", "Legion DoomBlade", "Mode: [select] only\nShould the bot buy \"Legion DoomBlade\" ?", false),
        new Option<bool>("28686", "Ultimate Dark Caster", "Mode: [select] only\nShould the bot buy \"Ultimate Dark Caster\" ?", false),
        new Option<bool>("28688", "Mystic Dark Caster Spikes", "Mode: [select] only\nShould the bot buy \"Mystic Dark Caster Spikes\" ?", false),
        new Option<bool>("28690", "Mystica Dark Caster Locks", "Mode: [select] only\nShould the bot buy \"Mystica Dark Caster Locks\" ?", false),
        new Option<bool>("53839", "Legion SwordMaster Assassin", "Mode: [select] only\nShould the bot buy \"Legion SwordMaster Assassin\" ?", false),
        new Option<bool>("38741", "Legion DoomKnight", "Mode: [select] only\nShould the bot buy \"Legion DoomKnight\" ?", false),
        new Option<bool>("42801", "Exalted Harbinger", "Mode: [select] only\nShould the bot buy \"Exalted Harbinger\" ?", false),
        new Option<bool>("43048", "Envy Armor", "Mode: [select] only\nShould the bot buy \"Envy Armor\" ?", false),
        new Option<bool>("47465", "Infinite Legion Dark Caster", "Mode: [select] only\nShould the bot buy \"Infinite Legion Dark Caster\" ?", false),
        new Option<bool>("47551", "Legion's Fiend", "Mode: [select] only\nShould the bot buy \"Legion's Fiend\" ?", false),
        new Option<bool>("47552", "Legion Fiend's Crested Helm", "Mode: [select] only\nShould the bot buy \"Legion Fiend's Crested Helm\" ?", false),
        new Option<bool>("47553", "Nation's Paragon Helm", "Mode: [select] only\nShould the bot buy \"Nation's Paragon Helm\" ?", false),
        new Option<bool>("47554", "Legion's Fiend Skull Helm", "Mode: [select] only\nShould the bot buy \"Legion's Fiend Skull Helm\" ?", false),
        new Option<bool>("47348", "Legion Sigil Pet", "Mode: [select] only\nShould the bot buy \"Legion Sigil Pet\" ?", false),
        new Option<bool>("41597", "Paragon of Loyalty", "Mode: [select] only\nShould the bot buy \"Paragon of Loyalty\" ?", false),
        new Option<bool>("41598", "Paragon of Loyalty Horns", "Mode: [select] only\nShould the bot buy \"Paragon of Loyalty Horns\" ?", false),
        new Option<bool>("41599", "Paragon of Loyalty Hood", "Mode: [select] only\nShould the bot buy \"Paragon of Loyalty Hood\" ?", false),
        new Option<bool>("41600", "Paragon of Loyalty Cloak", "Mode: [select] only\nShould the bot buy \"Paragon of Loyalty Cloak\" ?", false),
        new Option<bool>("41601", "Paragon of Loyalty Daggers", "Mode: [select] only\nShould the bot buy \"Paragon of Loyalty Daggers\" ?", false),
        new Option<bool>("41602", "Paragon of Loyalty Blade", "Mode: [select] only\nShould the bot buy \"Paragon of Loyalty Blade\" ?", false),
        new Option<bool>("41603", "Paragon of Loyalty Accoutrements", "Mode: [select] only\nShould the bot buy \"Paragon of Loyalty Accoutrements\" ?", false),
        new Option<bool>("53550", "Legion Marauder", "Mode: [select] only\nShould the bot buy \"Legion Marauder\" ?", false),
        new Option<bool>("53551", "Legion Scout", "Mode: [select] only\nShould the bot buy \"Legion Scout\" ?", false),
        new Option<bool>("53552", "Legion Scout's Hood", "Mode: [select] only\nShould the bot buy \"Legion Scout's Hood\" ?", false),
        new Option<bool>("53553", "Legion Marauder's Hood", "Mode: [select] only\nShould the bot buy \"Legion Marauder's Hood\" ?", false),
        new Option<bool>("53554", "Legion Scout's Skull Hood", "Mode: [select] only\nShould the bot buy \"Legion Scout's Skull Hood\" ?", false),
        new Option<bool>("53555", "Legion Marauder's Skull Hood", "Mode: [select] only\nShould the bot buy \"Legion Marauder's Skull Hood\" ?", false),
        new Option<bool>("53556", "Legion Scout's Skull Mask", "Mode: [select] only\nShould the bot buy \"Legion Scout's Skull Mask\" ?", false),
        new Option<bool>("53557", "Legion Marauder's Skull Mask", "Mode: [select] only\nShould the bot buy \"Legion Marauder's Skull Mask\" ?", false),
        new Option<bool>("53558", "Legion Scout's Skull Morph", "Mode: [select] only\nShould the bot buy \"Legion Scout's Skull Morph\" ?", false),
        new Option<bool>("53559", "Legion Marauder's Skull Morph", "Mode: [select] only\nShould the bot buy \"Legion Marauder's Skull Morph\" ?", false),
        new Option<bool>("53560", "Legion Scout's Cloak", "Mode: [select] only\nShould the bot buy \"Legion Scout's Cloak\" ?", false),
        new Option<bool>("53561", "Legion Marauder's Back Sword", "Mode: [select] only\nShould the bot buy \"Legion Marauder's Back Sword\" ?", false),
        new Option<bool>("53562", "Legion Marauder's Cloak + Sword", "Mode: [select] only\nShould the bot buy \"Legion Marauder's Cloak + Sword\" ?", false),
        new Option<bool>("53563", "Legion Marauder's Sword", "Mode: [select] only\nShould the bot buy \"Legion Marauder's Sword\" ?", false),
        new Option<bool>("53564", "Legion Scout's Sword", "Mode: [select] only\nShould the bot buy \"Legion Scout's Sword\" ?", false),
        new Option<bool>("53595", "Ultra FiendHunter", "Mode: [select] only\nShould the bot buy \"Ultra FiendHunter\" ?", false),
        new Option<bool>("53596", "Vhall's Dystopia", "Mode: [select] only\nShould the bot buy \"Vhall's Dystopia\" ?", false),
        new Option<bool>("53597", "Ultra FiendHunter's Skull Armet", "Mode: [select] only\nShould the bot buy \"Ultra FiendHunter's Skull Armet\" ?", false),
        new Option<bool>("53598", "Rheyl's Helm", "Mode: [select] only\nShould the bot buy \"Rheyl's Helm\" ?", false),
        new Option<bool>("53603", "Rheyl's Edge", "Mode: [select] only\nShould the bot buy \"Rheyl's Edge\" ?", false),
        new Option<bool>("53604", "Vhall's Violence", "Mode: [select] only\nShould the bot buy \"Vhall's Violence\" ?", false),
        new Option<bool>("53599", "Vhall's Cape", "Mode: [select] only\nShould the bot buy \"Vhall's Cape\" ?", false),
        new Option<bool>("53600", "Vhall's Cape + Sword", "Mode: [select] only\nShould the bot buy \"Vhall's Cape + Sword\" ?", false),
        new Option<bool>("53591", "Legion Sage Petite Morph", "Mode: [select] only\nShould the bot buy \"Legion Sage Petite Morph\" ?", false),
        new Option<bool>("53592", "Legion Sage Morph", "Mode: [select] only\nShould the bot buy \"Legion Sage Morph\" ?", false),
        new Option<bool>("53593", "Legion Sage's Disciple", "Mode: [select] only\nShould the bot buy \"Legion Sage's Disciple\" ?", false),
        new Option<bool>("53594", "Legion Sage's Curse", "Mode: [select] only\nShould the bot buy \"Legion Sage's Curse\" ?", false),
        new Option<bool>("53652", "Legion Devourer of Souls Scythe", "Mode: [select] only\nShould the bot buy \"Legion Devourer of Souls Scythe\" ?", false),
        new Option<bool>("53647", "Legion Devourer of Souls Blade", "Mode: [select] only\nShould the bot buy \"Legion Devourer of Souls Blade\" ?", false),
        new Option<bool>("53648", "Activated Legion Devourer of Souls Blade", "Mode: [select] only\nShould the bot buy \"Activated Legion Devourer of Souls Blade\" ?", false),
        new Option<bool>("53663", "Legion Devourer of Souls Blade Pet", "Mode: [select] only\nShould the bot buy \"Legion Devourer of Souls Blade Pet\" ?", false),
        new Option<bool>("53655", "Dark Devourer of Souls Scythe", "Mode: [select] only\nShould the bot buy \"Dark Devourer of Souls Scythe\" ?", false),
        new Option<bool>("53642", "Devourer of Souls Mask", "Mode: [select] only\nShould the bot buy \"Devourer of Souls Mask\" ?", false),
        new Option<bool>("59723", "Legion Operative", "Mode: [select] only\nShould the bot buy \"Legion Operative\" ?", false),
        new Option<bool>("59724", "Legion Operative Hood", "Mode: [select] only\nShould the bot buy \"Legion Operative Hood\" ?", false),
        new Option<bool>("59725", "Legion Operative Hood + Locks", "Mode: [select] only\nShould the bot buy \"Legion Operative Hood + Locks\" ?", false),
        new Option<bool>("59727", "Operative's Locks", "Mode: [select] only\nShould the bot buy \"Operative's Locks\" ?", false),
        new Option<bool>("59728", "Operative's Morph", "Mode: [select] only\nShould the bot buy \"Operative's Morph\" ?", false),
        new Option<bool>("59729", "Operative's Shadow", "Mode: [select] only\nShould the bot buy \"Operative's Shadow\" ?", false),
        new Option<bool>("59730", "Legion Operative's Gun", "Mode: [select] only\nShould the bot buy \"Legion Operative's Gun\" ?", false),
        new Option<bool>("59731", "Dual Legion Operative's Guns", "Mode: [select] only\nShould the bot buy \"Dual Legion Operative's Guns\" ?", false),
    };
}
