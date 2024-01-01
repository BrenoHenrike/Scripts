/*
name: Grim Gift Merge
description: This bot will farm the items belonging to the selected mode for the Grim Gift Merge [655] in /doomvault
tags: grim, gift, merge, doomvault, heroblade, lvl, , knight, errants, plate, broken, zweihander, battleworn, shield, mantle, rot, attached, pain, scythe, red, skull, blood, maul, weapon, enhancement, grimskull, ragged, rogue, grimhero, long
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DoomVault.cs
//cs_include Scripts/Story/DoomVaultB.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GrimGiftMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    private DoomVaultB DVB = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Hero's Hilt Fragment", "Hero's Blade Fragment", "Grime Token", "Binky's Uni-horn", "Grimskull's Face", "GrimBlade" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("doomvault", 655, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hero's Hilt Fragment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3002);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("doomvaultb", "Grim Shelleton", "Relic of Strength", 3);
                        Core.HuntMonster("doomvaultb", "Grim Fire Mage", "Relic of Heart ", 3);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Hero's Blade Fragment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3001);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("doomvaultb", "Grim Lich", "Relic of Courage", 3);
                        Core.HuntMonster("doomvaultb", "Grim Ectomancer", "Relic of Will", 3);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Grime Token":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("doomvault", "Grim Shelleton", req.Name, quant);
                    break;

                case "Binky's Uni-horn":
                    Core.EquipClass(ClassType.Solo);
                    Adv.KillUltra("doomvault", "r5", "Left", "Binky", req.Name, quant, req.Temp, publicRoom: true);
                    break;

                case "Grimskull's Face":
                    Core.HuntMonsterMapID("doomvaultb", 48, req.Name, isTemp: req.Temp);
                    break;

                case "GrimBlade":
                    if (!Core.isCompletedBefore(3004))
                        DVB.StoryLine();

                    if (!Core.CheckInventory("GrimBlade"))
                    {
                        Core.EnsureAccept(3004);
                        Core.HuntMonsterMapID("doomvaultb", 48, "Raxgore Slain");
                        Core.EnsureComplete(3004);
                        Bot.Wait.ForPickup("GrimBlade");
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("18157", "HeroBlade Lvl 1", "Mode: [select] only\nShould the bot buy \"HeroBlade Lvl 1\" ?", false),
        new Option<bool>("17958", "Knight Errant's Plate", "Mode: [select] only\nShould the bot buy \"Knight Errant's Plate\" ?", false),
        new Option<bool>("17959", "Broken Zweihander", "Mode: [select] only\nShould the bot buy \"Broken Zweihander\" ?", false),
        new Option<bool>("17960", "Battleworn Shield and Mantle", "Mode: [select] only\nShould the bot buy \"Battleworn Shield and Mantle\" ?", false),
        new Option<bool>("18003", "The Rot", "Mode: [select] only\nShould the bot buy \"The Rot\" ?", false),
        new Option<bool>("18004", "Attached", "Mode: [select] only\nShould the bot buy \"Attached\" ?", false),
        new Option<bool>("18005", "The Pain Scythe", "Mode: [select] only\nShould the bot buy \"The Pain Scythe\" ?", false),
        new Option<bool>("18006", "Red Skull", "Mode: [select] only\nShould the bot buy \"Red Skull\" ?", false),
        new Option<bool>("18063", "Blood Maul", "Mode: [select] only\nShould the bot buy \"Blood Maul\" ?", false),
        new Option<bool>("18196", "Grim Weapon Enhancement", "Mode: [select] only\nShould the bot buy \"Grim Weapon Enhancement\" ?", false),
        new Option<bool>("18197", "Grim 100 Weapon Enhancement", "Mode: [select] only\nShould the bot buy \"Grim 100 Weapon Enhancement\" ?", false),
        new Option<bool>("18199", "HeroBlade", "Mode: [select] only\nShould the bot buy \"HeroBlade\" ?", false),
        new Option<bool>("18200", "HeroBlade", "Mode: [select] only\nShould the bot buy \"HeroBlade\" ?", false),
        new Option<bool>("18201", "HeroBlade", "Mode: [select] only\nShould the bot buy \"HeroBlade\" ?", false),
        new Option<bool>("18202", "HeroBlade", "Mode: [select] only\nShould the bot buy \"HeroBlade\" ?", false),
        new Option<bool>("18203", "HeroBlade", "Mode: [select] only\nShould the bot buy \"HeroBlade\" ?", false),
        new Option<bool>("18139", "Grimskull Ragged Rogue", "Mode: [select] only\nShould the bot buy \"Grimskull Ragged Rogue\" ?", false),
        new Option<bool>("18140", "Grimskull Rogue Daggers", "Mode: [select] only\nShould the bot buy \"Grimskull Rogue Daggers\" ?", false),
        new Option<bool>("18142", "Grimskull Ragged Rogue Mask", "Mode: [select] only\nShould the bot buy \"Grimskull Ragged Rogue Mask\" ?", false),
        new Option<bool>("18143", "Grimskull Rogue Locks", "Mode: [select] only\nShould the bot buy \"Grimskull Rogue Locks\" ?", false),
        new Option<bool>("18205", "GrimHero Blade", "Mode: [select] only\nShould the bot buy \"GrimHero Blade\" ?", false),
        new Option<bool>("18207", "GrimHero Blade", "Mode: [select] only\nShould the bot buy \"GrimHero Blade\" ?", false),
        new Option<bool>("18208", "GrimHero Blade", "Mode: [select] only\nShould the bot buy \"GrimHero Blade\" ?", false),
        new Option<bool>("18209", "GrimHero Blade", "Mode: [select] only\nShould the bot buy \"GrimHero Blade\" ?", false),
        new Option<bool>("18210", "GrimHero Blade", "Mode: [select] only\nShould the bot buy \"GrimHero Blade\" ?", false),
        new Option<bool>("18211", "GrimHero Blade", "Mode: [select] only\nShould the bot buy \"GrimHero Blade\" ?", false),
        new Option<bool>("18228", "Grimskull Rogue Long Hair", "Mode: [select] only\nShould the bot buy \"Grimskull Rogue Long Hair\" ?", false),
        new Option<bool>("18141", "Grimskull Ragged Rogue Cape", "Mode: [select] only\nShould the bot buy \"Grimskull Ragged Rogue Cape\" ?", false),
    };
}
