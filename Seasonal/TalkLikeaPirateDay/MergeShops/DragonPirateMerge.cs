/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DragonPirateStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DragonPirateMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public DragonPirateStory DP = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Draconic Doubloon", "Lightning Pirate's Machine Pistol", "Lightning Pirate's Tricorn", "Lightning Pirate's Tricorn + Locks", "Lightning Pirate's Tricorn + Eyepatch", "Lightning Pirate's Tricorn Locks + Eyepatch " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DP.DragonPirate();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dragonpirate", 2048, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Draconic Doubloon":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8276);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dragonpirate", "Dragon Gunner", "Pirates Defeated", 10);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Lightning Pirate's Machine Pistol":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("dragonpirate", "Dragon Pirate", req.Name);
                    break;

                case "Lightning Pirate's Tricorn":
                case "Lightning Pirate's Tricorn + Locks":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("dragonpirate", "Dragon Gunner", req.Name);
                    break;

                case "Lightning Pirate's Tricorn + Eyepatch":
                case "Lightning Pirate's Tricorn Locks + Eyepatch":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("dragonpirate", "Dragon Pirate", req.Name);
                    break;

                case "Lightning Pirate":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dragonpirate", "Scalebeard", req.Name);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("63063", "Lightning Pirate's Cloud", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Cloud\" ?", false),
        new Option<bool>("63064", "Lightning Pirate's Charged Steering Wheel", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Charged Steering Wheel\" ?", false),
        new Option<bool>("63067", "Lightning Pirate's Dual Cutlasses", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Dual Cutlasses\" ?", false),
        new Option<bool>("63069", "Lightning Pirate's Cutlass + Charged Wheel", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Cutlass + Charged Wheel\" ?", false),
        new Option<bool>("63070", "Lightning Pirate's Dual Machine Pistols", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Dual Machine Pistols\" ?", false),
        new Option<bool>("63071", "Lightning Pirate's Dual WheelGuns", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Dual WheelGuns\" ?", false),
        new Option<bool>("63072", "Lightning Pirate's Dual Machine Guns", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Dual Machine Guns\" ?", false),
        new Option<bool>("63075", "Lightning Pirate's WheelGun", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's WheelGun\" ?", false),
        new Option<bool>("63076", "Lightning Pirate's Charged Wheel", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Charged Wheel\" ?", false),
        new Option<bool>("63078", "Lightning Pirate's Dual Charged Wheels", "Mode: [select] only\nShould the bot buy \"Lightning Pirate's Dual Charged Wheels\" ?", false),
        new Option<bool>("63082", "Thunderlight Pirate", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate\" ?", false),
        new Option<bool>("63083", "Thunderlight Pirate's Tricorn", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate's Tricorn\" ?", false),
        new Option<bool>("63084", "Thunderlight Pirate Tricorn + Locks", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate Tricorn + Locks\" ?", false),
        new Option<bool>("63085", "Thunderlight Pirate's Tricorn + Eyepatch", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate's Tricorn + Eyepatch\" ?", false),
        new Option<bool>("63086", "Thunderlight Pirate's Tricorn Locks + Eyepatch", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate's Tricorn Locks + Eyepatch\" ?", false),
        new Option<bool>("63087", "Thunderlight Pirate's Cloud", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate's Cloud\" ?", false),
        new Option<bool>("63088", "Thunderlight Pirate's Charged Steering Wheel", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate's Charged Steering Wheel\" ?", false),
        new Option<bool>("63094", "Thunderlight Pirate's Charged Wheel", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate's Charged Wheel\" ?", false),
        new Option<bool>("63095", "Thunderlight Pirate's Dual Charged Wheels", "Mode: [select] only\nShould the bot buy \"Thunderlight Pirate's Dual Charged Wheels\" ?", false),
    };
}
