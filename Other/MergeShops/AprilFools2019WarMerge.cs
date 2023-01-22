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

public class AprilFools2019WarMerge
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
        Core.BankingBlackList.AddRange(new[] { "Punadin Badge " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("pal9001", 1709, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Punadin Badge":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("pal9001", "Baby Sharkcaster", "Punadin Badge");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48035", "Nekomancer", "Mode: [select] only\nShould the bot buy \"Nekomancer\" ?", false),
        new Option<bool>("48042", "Nekomancer Morph Locks", "Mode: [select] only\nShould the bot buy \"Nekomancer Morph Locks\" ?", false),
        new Option<bool>("48040", "Nekomancer Locks", "Mode: [select] only\nShould the bot buy \"Nekomancer Locks\" ?", false),
        new Option<bool>("48041", "Nekomancer Hood Locks", "Mode: [select] only\nShould the bot buy \"Nekomancer Hood Locks\" ?", false),
        new Option<bool>("48043", "Nekomancer Morph Hood Locks", "Mode: [select] only\nShould the bot buy \"Nekomancer Morph Hood Locks\" ?", false),
        new Option<bool>("48038", "Nekomancer Morph Hair", "Mode: [select] only\nShould the bot buy \"Nekomancer Morph Hair\" ?", false),
        new Option<bool>("48037", "Nekomancer Hood Hair", "Mode: [select] only\nShould the bot buy \"Nekomancer Hood Hair\" ?", false),
        new Option<bool>("48039", "Nekomancer Morph Hood Hair", "Mode: [select] only\nShould the bot buy \"Nekomancer Morph Hood Hair\" ?", false),
        new Option<bool>("48036", "Nekomancer Hair", "Mode: [select] only\nShould the bot buy \"Nekomancer Hair\" ?", false),
        new Option<bool>("48045", "Nekosmasher Mace", "Mode: [select] only\nShould the bot buy \"Nekosmasher Mace\" ?", false),
        new Option<bool>("48046", "Neko Spindle Staff", "Mode: [select] only\nShould the bot buy \"Neko Spindle Staff\" ?", false),
        new Option<bool>("48049", "Nekomancer Deadly Empress Cape", "Mode: [select] only\nShould the bot buy \"Nekomancer Deadly Empress Cape\" ?", false),
        new Option<bool>("48044", "Nekomancer Tail", "Mode: [select] only\nShould the bot buy \"Nekomancer Tail\" ?", false),
    };
}
