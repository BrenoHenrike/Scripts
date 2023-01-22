/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BattleConGearMerge
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
        Core.BankingBlackList.AddRange(new[] { "DeadMog LED " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("arena", 1162, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "DeadMog LED":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(4576);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("arena", "Deadmoglinster", "DeadMoglinster Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("31649", "Scarf of Ambition", "Mode: [select] only\nShould the bot buy \"Scarf of Ambition\" ?", false),
        new Option<bool>("31647", "Scarf of Wisdom", "Mode: [select] only\nShould the bot buy \"Scarf of Wisdom\" ?", false),
        new Option<bool>("31650", "Scarf of Dedication", "Mode: [select] only\nShould the bot buy \"Scarf of Dedication\" ?", false),
        new Option<bool>("31648", "Scarf of Bravery", "Mode: [select] only\nShould the bot buy \"Scarf of Bravery\" ?", false),
        new Option<bool>("31646", "Ambitious Ferret", "Mode: [select] only\nShould the bot buy \"Ambitious Ferret\" ?", false),
        new Option<bool>("31671", "Dead Moglinster", "Mode: [select] only\nShould the bot buy \"Dead Moglinster\" ?", false),
        new Option<bool>("31652", "Magenta StarBlade", "Mode: [select] only\nShould the bot buy \"Magenta StarBlade\" ?", false),
        new Option<bool>("31653", "Azure StarBlade", "Mode: [select] only\nShould the bot buy \"Azure StarBlade\" ?", false),
        new Option<bool>("31654", "Flame StarBlade", "Mode: [select] only\nShould the bot buy \"Flame StarBlade\" ?", false),
        new Option<bool>("31655", "Verde StarBlade", "Mode: [select] only\nShould the bot buy \"Verde StarBlade\" ?", false),
        new Option<bool>("31736", "Fairy Tail", "Mode: [select] only\nShould the bot buy \"Fairy Tail\" ?", false),
    };
}
