/*
name: Cysero Merge
description: This bot will farm the items belonging to the selected mode for the Cysero Merge [668] in /battleontown
tags: cysero, merge, battleontown, mad, magic, manawalker, mana, green, sockatana, living, yogurt, warrior, berry, tasty, gilded, rainbow, wrap
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CyseroMerge
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
    //  If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Glowing Sock" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battleontown", 668, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Glowing Sock":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(2777);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5, true, false);
                        Core.HuntMonster("greenguardwest", "Wolf", "Furry Lost Sock", 2, true, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("18396", "Mad Magic Manawalker", "Mode: [select] only\nShould the bot buy \"Mad Magic Manawalker\" ?", false),
        new Option<bool>("18397", "Mad Magic Mana Helm", "Mode: [select] only\nShould the bot buy \"Mad Magic Mana Helm\" ?", false),
        new Option<bool>("18421", "Green Sockatana", "Mode: [select] only\nShould the bot buy \"Green Sockatana\" ?", false),
        new Option<bool>("18434", "Living Yogurt Warrior", "Mode: [select] only\nShould the bot buy \"Living Yogurt Warrior\" ?", false),
        new Option<bool>("18439", "Berry Tasty Helm", "Mode: [select] only\nShould the bot buy \"Berry Tasty Helm\" ?", false),
        new Option<bool>("44109", "Gilded Rainbow Wrap", "Mode: [select] only\nShould the bot buy \"Gilded Rainbow Wrap\" ?", false),
    };
}
