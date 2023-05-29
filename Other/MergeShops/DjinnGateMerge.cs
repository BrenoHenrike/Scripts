/*
name: Djinn Gate Merge
description: This bot will farm the items belonging to the selected mode for the Djinn Gate Merge [1511] in /djinngate
tags: djinn, gate, merge, djinngate, jafar, valthos, zulars, circlet, sigil, aya, ayas, tails, agares, morph
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DjinnGate.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DjinnGateMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    private DjinnGateStory DG = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Unseen Essence"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DG.DjinnGate();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("djinngate", 1511, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Unseen Essence":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6162);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("djinngate", "Harpy", "Potent Harpy Mana", 2, true, false);
                        Core.HuntMonster("djinngate", "Lamia", "Potent Lamia Mana", 2, true, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("42420", "Jafar Armor", "Mode: [select] only\nShould the bot buy \"Jafar Armor\" ?", false),
        new Option<bool>("42423", "Helm of Jafar", "Mode: [select] only\nShould the bot buy \"Helm of Jafar\" ?", false),
        new Option<bool>("42398", "Armor of Valthos", "Mode: [select] only\nShould the bot buy \"Armor of Valthos\" ?", false),
        new Option<bool>("42400", "Helm of Valthos", "Mode: [select] only\nShould the bot buy \"Helm of Valthos\" ?", false),
        new Option<bool>("42401", "Runes of Valthos", "Mode: [select] only\nShould the bot buy \"Runes of Valthos\" ?", false),
        new Option<bool>("42405", "Zular's Circlet", "Mode: [select] only\nShould the bot buy \"Zular's Circlet\" ?", false),
        new Option<bool>("42406", "Zular's Helm", "Mode: [select] only\nShould the bot buy \"Zular's Helm\" ?", false),
        new Option<bool>("42404", "Zular's Sigil Cape", "Mode: [select] only\nShould the bot buy \"Zular's Sigil Cape\" ?", false),
        new Option<bool>("42415", "Armor of Aya", "Mode: [select] only\nShould the bot buy \"Armor of Aya\" ?", false),
        new Option<bool>("42418", "Aya's Helm", "Mode: [select] only\nShould the bot buy \"Aya's Helm\" ?", false),
        new Option<bool>("42416", "Aya's Tails", "Mode: [select] only\nShould the bot buy \"Aya's Tails\" ?", false),
        new Option<bool>("42409", "Agares' Armor", "Mode: [select] only\nShould the bot buy \"Agares' Armor\" ?", false),
        new Option<bool>("42412", "Agares Morph", "Mode: [select] only\nShould the bot buy \"Agares Morph\" ?", false),
        new Option<bool>("42413", "Agares' Helm", "Mode: [select] only\nShould the bot buy \"Agares' Helm\" ?", false),
        new Option<bool>("42410", "Runes of Agares", "Mode: [select] only\nShould the bot buy \"Runes of Agares\" ?", false),
    };
}
