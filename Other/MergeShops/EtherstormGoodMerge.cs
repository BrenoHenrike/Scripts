/*
name: Etherstorm Good Merge
description: This bot will farm the items belonging to the selected mode for the Etherstorm Good Merge [378] in /etherwargood
tags: etherstorm, good, merge, etherwargood, golden, dragoon, ultra, lance, whisper, conquerer
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EtherstormGoodMerge
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
        Core.BankingBlackList.AddRange(new[] { "Water Defender Token", "Fire Defender Token", "Earth Defender Token", "Air Defender Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("etherwargood", 378, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Water Defender Token":
                case "Fire Defender Token":
                case "Earth Defender Token":
                case "Air Defender Token":
                    if (Core.IsMember)
                    {
                        Core.FarmingLogger(req.Name, quant);
                        Core.RegisterQuests(1716, 1717);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("etherwargood", "Tainted Emu", "Twisted Emu Feather", 6, true, false);
                            Core.HuntMonster("etherwargood", "Tainted Pelican", "Twisted Pelican Feather", 6, true, false);
                            Core.HuntMonster("etherwargood", "Tainted Hummingbird", "Twisted Hummingbird Feather", 6, true, false);
                            Core.HuntMonster("etherwargood", "Tainted Phoenix", "Twisted Phoenix Feather", 6, true, false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    else
                    {
                        Core.FarmingLogger(req.Name, quant);
                        Core.RegisterQuests(1717);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("etherwargood", "Tainted Emu", "Twisted Emu Feather", 3, true, false);
                            Core.HuntMonster("etherwargood", "Tainted Pelican", "Twisted Pelican Feather", 3, true, false);
                            Core.HuntMonster("etherwargood", "Tainted Hummingbird", "Twisted Hummingbird Feather", 3, true, false);
                            Core.HuntMonster("etherwargood", "Tainted Phoenix", "Twisted Phoenix Feather", 3, true, false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("11290", "Golden Dragoon Helm", "Mode: [select] only\nShould the bot buy \"Golden Dragoon Helm\" ?", false),
        new Option<bool>("11292", "Ultra Dragoon Lance", "Mode: [select] only\nShould the bot buy \"Ultra Dragoon Lance\" ?", false),
        new Option<bool>("11267", "Whisper of the Conquerer", "Mode: [select] only\nShould the bot buy \"Whisper of the Conquerer\" ?", false),
        new Option<bool>("11316", "Golden Dragoon", "Mode: [select] only\nShould the bot buy \"Golden Dragoon\" ?", false),
    };
}
