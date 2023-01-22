/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/LowTideStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/AluteaNursery.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AluteaNurseryMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public AluteaNursery AN = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Sea Salt", "Naval Guard", "Naval Guard's Tricorn + Hair", "Naval Guard's Tricorn + Locks", "Naval Guard's Cutlass", "Naval Guard's Cutlasses", "Naval Guard's Rapier", "Naval Guard's Rapiers", "DeepSea Star Pirate", "DeepSea Star Pirate's Hair", "DeepSea Star Pirate's Locks", "DeepSea Star Pirate's Morph", "DeepSea Star Pirate's Morph + Locks", "DeepSea Star Pirate's Light Gun", "DeepSea Star Pirate's Light Guns", "Naval Guard's ArmBlade", "DeepSea Smol Wave " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AN.DoAll();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("aluteanursery", 2168, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Sea Salt":
                case "Naval Guard":
                case "Naval Guard's Tricorn + Hair":
                case "Naval Guard's Tricorn + Locks":
                case "Naval Guard's Cutlass":
                case "Naval Guard's Cutlasses":
                case "Naval Guard's Rapier":
                case "Naval Guard's Rapiers":
                case "Naval Guard's ArmBlade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(8858);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("aluteanursery", "Last Alutian", "Angler Antena");
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("aluteanursery", "Stagnant Water", "Stale Seawater", 6);
                        Core.HuntMonster("aluteanursery", "Bone Crustacean", "Pale Shell", 6);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "DeepSea Star Pirate":
                case "DeepSea Star Pirate's Hair":
                case "DeepSea Star Pirate's Locks":
                case "DeepSea Star Pirate's Morph":
                case "DeepSea Star Pirate's Morph + Locks":
                case "DeepSea Star Pirate's Light Gun":
                case "DeepSea Star Pirate's Light Guns":
                case "DeepSea Smol Wave":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("aluteanursery", "Last Alutian", req.Name, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("71167", "Enchanted Naval Guard", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard\" ?", false),
        new Option<bool>("71168", "Enchanted Naval Guard's Tricorn", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard's Tricorn\" ?", false),
        new Option<bool>("71169", "Enchanted Naval Guard's Tricorn + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard's Tricorn + Locks\" ?", false),
        new Option<bool>("71170", "Enchanted Naval Guard's Cutlass", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard's Cutlass\" ?", false),
        new Option<bool>("71171", "Enchanted Naval Guard's Cutlasses", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard's Cutlasses\" ?", false),
        new Option<bool>("71172", "Enchanted Naval Guard's Rapier", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard's Rapier\" ?", false),
        new Option<bool>("71173", "Enchanted Naval Guard's Rapiers", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard's Rapiers\" ?", false),
        new Option<bool>("71867", "Magical DeepSea Star Pirate", "Mode: [select] only\nShould the bot buy \"Magical DeepSea Star Pirate\" ?", false),
        new Option<bool>("71873", "DeepSea Star Pirate's Hat", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Hat\" ?", false),
        new Option<bool>("71874", "DeepSea Star Pirate's Hat + Locks", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Hat + Locks\" ?", false),
        new Option<bool>("71875", "DeepSea Star Pirate's Morph Hat", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Morph Hat\" ?", false),
        new Option<bool>("71876", "DeepSea Star Pirate's Morph Hat + Locks", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Morph Hat + Locks\" ?", false),
        new Option<bool>("71885", "DeepSea Star Pirate's Spear", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Spear\" ?", false),
        new Option<bool>("71886", "DeepSea Star Pirate's Trident", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Trident\" ?", false),
        new Option<bool>("71889", "DeepSea Star Pirate's Dark Gun", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Dark Gun\" ?", false),
        new Option<bool>("71890", "DeepSea Star Pirate's Dark Guns", "Mode: [select] only\nShould the bot buy \"DeepSea Star Pirate's Dark Guns\" ?", false),
        new Option<bool>("72485", "Malevolent Marauder", "Mode: [select] only\nShould the bot buy \"Malevolent Marauder\" ?", false),
        new Option<bool>("72486", "Marauder's Hair", "Mode: [select] only\nShould the bot buy \"Marauder's Hair\" ?", false),
        new Option<bool>("72487", "Marauder's Beard", "Mode: [select] only\nShould the bot buy \"Marauder's Beard\" ?", false),
        new Option<bool>("72488", "Marauder's Locks", "Mode: [select] only\nShould the bot buy \"Marauder's Locks\" ?", false),
        new Option<bool>("72489", "Marauder's Hat", "Mode: [select] only\nShould the bot buy \"Marauder's Hat\" ?", false),
        new Option<bool>("72490", "Marauder's Hat + Beard", "Mode: [select] only\nShould the bot buy \"Marauder's Hat + Beard\" ?", false),
        new Option<bool>("72491", "Marauder's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Marauder's Hat + Locks\" ?", false),
        new Option<bool>("72492", "Marauder's Rapier Cape", "Mode: [select] only\nShould the bot buy \"Marauder's Rapier Cape\" ?", false),
        new Option<bool>("72493", "Marauder's Gear Cape", "Mode: [select] only\nShould the bot buy \"Marauder's Gear Cape\" ?", false),
        new Option<bool>("72494", "Marauder's Rapier", "Mode: [select] only\nShould the bot buy \"Marauder's Rapier\" ?", false),
        new Option<bool>("72495", "Marauder's Rapiers", "Mode: [select] only\nShould the bot buy \"Marauder's Rapiers\" ?", false),
        new Option<bool>("72496", "Marauder's Cutlass", "Mode: [select] only\nShould the bot buy \"Marauder's Cutlass\" ?", false),
        new Option<bool>("72497", "Marauder's Cutlasses", "Mode: [select] only\nShould the bot buy \"Marauder's Cutlasses\" ?", false),
        new Option<bool>("72498", "Marauder's Pistol", "Mode: [select] only\nShould the bot buy \"Marauder's Pistol\" ?", false),
        new Option<bool>("72499", "Marauder's Pistols", "Mode: [select] only\nShould the bot buy \"Marauder's Pistols\" ?", false),
        new Option<bool>("72500", "Marauder's Cane", "Mode: [select] only\nShould the bot buy \"Marauder's Cane\" ?", false),
        new Option<bool>("72501", "Marauder's Rapier + Cutlass", "Mode: [select] only\nShould the bot buy \"Marauder's Rapier + Cutlass\" ?", false),
        new Option<bool>("72502", "Marauder's Cutlass + Pistol", "Mode: [select] only\nShould the bot buy \"Marauder's Cutlass + Pistol\" ?", false),
        new Option<bool>("71174", "Enchanted Naval Guard's ArmBlade", "Mode: [select] only\nShould the bot buy \"Enchanted Naval Guard's ArmBlade\" ?", false),
        new Option<bool>("71880", "DeepSea Tidal Wave", "Mode: [select] only\nShould the bot buy \"DeepSea Tidal Wave\" ?", false),
    };
}
