/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NationWarChestMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    public CoreNation Nation = new();
    public JuggernautItemsofNulgath Jug = new();
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
        Core.BankingBlackList.AddRange(new[] { "Unidentified 13", "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath", "Voucher of Nulgath", "Voucher of Nulgath (non-mem)", "Overfiend Blade of Nulgath" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        if (!Core.isSeasonalMapActive("darkwarnation"))
            return;

        Adv.StartBuyAllMerge("darkwarnation", 2123, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Unidentified 13":
                    Nation.FarmUni13(quant);
                    break;

                case "Tainted Gem":
                    Nation.ForgeTaintedGems(quant);
                    Nation.SwindleBulk(quant);
                    break;

                case "Dark Crystal Shard":
                    Nation.ForgeDarkCrystalShards(quant);
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Voucher of Nulgath":
                    Nation.FarmVoucher(true);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Nation.FarmVoucher(false);
                    break;

                case "Overfiend Blade of Nulgath":
                    Jug.JuggItems(reward: JuggernautItemsofNulgath.RewardsSelection.Overfiend_Blade_of_Nulgath);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("69698", "Wrath of Nulgath", "Mode: [select] only\nShould the bot buy \"Wrath of Nulgath\" ?", false),
    };
}
