/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/SuperDeath.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SuperDeathMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public SuperDeath SuperDeath = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Fame Token", "Yergen's HeroSmash Trophy " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SuperDeath.StoryLine();
        Adv.StartBuyAllMerge("superdeath", 1992, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Fame Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8033);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("superdeath", "Cave Yeti|Rider", "Normal Monsters Defeated", 5);
                        Core.HuntMonster("superdeath", "Shadow Mutant|Shadow Scorpion", "Shadow Monsters Defeated", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Yergen's HeroSmash Trophy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("superdeath", "Super Death", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("60589", "Ultimate Evil Armor", "Mode: [select] only\nShould the bot buy \"Ultimate Evil Armor\" ?", false),
        new Option<bool>("60590", "Ultimate Evil Helm", "Mode: [select] only\nShould the bot buy \"Ultimate Evil Helm\" ?", false),
        new Option<bool>("60586", "Ultimate Evil Sword", "Mode: [select] only\nShould the bot buy \"Ultimate Evil Sword\" ?", false),
        new Option<bool>("60585", "Ultimate Evil Scythe", "Mode: [select] only\nShould the bot buy \"Ultimate Evil Scythe\" ?", false),
        new Option<bool>("60591", "Ultimate Good Armor", "Mode: [select] only\nShould the bot buy \"Ultimate Good Armor\" ?", false),
        new Option<bool>("60592", "Ultimate Good Helm", "Mode: [select] only\nShould the bot buy \"Ultimate Good Helm\" ?", false),
        new Option<bool>("60593", "Ultimate Good Poleaxe", "Mode: [select] only\nShould the bot buy \"Ultimate Good Poleaxe\" ?", false),
        new Option<bool>("60594", "Ultimate Good Sword", "Mode: [select] only\nShould the bot buy \"Ultimate Good Sword\" ?", false),
        new Option<bool>("60637", "Little Green Alien", "Mode: [select] only\nShould the bot buy \"Little Green Alien\" ?", false),
        new Option<bool>("60598", "Little Green Alien Morph", "Mode: [select] only\nShould the bot buy \"Little Green Alien Morph\" ?", false),
        new Option<bool>("60600", "Crashlanded Autopsy Morph", "Mode: [select] only\nShould the bot buy \"Crashlanded Autopsy Morph\" ?", false),
        new Option<bool>("60604", "Goblin Head", "Mode: [select] only\nShould the bot buy \"Goblin Head\" ?", false),
        new Option<bool>("60596", "Braindy Head", "Mode: [select] only\nShould the bot buy \"Braindy Head\" ?", false),
        new Option<bool>("60597", "Kitty Helm", "Mode: [select] only\nShould the bot buy \"Kitty Helm\" ?", false),
        new Option<bool>("60640", "Pencil", "Mode: [select] only\nShould the bot buy \"Pencil\" ?", false),
        new Option<bool>("60639", "Jumbo Pencil", "Mode: [select] only\nShould the bot buy \"Jumbo Pencil\" ?", false),
        new Option<bool>("60638", "Ebiltime Chatbomb", "Mode: [select] only\nShould the bot buy \"Ebiltime Chatbomb\" ?", false),
        new Option<bool>("60630", "Crusader's Blade", "Mode: [select] only\nShould the bot buy \"Crusader's Blade\" ?", false),
        new Option<bool>("60607", "Yergen's Award of Yergen", "Mode: [select] only\nShould the bot buy \"Yergen's Award of Yergen\" ?", false),
        new Option<bool>("60608", "Yergen's Award of Awards", "Mode: [select] only\nShould the bot buy \"Yergen's Award of Awards\" ?", false),
    };
}
