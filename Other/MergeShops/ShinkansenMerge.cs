/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Shinkansen.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShinkansenMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public Shinkansen Shink = new();

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
        Adv.StartBuyAllMerge("shinkansen", 2005, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ninjo":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Shink.Storyline();
                    Core.RegisterQuests(8124);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Shinkansen", "Crystallis Soldier", "Favor Done", 30);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("61529", "Purified Crystallis Soldier", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier\" ?", false),
        new Option<bool>("61547", "Apa's Outfit", "Mode: [select] only\nShould the bot buy \"Apa's Outfit\" ?", false),
        new Option<bool>("61555", "Eta's Outfit", "Mode: [select] only\nShould the bot buy \"Eta's Outfit\" ?", false),
        new Option<bool>("61536", "Purified Crystallis Soldier's Locks + Hat", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier's Locks + Hat\" ?", false),
        new Option<bool>("61537", "Purified Crystallis Soldier's Hat", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier's Hat\" ?", false),
        new Option<bool>("61538", "Purified Crystallis Soldier's Battle Gear", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier's Battle Gear\" ?", false),
        new Option<bool>("61539", "Purified Crystallis Soldier's Hat + Glasses", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier's Hat + Glasses\" ?", false),
        new Option<bool>("61548", "Apa's Morph + Locks", "Mode: [select] only\nShould the bot buy \"Apa's Morph + Locks\" ?", false),
        new Option<bool>("61549", "Apa's Locks", "Mode: [select] only\nShould the bot buy \"Apa's Locks\" ?", false),
        new Option<bool>("61556", "Eta's Morph", "Mode: [select] only\nShould the bot buy \"Eta's Morph\" ?", false),
        new Option<bool>("61557", "Eta's Hairstyle", "Mode: [select] only\nShould the bot buy \"Eta's Hairstyle\" ?", false),
        new Option<bool>("61541", "Purified Crystallis Soldier's Cape", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier's Cape\" ?", false),
        new Option<bool>("61542", "Crystallis Soldier's Hologram", "Mode: [select] only\nShould the bot buy \"Crystallis Soldier's Hologram\" ?", false),
        new Option<bool>("61550", "Apa's Lightblades", "Mode: [select] only\nShould the bot buy \"Apa's Lightblades\" ?", false),
        new Option<bool>("61558", "The First Saint's Halo", "Mode: [select] only\nShould the bot buy \"The First Saint's Halo\" ?", false),
        new Option<bool>("61544", "Purified Crystallis Soldier's Cannon", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier's Cannon\" ?", false),
        new Option<bool>("61546", "Purified Crystallis Soldier's Gun + Shield", "Mode: [select] only\nShould the bot buy \"Purified Crystallis Soldier's Gun + Shield\" ?", false),
        new Option<bool>("61551", "Apa's Grace", "Mode: [select] only\nShould the bot buy \"Apa's Grace\" ?", false),
        new Option<bool>("61552", "Purified Malice", "Mode: [select] only\nShould the bot buy \"Purified Malice\" ?", false),
        new Option<bool>("61553", "Sheathed Purified Malice", "Mode: [select] only\nShould the bot buy \"Sheathed Purified Malice\" ?", false),
        new Option<bool>("61559", "Tarnished Grace", "Mode: [select] only\nShould the bot buy \"Tarnished Grace\" ?", false),
        new Option<bool>("61560", "Eta's Malice", "Mode: [select] only\nShould the bot buy \"Eta's Malice\" ?", false),
        new Option<bool>("61561", "Sheathed Eta's Malice", "Mode: [select] only\nShould the bot buy \"Sheathed Eta's Malice\" ?", false),
        new Option<bool>("61554", "Apa's Grace Battlepet", "Mode: [select] only\nShould the bot buy \"Apa's Grace Battlepet\" ?", false),
        new Option<bool>("61562", "Eta's Malice Battlepet", "Mode: [select] only\nShould the bot buy \"Eta's Malice Battlepet\" ?", false),
    };
}
