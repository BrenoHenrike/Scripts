/*
name: Champion Drakath Merge
description: This bot will farm the items belonging to the selected mode for the Champion Drakath Merge [2055] in /championdrakath
tags: champion, drakath, merge, championdrakath, empowered, original, chaos, avengers, greatsword, avenger, polished, dragon, control, supreme, arcane, colorful, rose, royal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ChampionDrakathMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public Core13LoC LOC => new();
    private DrakathArmorBot DAB = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    // If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Drakath Armor", "Champion Drakath Insignia", "Original Drakath Armor", "Blade of Chaos", "Chaos Avenger's Greatsword", "Chaos Avenger Armor", "Legendary Sword of Dragon Control", "The Supreme Arcane Staff", "Discordia Rose of Chaos", "Chaos Rose" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isCompletedBefore(8301))
        {
            Core.Logger("Quest \"Chaos Avenger Class\" required, gl.. it requires an army");
            return;
        }
        LOC.Hero();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("championdrakath", 2055, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Drakath Armor":
                    DAB.DrakathArmorQuest();
                    break;

                case "Champion Drakath Insignia":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Logger("You have to kill Champion Drakath weekly to get his insignias, use your army to kill it easily");
                        return;
                    }
                    break;

                case "Original Drakath Armor":
                    DAB.DrakathOriginalArmor();
                    break;

                case "Blade of Chaos":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("ultradrakath", "Champion of Chaos", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Chaos Avenger's Greatsword":
                    Core.EquipClass(ClassType.Solo);
                    Adv.BuyItem("championdrakath", 2056, req.Name);
                    break;

                case "Legendary Sword of Dragon Control":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.KillVath(req.Name, quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "The Supreme Arcane Staff":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("ledgermayne", "Ledgermayne", "The Supreme Arcane Staff", 1, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Discordia Rose of Chaos":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("palooza", "Chaos Lord Discordia", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Chaos Rose":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("palooza", "Chaos Lord Discordia", req.Name, quant, false, false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("64151", "Empowered Drakath Armor", "Mode: [select] only\nShould the bot buy \"Empowered Drakath Armor\" ?", false),
        new Option<bool>("64152", "Empowered Original Drakath Armor", "Mode: [select] only\nShould the bot buy \"Empowered Original Drakath Armor\" ?", false),
        new Option<bool>("64153", "Empowered Blade of Chaos", "Mode: [select] only\nShould the bot buy \"Empowered Blade of Chaos\" ?", false),
        new Option<bool>("64154", "Empowered Chaos Avenger's Greatsword", "Mode: [select] only\nShould the bot buy \"Empowered Chaos Avenger's Greatsword\" ?", false),
        new Option<bool>("64155", "Empowered Chaos Avenger Armor", "Mode: [select] only\nShould the bot buy \"Empowered Chaos Avenger Armor\" ?", false),
        new Option<bool>("60988", "Polished Sword of Dragon Control", "Mode: [select] only\nShould the bot buy \"Polished Sword of Dragon Control\" ?", false),
        new Option<bool>("60990", "Polished Supreme Arcane Staff", "Mode: [select] only\nShould the bot buy \"Polished Supreme Arcane Staff\" ?", false),
        new Option<bool>("60991", "Colorful Chaos Rose", "Mode: [select] only\nShould the bot buy \"Colorful Chaos Rose\" ?", false),
        new Option<bool>("60992", "Royal Chaos Rose", "Mode: [select] only\nShould the bot buy \"Royal Chaos Rose\" ?", false),
    };
}
