/*
name: Yokai Pirate Treasures Merge
description: This bot will farm the items belonging to the selected mode for the Yokai Pirate Treasures Merge [2337] in /yokaipirate
tags: yokai, pirate, treasures, merge, yokaipirate, coastal, raider, raiders, battlescarred, morph, stetson, marauders, , beard, steampowered, shotgun, cannonblast, gunner, powder, monkey, disguised, pirates, enthusiastic, eyepatch, unloaded, cannon, swashbucklers, rapiers, cutlass, rapier
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YokaiPirateTreasuresMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreDOY DOY = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Pirate's Rag", "Yokai Gunpowder", "Coastal Raider's Beard", "Maurader's Mane", "Maurader's Mane + Beard", "Disguised Pirate's Hair", "Disguised Pirate's Tricorn", "Disguised Pirate's BattleGear", "Disguised Pirate's EyePatch", "Swashbuckler's Rapier", "Disguised Pirate's Cutlass" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DOY.YokaiPirate();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("yokaipirate", 2337, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Pirate's Rag":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9388);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("yokaipirate", "Lord Brentan", "Gold Leaf Brooch");
                        Core.HuntMonster("yokaipirate", "Neverglades  Knight", "Knight's Emblem", 7);

                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("yokaipirate", "Disguised Pirate", "Yokai Pirate's Piece", 7);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Yokai Gunpowder":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("yokaipirate", "Serpent Warrior Monster", req.Name, quant, req.Temp);
                    break;

                case "Maurader's Mane":
                case "Maurader's Mane + Beard":
                case "Disguised Pirate's Hair":
                case "Coastal Raider's Beard":
                case "Disguised Pirate's Tricorn":
                case "Disguised Pirate's BattleGear":
                case "Disguised Pirate's Cutlass":
                case "Disguised Pirate's EyePatch":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("yokaipirate", "Disguised Pirate", req.Name, quant, req.Temp);
                    break;
                case "Swashbuckler's Rapier":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("yokaipirate", "Neverglades  Knight", req.Name, quant, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79828", "Coastal Raider", "Mode: [select] only\nShould the bot buy \"Coastal Raider\" ?", false),
        new Option<bool>("79829", "Coastal Raider's Battlescarred Morph", "Mode: [select] only\nShould the bot buy \"Coastal Raider's Battlescarred Morph\" ?", false),
        new Option<bool>("79831", "Coastal Raider's Battlescarred Visage", "Mode: [select] only\nShould the bot buy \"Coastal Raider's Battlescarred Visage\" ?", false),
        new Option<bool>("79835", "Coastal Raider's Stetson", "Mode: [select] only\nShould the bot buy \"Coastal Raider's Stetson\" ?", false),
        new Option<bool>("79836", "Coastal Marauder's Stetson + Beard", "Mode: [select] only\nShould the bot buy \"Coastal Marauder's Stetson + Beard\" ?", false),
        new Option<bool>("79837", "Coastal Marauder's Stetson", "Mode: [select] only\nShould the bot buy \"Coastal Marauder's Stetson\" ?", false),
        new Option<bool>("79842", "Steampowered Raider's Gauntlet", "Mode: [select] only\nShould the bot buy \"Steampowered Raider's Gauntlet\" ?", false),
        new Option<bool>("79843", "Coastal Raider's Shotgun", "Mode: [select] only\nShould the bot buy \"Coastal Raider's Shotgun\" ?", false),
        new Option<bool>("80022", "Cannonblast Gunner", "Mode: [select] only\nShould the bot buy \"Cannonblast Gunner\" ?", false),
        new Option<bool>("80023", "Powder Monkey", "Mode: [select] only\nShould the bot buy \"Powder Monkey\" ?", false),
        new Option<bool>("80025", "Disguised Pirate's Morph", "Mode: [select] only\nShould the bot buy \"Disguised Pirate's Morph\" ?", false),
        new Option<bool>("80027", "Enthusiastic Pirate's Morph", "Mode: [select] only\nShould the bot buy \"Enthusiastic Pirate's Morph\" ?", false),
        new Option<bool>("80029", "Enthusiastic Pirate's Eyepatch Morph", "Mode: [select] only\nShould the bot buy \"Enthusiastic Pirate's Eyepatch Morph\" ?", false),
        new Option<bool>("80031", "Enthusiastic Pirate's Morph", "Mode: [select] only\nShould the bot buy \"Enthusiastic Pirate's Morph\" ?", false),
        new Option<bool>("80033", "Unloaded Cannon Cape", "Mode: [select] only\nShould the bot buy \"Unloaded Cannon Cape\" ?", false),
        new Option<bool>("80040", "Swashbuckler's Rapiers", "Mode: [select] only\nShould the bot buy \"Swashbuckler's Rapiers\" ?", false),
        new Option<bool>("80041", "Disguised Pirate's Cutlass + Rapier", "Mode: [select] only\nShould the bot buy \"Disguised Pirate's Cutlass + Rapier\" ?", false),
    };
}
