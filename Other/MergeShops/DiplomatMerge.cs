/*
name: Diplomat Merge
description: farms the materials and gets teh items from the diplomat merge in /castle
tags: diplomate merge, castle, swordhavenrep,
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DiplomatMerge
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
        Core.BankingBlackList.AddRange(new[] { "Elodie's Trinket"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Farm.SwordhavenREP();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("castle ", 2255, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Elodie's Trinket":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9155);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                       Core.HuntMonster("shogunwar", "Bamboo Treeant", "Bamboo Stalk", 7);
                       Core.HuntMonster("aozorahills", "Reishi", "Dried Reishi", 7);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("72035", "Swordhaven Emissary", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary\" ?", false),
        new Option<bool>("72036", "Swordhaven Emissary Hair", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Hair\" ?", false),
        new Option<bool>("72037", "Swordhaven Emissary Circlet", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Circlet\" ?", false),
        new Option<bool>("72038", "Swordhaven Emissary Locks", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Locks\" ?", false),
        new Option<bool>("72039", "Swordhaven Emissary Diadem", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Diadem\" ?", false),
        new Option<bool>("72040", "Swordhaven Emissary Hood", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Hood\" ?", false),
        new Option<bool>("72041", "Swordhaven Emissary Cowl", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Cowl\" ?", false),
        new Option<bool>("72042", "Emissary Arrow Quiver", "Mode: [select] only\nShould the bot buy \"Emissary Arrow Quiver\" ?", false),
        new Option<bool>("72043", "Swordhaven Emissary Cape", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Cape\" ?", false),
        new Option<bool>("72044", "Swordhaven Emissary Cape and Quiver", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Cape and Quiver\" ?", false),
        new Option<bool>("72045", "Swordhaven Emissary Bow", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Bow\" ?", false),
        new Option<bool>("72046", "Swordhaven Emissary Bow and Arrow", "Mode: [select] only\nShould the bot buy \"Swordhaven Emissary Bow and Arrow\" ?", false),
        new Option<bool>("77120", "Swordhaven Courtier", "Mode: [select] only\nShould the bot buy \"Swordhaven Courtier\" ?", false),
        new Option<bool>("77121", "Swordhaven Courtier Hat", "Mode: [select] only\nShould the bot buy \"Swordhaven Courtier Hat\" ?", false),
        new Option<bool>("77122", "Swordhaven Courtier Cap", "Mode: [select] only\nShould the bot buy \"Swordhaven Courtier Cap\" ?", false),
        new Option<bool>("77123", "Swordhaven Courtier Cape", "Mode: [select] only\nShould the bot buy \"Swordhaven Courtier Cape\" ?", false),
        new Option<bool>("77526", "Formal Swordhaven Courtier", "Mode: [select] only\nShould the bot buy \"Formal Swordhaven Courtier\" ?", false),
        new Option<bool>("77527", "Formal Courtier Hat", "Mode: [select] only\nShould the bot buy \"Formal Courtier Hat\" ?", false),
        new Option<bool>("77528", "Formal Courtier Cap", "Mode: [select] only\nShould the bot buy \"Formal Courtier Cap\" ?", false),
        new Option<bool>("77529", "Formal Courtier Cape", "Mode: [select] only\nShould the bot buy \"Formal Courtier Cape\" ?", false),
    };
}
