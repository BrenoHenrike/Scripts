/*
name: Zorbak's Merge Shop
description: This will get all or selected items on this merge shop.
tags: zorbaks-merge, seasonal, aqw-anniversary
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ZorbaksMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Copper Scale", "Gold Scale", "Platinum Scale", "Onyx Scale", "Hero Plushie", "Chaorrupted Button", "Cursed Pinata Candy", "Tinfoil Wrapper", "Knight Armor", "Knight Armet", "Knight Sallet", "Knight Great Helm", "Knight Cloak", "Knight Mace", "Knight Spear", "Knight Zweihander " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("birthday"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battleon", 1640, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Copper Scale":
                case "Gold Scale":
                case "Platinum Scale":
                case "Onyx Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);

                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        // 14th Anniversary Gifts 6554
                        Core.EnsureAccept(6554);

                        Core.HuntMonster("birthday", "Birthday Cake", "Top Cherry");
                        Core.HuntMonster("birthday", "Birthday Cake", "Copper Knife");
                        Core.HuntMonster("birthday", "Birthday Cake", "Platinum Fork");
                        Core.HuntMonster("birthday", "Birthday Cake", "Gold Spoon");

                        Core.EnsureComplete(6554, req.ID);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;


                case "Hero Plushie":
                case "Chaorrupted Button":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("birthday", "Birthday Cake", req.Name, quant, isTemp: false);
                    break;

                case "Cursed Pinata Candy":
                case "Tinfoil Wrapper":
                case "Knight Armet":
                case "Knight Armor":
                case "Knight Sallet":
                case "Knight Cloak":
                case "Knight Mace":
                case "Knight Spear":
                case "Knight Zweihander":
                case "Knight Great Helm":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("birthday", "Twilly Pinata", req.Name, quant, isTemp: false);
                    break;


            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("45297", "Copper Dragon Statue (R)", "Mode: [select] only\nShould the bot buy \"Copper Dragon Statue (R)\" ?", false),
        new Option<bool>("45298", "Gold Dragon Statue (R)", "Mode: [select] only\nShould the bot buy \"Gold Dragon Statue (R)\" ?", false),
        new Option<bool>("45299", "Platinum Dragon Statue (R)", "Mode: [select] only\nShould the bot buy \"Platinum Dragon Statue (R)\" ?", false),
        new Option<bool>("45300", "Copper Sepulchure Statue (R)", "Mode: [select] only\nShould the bot buy \"Copper Sepulchure Statue (R)\" ?", false),
        new Option<bool>("45301", "Gold Sepulchure Statue (R)", "Mode: [select] only\nShould the bot buy \"Gold Sepulchure Statue (R)\" ?", false),
        new Option<bool>("45302", "Platinum Sepulchure Statue (R)", "Mode: [select] only\nShould the bot buy \"Platinum Sepulchure Statue (R)\" ?", false),
        new Option<bool>("45304", "Copper Dragon Statue (L)", "Mode: [select] only\nShould the bot buy \"Copper Dragon Statue (L)\" ?", false),
        new Option<bool>("45305", "Gold Dragon Statue (L)", "Mode: [select] only\nShould the bot buy \"Gold Dragon Statue (L)\" ?", false),
        new Option<bool>("45306", "Platinum Dragon Statue (L)", "Mode: [select] only\nShould the bot buy \"Platinum Dragon Statue (L)\" ?", false),
        new Option<bool>("45307", "Copper Sepulchure Statue (L)", "Mode: [select] only\nShould the bot buy \"Copper Sepulchure Statue (L)\" ?", false),
        new Option<bool>("45308", "Gold Sepulchure Statue (L)", "Mode: [select] only\nShould the bot buy \"Gold Sepulchure Statue (L)\" ?", false),
        new Option<bool>("45309", "Platinum Sepulchure Statue (L)", "Mode: [select] only\nShould the bot buy \"Platinum Sepulchure Statue (L)\" ?", false),
        new Option<bool>("55418", "Royal Shadowslayer", "Mode: [select] only\nShould the bot buy \"Royal Shadowslayer\" ?", false),
        new Option<bool>("55419", "Royal Shadowslayer's Cap + Locks", "Mode: [select] only\nShould the bot buy \"Royal Shadowslayer's Cap + Locks\" ?", false),
        new Option<bool>("55420", "Royal Shadowslayer's Cap", "Mode: [select] only\nShould the bot buy \"Royal Shadowslayer's Cap\" ?", false),
        new Option<bool>("55421", "Royal Shadowslayer's Coat", "Mode: [select] only\nShould the bot buy \"Royal Shadowslayer's Coat\" ?", false),
        new Option<bool>("55422", "Elite Shadowslayer", "Mode: [select] only\nShould the bot buy \"Elite Shadowslayer\" ?", false),
        new Option<bool>("55424", "Elite Shadowslayer Locks", "Mode: [select] only\nShould the bot buy \"Elite Shadowslayer Locks\" ?", false),
        new Option<bool>("55426", "Elite Shadowslayer Hair", "Mode: [select] only\nShould the bot buy \"Elite Shadowslayer Hair\" ?", false),
        new Option<bool>("55414", "Royal Exalted ShadowSlayer", "Mode: [select] only\nShould the bot buy \"Royal Exalted ShadowSlayer\" ?", false),
        new Option<bool>("55415", "Royal Exalted ShadowSlayer Cap + Locks", "Mode: [select] only\nShould the bot buy \"Royal Exalted ShadowSlayer Cap + Locks\" ?", false),
        new Option<bool>("55416", "Royal Exalted ShadowSlayer Cap", "Mode: [select] only\nShould the bot buy \"Royal Exalted ShadowSlayer Cap\" ?", false),
        new Option<bool>("55417", "Royal Exalted ShadowSlayer Coat", "Mode: [select] only\nShould the bot buy \"Royal Exalted ShadowSlayer Coat\" ?", false),
        new Option<bool>("55423", "Royal Honor ShadowSlayer", "Mode: [select] only\nShould the bot buy \"Royal Honor ShadowSlayer\" ?", false),
        new Option<bool>("55425", "Royal Shadowslayer Locks + Glasses", "Mode: [select] only\nShould the bot buy \"Royal Shadowslayer Locks + Glasses\" ?", false),
        new Option<bool>("55427", "Royal ShadowSlayer Glasses", "Mode: [select] only\nShould the bot buy \"Royal ShadowSlayer Glasses\" ?", false),
        new Option<bool>("64552", "Escherion Plushie Mace", "Mode: [select] only\nShould the bot buy \"Escherion Plushie Mace\" ?", false),
        new Option<bool>("64553", "Xang Plushie Mace", "Mode: [select] only\nShould the bot buy \"Xang Plushie Mace\" ?", false),
        new Option<bool>("64554", "Vath Plushie Mace", "Mode: [select] only\nShould the bot buy \"Vath Plushie Mace\" ?", false),
        new Option<bool>("64555", "Kitsune Plushie Mace", "Mode: [select] only\nShould the bot buy \"Kitsune Plushie Mace\" ?", false),
        new Option<bool>("64556", "Wolfwing Plushie Mace", "Mode: [select] only\nShould the bot buy \"Wolfwing Plushie Mace\" ?", false),
        new Option<bool>("64557", "Kimberly Plushie Mace", "Mode: [select] only\nShould the bot buy \"Kimberly Plushie Mace\" ?", false),
        new Option<bool>("64558", "Ledgermayne Plushie Mace", "Mode: [select] only\nShould the bot buy \"Ledgermayne Plushie Mace\" ?", false),
        new Option<bool>("64559", "Tibicenas Plushie Mace", "Mode: [select] only\nShould the bot buy \"Tibicenas Plushie Mace\" ?", false),
        new Option<bool>("64560", "Khasaanda Plushie Mace", "Mode: [select] only\nShould the bot buy \"Khasaanda Plushie Mace\" ?", false),
        new Option<bool>("64561", "Iadoa Plushie Mace", "Mode: [select] only\nShould the bot buy \"Iadoa Plushie Mace\" ?", false),
        new Option<bool>("64562", "LionFang Plushie Mace", "Mode: [select] only\nShould the bot buy \"LionFang Plushie Mace\" ?", false),
        new Option<bool>("64563", "Alteon Plushie Mace", "Mode: [select] only\nShould the bot buy \"Alteon Plushie Mace\" ?", false),
        new Option<bool>("64564", "Drakath Plushie Mace", "Mode: [select] only\nShould the bot buy \"Drakath Plushie Mace\" ?", false),
        new Option<bool>("64565", "Xing and Xang Plushies Dagger", "Mode: [select] only\nShould the bot buy \"Xing and Xang Plushies Dagger\" ?", false),
        new Option<bool>("64598", "Xing Plushie Mace", "Mode: [select] only\nShould the bot buy \"Xing Plushie Mace\" ?", false),
        new Option<bool>("54839", "Gilded Knight Armor", "Mode: [select] only\nShould the bot buy \"Gilded Knight Armor\" ?", false),
        new Option<bool>("54840", "Gilded Knight Armet", "Mode: [select] only\nShould the bot buy \"Gilded Knight Armet\" ?", false),
        new Option<bool>("54841", "Haloed Gilded Knight Armet", "Mode: [select] only\nShould the bot buy \"Haloed Gilded Knight Armet\" ?", false),
        new Option<bool>("54842", "Gilded Knight Sallet", "Mode: [select] only\nShould the bot buy \"Gilded Knight Sallet\" ?", false),
        new Option<bool>("54843", "Gilded Knight Grand Sallet", "Mode: [select] only\nShould the bot buy \"Gilded Knight Grand Sallet\" ?", false),
        new Option<bool>("54844", "Gilded Knight Great Helm", "Mode: [select] only\nShould the bot buy \"Gilded Knight Great Helm\" ?", false),
        new Option<bool>("54845", "Gilded Knight Cloak", "Mode: [select] only\nShould the bot buy \"Gilded Knight Cloak\" ?", false),
        new Option<bool>("54846", "Gilded Knight Mace", "Mode: [select] only\nShould the bot buy \"Gilded Knight Mace\" ?", false),
        new Option<bool>("54847", "Gilded Knight Spear", "Mode: [select] only\nShould the bot buy \"Gilded Knight Spear\" ?", false),
        new Option<bool>("54848", "Gilded Knight Zweihander", "Mode: [select] only\nShould the bot buy \"Gilded Knight Zweihander\" ?", false),
        new Option<bool>("54849", "Dread Knight Armor", "Mode: [select] only\nShould the bot buy \"Dread Knight Armor\" ?", false),
        new Option<bool>("54850", "Dread Knight Armet", "Mode: [select] only\nShould the bot buy \"Dread Knight Armet\" ?", false),
        new Option<bool>("54851", "Haloed Dread Knight Armet", "Mode: [select] only\nShould the bot buy \"Haloed Dread Knight Armet\" ?", false),
        new Option<bool>("54852", "Dread Knight Sallet", "Mode: [select] only\nShould the bot buy \"Dread Knight Sallet\" ?", false),
        new Option<bool>("54853", "Draconic Dread Knight Sallet", "Mode: [select] only\nShould the bot buy \"Draconic Dread Knight Sallet\" ?", false),
        new Option<bool>("54854", "Dread Knight Great Helm", "Mode: [select] only\nShould the bot buy \"Dread Knight Great Helm\" ?", false),
        new Option<bool>("54855", "Dread Knight Cloak", "Mode: [select] only\nShould the bot buy \"Dread Knight Cloak\" ?", false),
        new Option<bool>("54856", "Dread Knight Mace", "Mode: [select] only\nShould the bot buy \"Dread Knight Mace\" ?", false),
        new Option<bool>("54857", "Dread Knight Spear", "Mode: [select] only\nShould the bot buy \"Dread Knight Spear\" ?", false),
        new Option<bool>("54858", "Dread Knight Zweihander", "Mode: [select] only\nShould the bot buy \"Dread Knight Zweihander\" ?", false),
        new Option<bool>("63973", "Chrono Raven", "Mode: [select] only\nShould the bot buy \"Chrono Raven\" ?", false),
        new Option<bool>("63975", "Chrono Sneevil", "Mode: [select] only\nShould the bot buy \"Chrono Sneevil\" ?", false),
    };
}
