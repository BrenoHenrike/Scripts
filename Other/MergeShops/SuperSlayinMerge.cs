/*
name: Super Slayin Merge
description: This bot will farm the items belonging to the selected mode for the Super Slayin Merge [2321] in /superslayin
tags: super, slayin, merge, superslayin, enchanted, martial, artists, gi, ryokus, masters, ryoku, morph
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SuperSlayinMerge
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
        Core.BankingBlackList.AddRange(new[] { "Martial Artist's Gi", "Fatal Lily", "Pockey Ball", "Dragon Orb", "Ryoku's Spikes", "Master's Gi", "Super Ryoku Morph", "Super Ryoku Spikes" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("superslayin", 2321, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Martial Artist's Gi":
                case "Ryoku's Spikes":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("dragonkoi", "Ryoku", req.Name, quant, false, false);
                    break;

                case "Fatal Lily":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("superslayin", "Newb Cybot", req.Name, quant, false, false);
                    break;

                case "Pockey Ball":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("superslayin", "Charidon", req.Name, quant, false, false);
                    break;

                case "Dragon Orb":
                case "Master's Gi":
                case "Super Ryoku Morph":
                case "Super Ryoku Spikes":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("superslayin", "Super Ryoku", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79174", "Enchanted Martial Artist's Gi", "Mode: [select] only\nShould the bot buy \"Enchanted Martial Artist's Gi\" ?", false),
        new Option<bool>("79175", "Enchanted Ryoku's Spikes", "Mode: [select] only\nShould the bot buy \"Enchanted Ryoku's Spikes\" ?", false),
        new Option<bool>("79176", "Enchanted Master's Gi", "Mode: [select] only\nShould the bot buy \"Enchanted Master's Gi\" ?", false),
        new Option<bool>("79177", "Enchanted Super Ryoku Morph", "Mode: [select] only\nShould the bot buy \"Enchanted Super Ryoku Morph\" ?", false),
        new Option<bool>("79311", "Enchanted Super Ryoku Spikes", "Mode: [select] only\nShould the bot buy \"Enchanted Super Ryoku Spikes\" ?", false),
    };
}
