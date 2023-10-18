/*
name: Celestias Rewards Merge
description: This bot will farm the items belonging to the selected mode for the Celestias Rewards Merge [2341] in /nerites
tags: celestias, rewards, merge, nerites, messina, mirage, corsair, patched, captain, sea, enchanter, flag, glint, edge, cutlasses, doubled, illusionary, grog, crews, gear, helmsmans, elite, zardic, swordmaster, savage, swordsmaster, beastly, kinsman, morph, scarred, kinsmans, single, partner, yokai, bandit, straw
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CelestiasRewardsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Nerites Scale", "Pirate Remains", "Glint Edge Cutlass" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("nerites", 2341, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Nerites Scale":
                case "Glint Edge Cutlass":
                    Core.Logger($"{req.Name} requires ultra boss, you need to farm it manually.", stopBot: true);
                    break;

                case "Pirate Remains":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9409);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("nerites", "Ghostly Eel", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79415", "Messina Mirage Corsair", "Mode: [select] only\nShould the bot buy \"Messina Mirage Corsair\" ?", false),
        new Option<bool>("79417", "Messina Mirage Patched Visage", "Mode: [select] only\nShould the bot buy \"Messina Mirage Patched Visage\" ?", false),
        new Option<bool>("79420", "Messina Mirage Captain Visage", "Mode: [select] only\nShould the bot buy \"Messina Mirage Captain Visage\" ?", false),
        new Option<bool>("79421", "Messina Mirage Hat Visage", "Mode: [select] only\nShould the bot buy \"Messina Mirage Hat Visage\" ?", false),
        new Option<bool>("79423", "Sea Enchanter Visage", "Mode: [select] only\nShould the bot buy \"Sea Enchanter Visage\" ?", false),
        new Option<bool>("79428", "Messina Mirage Flag", "Mode: [select] only\nShould the bot buy \"Messina Mirage Flag\" ?", false),
        new Option<bool>("79430", "Glint Edge Cutlasses", "Mode: [select] only\nShould the bot buy \"Glint Edge Cutlasses\" ?", false),
        new Option<bool>("79432", "Doubled Illusionary Grog", "Mode: [select] only\nShould the bot buy \"Doubled Illusionary Grog\" ?", false),
        new Option<bool>("79434", "Messina Mirage Crew's Gear", "Mode: [select] only\nShould the bot buy \"Messina Mirage Crew's Gear\" ?", false),
        new Option<bool>("79435", "Messina Helmsman's Gear", "Mode: [select] only\nShould the bot buy \"Messina Helmsman's Gear\" ?", false),
        new Option<bool>("80205", "Elite Zardic Swordmaster", "Mode: [select] only\nShould the bot buy \"Elite Zardic Swordmaster\" ?", false),
        new Option<bool>("80206", "Savage Zardic Swordsmaster", "Mode: [select] only\nShould the bot buy \"Savage Zardic Swordsmaster\" ?", false),
        new Option<bool>("80211", "Beastly Kinsman Morph", "Mode: [select] only\nShould the bot buy \"Beastly Kinsman Morph\" ?", false),
        new Option<bool>("80212", "Beastly Kinsman Visage", "Mode: [select] only\nShould the bot buy \"Beastly Kinsman Visage\" ?", false),
        new Option<bool>("80213", "Beastly Kinsman Scarred Morph", "Mode: [select] only\nShould the bot buy \"Beastly Kinsman Scarred Morph\" ?", false),
        new Option<bool>("80227", "Kinsman's Single Blade", "Mode: [select] only\nShould the bot buy \"Kinsman's Single Blade\" ?", false),
        new Option<bool>("80228", "Kinsman's Partner Blades", "Mode: [select] only\nShould the bot buy \"Kinsman's Partner Blades\" ?", false),
        new Option<bool>("80321", "Yokai Bandit", "Mode: [select] only\nShould the bot buy \"Yokai Bandit\" ?", false),
        new Option<bool>("80324", "Yokai Bandit Straw Hat", "Mode: [select] only\nShould the bot buy \"Yokai Bandit Straw Hat\" ?", false),
        new Option<bool>("80325", "Yokai Bandit Straw Locks", "Mode: [select] only\nShould the bot buy \"Yokai Bandit Straw Locks\" ?", false),
    };
}
