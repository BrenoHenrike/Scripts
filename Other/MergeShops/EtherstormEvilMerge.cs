/*
name: Etherstorm Evil Merge
description: This bot will farm the items belonging to the selected mode for the Etherstorm Evil Merge [382] in /etherwarevil
tags: etherstorm, evil, merge, etherwarevil, terragons, shout, rippled, bone, jagged, kill, dark, water
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EtherstormEvilMerge
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
        Core.BankingBlackList.AddRange(new[] { "Dark Earth Token", "Dark Water Token", "Dark Fire Token", "Dark Air Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("etherwarevil", 382, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dark Earth Token":
                case "Dark Water Token":
                case "Dark Fire Token":
                case "Dark Air Token":
                    if (Core.IsMember)
                    {
                        Core.FarmingLogger(req.Name, quant);
                        Core.RegisterQuests(1718, 1719);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("etherwarevil", "Tainted Emu", "Twisted Emu Feather", 6, true, false);
                            Core.HuntMonster("etherwarevil", "Tainted Pelican", "Twisted Pelican Feather", 6, true, false);
                            Core.HuntMonster("etherwarevil", "Tainted Hummingbird", "Twisted Hummingbird Feather", 6, true, false);
                            Core.HuntMonster("etherwarevil", "Tainted Phoenix", "Twisted Phoenix Feather", 6, true, false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    else
                    {
                        Core.FarmingLogger(req.Name, quant);
                        Core.RegisterQuests(1719);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("etherwarevil", "Tainted Emu", "Twisted Emu Feather", 3, true, false);
                            Core.HuntMonster("etherwarevil", "Tainted Pelican", "Twisted Pelican Feather", 3, true, false);
                            Core.HuntMonster("etherwarevil", "Tainted Hummingbird", "Twisted Hummingbird Feather", 3, true, false);
                            Core.HuntMonster("etherwarevil", "Tainted Phoenix", "Twisted Phoenix Feather", 3, true, false);
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
        new Option<bool>("11306", "Terragon's Shout", "Mode: [select] only\nShould the bot buy \"Terragon's Shout\" ?", false),
        new Option<bool>("11269", "Rippled Bone Sword", "Mode: [select] only\nShould the bot buy \"Rippled Bone Sword\" ?", false),
        new Option<bool>("11266", "Jagged Kill", "Mode: [select] only\nShould the bot buy \"Jagged Kill\" ?", false),
        new Option<bool>("11315", "Dark Water", "Mode: [select] only\nShould the bot buy \"Dark Water\" ?", false),
    };
}
