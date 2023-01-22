/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ByrodaxMerge
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
        Core.BankingBlackList.AddRange(new[] { "Space Jetsam", "Space Flotsam"});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("byrodax", 1886, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Space Jetsam":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("byrodax", "r9", "Right", "Byro-Dax Monstrosity", "Space Jetsam", quant, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Space Flotsam":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("byrodax", "Security Droid", "Space Flotsam", 150, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48722", "Hyperspace Marine", "Mode: [select] only\nShould the bot buy \"Hyperspace Marine\" ?", false),
        new Option<bool>("48723", "Hyperspace Marine Helm", "Mode: [select] only\nShould the bot buy \"Hyperspace Marine Helm\" ?", false),
        new Option<bool>("48724", "Hyperspace Marine Hairstyle", "Mode: [select] only\nShould the bot buy \"Hyperspace Marine Hairstyle\" ?", false),
        new Option<bool>("48725", "Hyperspace Marine Cape", "Mode: [select] only\nShould the bot buy \"Hyperspace Marine Cape\" ?", false),
        new Option<bool>("48726", "Hyperspace Marine Sword", "Mode: [select] only\nShould the bot buy \"Hyperspace Marine Sword\" ?", false),
        new Option<bool>("48728", "Hyperspace Marine Sword + Shield", "Mode: [select] only\nShould the bot buy \"Hyperspace Marine Sword + Shield\" ?", false),
        new Option<bool>("54902", "Tiny Goo'd Rubber Treeant", "Mode: [select] only\nShould the bot buy \"Tiny Goo'd Rubber Treeant\" ?", false),
    };
}
