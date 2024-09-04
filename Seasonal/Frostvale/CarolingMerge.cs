/*
name: Caroling Merge
description: This bot will farm the items belonging to the selected mode for the Caroling Merge [2197] in /caroltown
tags: caroling, merge, caroltown, midwinter, cheermaker, cutie, antlered, beanie, snowflakes, snowy, nimbo, licorice, candy, cane, darkwood, carved, armaments, northlands, paladin, frozen, paladins, glacial, light, destiny, ascended, aurum, wings, noble, leo, scion, scions, requiem, heraldic, lion, companion, frostval, party, favor, gifts, holiday, hoodie, , morph, holly, twilly, twig, zorbak
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CarolingMerge
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
        Core.BankingBlackList.AddRange(new[] { "Red Ribbon", "Wrapping Paper", "Icy Fur", "Silver Tinsel", "Aurum Wings Blade", "Jingle Bells", "Brunswick Leo Scion", "Brunswick Leo's Requiem", "Brunswick Leo Scion Cane", "Spearmint Candy Cane", "100 Pound Gift", "Chill Hoodie Outfit", "Chill Hat + Hair", "Chill Hat + Locks", "Chill Hat Morph", "Chill Hat Visage" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("caroltown", 2197, findIngredients, buyOnlyThis, buyMode: buyMode);

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
            #endregion Dont edit this part

            switch (req.Name)
            {
                case "Aurum Wings Blade":
                case "Brunswick Leo Scion":
                case "Brunswick Leo's Requiem":
                case "Brunswick Leo Scion Cane":
                case "Spearmint Candy Cane":
                case "100 Pound Gift":
                case "Chill Hoodie Outfit":
                case "Chill Hat + Hair":
                case "Chill Hat + Locks":
                case "Chill Hat Visage":
                case "Chill Hat Morph":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonsterMapID("caroltown", 6, req.Name, quant, false);
                    break;

                case "Red Ribbon":
                case "Silver Tinsel":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Join("whitemap");
                        Core.Join("caroling");

                        for (int killCount = 0; killCount < 3 && !Bot.ShouldExit; killCount++)
                        {
                            Bot.Kill.Monster(1);

                            Core.Logger($"Kill: {killCount + 1}/3, {(killCount < 2 ? "Swapping Map at 3" : "Swapping map to respawn mob")}");
                            Bot.Wait.ForMonsterSpawn(1);
                        }

                        Core.Join("whitemap");
                        Core.Join("caroling");
                    }

                    break;

                case "Jingle Bells":
                    Core.EquipClass(ClassType.Farm);
                    // Jingle Spells - 9520
                    Core.RegisterQuests(9520);
                    Core.HuntMonster("caroltown", "Frostval Deer", req.Name, quant, false);
                    Bot.Wait.ForPickup(req.ID);
                    Core.CancelRegisteredQuests();
                    break;

                case "Icy Fur":
                case "Wrapping Paper":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("caroltown", "Krumpet", req.Name, quant, false);
                    break;

                default:
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("75005", "Midwinter Cheermaker", "Mode: [select] only\nShould the bot buy \"Midwinter Cheermaker\" ?", false),
        new Option<bool>("75006", "Midwinter Cutie Hat", "Mode: [select] only\nShould the bot buy \"Midwinter Cutie Hat\" ?", false),
        new Option<bool>("75007", "Midwinter Cutie Hat and Locks", "Mode: [select] only\nShould the bot buy \"Midwinter Cutie Hat and Locks\" ?", false),
        new Option<bool>("75008", "Midwinter Antlered Hat", "Mode: [select] only\nShould the bot buy \"Midwinter Antlered Hat\" ?", false),
        new Option<bool>("75009", "Midwinter Antlered Hat and Locks", "Mode: [select] only\nShould the bot buy \"Midwinter Antlered Hat and Locks\" ?", false),
        new Option<bool>("75010", "Midwinter Beanie", "Mode: [select] only\nShould the bot buy \"Midwinter Beanie\" ?", false),
        new Option<bool>("75011", "Midwinter Beanie and Locks", "Mode: [select] only\nShould the bot buy \"Midwinter Beanie and Locks\" ?", false),
        new Option<bool>("75012", "Midwinter Snowflakes", "Mode: [select] only\nShould the bot buy \"Midwinter Snowflakes\" ?", false),
        new Option<bool>("75013", "Midwinter Cutie Cape", "Mode: [select] only\nShould the bot buy \"Midwinter Cutie Cape\" ?", false),
        new Option<bool>("75014", "Midwinter Snowy Nimbo", "Mode: [select] only\nShould the bot buy \"Midwinter Snowy Nimbo\" ?", false),
        new Option<bool>("75015", "Licorice Candy Cane", "Mode: [select] only\nShould the bot buy \"Licorice Candy Cane\" ?", false),
        new Option<bool>("75016", "Darkwood Carved Armaments", "Mode: [select] only\nShould the bot buy \"Darkwood Carved Armaments\" ?", false),
        new Option<bool>("73885", "Northlands Paladin", "Mode: [select] only\nShould the bot buy \"Northlands Paladin\" ?", false),
        new Option<bool>("73887", "Frozen Paladin's Helm", "Mode: [select] only\nShould the bot buy \"Frozen Paladin's Helm\" ?", false),
        new Option<bool>("73890", "Glacial Light of Destiny", "Mode: [select] only\nShould the bot buy \"Glacial Light of Destiny\" ?", false),
        new Option<bool>("69478", "Ascended Aurum Wings Blade", "Mode: [select] only\nShould the bot buy \"Ascended Aurum Wings Blade\" ?", false),
        new Option<bool>("79340", "Noble Leo Scion", "Mode: [select] only\nShould the bot buy \"Noble Leo Scion\" ?", false),
        new Option<bool>("79342", "Noble Leo Scion Hair", "Mode: [select] only\nShould the bot buy \"Noble Leo Scion Hair\" ?", false),
        new Option<bool>("79343", "Noble Leo Scion Locks", "Mode: [select] only\nShould the bot buy \"Noble Leo Scion Locks\" ?", false),
        new Option<bool>("79344", "Leo Scion's Requiem", "Mode: [select] only\nShould the bot buy \"Leo Scion's Requiem\" ?", false),
        new Option<bool>("79346", "Heraldic Lion Companion", "Mode: [select] only\nShould the bot buy \"Heraldic Lion Companion\" ?", false),
        new Option<bool>("79347", "Noble Leo Scion Cane", "Mode: [select] only\nShould the bot buy \"Noble Leo Scion Cane\" ?", false),
        new Option<bool>("82548", "Frostval Party Favor", "Mode: [select] only\nShould the bot buy \"Frostval Party Favor\" ?", false),
        new Option<bool>("82560", "Frostval Party Gifts", "Mode: [select] only\nShould the bot buy \"Frostval Party Gifts\" ?", false),
        new Option<bool>("82778", "Northlands Holiday Hoodie", "Mode: [select] only\nShould the bot buy \"Northlands Holiday Hoodie\" ?", false),
        new Option<bool>("82779", "Northlands Holiday Hat", "Mode: [select] only\nShould the bot buy \"Northlands Holiday Hat\" ?", false),
        new Option<bool>("82780", "Northlands Holiday Hat + Locks", "Mode: [select] only\nShould the bot buy \"Northlands Holiday Hat + Locks\" ?", false),
        new Option<bool>("82781", "Northlands Holiday Morph", "Mode: [select] only\nShould the bot buy \"Northlands Holiday Morph\" ?", false),
        new Option<bool>("82782", "Northlands Holiday Visage", "Mode: [select] only\nShould the bot buy \"Northlands Holiday Visage\" ?", false),
        new Option<bool>("82788", "Holly Holiday Twilly", "Mode: [select] only\nShould the bot buy \"Holly Holiday Twilly\" ?", false),
        new Option<bool>("82789", "Holly Holiday Twig", "Mode: [select] only\nShould the bot buy \"Holly Holiday Twig\" ?", false),
        new Option<bool>("82790", "Holly Holiday Zorbak", "Mode: [select] only\nShould the bot buy \"Holly Holiday Zorbak\" ?", false),
    };
}