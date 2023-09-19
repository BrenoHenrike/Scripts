/*
name: Iara Merge
description: This bot will farm the items belonging to the selected mode for the Iara Merge [2047] in /iara
tags: iara, merge, iara, iaras, morph, symbol, bubbles, harp, acolyte, acolytes, flowered, coral, brush, water, orb
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\ObrigadoBrasil\IaraStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class IaraMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Iara I = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Keeper of the Amazon", "Iara Insignia" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        I.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("iara", 2047, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Keeper of the Amazon":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8261);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("iara", "Iara");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Iara Insignia":
                    Core.Logger($"{req.Name}" + " requires ultra boss, you need to farm it manually.");
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("62821", "Iara's Armor", "Mode: [select] only\nShould the bot buy \"Iara's Armor\" ?", false),
        new Option<bool>("62822", "Iara's Morph", "Mode: [select] only\nShould the bot buy \"Iara's Morph\" ?", false),
        new Option<bool>("62823", "Symbol of Iara", "Mode: [select] only\nShould the bot buy \"Symbol of Iara\" ?", false),
        new Option<bool>("63162", "Iara's Bubbles", "Mode: [select] only\nShould the bot buy \"Iara's Bubbles\" ?", false),
        new Option<bool>("63132", "Iara's Harp", "Mode: [select] only\nShould the bot buy \"Iara's Harp\" ?", false),
        new Option<bool>("63156", "Iara's Acolyte", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte\" ?", false),
        new Option<bool>("63157", "Iara's Acolyte's Hair", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte's Hair\" ?", false),
        new Option<bool>("63158", "Iara's Acolyte's Morph Hair", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte's Morph Hair\" ?", false),
        new Option<bool>("63159", "Iara's Acolyte's Flowered Locks", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte's Flowered Locks\" ?", false),
        new Option<bool>("63160", "Iara's Acolyte's Flowered Morph Locks", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte's Flowered Morph Locks\" ?", false),
        new Option<bool>("63161", "Iara's Acolyte's Locks", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte's Locks\" ?", false),
        new Option<bool>("63163", "Iara's Coral", "Mode: [select] only\nShould the bot buy \"Iara's Coral\" ?", false),
        new Option<bool>("63164", "Iara's Acolyte's Cape", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte's Cape\" ?", false),
        new Option<bool>("63166", "Iara's Brush", "Mode: [select] only\nShould the bot buy \"Iara's Brush\" ?", false),
        new Option<bool>("63167", "Iara's Water Orb", "Mode: [select] only\nShould the bot buy \"Iara's Water Orb\" ?", false),
        new Option<bool>("63165", "Iara's Acolyte's Staff", "Mode: [select] only\nShould the bot buy \"Iara's Acolyte's Staff\" ?", false),
    };
}
