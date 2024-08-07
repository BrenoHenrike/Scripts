/*
name: Hydra Merge
description: This bot will farm the items belonging to the selected mode for the Hydra Merge [1597] in /hydrachallenge
tags: hydra, merge, hydrachallenge, chaos, hydraslayer, cloak, backblade, hammer, head, scarf, visor, dew, drops, dress, long, faerie, escherions, evolved, purified, plate, morph, guardian, trident
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HydraMerge
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
        Core.BankingBlackList.AddRange(new[] { "Hydra Scale Piece", "Staff of Inversion" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hydrachallenge", 1597, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hydra Scale Piece":
                    Core.HuntMonster("hydrachallenge", "Hydra Head 25", req.Name, req.Quantity, isTemp: false, true);

                    break;

                case "Staff of Inversion":
                    Core.KillEscherion(req.Name, 1, false, true);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43593", "Chaos HydraSlayer", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer\" ?", false),
        new Option<bool>("43601", "Chaos HydraSlayer Cloak", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer Cloak\" ?", false),
        new Option<bool>("43600", "Chaos HydraSlayer BackBlade", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer BackBlade\" ?", false),
        new Option<bool>("43598", "Chaos HydraSlayer Hammer", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer Hammer\" ?", false),
        new Option<bool>("43595", "Chaos HydraSlayer Helm", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer Helm\" ?", false),
        new Option<bool>("43594", "Chaos HydraSlayer Head", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer Head\" ?", false),
        new Option<bool>("43596", "Chaos HydraSlayer Scarf", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer Scarf\" ?", false),
        new Option<bool>("43599", "Chaos HydraSlayer Blade", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer Blade\" ?", false),
        new Option<bool>("43597", "Chaos HydraSlayer Visor", "Mode: [select] only\nShould the bot buy \"Chaos HydraSlayer Visor\" ?", false),
        new Option<bool>("43794", "Dew Drop's Dress Armor", "Mode: [select] only\nShould the bot buy \"Dew Drop's Dress Armor\" ?", false),
        new Option<bool>("43795", "Long Faerie Hair", "Mode: [select] only\nShould the bot buy \"Long Faerie Hair\" ?", false),
        new Option<bool>("43798", "Faerie Hair", "Mode: [select] only\nShould the bot buy \"Faerie Hair\" ?", false),
        new Option<bool>("43799", "Escherion's Evolved Staff", "Mode: [select] only\nShould the bot buy \"Escherion's Evolved Staff\" ?", false),
        new Option<bool>("83390", "Purified HydraSlayer Plate", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Plate\" ?", false),
        new Option<bool>("83391", "Purified HydraSlayer Helm", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Helm\" ?", false),
        new Option<bool>("83392", "Purified HydraSlayer Morph Hood", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Morph Hood\" ?", false),
        new Option<bool>("83393", "Purified HydraSlayer Hood", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Hood\" ?", false),
        new Option<bool>("83394", "Hydra Head Guardian Cape", "Mode: [select] only\nShould the bot buy \"Hydra Head Guardian Cape\" ?", false),
        new Option<bool>("83395", "Purified HydraSlayer Blade", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Blade\" ?", false),
        new Option<bool>("83396", "Purified HydraSlayer Blades", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Blades\" ?", false),
        new Option<bool>("83397", "Purified HydraSlayer Polearm", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Polearm\" ?", false),
        new Option<bool>("83398", "Purified HydraSlayer Trident", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Trident\" ?", false),
        new Option<bool>("83399", "Purified HydraSlayer Staff", "Mode: [select] only\nShould the bot buy \"Purified HydraSlayer Staff\" ?", false),
    };
}
