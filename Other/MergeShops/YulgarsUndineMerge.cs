/*
name: Yulgars Undine Merge
description: This bot will farm the items belonging to the selected mode for the Yulgars Undine Merge [2297] in /sunlightzone
tags: yulgars, undine, merge, sunlightzone, poisonous, rogue, nightshade, thorn, assasasin, reversed, thorns, envenomed, whip, agony, elven, assassins, scarf, , assassin, guardian, wreath
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/AgeofRuin/CoreAOR.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YulgarsUndineMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AoR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Venomous Rose", "Undine Visitor Badge" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AoR.YulgarAria();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sunlightzone", 2297, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Venomous Rose":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9274);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("twilightzone", "Leviathan", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Undine Visitor Badge":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("sunlightzone", "Astravian Illusion", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("78285", "Poisonous Rogue", "Mode: [select] only\nShould the bot buy \"Poisonous Rogue\" ?", false),
        new Option<bool>("78286", "Nightshade Thorn Assasasin", "Mode: [select] only\nShould the bot buy \"Nightshade Thorn Assasasin\" ?", false),
        new Option<bool>("78293", "Reversed Blade of Thorns", "Mode: [select] only\nShould the bot buy \"Reversed Blade of Thorns\" ?", false),
        new Option<bool>("78294", "Reversed Daggers of Thorns", "Mode: [select] only\nShould the bot buy \"Reversed Daggers of Thorns\" ?", false),
        new Option<bool>("78296", "Envenomed Gauntlet", "Mode: [select] only\nShould the bot buy \"Envenomed Gauntlet\" ?", false),
        new Option<bool>("78295", "Envenomed Whip of Agony", "Mode: [select] only\nShould the bot buy \"Envenomed Whip of Agony\" ?", false),
        new Option<bool>("78288", "Elven Assassin's Scarf", "Mode: [select] only\nShould the bot buy \"Elven Assassin's Scarf\" ?", false),
        new Option<bool>("78290", "Elven Assassin’s Scarf + Locks", "Mode: [select] only\nShould the bot buy \"Elven Assassin’s Scarf + Locks\" ?", false),
        new Option<bool>("78292", "Nightshade Assassin Guardian", "Mode: [select] only\nShould the bot buy \"Nightshade Assassin Guardian\" ?", false),
        new Option<bool>("78287", "Elven Assassin's Hair", "Mode: [select] only\nShould the bot buy \"Elven Assassin's Hair\" ?", false),
        new Option<bool>("78289", "Elven Assassin Locks", "Mode: [select] only\nShould the bot buy \"Elven Assassin Locks\" ?", false),
        new Option<bool>("78291", "Poisonous Thorn Wreath", "Mode: [select] only\nShould the bot buy \"Poisonous Thorn Wreath\" ?", false),
    };
}