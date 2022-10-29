//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShipWreck.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShipWreckMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public ShipWreck ShipWreck = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Enchanted Gauntlet Leather", "Anti-Au Crystals "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        ShipWreck.StoryLine();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shipwreck", 105, findIngredients);

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

                case "Enchanted Gauntlet Leather":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4429);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shipwreck", "Gilded Water", "Lifeless Water", 14);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Anti-Au Crystals":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4430);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shipwreck", "Gilded Crystal Undead", "Crystal Crew Shards", 8);
                        Core.HuntMonster("shipwreck", "Captain Nubar", "Pirate Pistols", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30503", "Gilded Dragonlord", "Mode: [select] only\nShould the bot buy \"Gilded Dragonlord\" ?", false),
        new Option<bool>("30504", "Gilded Dragon Faceplate", "Mode: [select] only\nShould the bot buy \"Gilded Dragon Faceplate\" ?", false),
        new Option<bool>("30505", "Gilded Dragon Crown", "Mode: [select] only\nShould the bot buy \"Gilded Dragon Crown\" ?", false),
        new Option<bool>("30506", "Amber Dragon Cape", "Mode: [select] only\nShould the bot buy \"Amber Dragon Cape\" ?", false),
        new Option<bool>("30507", "Amber Dragon Wings", "Mode: [select] only\nShould the bot buy \"Amber Dragon Wings\" ?", false),
        new Option<bool>("30508", "Aurous Blessing", "Mode: [select] only\nShould the bot buy \"Aurous Blessing\" ?", false),
        new Option<bool>("30509", "Golden Dragon Cape", "Mode: [select] only\nShould the bot buy \"Golden Dragon Cape\" ?", false),
        new Option<bool>("30510", "Golden Dragon Wings", "Mode: [select] only\nShould the bot buy \"Golden Dragon Wings\" ?", false),
        new Option<bool>("30441", "Killer Coral Crown", "Mode: [select] only\nShould the bot buy \"Killer Coral Crown\" ?", false),
        new Option<bool>("30442", "Shell Warrior Shag", "Mode: [select] only\nShould the bot buy \"Shell Warrior Shag\" ?", false),
        new Option<bool>("30443", "Shell Warrior Locks", "Mode: [select] only\nShould the bot buy \"Shell Warrior Locks\" ?", false),
        new Option<bool>("30445", "Splashy Shag", "Mode: [select] only\nShould the bot buy \"Splashy Shag\" ?", false),
        new Option<bool>("30446", "Splashy Locks", "Mode: [select] only\nShould the bot buy \"Splashy Locks\" ?", false),
        new Option<bool>("30092", "Wrath of Lobthulu", "Mode: [select] only\nShould the bot buy \"Wrath of Lobthulu\" ?", false),
        new Option<bool>("30093", "Jewel of the Sea", "Mode: [select] only\nShould the bot buy \"Jewel of the Sea\" ?", false),
        new Option<bool>("30094", "Gilded Widowmaker", "Mode: [select] only\nShould the bot buy \"Gilded Widowmaker\" ?", false),
        new Option<bool>("30433", "Aquatic Defender", "Mode: [select] only\nShould the bot buy \"Aquatic Defender\" ?", false),
        new Option<bool>("30440", "Killer Coral Crown Locks", "Mode: [select] only\nShould the bot buy \"Killer Coral Crown Locks\" ?", false),
        new Option<bool>("30556", "Treasure Egg", "Mode: [select] only\nShould the bot buy \"Treasure Egg\" ?", false),
    };
}
