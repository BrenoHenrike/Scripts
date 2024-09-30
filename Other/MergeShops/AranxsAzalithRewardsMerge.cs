/*
name: Aranxs Azalith Rewards Merge
description: This bot will farm the items belonging to the selected mode for the Aranxs Azalith Rewards Merge [2488] in /apexazalith
tags: aranxs, azalith, rewards, merge, apexazalith, akhas, wings, akha, companion, spear, shield, celestial, wanderer, annals, runic, choir, wanderers, sceptre, scythe, infernal, maahnas, robes, freed
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AranxsAzalithRewardsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Infernalis Penna", "Infernalis Oculus", "Divina Voluntas" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("apexazalith", 2488, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Infernalis Penna":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonsterQuest(9887, new (string? mapName, string? monsterName, ClassType classType)[]
                        {
                            ("champazalith", "Maah-na", ClassType.Solo),
                            ("champazalith", "Akh-a", ClassType.Solo)
                        });
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Infernalis Oculus":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonsterQuest(9888, new (string? mapName, string? monsterName, ClassType classType)[]
                        {
                            ("champazalith", "Azalith", ClassType.Solo)
                        });
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Divina Voluntas":
                    Farm.Experience(80);
                    Core.Logger($"{req.Name} cannot be farmed solo, use army.");
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("88586", "Akh-a's Wings", "Mode: [select] only\nShould the bot buy \"Akh-a's Wings\" ?", false),
        new Option<bool>("88587", "Akh-a Companion", "Mode: [select] only\nShould the bot buy \"Akh-a Companion\" ?", false),
        new Option<bool>("88588", "Akh-a's Spear", "Mode: [select] only\nShould the bot buy \"Akh-a's Spear\" ?", false),
        new Option<bool>("88589", "Akh-a's Spear and Shield", "Mode: [select] only\nShould the bot buy \"Akh-a's Spear and Shield\" ?", false),
        new Option<bool>("88590", "Celestial Wanderer", "Mode: [select] only\nShould the bot buy \"Celestial Wanderer\" ?", false),
        new Option<bool>("88591", "Celestial Wanderer Hair", "Mode: [select] only\nShould the bot buy \"Celestial Wanderer Hair\" ?", false),
        new Option<bool>("88592", "Celestial Annals", "Mode: [select] only\nShould the bot buy \"Celestial Annals\" ?", false),
        new Option<bool>("88593", "Celestial Runic Choir", "Mode: [select] only\nShould the bot buy \"Celestial Runic Choir\" ?", false),
        new Option<bool>("88594", "Celestial Wanderer's Sceptre", "Mode: [select] only\nShould the bot buy \"Celestial Wanderer's Sceptre\" ?", false),
        new Option<bool>("88595", "Celestial Wanderer's Scythe", "Mode: [select] only\nShould the bot buy \"Celestial Wanderer's Scythe\" ?", false),
        new Option<bool>("88596", "Infernal Wanderer", "Mode: [select] only\nShould the bot buy \"Infernal Wanderer\" ?", false),
        new Option<bool>("88597", "Infernal Wanderer Hood", "Mode: [select] only\nShould the bot buy \"Infernal Wanderer Hood\" ?", false),
        new Option<bool>("88598", "Infernal Annals", "Mode: [select] only\nShould the bot buy \"Infernal Annals\" ?", false),
        new Option<bool>("88599", "Infernal Runic Choir", "Mode: [select] only\nShould the bot buy \"Infernal Runic Choir\" ?", false),
        new Option<bool>("88600", "Infernal Wanderer's Scythe", "Mode: [select] only\nShould the bot buy \"Infernal Wanderer's Scythe\" ?", false),
        new Option<bool>("88601", "Maah-na's Robes", "Mode: [select] only\nShould the bot buy \"Maah-na's Robes\" ?", false),
        new Option<bool>("88602", "Maah-na's Hood", "Mode: [select] only\nShould the bot buy \"Maah-na's Hood\" ?", false),
        new Option<bool>("88603", "Maah-na's Wings", "Mode: [select] only\nShould the bot buy \"Maah-na's Wings\" ?", false),
        new Option<bool>("88604", "Maah-na's Freed Wings", "Mode: [select] only\nShould the bot buy \"Maah-na's Freed Wings\" ?", false),
        new Option<bool>("88636", "Celestial Wanderer Hood", "Mode: [select] only\nShould the bot buy \"Celestial Wanderer Hood\" ?", false),
    };
}
