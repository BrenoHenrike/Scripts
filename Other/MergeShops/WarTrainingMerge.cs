/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Armor/FireChampionsArmor.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class WarTrainingMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoW Tynd = new();
    public WarfuryEmblem Emblem = new();
    public DragonslayerGeneral DSG = new();
    public FireChampionsArmor FCA = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Tynd.Tyndarius(true);
        Adv.StartBuyAllMerge("wartraining", 2035, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Warfury Emblem":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Emblem.WarfuryEmblemFarm(quant);
                    break;

                case "WarFury Soldier's Morph":
                case "WarFury Soldier's Armor":
                case "WarFury Soldier's Blade":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("wartraining", "Varga", req.Name, isTemp: false);
                    break;

                case "Enchanted Scale":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    DSG.EnchantedScaleandClaw(quant, 0);
                    break;

                case "Flame-Forged Metal":
                    if (!Bot.Quests.IsAvailable(793))
                    {
                        Core.Logger("Legion required.");
                        return;
                    }
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    FCA.FlameForgedMetal(quant);
                    break;

                case "Void Scale":
                    Core.FarmingLogger($"{req.Name}", quant);
                    FCA.VoidScale(quant);
                    break;

                case "Dragon Scale":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("lair", "Bronze Draconian|Dark Draconian", req.Name, quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("61750", "Ruby DragonTamer", "Mode: [select] only\nShould the bot buy \"Ruby DragonTamer\" ?", false),
        new Option<bool>("61751", "Ruby DragonTamer's Helmet", "Mode: [select] only\nShould the bot buy \"Ruby DragonTamer's Helmet\" ?", false),
        new Option<bool>("61752", "DragonTamer's Morph + Locks", "Mode: [select] only\nShould the bot buy \"DragonTamer's Morph + Locks\" ?", false),
        new Option<bool>("61753", "DragonTamer's Morph", "Mode: [select] only\nShould the bot buy \"DragonTamer's Morph\" ?", false),
        new Option<bool>("61754", "DragonTamer's Blade", "Mode: [select] only\nShould the bot buy \"DragonTamer's Blade\" ?", false),
        new Option<bool>("61755", "DragonTamer's Poleaxe", "Mode: [select] only\nShould the bot buy \"DragonTamer's Poleaxe\" ?", false),
        new Option<bool>("61756", "DragonTamer's Tail", "Mode: [select] only\nShould the bot buy \"DragonTamer's Tail\" ?", false),
        new Option<bool>("61757", "DragonTamer's Wings", "Mode: [select] only\nShould the bot buy \"DragonTamer's Wings\" ?", false),
        new Option<bool>("61758", "DragonTamer's Wings + Tail", "Mode: [select] only\nShould the bot buy \"DragonTamer's Wings + Tail\" ?", false),
        new Option<bool>("58462", "Polished DragonSlayer", "Mode: [select] only\nShould the bot buy \"Polished DragonSlayer\" ?", false),
        new Option<bool>("58463", "Polished DragonSlayer's Helm", "Mode: [select] only\nShould the bot buy \"Polished DragonSlayer's Helm\" ?", false),
        new Option<bool>("58464", "Woven DragonSlayer's Cloak", "Mode: [select] only\nShould the bot buy \"Woven DragonSlayer's Cloak\" ?", false),
        new Option<bool>("62570", "Fire Champion's Armor", "Mode: [select] only\nShould the bot buy \"Fire Champion's Armor\" ?", false),
        new Option<bool>("62571", "Fire Champion's Armet", "Mode: [select] only\nShould the bot buy \"Fire Champion's Armet\" ?", false),
        new Option<bool>("62572", "Fire Champion's Cloak", "Mode: [select] only\nShould the bot buy \"Fire Champion's Cloak\" ?", false),
        new Option<bool>("62607", "WarFury Elite's Armor", "Mode: [select] only\nShould the bot buy \"WarFury Elite's Armor\" ?", false),
        new Option<bool>("62608", "WarFury Elite's Morph", "Mode: [select] only\nShould the bot buy \"WarFury Elite's Morph\" ?", false),
        new Option<bool>("62609", "WarFury Elite's Blade", "Mode: [select] only\nShould the bot buy \"WarFury Elite's Blade\" ?", false),
    };
}
