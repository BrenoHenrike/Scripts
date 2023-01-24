/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DilligasMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreNation Nation = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Diamond of Nulgath", "Archfiend's Favor", "Tainted Gem", "Dark Crystal Shard", "Totem of Nulgath", "Gem of Nulgath", "Blood Gem of the Archfiend" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("evilwarnul", 465, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Archfiend's Favor":
                    Nation.ApprovalAndFavor(0, quant);
                    break;

                case "Tainted Gem":
                    Nation.SwindleBulk(quant);
                    break;

                case "Dark Crystal Shard":
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Totem of Nulgath":
                    Nation.FarmTotemofNulgath(quant);
                    break;

                case "Gem of Nulgath":
                    Nation.FarmGemofNulgath(quant);
                    break;

                case "Blood Gem of the Archfiend":
                    Nation.FarmBloodGem(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("29580", "Skullborne Sword", "Mode: [select] only\nShould the bot buy \"Skullborne Sword\" ?", false),
        new Option<bool>("69959", "Crypt Soldier", "Mode: [select] only\nShould the bot buy \"Crypt Soldier\" ?", false),
        new Option<bool>("69961", "Crypt Soldier Sword and Shield", "Mode: [select] only\nShould the bot buy \"Crypt Soldier Sword and Shield\" ?", false),
        new Option<bool>("69960", "Crypt Soldier Helmet", "Mode: [select] only\nShould the bot buy \"Crypt Soldier Helmet\" ?", false),
        new Option<bool>("69955", "Crypt Rogue", "Mode: [select] only\nShould the bot buy \"Crypt Rogue\" ?", false),
        new Option<bool>("69957", "Crypt Rogue BackBlade", "Mode: [select] only\nShould the bot buy \"Crypt Rogue BackBlade\" ?", false),
        new Option<bool>("69958", "Crypt Rogue BackBlades", "Mode: [select] only\nShould the bot buy \"Crypt Rogue BackBlades\" ?", false),
        new Option<bool>("69956", "Crypt Rogue Helmet", "Mode: [select] only\nShould the bot buy \"Crypt Rogue Helmet\" ?", false),
        new Option<bool>("69951", "Crypt Knight", "Mode: [select] only\nShould the bot buy \"Crypt Knight\" ?", false),
        new Option<bool>("69953", "Crypt Knight Sword", "Mode: [select] only\nShould the bot buy \"Crypt Knight Sword\" ?", false),
        new Option<bool>("69954", "Crypt Knight Swords", "Mode: [select] only\nShould the bot buy \"Crypt Knight Swords\" ?", false),
        new Option<bool>("69952", "Crypt Knight Helm", "Mode: [select] only\nShould the bot buy \"Crypt Knight Helm\" ?", false),
        new Option<bool>("69947", "Crypt Samurai", "Mode: [select] only\nShould the bot buy \"Crypt Samurai\" ?", false),
        new Option<bool>("69950", "Crypt Samurai Blades", "Mode: [select] only\nShould the bot buy \"Crypt Samurai Blades\" ?", false),
        new Option<bool>("69949", "Crypt Samurai Blade", "Mode: [select] only\nShould the bot buy \"Crypt Samurai Blade\" ?", false),
        new Option<bool>("69948", "Crypt Samurai Helmet", "Mode: [select] only\nShould the bot buy \"Crypt Samurai Helmet\" ?", false),
        new Option<bool>("69940", "Ancient Shogun Armor", "Mode: [select] only\nShould the bot buy \"Ancient Shogun Armor\" ?", false),
        new Option<bool>("69946", "Ancient Shogun Katana And Sheath", "Mode: [select] only\nShould the bot buy \"Ancient Shogun Katana And Sheath\" ?", false),
        new Option<bool>("69944", "Ancient Shogun Katanas", "Mode: [select] only\nShould the bot buy \"Ancient Shogun Katanas\" ?", false),
        new Option<bool>("69943", "Ancient Shogun Katana", "Mode: [select] only\nShould the bot buy \"Ancient Shogun Katana\" ?", false),
        new Option<bool>("69945", "Ancient Shogun Sheathed Katana", "Mode: [select] only\nShould the bot buy \"Ancient Shogun Sheathed Katana\" ?", false),
        new Option<bool>("69941", "Ancient Shogun Helm", "Mode: [select] only\nShould the bot buy \"Ancient Shogun Helm\" ?", false),
        new Option<bool>("69942", "Ancient Shogun Back Arms", "Mode: [select] only\nShould the bot buy \"Ancient Shogun Back Arms\" ?", false),
    };
}
