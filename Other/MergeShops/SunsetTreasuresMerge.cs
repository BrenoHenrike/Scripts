/*
name: Sunset Treasures Merge
description: This bot will farm the items belonging to the selected mode for the Sunset Treasures Merge [2451] in /sunsetdunes
tags: sunset, treasures, merge, sunsetdunes, phoenixs, glory, golden, phoenix, wings, fury, great, firebirds, spear, immortal, in, flames
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SunsetTreasuresMerge
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
        Core.BankingBlackList.AddRange(new[] { "Anqa's Feather", "Glowing Ember", "Golden Firebird's Spear", "Golden Firebird's Blade", "Golden Firebird's Blades", "Miniature Phoenix Guest" });
        if (Bot.ShowMessageBox("this script requires you to kill an ultra, so just run this on like 7 accounds... continue?", "**WARNING**", true) is not true)
            Bot.Stop();

        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sunsetdunes", 2451, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Anqa's Feather":
                case "Glowing Ember":
                case "Golden Firebird's Spear":
                case "Golden Firebird's Blade":
                case "Golden Firebird's Blades":
                case "Miniature Phoenix Guest":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    //incase u get teh otehr drops along the way vvv
                    Core.AddDrop(new[] { "Glowing Ember", "Golden Firebird's Spear", "Golden Firebird's Blade", "Golden Firebird's Blades" });
                    Core.RegisterQuests(9752);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("sunsetdunes", "Anqa", req.Name, quant, req.Temp);
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("86290", "Phoenix's Glory", "Mode: [select] only\nShould the bot buy \"Phoenix's Glory\" ?", false),
        new Option<bool>("86292", "Phoenix's Glory Helm", "Mode: [select] only\nShould the bot buy \"Phoenix's Glory Helm\" ?", false),
        new Option<bool>("86298", "Golden Phoenix's Glory Blade", "Mode: [select] only\nShould the bot buy \"Golden Phoenix's Glory Blade\" ?", false),
        new Option<bool>("86300", "Golden Phoenix's Glory Blades", "Mode: [select] only\nShould the bot buy \"Golden Phoenix's Glory Blades\" ?", false),
        new Option<bool>("86294", "Golden Phoenix Wings", "Mode: [select] only\nShould the bot buy \"Golden Phoenix Wings\" ?", false),
        new Option<bool>("86291", "Phoenix's Fury", "Mode: [select] only\nShould the bot buy \"Phoenix's Fury\" ?", false),
        new Option<bool>("86293", "Phoenix's Fury Helm", "Mode: [select] only\nShould the bot buy \"Phoenix's Fury Helm\" ?", false),
        new Option<bool>("86297", "Golden Phoenix's Fury Blade", "Mode: [select] only\nShould the bot buy \"Golden Phoenix's Fury Blade\" ?", false),
        new Option<bool>("86299", "Golden Phoenix's Fury Blades", "Mode: [select] only\nShould the bot buy \"Golden Phoenix's Fury Blades\" ?", false),
        new Option<bool>("86295", "Golden Phoenix Great Wings", "Mode: [select] only\nShould the bot buy \"Golden Phoenix Great Wings\" ?", false),
        new Option<bool>("86279", "Firebird's Fury Spear", "Mode: [select] only\nShould the bot buy \"Firebird's Fury Spear\" ?", false),
        new Option<bool>("86281", "Firebird's Fury Blade", "Mode: [select] only\nShould the bot buy \"Firebird's Fury Blade\" ?", false),
        new Option<bool>("86306", "Firebird's Fury Blades", "Mode: [select] only\nShould the bot buy \"Firebird's Fury Blades\" ?", false),
        new Option<bool>("86302", "Immortal Phoenix in Flames", "Mode: [select] only\nShould the bot buy \"Immortal Phoenix in Flames\" ?", false),
    };
}
