//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Darkon/CoreDarkon.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AstraviaCastleMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public CoreDarkon Darkon = new();

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
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("astraviacastle", 2043, findIngredients);

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

                case "A Melody":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Logger("This item is not setup yet");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Re's Party Attire":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("astraviajudgement", "La", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("62788", "Astravian Musician", "Mode: [select] only\nShould the bot buy \"Astravian Musician\" ?", false),
        new Option<bool>("62790", "Astravian Musician's Hair", "Mode: [select] only\nShould the bot buy \"Astravian Musician's Hair\" ?", false),
        new Option<bool>("62791", "Astravian Musician's Locks", "Mode: [select] only\nShould the bot buy \"Astravian Musician's Locks\" ?", false),
        new Option<bool>("62796", "Astravian Violinist's Bow", "Mode: [select] only\nShould the bot buy \"Astravian Violinist's Bow\" ?", false),
        new Option<bool>("62913", "Astravian Royal Guard", "Mode: [select] only\nShould the bot buy \"Astravian Royal Guard\" ?", false),
        new Option<bool>("62914", "Astravian Royal Guard's Helmet", "Mode: [select] only\nShould the bot buy \"Astravian Royal Guard's Helmet\" ?", false),
        new Option<bool>("62915", "Astravian Royal Wings", "Mode: [select] only\nShould the bot buy \"Astravian Royal Wings\" ?", false),
        new Option<bool>("62794", "Astravian Sheathed Rapier", "Mode: [select] only\nShould the bot buy \"Astravian Sheathed Rapier\" ?", false),
        new Option<bool>("62916", "Astravian Royal Sword", "Mode: [select] only\nShould the bot buy \"Astravian Royal Sword\" ?", false),
        new Option<bool>("62795", "Astravian Royal Rapier", "Mode: [select] only\nShould the bot buy \"Astravian Royal Rapier\" ?", false),
        new Option<bool>("62917", "Astravian Royal Staff", "Mode: [select] only\nShould the bot buy \"Astravian Royal Staff\" ?", false),
        new Option<bool>("62918", "Astravian Royal Daggers", "Mode: [select] only\nShould the bot buy \"Astravian Royal Daggers\" ?", false),
        new Option<bool>("63000", "Drago's Attire", "Mode: [select] only\nShould the bot buy \"Drago's Attire\" ?", false),
        new Option<bool>("63001", "Drago's Cloaked Attire", "Mode: [select] only\nShould the bot buy \"Drago's Cloaked Attire\" ?", false),
        new Option<bool>("63003", "Drago's Short Hair", "Mode: [select] only\nShould the bot buy \"Drago's Short Hair\" ?", false),
        new Option<bool>("63002", "Drago's Hair", "Mode: [select] only\nShould the bot buy \"Drago's Hair\" ?", false),
        new Option<bool>("63004", "Drago's Beard", "Mode: [select] only\nShould the bot buy \"Drago's Beard\" ?", false),
        new Option<bool>("63006", "Drago's Red Cloak", "Mode: [select] only\nShould the bot buy \"Drago's Red Cloak\" ?", false),
        new Option<bool>("63005", "Drago's Cloak", "Mode: [select] only\nShould the bot buy \"Drago's Cloak\" ?", false),
        new Option<bool>("63011", "Astravian Royal Spear", "Mode: [select] only\nShould the bot buy \"Astravian Royal Spear\" ?", false),
    };
}
