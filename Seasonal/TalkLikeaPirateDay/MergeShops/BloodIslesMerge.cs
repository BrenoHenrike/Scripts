/*
name: Blood Isles Merge
description: This bot will farm the items belonging to the selected mode for the Blood Isles Merge [2484] in /bloodisles
tags: blood, isles, merge, bloodisles, copper, golem, commander, bandana, cap, commanders, navigator, silver, swashbuckler, morph, captain, swashbucklers, fancy, golden, armblade, armblades, testament, pirate
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/BloodIslesStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BloodIslesMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private BloodIsles BI = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Blood Isle Booty", "Amira 2.0 Gear", "Fancy Golden Scissors", "Dual Fancy Golden Scissors" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        BI.DoStory();
        Core.EquipClass(ClassType.Solo);
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("bloodisles", 2484, findIngredients, buyOnlyThis, buyMode: buyMode);

        #region Dont edit this part
        void findIngredients()
        {
            string[] UseableMonsters = new[]
            {
                "Blood Moon Pirate", // UseableMonsters[0],
                "Drowned Werewolf", // UseableMonsters[1],
                "Vampiric Lamprey", // UseableMonsters[2],
                "Drowned Vampire", // UseableMonsters[3],
                "Blood Veil Pirate", // UseableMonsters[4],
                "Drowned Horde", // UseableMonsters[5],
                "Bloodthirsty Bonnie", // UseableMonsters[6],
                "Sea King Kurok", // UseableMonsters[7],
                "Merpyre", // UseableMonsters[8]
                "Feral Flintfang", // UseableMonsters[9]
            };

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

                case "Blood Isle Booty":
                    Core.FarmingLogger(req.Name, quant);
                    // 9886 | Petty Proposition
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(9886);
                        Core.HuntMonster("bloodisles", UseableMonsters[6], "Blood Captain Cap");
                        Core.HuntMonster("bloodisles", UseableMonsters[7], "Kurok's Moon Ring");
                        Core.HuntMonster("bloodisles", UseableMonsters[8], "Merpyre Scale");
                        Core.EnsureComplete(9886);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Amira 2.0 Gear":
                    if (!Core.IsMember)
                    {
                        Core.Logger("Members only map");
                        return;
                    }
                    Core.HuntMonster("amira", "Amira 2.0", req.Name, req.Quantity, req.Temp);
                    break;

                case "Fancy Golden Scissors":
                    Core.HuntMonster("bloodisles", UseableMonsters[8], req.Name, req.Quantity, req.Temp);
                    break;

                case "Dual Fancy Golden Scissors":
                    Core.HuntMonster("bloodisles", UseableMonsters[8], req.Name, req.Quantity, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79908", "Copper Golem Commander", "Mode: [select] only\nShould the bot buy \"Copper Golem Commander\" ?", false),
        new Option<bool>("79912", "Copper Golem Commander Visage", "Mode: [select] only\nShould the bot buy \"Copper Golem Commander Visage\" ?", false),
        new Option<bool>("79913", "Copper Golem Commander Bandana", "Mode: [select] only\nShould the bot buy \"Copper Golem Commander Bandana\" ?", false),
        new Option<bool>("79914", "Copper Golem Commander Cap", "Mode: [select] only\nShould the bot buy \"Copper Golem Commander Cap\" ?", false),
        new Option<bool>("79915", "Copper Golem Commander's Navigator", "Mode: [select] only\nShould the bot buy \"Copper Golem Commander's Navigator\" ?", false),
        new Option<bool>("79916", "Copper Golem Commander Gauntlets", "Mode: [select] only\nShould the bot buy \"Copper Golem Commander Gauntlets\" ?", false),
        new Option<bool>("79917", "Silver Swashbuckler", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler\" ?", false),
        new Option<bool>("79918", "Silver Swashbuckler Morph", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler Morph\" ?", false),
        new Option<bool>("79919", "Silver Swashbuckler Captain Morph", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler Captain Morph\" ?", false),
        new Option<bool>("79920", "Silver Swashbuckler Bandana Visage", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler Bandana Visage\" ?", false),
        new Option<bool>("79921", "Silver Swashbuckler Visage", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler Visage\" ?", false),
        new Option<bool>("79922", "Silver Swashbuckler Bandana", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler Bandana\" ?", false),
        new Option<bool>("79923", "Silver Swashbuckler Cap", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler Cap\" ?", false),
        new Option<bool>("79924", "Silver Swashbuckler's Navigator", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler's Navigator\" ?", false),
        new Option<bool>("79925", "Silver Swashbuckler Gauntlets", "Mode: [select] only\nShould the bot buy \"Silver Swashbuckler Gauntlets\" ?", false),
        new Option<bool>("88227", "Fancy Golden Armblade", "Mode: [select] only\nShould the bot buy \"Fancy Golden Armblade\" ?", false),
        new Option<bool>("88228", "Fancy Golden Armblades", "Mode: [select] only\nShould the bot buy \"Fancy Golden Armblades\" ?", false),
        new Option<bool>("88482", "Blood Testament Pirate", "Mode: [select] only\nShould the bot buy \"Blood Testament Pirate\" ?", false),
        new Option<bool>("88483", "Blood Testament Pirate Visage", "Mode: [select] only\nShould the bot buy \"Blood Testament Pirate Visage\" ?", false),
        new Option<bool>("88484", "Blood Testament Pirate Morph", "Mode: [select] only\nShould the bot buy \"Blood Testament Pirate Morph\" ?", false),
    };
}
