/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DeadLinesMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoW SoW = new();


    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Unbound Thread " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("deadlines", 2169, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Unbound Thread":
                    SoW.DeadLines();
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(8869);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Fallen Branches 8869
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("DeadLines", "Frenzied Mana", "Captured Mana", 8);
                        Core.HuntMonster("DeadLines", "Shadowfall Warrior", "Armor Scrap", 8);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("DeadLines", "Eternal Dragon", "Eternal Dragon Scale");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70580", "Timestream Ravager", "Mode: [select] only\nShould the bot buy \"Timestream Ravager\" ?", false),
        new Option<bool>("70581", "Timestream Ravager's Visor", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Visor\" ?", false),
        new Option<bool>("70582", "Timestream Ravager's Hat", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Hat\" ?", false),
        new Option<bool>("70583", "Timestream Ravager's Morph", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Morph\" ?", false),
        new Option<bool>("70584", "Timestream Ravager's Morph + Hat", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Morph + Hat\" ?", false),
        new Option<bool>("70586", "Timestream Ravager's Symbol", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Symbol\" ?", false),
        new Option<bool>("70588", "Timestream Ravager's Cutlass", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Cutlass\" ?", false),
        new Option<bool>("70589", "Timestream Ravager's Cutlasses", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Cutlasses\" ?", false),
        new Option<bool>("70590", "Timestream Ravager's Dark Hook", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Dark Hook\" ?", false),
        new Option<bool>("70591", "Timestream Ravager's Dark Hooks", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Dark Hooks\" ?", false),
        new Option<bool>("70592", "Timestream Ravager's Pistol", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Pistol\" ?", false),
        new Option<bool>("70593", "Timestream Ravager's Pistols", "Mode: [select] only\nShould the bot buy \"Timestream Ravager's Pistols\" ?", false),
        new Option<bool>("72292", "Enchanted ShadowFlame Portal", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowFlame Portal\" ?", false),
    };
}
