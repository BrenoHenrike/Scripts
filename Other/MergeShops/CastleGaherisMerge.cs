/*
name: CastleGaheris Merge
description: This bot will farm the items belonging to the selected mode for the CastleGaheris Merge [2465] in /castlegaheris
tags: castlegaheris, merge, castlegaheris, courtly, mana, scholar, scholars, warlocks, witchs, golden, snowflake, rapier, rapiers, hoarfrost, winter, solstice, arcane, craft
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

public class CastleGaherisMerge
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
        Core.BankingBlackList.AddRange(new[] { "Gaheris Sigil", "Courtly Mana Scholar Hair", "Courtly Mana Scholar Locks", "Delicate Snowflake Rapier", "Delicate Snowflake Rapiers", "Militis Snowflake Rapier", "Militis Snowflake Rapiers", "Grimoire of Abra-Melin" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.CastleGaheris();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("castlegaheris", 2465, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Gaheris Sigil":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9829);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("castlegaheris", "Glacial Crystal", "Glacial Memory", 30, log: false);
                        Core.HuntMonster("castlegaheris", "Elemental Hybrid", "Hybrid Residue", 9, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("castlegaheris", "Thundersnow Storm", "Thundersnow Sigh", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Courtly Mana Scholar Hair":
                case "Courtly Mana Scholar Locks":
                case "Militis Snowflake Rapier":
                case "Militis Snowflake Rapiers":
                case "Grimoire of Abra-Melin":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("castlegaheris", "Thundersnow Storm", req.Name, quant, req.Temp, false);
                    break;

                case "Delicate Snowflake Rapier":
                case "Delicate Snowflake Rapiers":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("castlegaheris", "Glacial Crystal", req.Name, quant, req.Temp, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("82701", "Courtly Mana Scholar", "Mode: [select] only\nShould the bot buy \"Courtly Mana Scholar\" ?", false),
        new Option<bool>("82704", "Mana Scholar's Warlock's Hat", "Mode: [select] only\nShould the bot buy \"Mana Scholar's Warlock's Hat\" ?", false),
        new Option<bool>("82705", "Mana Scholar's Witch's Hat", "Mode: [select] only\nShould the bot buy \"Mana Scholar's Witch's Hat\" ?", false),
        new Option<bool>("82709", "Golden Snowflake Rapier", "Mode: [select] only\nShould the bot buy \"Golden Snowflake Rapier\" ?", false),
        new Option<bool>("82710", "Golden Snowflake Rapiers", "Mode: [select] only\nShould the bot buy \"Golden Snowflake Rapiers\" ?", false),
        new Option<bool>("82713", "Hoarfrost Snowflake Rapier", "Mode: [select] only\nShould the bot buy \"Hoarfrost Snowflake Rapier\" ?", false),
        new Option<bool>("82714", "Hoarfrost Snowflake Rapiers", "Mode: [select] only\nShould the bot buy \"Hoarfrost Snowflake Rapiers\" ?", false),
        new Option<bool>("82716", "Winter Solstice Staff", "Mode: [select] only\nShould the bot buy \"Winter Solstice Staff\" ?", false),
        new Option<bool>("82717", "Mana Scholar's Arcane Craft", "Mode: [select] only\nShould the bot buy \"Mana Scholar's Arcane Craft\" ?", false),
    };
}
