/*
name: ManaCradleMerge
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/ShadowsOfWar/CoreSOfWar.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Skills;
using Skua.Core.Options;

public class ManaCradleMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreSOfWar SofWar = new();
    private CoreSoW SoW = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Willpower", "Garish Remnant", "Prismatic Seams", "Unbound Thread", "Acquiescence", "Elemental Core", "Mainyu Tail", "Mainyu Rune", "Mainyu Wings" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SoW.CompleteCoreSoW();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("manacradle", 2242, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Willpower":
                    Core.AddDrop("ShadowFlame Healer", "ShadowFlame Mage");
                    SofWar.Willpower(quant);
                    break;

                case "Garish Remnant":
                    SofWar.GarishRemnant(quant);
                    break;

                case "Prismatic Seams":
                    SofWar.PrismaticSeams(quant);
                    break;

                case "Unbound Thread":
                    SofWar.UnboundThread(quant);
                    break;

                case "Acquiescence":
                    SofWar.Acquiescence(quant);
                    break;

                case "Elemental Core":
                    SofWar.ElementalCore(quant);
                    break;

                case "Mainyu Rune":
                case "Mainyu Wings":
                case "Mainyu Tail":
                    Core.FarmingLogger(req.Name, quant);
                    if (Core.CheckInventory("Yami no Ronin") || Core.CheckInventory("Dragon of Time"))
                    {
                        Core.AddDrop(SoW.MalgorDrops.Concat(SoW.MainyuDrops).ToArray());
                        Bot.Skills.StartAdvanced(Core.CheckInventory("Yami no Ronin") ? "Yami no Ronin" : "Dragon of Time", true, ClassUseMode.Solo);
                    }
                    else Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("manacradle", "The Mainyu", req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76482", "Dragon's Tear", "Mode: [select] only\nShould the bot buy \"Dragon's Tear\" ?", false),
        new Option<bool>("73781", "Shadowflame Vanguard", "Mode: [select] only\nShould the bot buy \"Shadowflame Vanguard\" ?", false),
        new Option<bool>("73784", "Mana Overdrive Armet", "Mode: [select] only\nShould the bot buy \"Mana Overdrive Armet\" ?", false),
        new Option<bool>("73785", "Mana Overdrive Armet Locks", "Mode: [select] only\nShould the bot buy \"Mana Overdrive Armet Locks\" ?", false),
        new Option<bool>("73787", "Mana Overdrive Shroud", "Mode: [select] only\nShould the bot buy \"Mana Overdrive Shroud\" ?", false),
        new Option<bool>("73789", "Mana Overdriven Devastation", "Mode: [select] only\nShould the bot buy \"Mana Overdriven Devastation\" ?", false),
        new Option<bool>("73791", "Mana Overdriven Spear", "Mode: [select] only\nShould the bot buy \"Mana Overdriven Spear\" ?", false),
        new Option<bool>("73792", "Mana Surge Gauntlet", "Mode: [select] only\nShould the bot buy \"Mana Surge Gauntlet\" ?", false),
        new Option<bool>("73793", "Mana Surge Gauntlets", "Mode: [select] only\nShould the bot buy \"Mana Surge Gauntlets\" ?", false),
        new Option<bool>("76566", "Mainyu Tail and Rune", "Mode: [select] only\nShould the bot buy \"Mainyu Tail and Rune\" ?", false),
        new Option<bool>("76565", "Mainyu Wings and Rune", "Mode: [select] only\nShould the bot buy \"Mainyu Wings and Rune\" ?", false),
        new Option<bool>("76564", "Mainyu Wings and Tail", "Mode: [select] only\nShould the bot buy \"Mainyu Wings and Tail\" ?", false),
        new Option<bool>("76563", "Mainyu Aspects", "Mode: [select] only\nShould the bot buy \"Mainyu Aspects\" ?", false),
    };
}
