/*
name: Forgotten Tombs Merge
description: This bot will farm the items belonging to the selected mode for the Forgotten Tombs Merge [1124] in /legioncrypt
tags: forgotten, tombs, merge, legioncrypt, seraphic, grave, digger, tools, accoutrements, pistol, jade, mages, cursed, sorcerers, underworld
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ForgottenTombsMerge
{
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreAdvanced Adv = new();
    private readonly static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private readonly bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Ancient Sigil", "Broken Staff" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("legioncrypt", 1124, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ancient Sigil":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(4196, 4197);
                    Core.KillMonster("legioncrypt", "r3", "Top", "*", req.Name, quant, isTemp: false, log: false);
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

                case "Broken Staff":
                    Dictionary<string, (string, ClassType)> staffPieces = new()
                        {
                            {"Gravedigger", ("1st Piece of the Staff", ClassType.Farm)},
                            {"Undead Infantry", ("2nd Piece of the Staff", ClassType.Farm)},
                            {"Legion Doomknight", ("3rd Piece of the Staff", ClassType.Farm)},
                            {"Brutus", ("4th Piece of the Staff", ClassType.Solo)}
                        };

                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(9664);
                        foreach (var kvp in staffPieces)
                        {
                            if (Core.CheckInventory(req.Name, quant))
                                break;

                            Core.EquipClass(kvp.Value.Item2);
                            Core.KillMonster("legioncrypt", kvp.Key != "Brutus" ? "r3" : "r9", kvp.Key != "Brutus" ? "Top" : "Bottom", kvp.Key != "Brutus" ? "*" : kvp.Key, kvp.Value.Item1);
                            Bot.Wait.ForPickup(kvp.Value.Item1);
                        }
                        Core.EnsureComplete(9664);
                    }
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("29567", "Seraphic Grave Digger", "Mode: [select] only\nShould the bot buy \"Seraphic Grave Digger\" ?", false),
        new Option<bool>("29568", "Grave Digger Tools", "Mode: [select] only\nShould the bot buy \"Grave Digger Tools\" ?", false),
        new Option<bool>("29569", "Grave Digger Accoutrements", "Mode: [select] only\nShould the bot buy \"Grave Digger Accoutrements\" ?", false),
        new Option<bool>("29570", "Seraphic Pistol", "Mode: [select] only\nShould the bot buy \"Seraphic Pistol\" ?", false),
        new Option<bool>("29571", "Seraphic Grave Digger Hat", "Mode: [select] only\nShould the bot buy \"Seraphic Grave Digger Hat\" ?", false),
        new Option<bool>("29572", "Mask of the Grave Digger", "Mode: [select] only\nShould the bot buy \"Mask of the Grave Digger\" ?", false),
        new Option<bool>("29573", "Seraphic Grave Digger Mask", "Mode: [select] only\nShould the bot buy \"Seraphic Grave Digger Mask\" ?", false),
        new Option<bool>("25315", "Jade Mage's Staff", "Mode: [select] only\nShould the bot buy \"Jade Mage's Staff\" ?", false),
        new Option<bool>("85026", "Cursed Sorcerer’s Underworld Staff", "Mode: [select] only\nShould the bot buy \"Cursed Sorcerer’s Underworld Staff\" ?", false),
    };
}
