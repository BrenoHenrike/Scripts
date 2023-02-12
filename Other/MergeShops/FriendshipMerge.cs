/*
name: Friendship Merge
description: This script will farm materials to buy items from the Friendship Merge shop.
tags: friendship,merge,battleodium
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Friendship.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FriendshipMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Friendship Fr = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Faded Pigment" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Fr.CompleteStory();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battleodium", 2236, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Faded Pigment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.AddDrop("Roses", "Strawberries", "Rubies");
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9107);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("battleodium", "Widowing", "Roses", 1, false, false);
                        Core.KillMonster("battleodium", "r6", "Left", "*", "Strawberries", 1, false, false);
                        while (!Bot.ShouldExit && !Core.CheckInventory(76286)) ///multiple items with name "Rubies"
                            Core.KillMonster("battleodium", "r6", "Left", "*", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Grapes":
                case "Diamonds":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battleodium", "r6", "Left", "*", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76337", "Shadow Rouge Armor", "Mode: [select] only\nShould the bot buy \"Shadow Rouge Armor\" ?", false),
        new Option<bool>("76338", "Shadowed Love Hat", "Mode: [select] only\nShould the bot buy \"Shadowed Love Hat\" ?", false),
        new Option<bool>("76341", "Dark Axe of Friendship", "Mode: [select] only\nShould the bot buy \"Dark Axe of Friendship\" ?", false),
        new Option<bool>("76342", "Violent Axe of Friendship", "Mode: [select] only\nShould the bot buy \"Violent Axe of Friendship\" ?", false),
        new Option<bool>("76343", "Ardent Axe of Friendship", "Mode: [select] only\nShould the bot buy \"Ardent Axe of Friendship\" ?", false),
        new Option<bool>("76344", "Passionate Axe of Friendship", "Mode: [select] only\nShould the bot buy \"Passionate Axe of Friendship\" ?", false),
        new Option<bool>("76242", "Lovely Deity", "Mode: [select] only\nShould the bot buy \"Lovely Deity\" ?", false),
        new Option<bool>("76243", "Love Thyself Outfit", "Mode: [select] only\nShould the bot buy \"Love Thyself Outfit\" ?", false),
        new Option<bool>("76244", "Lovely Deity's Hair", "Mode: [select] only\nShould the bot buy \"Lovely Deity's Hair\" ?", false),
        new Option<bool>("76245", "Lovely Deity's Locks", "Mode: [select] only\nShould the bot buy \"Lovely Deity's Locks\" ?", false),
        new Option<bool>("76246", "Lovely Deity's Cascade", "Mode: [select] only\nShould the bot buy \"Lovely Deity's Cascade\" ?", false),
        new Option<bool>("76247", "Lovely Deity Winged Halo", "Mode: [select] only\nShould the bot buy \"Lovely Deity Winged Halo\" ?", false),
        new Option<bool>("76248", "Lovely Deity Wings", "Mode: [select] only\nShould the bot buy \"Lovely Deity Wings\" ?", false),
        new Option<bool>("76249", "Lovely Deity Winglets", "Mode: [select] only\nShould the bot buy \"Lovely Deity Winglets\" ?", false),
        new Option<bool>("76250", "Dark Lovely Deity Wings", "Mode: [select] only\nShould the bot buy \"Dark Lovely Deity Wings\" ?", false),
        new Option<bool>("76253", "Lovely Deity's Bow", "Mode: [select] only\nShould the bot buy \"Lovely Deity's Bow\" ?", false),
        new Option<bool>("76347", "Blessed Lovely Deity Hair", "Mode: [select] only\nShould the bot buy \"Blessed Lovely Deity Hair\" ?", false),
        new Option<bool>("76345", "Dark Lovely Deity", "Mode: [select] only\nShould the bot buy \"Dark Lovely Deity\" ?", false),
        new Option<bool>("76350", "Dark Lovely Deity Winglets", "Mode: [select] only\nShould the bot buy \"Dark Lovely Deity Winglets\" ?", false),
        new Option<bool>("76351", "Dark Blessed Deity Wings", "Mode: [select] only\nShould the bot buy \"Dark Blessed Deity Wings\" ?", false),
        new Option<bool>("76352", "Dark Lovely Deity's Bow", "Mode: [select] only\nShould the bot buy \"Dark Lovely Deity's Bow\" ?", false),
    };
}
