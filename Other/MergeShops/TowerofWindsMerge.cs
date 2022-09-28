//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Glacera.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TowerofWindsMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public GlaceraStory Glacera = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Frozen Tower Merge Token", "Bits of Cloth", "Pieces of Glass", "Metal bits", "Bits of Hair", "Pieces of Cloth", "Ice Crystals", "Mercury", "Metal Pieces", "Flame of Courage", "Karok's Glaceran Gem " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("glacera", 1055, findIngredients);

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

                case "Frozen Tower Merge Token":
                    Glacera.FrozenTower();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3939);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Clear the Wolves 3939
                        Core.HuntMonster("frozentower", "Ice Wolf", "Ice Wolf Slain", 7);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bits of Cloth":
                case "Pieces of Glass":
                case "Metal bits":
                case "Bits of Hair":
                    Core.EquipClass(ClassType.Farm);
                    Core.EnsureAccept(3955);
                    Core.HuntMonster("frozentower", "Frostwyrm", req.Name, quant, isTemp: false);
                    break;

                case "Pieces of Cloth":
                case "Ice Crystals":
                case "Metal Pieces":
                    Core.EquipClass(ClassType.Farm);
                    Core.EnsureAccept(3955);
                    Core.HuntMonster("frozentower", "Polar Elemental", req.Name, quant, isTemp: false);
                    break;


                case "Flame of Courage":
                    Glacera.FrozenTower();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3955);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Flame of Courage 3955
                        Core.HuntMonster("frozenruins", "Frost Invader", "Spark of Courage");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Karok's Glaceran Gem":
                    Core.EnsureAccept(3955);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("northstar", "Karok the Fallen", req.Name, quant, isTemp: false);
                    break;


                case "Mercury":
                    Glacera.FrozenTower();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3956);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        ///Quick as silver 3956
                        Core.HuntMonster("frozenruins", "Arctic Eel", "Quicker Silver");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("27479", "Northern Lights Wanderer", "Mode: [select] only\nShould the bot buy \"Northern Lights Wanderer\" ?", false),
        new Option<bool>("27469", "Northern Lights Polearm", "Mode: [select] only\nShould the bot buy \"Northern Lights Polearm\" ?", false),
        new Option<bool>("27470", "Aurora Wand", "Mode: [select] only\nShould the bot buy \"Aurora Wand\" ?", false),
        new Option<bool>("27475", "Frozen Fear Lad", "Mode: [select] only\nShould the bot buy \"Frozen Fear Lad\" ?", false),
        new Option<bool>("27474", "Frozen Fear Lass", "Mode: [select] only\nShould the bot buy \"Frozen Fear Lass\" ?", false),
        new Option<bool>("27473", "Astromancer Locks", "Mode: [select] only\nShould the bot buy \"Astromancer Locks\" ?", false),
        new Option<bool>("27472", "Astromancer Hair", "Mode: [select] only\nShould the bot buy \"Astromancer Hair\" ?", false),
        new Option<bool>("27471", "Aurora Dawn Cape", "Mode: [select] only\nShould the bot buy \"Aurora Dawn Cape\" ?", false),
        new Option<bool>("27434", "Winged Aurora Staff", "Mode: [select] only\nShould the bot buy \"Winged Aurora Staff\" ?", false),
        new Option<bool>("25007", "Holy Healer", "Mode: [select] only\nShould the bot buy \"Holy Healer\" ?", false),
        new Option<bool>("25009", "Holy Healer Locks", "Mode: [select] only\nShould the bot buy \"Holy Healer Locks\" ?", false),
        new Option<bool>("25010", "Holy Healer Hair", "Mode: [select] only\nShould the bot buy \"Holy Healer Hair\" ?", false),
        new Option<bool>("25011", "Holy Healer Halo", "Mode: [select] only\nShould the bot buy \"Holy Healer Halo\" ?", false),
        new Option<bool>("25012", "Holy Healer Halo Locks", "Mode: [select] only\nShould the bot buy \"Holy Healer Halo Locks\" ?", false),
        new Option<bool>("27458", "BitterEdge Broadsword", "Mode: [select] only\nShould the bot buy \"BitterEdge Broadsword\" ?", false),
        new Option<bool>("27464", "BitterEdge Wand", "Mode: [select] only\nShould the bot buy \"BitterEdge Wand\" ?", false),
        new Option<bool>("27484", "Frozen FireOrb Mace", "Mode: [select] only\nShould the bot buy \"Frozen FireOrb Mace\" ?", false),
        new Option<bool>("27485", "Frozen FireOrb Staff", "Mode: [select] only\nShould the bot buy \"Frozen FireOrb Staff\" ?", false),
        new Option<bool>("27706", "Scythe of Vengeance", "Mode: [select] only\nShould the bot buy \"Scythe of Vengeance\" ?", false),
        new Option<bool>("27774", "Cold Scythe of Vengeance", "Mode: [select] only\nShould the bot buy \"Cold Scythe of Vengeance\" ?", false),
        new Option<bool>("27775", "Frigid Scythe of Vengeance", "Mode: [select] only\nShould the bot buy \"Frigid Scythe of Vengeance\" ?", false),
        new Option<bool>("27776", "Fallen Scythe of Vengeance", "Mode: [select] only\nShould the bot buy \"Fallen Scythe of Vengeance\" ?", false),
    };
}
