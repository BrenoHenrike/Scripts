/*
name: Lore Trek Merge
description: This will get all or selected items on this merge shop.
tags: lore-trek-merge, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LoreTrekMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreFriday13th F13 = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "LoreTrek Token", "Matted Dust Bunny" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        F13.Wormhole();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("wormhole", 1250, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "LoreTrek Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("wormhole", "r2", "Left", "*", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Matted Dust Bunny":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5067);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("wormhole", "r5", "Left", "Blue Trobbolier", "Blue Trobbolier Fluff", 4);
                        Core.KillMonster("wormhole", "r8", "Left", "Purple Trobbolier", "Purple Trobbolier Fluff", 4);
                        Core.KillMonster("wormhole", "r8", "Left", "Green Trobbolier", "Green Trobbolier Fluff", 4);
                        Core.KillMonster("wormhole", "r5", "Left", "Red Trobbolier", "Red Trobbolier Fluff", 4);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34979", "Cybernetic Space Marine", "Mode: [select] only\nShould the bot buy \"Cybernetic Space Marine\" ?", false),
        new Option<bool>("34980", "Cybernetic Space Marine Mask", "Mode: [select] only\nShould the bot buy \"Cybernetic Space Marine Mask\" ?", false),
        new Option<bool>("34981", "Damaged Space Marine Mask", "Mode: [select] only\nShould the bot buy \"Damaged Space Marine Mask\" ?", false),
        new Option<bool>("34984", "Bald Space Marine", "Mode: [select] only\nShould the bot buy \"Bald Space Marine\" ?", false),
        new Option<bool>("34983", "Cybernetic Space Slicer", "Mode: [select] only\nShould the bot buy \"Cybernetic Space Slicer\" ?", false),
        new Option<bool>("34982", "Space Slicer Cape", "Mode: [select] only\nShould the bot buy \"Space Slicer Cape\" ?", false),
        new Option<bool>("35001", "Stormslasher", "Mode: [select] only\nShould the bot buy \"Stormslasher\" ?", false),
        new Option<bool>("35002", "Stormslasher Helm", "Mode: [select] only\nShould the bot buy \"Stormslasher Helm\" ?", false),
        new Option<bool>("35003", "Stormslasher Stick", "Mode: [select] only\nShould the bot buy \"Stormslasher Stick\" ?", false),
        new Option<bool>("34998", "SwarmTrooper", "Mode: [select] only\nShould the bot buy \"SwarmTrooper\" ?", false),
        new Option<bool>("34999", "Swarm Trooper Helm", "Mode: [select] only\nShould the bot buy \"Swarm Trooper Helm\" ?", false),
        new Option<bool>("35000", "SwarmTrooper Stick", "Mode: [select] only\nShould the bot buy \"SwarmTrooper Stick\" ?", false),
        new Option<bool>("40268", "Trob-NOM NOM NOM", "Mode: [select] only\nShould the bot buy \"Trob-NOM NOM NOM\" ?", false),
        new Option<bool>("40267", "Trobbo-Locks", "Mode: [select] only\nShould the bot buy \"Trobbo-Locks\" ?", false),
        new Option<bool>("34978", "Cursed Alien", "Mode: [select] only\nShould the bot buy \"Cursed Alien\" ?", false),
        new Option<bool>("35040", "Swarmobite Helm", "Mode: [select] only\nShould the bot buy \"Swarmobite Helm\" ?", false),
    };
}
