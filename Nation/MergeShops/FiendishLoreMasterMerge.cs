//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;
using Skua.Core.Models.Skills;


public class FiendishLoreMasterMerge
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
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Abyssal Lore Scrap", "ArchFiend Mage's Wand", "ArchFiend Mage's Tome " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("tercessuinotlim", 2103, findIngredients);

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

                case "Abyssal Lore Scrap":
                    Core.FarmingLogger(req.Name, quant);

                    if (Core.CheckInventory("Great Thief"))
                        Bot.Skills.StartAdvanced("Great Thief", true, ClassUseMode.Def);
                    else if (Core.CheckInventory("Yami no Ronin"))
                        Bot.Skills.StartAdvanced("Yami no Ronin)", true, ClassUseMode.Def);
                    else Core.EquipClass(ClassType.Solo);

                    Core.RegisterQuests(8475);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Room for Improvement 8475
                        Core.KillMonster("Tercessuinotlim", "Boss2", "Right", "Nulgath", "Archfiend Analysis");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "ArchFiend Mage's Wand":
                case "ArchFiend Mage's Tome":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Tercessuinotlim", "Evil Elemental", req.Name, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("66936", "ArchFiend Warrior", "Mode: [select] only\nShould the bot buy \"ArchFiend Warrior\" ?", false),
        new Option<bool>("66937", "ArchFiend Warrior Armet", "Mode: [select] only\nShould the bot buy \"ArchFiend Warrior Armet\" ?", false),
        new Option<bool>("66938", "ArchFiend Warrior Helm", "Mode: [select] only\nShould the bot buy \"ArchFiend Warrior Helm\" ?", false),
        new Option<bool>("66939", "ArchFiend Warrior Sword", "Mode: [select] only\nShould the bot buy \"ArchFiend Warrior Sword\" ?", false),
        new Option<bool>("66940", "Dual ArchFiend Warrior Swords", "Mode: [select] only\nShould the bot buy \"Dual ArchFiend Warrior Swords\" ?", false),
        new Option<bool>("66941", "ArchFiend Warrior Champion Sword", "Mode: [select] only\nShould the bot buy \"ArchFiend Warrior Champion Sword\" ?", false),
        new Option<bool>("66942", "Dual ArchFiend Warrior Champion Swords", "Mode: [select] only\nShould the bot buy \"Dual ArchFiend Warrior Champion Swords\" ?", false),
        new Option<bool>("66950", "ArchFiend Mage", "Mode: [select] only\nShould the bot buy \"ArchFiend Mage\" ?", false),
        new Option<bool>("66951", "ArchFiend Mage Hat", "Mode: [select] only\nShould the bot buy \"ArchFiend Mage Hat\" ?", false),
        new Option<bool>("66952", "ArchFiend Mage Hood", "Mode: [select] only\nShould the bot buy \"ArchFiend Mage Hood\" ?", false),
        new Option<bool>("66955", "ArchFiend Mage Book + Wand", "Mode: [select] only\nShould the bot buy \"ArchFiend Mage Book + Wand\" ?", false),
        new Option<bool>("66962", "ArchFiend Rogue", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue\" ?", false),
        new Option<bool>("66963", "ArchFiend Rogue Hood", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue Hood\" ?", false),
        new Option<bool>("66964", "ArchFiend Rogue Mask", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue Mask\" ?", false),
        new Option<bool>("66965", "ArchFiend Rogue Mask + Hood", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue Mask + Hood\" ?", false),
        new Option<bool>("66966", "ArchFiend Rogue Backwards Knife", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue Backwards Knife\" ?", false),
        new Option<bool>("66967", "ArchFiend Rogue Backwards Knives", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue Backwards Knives\" ?", false),
        new Option<bool>("66968", "ArchFiend Rogue Knife", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue Knife\" ?", false),
        new Option<bool>("66969", "ArchFiend Rogue Knives", "Mode: [select] only\nShould the bot buy \"ArchFiend Rogue Knives\" ?", false),
        new Option<bool>("66975", "ArchFiend Healer", "Mode: [select] only\nShould the bot buy \"ArchFiend Healer\" ?", false),
        new Option<bool>("66976", "ArchFiend Healer Hood", "Mode: [select] only\nShould the bot buy \"ArchFiend Healer Hood\" ?", false),
        new Option<bool>("66977", "ArchFiend Healer Staff", "Mode: [select] only\nShould the bot buy \"ArchFiend Healer Staff\" ?", false),
        new Option<bool>("67085", "Fiendish Librarian Hair + Glasses", "Mode: [select] only\nShould the bot buy \"Fiendish Librarian Hair + Glasses\" ?", false),
        new Option<bool>("67086", "Fiendish Librarian Locks + Glasses", "Mode: [select] only\nShould the bot buy \"Fiendish Librarian Locks + Glasses\" ?", false),
        new Option<bool>("66928", "ArchVoid Warrior", "Mode: [select] only\nShould the bot buy \"ArchVoid Warrior\" ?", false),
        new Option<bool>("66929", "ArchVoid Warrior Mask", "Mode: [select] only\nShould the bot buy \"ArchVoid Warrior Mask\" ?", false),
        new Option<bool>("66930", "ArchVoid Mage", "Mode: [select] only\nShould the bot buy \"ArchVoid Mage\" ?", false),
        new Option<bool>("66931", "ArchVoid Mage Hood", "Mode: [select] only\nShould the bot buy \"ArchVoid Mage Hood\" ?", false),
        new Option<bool>("66932", "ArchVoid Rogue", "Mode: [select] only\nShould the bot buy \"ArchVoid Rogue\" ?", false),
        new Option<bool>("66933", "ArchVoid Rogue Horn", "Mode: [select] only\nShould the bot buy \"ArchVoid Rogue Horn\" ?", false),
        new Option<bool>("66934", "ArchVoid Healer", "Mode: [select] only\nShould the bot buy \"ArchVoid Healer\" ?", false),
        new Option<bool>("66935", "ArchVoid Healer Horn", "Mode: [select] only\nShould the bot buy \"ArchVoid Healer Horn\" ?", false),
        new Option<bool>("67082", "Fiendish Loremaster", "Mode: [select] only\nShould the bot buy \"Fiendish Loremaster\" ?", false),
    };
}
