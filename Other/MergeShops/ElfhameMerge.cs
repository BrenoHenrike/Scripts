/*
name: Elfhame Merge
description: This bot will farm the items belonging to the selected mode for the Elfhame Merge [1188] in /elfhame
tags: elfhame, merge, elfhame, ruin, stalker, pet, guardian, spirit, morph, twisted, deer, ratawampus, toxic
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ElfhameMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    private BrightOak BO = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Flower of Renewal"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        BO.AvenGreywhorl();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("elfhame", 1188, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Flower of Renewal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4669);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("elfhame", "Blighted Deer", "Deer Horn", 2, true, false);
                        Core.HuntMonster("elfhame", "Wolfrider", "Elfhame Wolf Pelt", 2, true, false);
                        Core.HuntMonster("elfhame", "Ruin Dweller", "Ruin Dweller Remains", 3, true, false);
                        Core.HuntMonster("elfhame", "Ratawampus", "Ratawampus Tail", 2, true, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("32216", "Ruin Stalker Pet", "Mode: [select] only\nShould the bot buy \"Ruin Stalker Pet\" ?", false),
        new Option<bool>("32213", "Guardian Spirit", "Mode: [select] only\nShould the bot buy \"Guardian Spirit\" ?", false),
        new Option<bool>("32214", "Guardian Spirit Morph", "Mode: [select] only\nShould the bot buy \"Guardian Spirit Morph\" ?", false),
        new Option<bool>("32219", "Guardian Spirit Sword", "Mode: [select] only\nShould the bot buy \"Guardian Spirit Sword\" ?", false),
        new Option<bool>("32218", "Twisted Deer Pet", "Mode: [select] only\nShould the bot buy \"Twisted Deer Pet\" ?", false),
        new Option<bool>("32215", "Ratawampus Pet", "Mode: [select] only\nShould the bot buy \"Ratawampus Pet\" ?", false),
        new Option<bool>("32250", "Toxic Spirit", "Mode: [select] only\nShould the bot buy \"Toxic Spirit\" ?", false),
        new Option<bool>("32251", "Toxic Spirit Morph", "Mode: [select] only\nShould the bot buy \"Toxic Spirit Morph\" ?", false),
        new Option<bool>("32252", "Toxic Spirit Sword", "Mode: [select] only\nShould the bot buy \"Toxic Spirit Sword\" ?", false),
    };
}
