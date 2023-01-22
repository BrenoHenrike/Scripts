/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Other/Materials/DarknessShard.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LegionPyromancerMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();
    public DarknessShard DShard = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("underworld", 1734, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Crystallized Blood":
                    Core.FarmingLogger($"{req.Name}", quant);
                    if (!Bot.Quests.IsAvailable(793))
                        return;
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6976);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Bloodfiend", "Fiend Blood", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Token":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Legion.FarmLegionToken(quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Flaming Skull":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Frozen Pyromancer", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Darkness Shard":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        DShard.GetShard(quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Flame-Forged Metal":
                    if (!Bot.Quests.IsAvailable(793))
                        return;
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6975);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Frozen Pyromancer", "Stolen Flame");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Soul-Forged Metal":
                    if (!Bot.Quests.IsAvailable(793))
                        return;
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6977);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Frozen Pyromancer", "Pyromancer Soul Shard");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48692", "Legion Pyromancer", "Mode: [select] only\nShould the bot buy \"Legion Pyromancer\" ?", false),
        new Option<bool>("48693", "Darkflame Hair", "Mode: [select] only\nShould the bot buy \"Darkflame Hair\" ?", false),
        new Option<bool>("48694", "Darkflame Morph", "Mode: [select] only\nShould the bot buy \"Darkflame Morph\" ?", false),
        new Option<bool>("48696", "Darkflame Denizen Morph", "Mode: [select] only\nShould the bot buy \"Darkflame Denizen Morph\" ?", false),
        new Option<bool>("48695", "Darkflame Horned Morph", "Mode: [select] only\nShould the bot buy \"Darkflame Horned Morph\" ?", false),
        new Option<bool>("48697", "Darkflame Ponytail", "Mode: [select] only\nShould the bot buy \"Darkflame Ponytail\" ?", false),
        new Option<bool>("48698", "Darkflame Morph Locks", "Mode: [select] only\nShould the bot buy \"Darkflame Morph Locks\" ?", false),
        new Option<bool>("48700", "Infernal Legion Athame", "Mode: [select] only\nShould the bot buy \"Infernal Legion Athame\" ?", false),
        new Option<bool>("48701", "Reversed Legion Athame", "Mode: [select] only\nShould the bot buy \"Reversed Legion Athame\" ?", false),
        new Option<bool>("48702", "Dark Flame of Fury", "Mode: [select] only\nShould the bot buy \"Dark Flame of Fury\" ?", false),
        new Option<bool>("48703", "Dark Flames of Fury", "Mode: [select] only\nShould the bot buy \"Dark Flames of Fury\" ?", false),
        new Option<bool>("48704", "Legion Invocation Tome", "Mode: [select] only\nShould the bot buy \"Legion Invocation Tome\" ?", false),
        new Option<bool>("48705", "Inferno's Legion Staff", "Mode: [select] only\nShould the bot buy \"Inferno's Legion Staff\" ?", false),
        new Option<bool>("48699", "Infernal Legion Minion", "Mode: [select] only\nShould the bot buy \"Infernal Legion Minion\" ?", false),
        new Option<bool>("48706", "Legion Moglin Minion", "Mode: [select] only\nShould the bot buy \"Legion Moglin Minion\" ?", false),
        new Option<bool>("48707", "Legion Moglin Minion Battlepet", "Mode: [select] only\nShould the bot buy \"Legion Moglin Minion Battlepet\" ?", false),
        new Option<bool>("49143", "Chanky Pet", "Mode: [select] only\nShould the bot buy \"Chanky Pet\" ?", false),
    };
}
