/*
name: Archive of Time Merge
description: This bot will farm the items belonging to the selected mode for the Archive of Time Merge [1347] in /cathedral
tags: archive, of, time, merge, cathedral, aegis, guardian, savior, divine, guardians, scythe, wings
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArchiveofTimeMerge
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
        Core.BankingBlackList.AddRange(new[] { "Aegis Armor", "Aegis Robe", "Aegis Ward", "Blessed Metal", "Golden Faceplate", "Data Scroll", "Time Key" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("cathedral", 1347, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Aegis Armor":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("cathedral", "Skeletal Warrior", req.Name, quant, false, false);
                    break;

                case "Aegis Robe":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("cathedral", "Infernal Knight", req.Name, quant, false, false);
                    break;

                case "Aegis Ward":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("cathedral", "Pactagonal Knight", req.Name, quant, false, false);
                    break;

                case "Blessed Metal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("cathedral", "Corrupted Sentry", req.Name, quant, false, false);
                    break;

                case "Golden Faceplate":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("cathedral", "Flying Pieball", req.Name, quant, false, false);
                    break;

                case "Data Scroll":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("cathedral", "Data Glitch", req.Name, quant, false, false);
                    break;

                case "Time Key":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("cathedral", "Incarnation of Time", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("37483", "Aegis Guardian", "Mode: [select] only\nShould the bot buy \"Aegis Guardian\" ?", false),
        new Option<bool>("37489", "Savior Of Aegis", "Mode: [select] only\nShould the bot buy \"Savior Of Aegis\" ?", false),
        new Option<bool>("37494", "Divine Guardian Of Aegis", "Mode: [select] only\nShould the bot buy \"Divine Guardian Of Aegis\" ?", false),
        new Option<bool>("37495", "Divine Guardian's Scythe", "Mode: [select] only\nShould the bot buy \"Divine Guardian's Scythe\" ?", false),
        new Option<bool>("37496", "Divine Guardian Wings", "Mode: [select] only\nShould the bot buy \"Divine Guardian Wings\" ?", false),
    };
}
