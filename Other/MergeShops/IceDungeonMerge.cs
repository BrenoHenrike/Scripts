//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Glacera.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class IceDungeonMerge
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
        Core.BankingBlackList.AddRange(new[] { "Icy Token I", "Icy Token II", "Icy Token III", "Icy Token IV", "Warrior of Kyanos", "Glacial Envoy’s Helm", "Glacial Portal", "Glacial Envoy’s Wrap", "Floating Glacial Shards Mace", "Warrior of Kyanos Daggers", "Glacial Envoy's Buzzcut", "Glacial Envoy's Locks " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("icedungeon", 1948, findIngredients);

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

                case "Icy Token I":
                    Glacera.IceDungeon();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7838);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Basic Ingredients 7838
                        Core.HuntMonster("icedungeon", "Frosted Banshee", "Frosted Banshee Defeated", 10);
                        Core.HuntMonster("icedungeon", "Frozen Undead", "Frozen Undead Defeated", 10);
                        Core.HuntMonster("icedungeon", "Ice Symbiote", "Ice Symbiote Defeated", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Icy Token II":
                    Glacera.IceDungeon();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7839);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Cool Flavor 7839
                        Core.HuntMonster("icedungeon", "Spirit of Ice", "Spirit of Ice Defeated", 10);
                        Core.HuntMonster("icedungeon", "Ice Crystal", "Ice Crystal Defeated", 10);
                        Core.HuntMonster("icedungeon", "Frigid Spirit", "Frigid Spirit Defeated", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Icy Token III":
                    Glacera.IceDungeon();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7840);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Chilled to Perfection 7840
                        Core.HuntMonster("icedungeon", "Living Ice", "Living Ice Defeated", 5);
                        Core.HuntMonster("icedungeon", "Crystallized Elemental", "Crystallized Elemental Defeated", 5);
                        Core.HuntMonster("icedungeon", "Frozen Demon", "Frozen Demon Defeated", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Icy Token IV":
                    Glacera.IceDungeon();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7841);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Icing on the Cake 7841
                        Core.HuntMonster("icedungeon", "Image of Glace", "Glace's Approval");
                        Core.HuntMonster("icedungeon", "Abel", "Abel's Approval");
                        Core.HuntMonster("icedungeon", "Shade of Kyanos", "Kyanos' Approval");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Warrior of Kyanos":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("icedungeon", "Shade of Kyanos", req.Name, isTemp: false);
                    break;

                case "Glacial Envoy’s Helm":
                case "Glacial Envoy’s Wrap":
                case "Warrior of Kyanos Daggers":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("icedungeon", "Abel", req.Name, isTemp: false);
                    break;

                case "Glacial Portal":
                case "Floating Glacial Shards Mace":
                case "Glacial Envoy's Buzzcut":
                case "Glacial Envoy's Locks":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("icedungeon", "Image of Glace", req.Name, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("58262", "Favored of Kyanos", "Mode: [select] only\nShould the bot buy \"Favored of Kyanos\" ?", false),
        new Option<bool>("58266", "Glacial Envoy's Crested Helm", "Mode: [select] only\nShould the bot buy \"Glacial Envoy's Crested Helm\" ?", false),
        new Option<bool>("58267", "Glacial Envoy's Hood", "Mode: [select] only\nShould the bot buy \"Glacial Envoy's Hood\" ?", false),
        new Option<bool>("58279", "Kyanos' Spirit + Portal", "Mode: [select] only\nShould the bot buy \"Kyanos' Spirit + Portal\" ?", false),
        new Option<bool>("58278", "Glacial Arch + Wrap", "Mode: [select] only\nShould the bot buy \"Glacial Arch + Wrap\" ?", false),
        new Option<bool>("58282", "Dual Floating Glacial Shards", "Mode: [select] only\nShould the bot buy \"Dual Floating Glacial Shards\" ?", false),
        new Option<bool>("58283", "Favored of Kyanos Polearm", "Mode: [select] only\nShould the bot buy \"Favored of Kyanos Polearm\" ?", false),
        new Option<bool>("58261", "Envoy of Kyanos", "Mode: [select] only\nShould the bot buy \"Envoy of Kyanos\" ?", false),
        new Option<bool>("58269", "Frozen Envoy's Crested Helm", "Mode: [select] only\nShould the bot buy \"Frozen Envoy's Crested Helm\" ?", false),
        new Option<bool>("58268", "Glacial Envoy's Furred Hood", "Mode: [select] only\nShould the bot buy \"Glacial Envoy's Furred Hood\" ?", false),
        new Option<bool>("58271", "Envoy's Icy Morph + Hair", "Mode: [select] only\nShould the bot buy \"Envoy's Icy Morph + Hair\" ?", false),
        new Option<bool>("58273", "Envoy's Icy Morph + Locks", "Mode: [select] only\nShould the bot buy \"Envoy's Icy Morph + Locks\" ?", false),
        new Option<bool>("58277", "Kyanos' Spirit + Wrap", "Mode: [select] only\nShould the bot buy \"Kyanos' Spirit + Wrap\" ?", false),
        new Option<bool>("58284", "Envoy of Kyanos Blade", "Mode: [select] only\nShould the bot buy \"Envoy of Kyanos Blade\" ?", false),
    };
}
