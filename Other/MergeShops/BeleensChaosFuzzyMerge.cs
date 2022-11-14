//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BeleensChaosFuzzyMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSummer Summer = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Chaos Fuzzies "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        Summer.Beleen();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("pastelia", 135, findIngredients);

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

                case "Chaos Fuzzies":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.GetMapItem(3481, 1, "citadel");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30121", "Chaos Fuzzy", "Mode: [select] only\nShould the bot buy \"Chaos Fuzzy\" ?", false),
        new Option<bool>("30185", "Chibi Nulgath Armor", "Mode: [select] only\nShould the bot buy \"Chibi Nulgath Armor\" ?", false),
        new Option<bool>("30089", "Spikes of Pink Love", "Mode: [select] only\nShould the bot buy \"Spikes of Pink Love\" ?", false),
        new Option<bool>("30088", "Locks of Pink Love", "Mode: [select] only\nShould the bot buy \"Locks of Pink Love\" ?", false),
        new Option<bool>("30169", "Chaos Beleen Polearm", "Mode: [select] only\nShould the bot buy \"Chaos Beleen Polearm\" ?", false),
        new Option<bool>("30189", "Reavers of Chaos", "Mode: [select] only\nShould the bot buy \"Reavers of Chaos\" ?", false),
        new Option<bool>("30171", "Chaos Beleen Blade", "Mode: [select] only\nShould the bot buy \"Chaos Beleen Blade\" ?", false),
        new Option<bool>("30172", "Chaos Beleen Axe", "Mode: [select] only\nShould the bot buy \"Chaos Beleen Axe\" ?", false),
        new Option<bool>("30173", "Chaos Beleen Scythe", "Mode: [select] only\nShould the bot buy \"Chaos Beleen Scythe\" ?", false),
        new Option<bool>("30187", "Chaos Beleen Cloud", "Mode: [select] only\nShould the bot buy \"Chaos Beleen Cloud\" ?", false),
        new Option<bool>("30175", "Chibi Nulgath Pet", "Mode: [select] only\nShould the bot buy \"Chibi Nulgath Pet\" ?", false),
        new Option<bool>("30177", "Lotsa Love Makai Battlepet", "Mode: [select] only\nShould the bot buy \"Lotsa Love Makai Battlepet\" ?", false),
        new Option<bool>("30170", "Cutie Makai On Your Back", "Mode: [select] only\nShould the bot buy \"Cutie Makai On Your Back\" ?", false),
        new Option<bool>("30192", "Chibi Nulgath Bank Pet", "Mode: [select] only\nShould the bot buy \"Chibi Nulgath Bank Pet\" ?", false),
        new Option<bool>("30186", "Chibi Nulgath Morph", "Mode: [select] only\nShould the bot buy \"Chibi Nulgath Morph\" ?", false),
    };
}
