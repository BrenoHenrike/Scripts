/*
name: Temple Delve Merge
description: This will get all or selected items on this merge shop.
tags: temple. delve, merge, seasonal, nulgath, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelve.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TempleDelveMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreNation Nation = new();
    private TempleDelve TD = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Doomed Extract", "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath", "Nation Ritualist", "Void Nation Ritualist"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("templedelve"))
            return;

        TD.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("templedelve", 2232, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Doomed Extract":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9090);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("templedelve", "Delirious Elemental", "Elemental Study", 6, log: false);
                        Core.HuntMonster("templedelve", "Infested Nation", "Infestation Study", 6, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("templedelve", "Doomed Fiend", "Fiend Worm", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Tainted Gem":
                    Nation.SwindleBulk(quant);
                    break;

                case "Dark Crystal Shard":
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Nation Ritualist":
                case "Void Nation Ritualist":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("templedelve", "Doomed Fiend", req.Name, isTemp: false, log: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("75493", "Nation Ritualist Visage", "Mode: [select] only\nShould the bot buy \"Nation Ritualist Visage\" ?", false),
        new Option<bool>("75495", "Nation Ritualist Morph + Locks", "Mode: [select] only\nShould the bot buy \"Nation Ritualist Morph + Locks\" ?", false),
        new Option<bool>("75491", "Horned Void Nation Ritualist Visage", "Mode: [select] only\nShould the bot buy \"Horned Void Nation Ritualist Visage\" ?", false),
        new Option<bool>("75498", "Nation Ritualist Void Gate", "Mode: [select] only\nShould the bot buy \"Nation Ritualist Void Gate\" ?", false),
        new Option<bool>("75497", "Nation Ritualist Gateway", "Mode: [select] only\nShould the bot buy \"Nation Ritualist Gateway\" ?", false),
        new Option<bool>("75499", "Nation Ritualist Orb Pet", "Mode: [select] only\nShould the bot buy \"Nation Ritualist Orb Pet\" ?", false),
        new Option<bool>("75500", "Nation Ritualist Rune", "Mode: [select] only\nShould the bot buy \"Nation Ritualist Rune\" ?", false),
        new Option<bool>("75501", "Nation Ritualist's Sigil", "Mode: [select] only\nShould the bot buy \"Nation Ritualist's Sigil\" ?", false),
        new Option<bool>("75503", "Nation Ritualist's Dual Swirling Sigils", "Mode: [select] only\nShould the bot buy \"Nation Ritualist's Dual Swirling Sigils\" ?", false),
        new Option<bool>("75505", "Void Nation Ritualist Spear", "Mode: [select] only\nShould the bot buy \"Void Nation Ritualist Spear\" ?", false),
        new Option<bool>("75487", "Void Nation Caster", "Mode: [select] only\nShould the bot buy \"Void Nation Caster\" ?", false),
        new Option<bool>("75489", "Nation Caster", "Mode: [select] only\nShould the bot buy \"Nation Caster\" ?", false),
        new Option<bool>("75681", "Scholar of Nulgath", "Mode: [select] only\nShould the bot buy \"Scholar of Nulgath\" ?", false),
        new Option<bool>("75682", "Scholar of Nulgath Hair", "Mode: [select] only\nShould the bot buy \"Scholar of Nulgath Hair\" ?", false),
        new Option<bool>("75683", "Scholar of Nulgath Ponytail", "Mode: [select] only\nShould the bot buy \"Scholar of Nulgath Ponytail\" ?", false),
        new Option<bool>("75684", "Scholar of Nulgath Scarf", "Mode: [select] only\nShould the bot buy \"Scholar of Nulgath Scarf\" ?", false),
        new Option<bool>("75685", "Prof Polish", "Mode: [select] only\nShould the bot buy \"Prof Polish\" ?", false),
    };
}
