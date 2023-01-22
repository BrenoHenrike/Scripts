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

public class HallofClassesMerge
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
        Core.BankingBlackList.AddRange(new[] { "Sword and Scroll Badge " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("classhall", 1876, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Sword and Scroll Badge":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7495);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Studying the Bard 7495
                        Core.KillMonster("palooza", "Act6", "Left", "Music Pirate", "Lo-Fi Recording", 4);
                        Core.HuntMonster("Stairway", "Elwood Bruise", "Scroll: O'Carolan's Reel");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54598", "Shinobi Armor", "Mode: [select] only\nShould the bot buy \"Shinobi Armor\" ?", false),
        new Option<bool>("54599", "Shinobi X", "Mode: [select] only\nShould the bot buy \"Shinobi X\" ?", false),
        new Option<bool>("54601", "Shinobi Headband", "Mode: [select] only\nShould the bot buy \"Shinobi Headband\" ?", false),
        new Option<bool>("54603", "Shinobi Headband + Locks", "Mode: [select] only\nShould the bot buy \"Shinobi Headband + Locks\" ?", false),
        new Option<bool>("54600", "Shinobi Mask", "Mode: [select] only\nShould the bot buy \"Shinobi Mask\" ?", false),
        new Option<bool>("54602", "Shinobi Mask + Locks", "Mode: [select] only\nShould the bot buy \"Shinobi Mask + Locks\" ?", false),
        new Option<bool>("54604", "Shinobi Masked Hood", "Mode: [select] only\nShould the bot buy \"Shinobi Masked Hood\" ?", false),
        new Option<bool>("54605", "Shinobi Hood", "Mode: [select] only\nShould the bot buy \"Shinobi Hood\" ?", false),
        new Option<bool>("54675", "Shinobi Katana", "Mode: [select] only\nShould the bot buy \"Shinobi Katana\" ?", false),
        new Option<bool>("54681", "Dual Shinobi Katanas", "Mode: [select] only\nShould the bot buy \"Dual Shinobi Katanas\" ?", false),
        new Option<bool>("54586", "Battle Berserker", "Mode: [select] only\nShould the bot buy \"Battle Berserker\" ?", false),
        new Option<bool>("54587", "Blooded Berserker", "Mode: [select] only\nShould the bot buy \"Blooded Berserker\" ?", false),
        new Option<bool>("54588", "Battle Berserker Faceguard", "Mode: [select] only\nShould the bot buy \"Battle Berserker Faceguard\" ?", false),
        new Option<bool>("54589", "Battle Berserker Helmet", "Mode: [select] only\nShould the bot buy \"Battle Berserker Helmet\" ?", false),
        new Option<bool>("54590", "Battle Berserker Horned Faceguard", "Mode: [select] only\nShould the bot buy \"Battle Berserker Horned Faceguard\" ?", false),
        new Option<bool>("54591", "Battle Berserker Horned Helmet", "Mode: [select] only\nShould the bot buy \"Battle Berserker Horned Helmet\" ?", false),
        new Option<bool>("54585", "Wee Training Dummy", "Mode: [select] only\nShould the bot buy \"Wee Training Dummy\" ?", false),
    };
}
