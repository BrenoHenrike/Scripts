/*
name: Ectocave Mergeshop
description: Gets any item you want in the Ectocave Mergeshop
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EctocaveMerge
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
        Core.BankingBlackList.AddRange(new[] { "Ichorus Scythe Piece", "Ichorus Egg", "Pure Ichor Gem", "Slime", "Dragon Rogue", "Dragon Rogue Klinge", "Dragon Rogue Hood", "Dragon Rogue Twin Klinge Cape", "Piece of Fabric", "Bone"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ectocave", 1039, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ichorus Scythe Piece":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3874);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ectocave", "Ichor Dracolich", "Uncut Emerald", 9, log: false);
                        Core.HuntMonster("ectocave", "Ektorax", "Brilliant Diamond", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ichorus Egg":
                    if (!Bot.Player.IsMember)
                        break;

                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ectocave", "Ektorax", "Ichorus Egg");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Pure Ichor Gem":
                    if (!Bot.Player.IsMember)
                        break;

                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3873);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ectocave", "Ichor Draconian", "Uncut Ichor Gem", 50, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Slime":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ectocave", "Swamp Lurker");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
    
                case "Dragon Rogue Klinge":
                case "Dragon Rogue":
                case "Dragon Rogue Hood":
                case "Dragon Rogue Twin Klinge Cape":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ectocave", "Ektorax");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Piece of Fabric":
                case "Bone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ectocave", "Ichor Draconian");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
                    
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("26839", "Slimed DragonMaster Scythe", "Mode: [select] only\nShould the bot buy \"Slimed DragonMaster Scythe\" ?", false),
        new Option<bool>("26838", "Ektorax Pet", "Mode: [select] only\nShould the bot buy \"Ektorax Pet\" ?", false),
        new Option<bool>("26076", "Slime Dragon Rogue", "Mode: [select] only\nShould the bot buy \"Slime Dragon Rogue\" ?", false),
        new Option<bool>("26077", "Slime Dragon Rogue Lann", "Mode: [select] only\nShould the bot buy \"Slime Dragon Rogue Lann\" ?", false),
        new Option<bool>("26078", "Slime Dragon Rogue Helm", "Mode: [select] only\nShould the bot buy \"Slime Dragon Rogue Helm\" ?", false),
        new Option<bool>("26079", "Slime Dragon Rogue Lann Cape", "Mode: [select] only\nShould the bot buy \"Slime Dragon Rogue Lann Cape\" ?", false),
        new Option<bool>("26300", "Slime Cloak", "Mode: [select] only\nShould the bot buy \"Slime Cloak\" ?", false),
        new Option<bool>("26299", "Slime Sword", "Mode: [select] only\nShould the bot buy \"Slime Sword\" ?", false),
        new Option<bool>("26263", "Sassy Slime Helm", "Mode: [select] only\nShould the bot buy \"Sassy Slime Helm\" ?", false),
        new Option<bool>("26262", "Slime Helm", "Mode: [select] only\nShould the bot buy \"Slime Helm\" ?", false),
    };
}
