/*
name: Vinaras Odd Merge
description: This bot will farm the items belonging to the selected mode for the Vinaras Odd Merge [1539] in /curio
tags: vinaras, odd, merge, curio, googly, head, great, mace, ode, to, joy, scythe, gold, empty, cauldron, cursed, bards, strange, cat, face, borked, dragon, control, seraph, avenger, necrotic, dark, infernal, companion, grandpapa, golden, sneevil, morph, papa
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class VinarasOddMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreDailies Daily = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Crypto Token", "Shadowed Infernal Companion", "GrandPapa the Golden Sneevil Morph", "Papa the Sneevil" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("curio", 1539, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Crypto Token":
                    Core.Logger($"{req.Name} is daily, make sure you have enough.");
                    Daily.CryptoToken();
                    break;

                case "Shadowed Infernal Companion":
                case "GrandPapa the Golden Sneevil Morph":
                case "Papa the Sneevil":
                    Core.Logger($"{req.Name} is rare, it cannot be farmed.");
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("40676", "Googly Head", "Mode: [select] only\nShould the bot buy \"Googly Head\" ?", false),
        new Option<bool>("40857", "Great Mace", "Mode: [select] only\nShould the bot buy \"Great Mace\" ?", false),
        new Option<bool>("42117", "Ode to Joy Scythe", "Mode: [select] only\nShould the bot buy \"Ode to Joy Scythe\" ?", false),
        new Option<bool>("41857", "Gold Rune Staff", "Mode: [select] only\nShould the bot buy \"Gold Rune Staff\" ?", false),
        new Option<bool>("41710", "Empty Cauldron Helm", "Mode: [select] only\nShould the bot buy \"Empty Cauldron Helm\" ?", false),
        new Option<bool>("33549", "Cursed Bard's Cape", "Mode: [select] only\nShould the bot buy \"Cursed Bard's Cape\" ?", false),
        new Option<bool>("35246", "Strange Cat Face", "Mode: [select] only\nShould the bot buy \"Strange Cat Face\" ?", false),
        new Option<bool>("29768", "Borked Blade of Dragon Control", "Mode: [select] only\nShould the bot buy \"Borked Blade of Dragon Control\" ?", false),
        new Option<bool>("29459", "Seraph Avenger", "Mode: [select] only\nShould the bot buy \"Seraph Avenger\" ?", false),
        new Option<bool>("29458", "Necrotic Avenger", "Mode: [select] only\nShould the bot buy \"Necrotic Avenger\" ?", false),
        new Option<bool>("46076", "Dark Infernal Companion", "Mode: [select] only\nShould the bot buy \"Dark Infernal Companion\" ?", false),
        new Option<bool>("48604", "GrandPapa the Golden Sneevil Morph", "Mode: [select] only\nShould the bot buy \"GrandPapa the Golden Sneevil Morph\" ?", false),
        new Option<bool>("48606", "Papa the Sneevil", "Mode: [select] only\nShould the bot buy \"Papa the Sneevil\" ?", false),
    };
}
