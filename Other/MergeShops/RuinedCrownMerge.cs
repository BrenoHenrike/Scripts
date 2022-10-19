//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class RuinedCrownMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoW SoW = new();

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

    public void BuyAllMerge()
    {
        SoW.RuinedCrown();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ruinedcrown", 2156, findIngredients);

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
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(8788);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster($"ruinedcrown", "Calamitous Warlic", "Warlic’s Favor");
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8);
                        Core.HuntMonster($"ruinedcrown", "Mana-Burdened Mage", "Mage’s Blood Sample", 8);
                        Core.EnsureComplete(8788);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "ShadowFlame Warrior":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ruinedcrown", "Mana-Burdened Knight", req.Name, isTemp: false);
                    }
                    break;

                case "ShadowFlame Mage":
                case "ShadowFlame Healer":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ruinedcrown", "Mana-Burdened Mage", req.Name, isTemp: false);
                    }
                    break;

                case "ShadowFlame Rogue":
                case "ShadowFlame Rogue’s Mask":
                case "ShadowFlame Rogue’s Mortal Locks":
                case "ShadowFlame Rogue’s Locks":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ruinedcrown", "Mana-Burdened Minion", req.Name, isTemp: false);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70606", "ShadowFlame Defender", "Mode: [select] only\nShould the bot buy \"ShadowFlame Defender\" ?", false),
        new Option<bool>("70607", "ShadowFlame Defender’s Crest", "Mode: [select] only\nShould the bot buy \"ShadowFlame Defender’s Crest\" ?", false),
        new Option<bool>("70608", "ShadowFlame Defender’s Hair", "Mode: [select] only\nShould the bot buy \"ShadowFlame Defender’s Hair\" ?", false),
        new Option<bool>("70609", "ShadowFlame Defender’s Horn", "Mode: [select] only\nShould the bot buy \"ShadowFlame Defender’s Horn\" ?", false),
        new Option<bool>("70611", "ShadowFlame Defender’s Horned Skull", "Mode: [select] only\nShould the bot buy \"ShadowFlame Defender’s Horned Skull\" ?", false),
        new Option<bool>("70612", "ShadowFlame Defender’s Wing", "Mode: [select] only\nShould the bot buy \"ShadowFlame Defender’s Wing\" ?", false),
        new Option<bool>("70616", "ShadowFlame Defender’s Spear", "Mode: [select] only\nShould the bot buy \"ShadowFlame Defender’s Spear\" ?", false),
        new Option<bool>("71601", "Enchanted ShadowFlame Warrior", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowFlame Warrior\" ?", false),
        new Option<bool>("71602", "Enchanted ShadowFlame Mage", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowFlame Mage\" ?", false),
        new Option<bool>("71603", "Enchanted ShadowFlame Healer", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowFlame Healer\" ?", false),
        new Option<bool>("71604", "Enchanted ShadowFlame Rogue", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowFlame Rogue\" ?", false),
        new Option<bool>("71605", "Enchanted Rogue’s Mask", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue’s Mask\" ?", false),
        new Option<bool>("71606", "Enchanted Rogue’s Mortal Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue’s Mortal Locks\" ?", false),
        new Option<bool>("71607", "Enchanted Rogue’s Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue’s Locks\" ?", false),
    };
}
