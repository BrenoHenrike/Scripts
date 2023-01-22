/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DarkbloodWarMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreQOM QOM = new();
    public static CoreAdvanced sAdv = new();
    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Darkblood War Medal " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        QOM.CompleteEverything();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("kolyaban", 1420, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Darkblood War Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5874, 5875); //Acolyte's Medallions 5874, Acolyte's Mega Medallions 5875
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("kolyaban", "r2", "Left", "*", "Acolyte's Medallion", 4);
                        Core.KillMonster("kolyaban", "r2", "Left", "*", "Acolyte's Mega Medallion", 2);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("40270", "Eremon's Armor", "Mode: [select] only\nShould the bot buy \"Eremon's Armor\" ?", false),
        new Option<bool>("40154", "Anti-Kolyaban Armor", "Mode: [select] only\nShould the bot buy \"Anti-Kolyaban Armor\" ?", false),
        new Option<bool>("40156", "Anti-Kolyaban Helm", "Mode: [select] only\nShould the bot buy \"Anti-Kolyaban Helm\" ?", false),
        new Option<bool>("40157", "Anti-Kolyaban Cape", "Mode: [select] only\nShould the bot buy \"Anti-Kolyaban Cape\" ?", false),
        new Option<bool>("40155", "Anti-Kolyaban Trident", "Mode: [select] only\nShould the bot buy \"Anti-Kolyaban Trident\" ?", false),
        new Option<bool>("40091", "Reshaper", "Mode: [select] only\nShould the bot buy \"Reshaper\" ?", false),
        new Option<bool>("40092", "Reshaper Sword", "Mode: [select] only\nShould the bot buy \"Reshaper Sword\" ?", false),
        new Option<bool>("40277", "Reshaper Swords", "Mode: [select] only\nShould the bot buy \"Reshaper Swords\" ?", false),
        new Option<bool>("40093", "Reshaper Hair", "Mode: [select] only\nShould the bot buy \"Reshaper Hair\" ?", false),
        new Option<bool>("40094", "Reshaper Helm", "Mode: [select] only\nShould the bot buy \"Reshaper Helm\" ?", false),
        new Option<bool>("40095", "Horned Reshaper Helm", "Mode: [select] only\nShould the bot buy \"Horned Reshaper Helm\" ?", false),
        new Option<bool>("40096", "Reshaper Hood", "Mode: [select] only\nShould the bot buy \"Reshaper Hood\" ?", false),
    };
}
