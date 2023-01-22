/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Nation/FiendPast.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FiendPastMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public FiendPast Fiend = new();
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
        Fiend.DoAll();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("fiendpast", 2106, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Nation Medallion":
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8495);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fiendpast", "Proto-Legion Knight", "Legionnaire Defeated", 10);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fiendish Outlaw Revolver":
                case "Fiendish Outlaw Bowie Knife":
                case "Fiendish Outlaw Sheathed Shotgun":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("fiendpast", "Dage the Lich", req.Name, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("61121", "Vengeance of Nulgath", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath\" ?", false),
        new Option<bool>("61122", "Vengeance of Nulgath Hair", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Hair\" ?", false),
        new Option<bool>("61123", "Vengeance of Nulgath Helm", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Helm\" ?", false),
        new Option<bool>("61124", "Vengeance of Nulgath Morph", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Morph\" ?", false),
        new Option<bool>("61125", "Vengeance of Nulgath Ascension Helm", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Ascension Helm\" ?", false),
        new Option<bool>("61126", "Vengeance of Nulgath Cape", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Cape\" ?", false),
        new Option<bool>("61127", "Vengeance of Nulgath Ascension Cape", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Ascension Cape\" ?", false),
        new Option<bool>("61128", "Vengeance of Nulgath Axe", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Axe\" ?", false),
        new Option<bool>("61129", "Vengeance of Nulgath Spear", "Mode: [select] only\nShould the bot buy \"Vengeance of Nulgath Spear\" ?", false),
        new Option<bool>("61130", "Fiendish Vengeance GreatSword", "Mode: [select] only\nShould the bot buy \"Fiendish Vengeance GreatSword\" ?", false),
        new Option<bool>("61131", "Nulgath’s Vengeance Orb Pet", "Mode: [select] only\nShould the bot buy \"Nulgath’s Vengeance Orb Pet\" ?", false),
        new Option<bool>("61132", "Vengeance Blade BattlePet", "Mode: [select] only\nShould the bot buy \"Vengeance Blade BattlePet\" ?", false),
        new Option<bool>("66991", "Fiendish Outlaw Battlegear", "Mode: [select] only\nShould the bot buy \"Fiendish Outlaw Battlegear\" ?", false),
        new Option<bool>("66992", "Fiendish Outlaw Quickdraw", "Mode: [select] only\nShould the bot buy \"Fiendish Outlaw Quickdraw\" ?", false),
    };
}
