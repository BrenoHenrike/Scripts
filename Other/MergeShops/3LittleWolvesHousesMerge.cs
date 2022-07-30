//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ThreeLittleWolvesHousesMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = true;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("buyHouse", 1729, findIngredients);

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
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping."), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                // Add how to get items here
                case "Building Material":
                    Core.RegisterQuests(6915);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("farm", "Treeant", "Wooden Planks", 5);
                        Core.HuntMonster("bloodtusk", "Rhison", "Glue");
                        Core.HuntMonster("crashsite", "ProtoSartorium", "Nails", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Foundation Material":
                    Core.RegisterQuests(6916);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("river", "Zardman Fisher", "River Stones", 5);
                        Core.HuntMonster("dwarfprison", "Balboa", "Boulder", 3);
                        Core.HuntMonster("dragonplane", "Earth Elemental", "Marble");
                        Core.HuntMonster("gilead", "Fire Elemental", "Flames", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Decor Material":
                    Core.RegisterQuests(6917);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("farm", "Scarecrow", "Fabric", 5);
                        Core.HuntMonster("goose", "Can of Paint", "Paint", 5);
                        Core.HuntMonster("undergroundlabb", "Window", "Glass", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48516", "Dragonrune House", "Mode: [select] only\nShould the bot buy \"Dragonrune House\" ?", false),
        new Option<bool>("48517", "Dragonrune Hall", "Mode: [select] only\nShould the bot buy \"Dragonrune Hall\" ?", false),
        new Option<bool>("48518", "Arcangrove Tower House", "Mode: [select] only\nShould the bot buy \"Arcangrove Tower House\" ?", false),
        new Option<bool>("48765", "Tower of Magic House", "Mode: [select] only\nShould the bot buy \"Tower of Magic House\" ?", false),
        new Option<bool>("48766", "Falcontower House", "Mode: [select] only\nShould the bot buy \"Falcontower House\" ?", false),
        new Option<bool>("48767", "Citadel Caverns House", "Mode: [select] only\nShould the bot buy \"Citadel Caverns House\" ?", false),
        new Option<bool>("48771", "Citadel House", "Mode: [select] only\nShould the bot buy \"Citadel House\" ?", false),
        new Option<bool>("48768", "Seraphic Fortress", "Mode: [select] only\nShould the bot buy \"Seraphic Fortress\" ?", false),
        new Option<bool>("48769", "Hachiko Hotel", "Mode: [select] only\nShould the bot buy \"Hachiko Hotel\" ?", false),
        new Option<bool>("48770", "Clubhouse", "Mode: [select] only\nShould the bot buy \"Clubhouse\" ?", false),
    };
}

