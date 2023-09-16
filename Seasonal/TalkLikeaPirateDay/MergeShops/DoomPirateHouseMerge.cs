/*
name: DoomPirate House Merge
description: This bot will farm the items belonging to the selected mode for the DoomPirate House Merge [2331] in /doompirate
tags: doompirate, house, merge, doompirate, shadowscythe, pirate, warship, commander, gallaeon, guest, statue
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal\TalkLikeaPirateDay\DoomPirateStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DoomPirateHouseMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private DoomPirate DP = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Gallaeon's Piece of Eight", "Doom Doubloon" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DP.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("doompirate", 2331, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Gallaeon's Piece of Eight":
                    Core.Logger($"{req.Name}" + " requires ultra boss, you need to prefarm it yourself.");
                    break;

                case "Doom Doubloon":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9354);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("doompirate", "Gallaeon", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79508", "Shadowscythe Pirate Warship", "Mode: [select] only\nShould the bot buy \"Shadowscythe Pirate Warship\" ?", false),
        new Option<bool>("79414", "Commander Gallaeon House Guest", "Mode: [select] only\nShould the bot buy \"Commander Gallaeon House Guest\" ?", false),
        new Option<bool>("79623", "Commander Gallaeon Statue", "Mode: [select] only\nShould the bot buy \"Commander Gallaeon Statue\" ?", false),
    };
}
