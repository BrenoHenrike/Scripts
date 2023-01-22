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
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class StreamwarMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreSoW SoW = new();
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
        SoW.CompleteCoreSoW();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("streamwar", 2163, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Avatar's Flame Bow":
                case "Avatar's Flame Spikes":
                case "Avatar's Flame Banners":
                case "Avatar's Flame Sabre":
                case "Avatar's Flame":
                case "Avatar's Flame Guard":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Streamwar", "Second Speaker", req.Name, isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Willpower":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8788);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8);
                        Core.HuntMonster($"ruinedcrown", "Mana-Burdened Mage", "Mage’s Blood Sample", 8);
                        Core.HuntMonster($"ruinedcrown", "Calamitous Warlic", "Warlic’s Favor");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Garish Remnant":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.RegisterQuests(8813);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("Timekeep", "Mal-formed Gar", "Gar's Resignation Letter");
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("Timekeep", "Mumbler", "Mumbler Drool", 8);
                        Core.HuntMonster("Timekeep", "Decaying Locust", "Locust Wings", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Prismatic Seams":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8814, 8815);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.KillMonster("Streamwar", "r3a", "Left", "*", log: false);
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("71904", "Bright Mana Flame", "Mode: [select] only\nShould the bot buy \"Bright Mana Flame\" ?", false),
        new Option<bool>("71899", "Bright Mana Flame Guard", "Mode: [select] only\nShould the bot buy \"Bright Mana Flame Guard\" ?", false),
        new Option<bool>("71900", "Bright Mana Flame Spikes", "Mode: [select] only\nShould the bot buy \"Bright Mana Flame Spikes\" ?", false),
        new Option<bool>("71901", "Bright Mana Flame Banner", "Mode: [select] only\nShould the bot buy \"Bright Mana Flame Banner\" ?", false),
        new Option<bool>("71902", "Bright Mana Flame Sabre", "Mode: [select] only\nShould the bot buy \"Bright Mana Flame Sabre\" ?", false),
        new Option<bool>("71903", "Bright Mana Flame Bow", "Mode: [select] only\nShould the bot buy \"Bright Mana Flame Bow\" ?", false),
        new Option<bool>("72003", "Dark Mana Flame", "Mode: [select] only\nShould the bot buy \"Dark Mana Flame\" ?", false),
        new Option<bool>("72004", "Mana Flame Skull", "Mode: [select] only\nShould the bot buy \"Mana Flame Skull\" ?", false),
        new Option<bool>("72005", "Mana Flame Hair", "Mode: [select] only\nShould the bot buy \"Mana Flame Hair\" ?", false),
        new Option<bool>("72006", "Dark Mana Flame Guard", "Mode: [select] only\nShould the bot buy \"Dark Mana Flame Guard\" ?", false),
        new Option<bool>("72007", "Dark Mana Flame Spikes", "Mode: [select] only\nShould the bot buy \"Dark Mana Flame Spikes\" ?", false),
        new Option<bool>("72008", "Dark Mana Flame Banner", "Mode: [select] only\nShould the bot buy \"Dark Mana Flame Banner\" ?", false),
        new Option<bool>("72009", "Dark Mana Flame Sabre", "Mode: [select] only\nShould the bot buy \"Dark Mana Flame Sabre\" ?", false),
        new Option<bool>("72010", "Dark Mana Flame Bow", "Mode: [select] only\nShould the bot buy \"Dark Mana Flame Bow\" ?", false),
        new Option<bool>("71827", "Green Dragon Slayer", "Mode: [select] only\nShould the bot buy \"Green Dragon Slayer\" ?", false),
        new Option<bool>("71828", "Green Dragon Slayer's Sallet", "Mode: [select] only\nShould the bot buy \"Green Dragon Slayer's Sallet\" ?", false),
        new Option<bool>("71829", "Green Dragon Slayer's Winged Sallet", "Mode: [select] only\nShould the bot buy \"Green Dragon Slayer's Winged Sallet\" ?", false),
        new Option<bool>("71830", "Green Dragon Slayer's Cloak", "Mode: [select] only\nShould the bot buy \"Green Dragon Slayer's Cloak\" ?", false),
        new Option<bool>("71831", "Green Dragon Slayer's Halberd", "Mode: [select] only\nShould the bot buy \"Green Dragon Slayer's Halberd\" ?", false),
        new Option<bool>("71905", "Dark Dragon Slayer", "Mode: [select] only\nShould the bot buy \"Dark Dragon Slayer\" ?", false),
        new Option<bool>("71906", "Dark Dragon Slayer's Sallet", "Mode: [select] only\nShould the bot buy \"Dark Dragon Slayer's Sallet\" ?", false),
        new Option<bool>("71907", "Dark Dragon Slayer's Winged Sallet", "Mode: [select] only\nShould the bot buy \"Dark Dragon Slayer's Winged Sallet\" ?", false),
        new Option<bool>("71908", "Dark Dragon Slayer's Cloak", "Mode: [select] only\nShould the bot buy \"Dark Dragon Slayer's Cloak\" ?", false),
        new Option<bool>("71909", "Dark Dragon Slayer's Halberd", "Mode: [select] only\nShould the bot buy \"Dark Dragon Slayer's Halberd\" ?", false),
    };
}
