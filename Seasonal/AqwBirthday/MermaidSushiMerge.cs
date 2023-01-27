/*
name: Mermaid Sushi Merge
description: This will get all or selected items on this merge shop.
tags: mermaid-sushi-merge, seasonal, aqw-anniversary
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MermaidSushiMerge
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
        Core.BankingBlackList.AddRange(new[] { "Scarbucks Gift Card " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Bot.Quests.UpdateQuest(8892);
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("mermaidsushi", 2178, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Scarbucks Gift Card":
                    Core.EquipClass(ClassType.Solo);
                    Core.KillMonster("mermaidsushi", "r7a", "Left", "*", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("72957", "Coffeemancer", "Mode: [select] only\nShould the bot buy \"Coffeemancer\" ?", false),
        new Option<bool>("72958", "Barista Beard", "Mode: [select] only\nShould the bot buy \"Barista Beard\" ?", false),
        new Option<bool>("72960", "Barista Bun and Glasses", "Mode: [select] only\nShould the bot buy \"Barista Bun and Glasses\" ?", false),
        new Option<bool>("72963", "Coffee Guzzler", "Mode: [select] only\nShould the bot buy \"Coffee Guzzler\" ?", false),
        new Option<bool>("72964", "Zorbak the Coffee Fiend", "Mode: [select] only\nShould the bot buy \"Zorbak the Coffee Fiend\" ?", false),
        new Option<bool>("72965", "Scarbucks Carpet", "Mode: [select] only\nShould the bot buy \"Scarbucks Carpet\" ?", false),
        new Option<bool>("72968", "Scarbucks Barista Gear", "Mode: [select] only\nShould the bot buy \"Scarbucks Barista Gear\" ?", false),
        new Option<bool>("72969", "Scarbucks Tray", "Mode: [select] only\nShould the bot buy \"Scarbucks Tray\" ?", false),
    };
}
