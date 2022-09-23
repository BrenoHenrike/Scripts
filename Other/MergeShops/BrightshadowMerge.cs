//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BrightshadowMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoC SoC = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Shadow BeastMaster", "Venerated Essence", "Shadow BeastMaster's Locks", "Shadow BeastMaster's Shag", "Shadow BeastMaster's Beard", "Shadow BeastMaster Bow", "Shadow BeastMaster Knuckle", "ShadowFlame Glaive", "Shadow BeastMaster Hood", "Shadow BeastMaster Hood + Mask", "Shadow BeastMaster Quiver", "ShadowFlame Spellsword", "Blight Essence", "ShadowFlame Spellsword's Sheathed Blade", "ShadowFlame Spellsword's Hip Blade", "SpellSword's Flame Blade", "ShadowFlame SpellSword's Blade", "ShadowFlame SpellSword's Daggers", "SpellSword's Reversed Daggers", "ShadowFlame SpellSword's Tome " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        SoC.BrightShadow();
        SoC.BrightChaos();
        Adv.StartBuyAllMerge("brightshadow", 1921, findIngredients);

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

                case "Shadow BeastMaster":
                case "Shadow BeastMaster Bow":
                case "Shadow BeastMaster Hood":
                case "Shadow BeastMaster Hood + Mask":
                case "Shadow BeastMaster Knuckle":
                case "Shadow BeastMaster Quiver":
                case "Shadow BeastMaster's Beard":
                case "Shadow BeastMaster's Locks":
                case "Shadow BeastMaster's Shag":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("brightshadow", "Gravelyn the Good", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Venerated Essence":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7738);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("brightshadow", "Brightfall Light", "BrightFall Light", 5);
                        Core.HuntMonster("brightshadow", "Brightfall Guard|Shadowflame Paladin", "BrightFall Dark", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "ShadowFlame Glaive":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("chaosamulet", "Shadowflame Warrior", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;


                case "ShadowFlame Spellsword":
                case "ShadowFlame Spellsword's Sheathed Blade":
                case "ShadowFlame Spellsword's Hip Blade":
                case "ShadowFlame SpellSword's Blade":
                case "ShadowFlame SpellSword's Daggers":
                case "ShadowFlame SpellSword's Tome":
                case "SpellSword's Flame Blade":
                case "SpellSword's Reversed Daggers":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("brightchaos", "Blight", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Blight Essence":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7750);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("brightchaos", "Blight", "Blight Subdued", 4);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("56461", "Eternal BeastMaster", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster\" ?", false),
        new Option<bool>("56462", "Eternal BeastMaster's Locks", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster's Locks\" ?", false),
        new Option<bool>("56463", "Eternal BeastMaster's Shag", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster's Shag\" ?", false),
        new Option<bool>("56464", "Eternal Beastmaster's Beard", "Mode: [select] only\nShould the bot buy \"Eternal Beastmaster's Beard\" ?", false),
        new Option<bool>("56465", "Eternal BeastMaster's Bow", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster's Bow\" ?", false),
        new Option<bool>("56466", "Eternal BeastMaster's Knuckle", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster's Knuckle\" ?", false),
        new Option<bool>("56467", "Eternal BeastMaster's Glaive", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster's Glaive\" ?", false),
        new Option<bool>("57089", "Eternal BeastMaster Hood", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster Hood\" ?", false),
        new Option<bool>("57090", "Eternal BeastMaster Hood + Mask", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster Hood + Mask\" ?", false),
        new Option<bool>("57094", "Eternal BeastMaster Quiver", "Mode: [select] only\nShould the bot buy \"Eternal BeastMaster Quiver\" ?", false),
        new Option<bool>("56269", "Enchanted Spellsword", "Mode: [select] only\nShould the bot buy \"Enchanted Spellsword\" ?", false),
        new Option<bool>("56280", "Enchanted Spellsword's Sheathed Blade", "Mode: [select] only\nShould the bot buy \"Enchanted Spellsword's Sheathed Blade\" ?", false),
        new Option<bool>("56281", "Enchanted Spellsword's Hip Blade", "Mode: [select] only\nShould the bot buy \"Enchanted Spellsword's Hip Blade\" ?", false),
        new Option<bool>("56284", "Enchanted SpellSword's Flame Blade", "Mode: [select] only\nShould the bot buy \"Enchanted SpellSword's Flame Blade\" ?", false),
        new Option<bool>("56285", "Enchanted SpellSword's Blade", "Mode: [select] only\nShould the bot buy \"Enchanted SpellSword's Blade\" ?", false),
        new Option<bool>("56288", "Enchanted SpellSword's Daggers", "Mode: [select] only\nShould the bot buy \"Enchanted SpellSword's Daggers\" ?", false),
        new Option<bool>("56289", "Enchanted Reversed Daggers", "Mode: [select] only\nShould the bot buy \"Enchanted Reversed Daggers\" ?", false),
        new Option<bool>("56291", "Enchanted SpellSword's Tome", "Mode: [select] only\nShould the bot buy \"Enchanted SpellSword's Tome\" ?", false),
    };
}
