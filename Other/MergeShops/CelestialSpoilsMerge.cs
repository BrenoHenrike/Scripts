/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Story/DjinnGuard.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CelestialSpoilsMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public DjinnGuard DjinnGuard = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Celestial Coin", "Blade of the Fallen Djinn", "Blade of the Djinn King " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("djinnguard", 1582, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Celestial Coin":
                    DjinnGuard.CompleteDjinnGuard();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6275);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Celestial Spoils: Djinn Warrior 6275
                        Core.HuntMonster("DjinnGuard", "Air Spirit", "Air Essence", 3, log: false);
                        Core.HuntMonster("DjinnGuard", "Water Spirit", "Water Essence", 3, log: false);
                        Core.HuntMonster("DjinnGuard", "Earth Spirit", "Earth Essence", 3, log: false);
                        Core.HuntMonster("DjinnGuard", "Fire Spirit", "Fire Essence", 3, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Blade of the Fallen Djinn":
                case "Blade of the Djinn King":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("DjinnGuard", "Image of Crulon", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43411", "Djinn Warrior", "Mode: [select] only\nShould the bot buy \"Djinn Warrior\" ?", false),
        new Option<bool>("43412", "Djinn Warrior Hair", "Mode: [select] only\nShould the bot buy \"Djinn Warrior Hair\" ?", false),
        new Option<bool>("43413", "Djinn Warrior Locks", "Mode: [select] only\nShould the bot buy \"Djinn Warrior Locks\" ?", false),
        new Option<bool>("43414", "Djinn Warrior Sack", "Mode: [select] only\nShould the bot buy \"Djinn Warrior Sack\" ?", false),
        new Option<bool>("43415", "Djinn Warrior Blade + Sack", "Mode: [select] only\nShould the bot buy \"Djinn Warrior Blade + Sack\" ?", false),
        new Option<bool>("43416", "Djinn Warrior Blade", "Mode: [select] only\nShould the bot buy \"Djinn Warrior Blade\" ?", false),
        new Option<bool>("43417", "Djinn Warrior Daggers", "Mode: [select] only\nShould the bot buy \"Djinn Warrior Daggers\" ?", false),
        new Option<bool>("43438", "Blades of the Fallen Djinn", "Mode: [select] only\nShould the bot buy \"Blades of the Fallen Djinn\" ?", false),
        new Option<bool>("43439", "Blades of the Djinn King", "Mode: [select] only\nShould the bot buy \"Blades of the Djinn King\" ?", false),
        new Option<bool>("44337", "Dragon Dicers", "Mode: [select] only\nShould the bot buy \"Dragon Dicers\" ?", false),
        new Option<bool>("44338", "Greatsword of Emeralds", "Mode: [select] only\nShould the bot buy \"Greatsword of Emeralds\" ?", false),
        new Option<bool>("44339", "Greatsword of Sapphires", "Mode: [select] only\nShould the bot buy \"Greatsword of Sapphires\" ?", false),
        new Option<bool>("44340", "Katana of Sapphires", "Mode: [select] only\nShould the bot buy \"Katana of Sapphires\" ?", false),
        new Option<bool>("44341", "Katana of Emeralds", "Mode: [select] only\nShould the bot buy \"Katana of Emeralds\" ?", false),
        new Option<bool>("44342", "Staff of Sapphires", "Mode: [select] only\nShould the bot buy \"Staff of Sapphires\" ?", false),
        new Option<bool>("44343", "Staff of Emeralds", "Mode: [select] only\nShould the bot buy \"Staff of Emeralds\" ?", false),
        new Option<bool>("61899", "Toxic Wanderer", "Mode: [select] only\nShould the bot buy \"Toxic Wanderer\" ?", false),
        new Option<bool>("61900", "Toxic Wanderer's Hood", "Mode: [select] only\nShould the bot buy \"Toxic Wanderer's Hood\" ?", false),
        new Option<bool>("61901", "Toxic Wanderer's Sheathed Blade", "Mode: [select] only\nShould the bot buy \"Toxic Wanderer's Sheathed Blade\" ?", false),
        new Option<bool>("61902", "Toxic Wanderer's Khopesh", "Mode: [select] only\nShould the bot buy \"Toxic Wanderer's Khopesh\" ?", false),
    };
}
