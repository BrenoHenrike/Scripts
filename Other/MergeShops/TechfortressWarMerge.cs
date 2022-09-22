//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TechfortressWarMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public PinkBladeOfDestruciton PBOD = new();
    public CoreDailies Dailies = new();
    public CoreBLOD BLOD = new();

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
        Adv.StartBuyAllMerge("techfortress", 1902, findIngredients);

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

                case "Deadtech War Medal":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7638, 7638, 7639, 7641);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("techfortress", "Enter", "Spawn", "*", log: false);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Pink Blade of Destruction":
                    PBOD.GetPBoD();
                    break;

                case "Unicorn Essence":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("undergroundlabb", "Ultra Brutalcorn", "Unicorn Essence", 5, false);
                    break;

                case "Silver":
                    Dailies.MineCrafting(new[] { "Silver" }, quant);
                    break;

                case "Spirit Orb":
                    BLOD.SpiritOrb(quant);
                    break;

                case "Loyal Spirit Orb":
                    BLOD.FindingFragmentsBlade(0, quant);
                    break;

                case "Blinding Aura":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.CheckInventory("Blinding Bow of Destiny"))
                        BLOD.BlindingBow();
                    while (!Bot.ShouldExit && !Core.CheckInventory("Blinding Aura"))
                    {
                        BLOD.FindingFragments(2174);
                        Bot.Wait.ForPickup("Blinding Aura");
                    }
                    break;

                case "Sanctified Silver of Destiny":
                    Core.FarmingLogger($"{req.Name}", quant);
                    if (!Core.CheckInventory("Sanctified Silver"))
                    {
                        Core.FarmingLogger("Sanctified Silver", 1);
                        Core.EnsureAccept(2108);
                        Farm.BattleUnderB("Undead Energy", 25);
                        Dailies.MineCrafting(new[] { "Silver" });
                        if (!Core.CheckInventory("Silver"))
                            Core.Logger("Can't complete Sanctified Silver Enchantment (Missing Silver).", messageBox: true, stopBot: true);
                        BLOD.UltimateWK("Spirit Orb", 5);
                        Core.HuntMonster("arcangrove", "Seed Spitter", "Paladaffodil", 25);
                        Core.EnsureComplete(2108);
                    }
                    Core.Logger("Farming for Sanctified Silver of Destiny");
                    BLOD.UltimateWK("Loyal Spirit Orb", 5);
                    BLOD.UltimateWK("Bright Aura", 2);
                    Core.BuyItem("dwarfhold", 434, "Sanctified Silver of Destiny");
                    break;

                case "Brilliant Aura":
                    BLOD.FindingFragmentsBow(quant);
                    break;

                case "Shard of An Orb":
                    Core.FarmingLogger($"{req.Name}", quant);
                    BLOD.DoAll();
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7654);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster($"dflesson", "r12", "Right", "Fluffy the Dracolich", "Fluffy’s Bones", 10, isTemp: false);
                        Core.KillMonster("dflesson", "r3", "Right", "Fire Elemental", "Fire Elemental's Bracer", 5, isTemp: false);
                        Core.KillMonster("dflesson", "r6", "Right", "Tog", "Tog Claw", 5, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Purified Undead Dragon Essence":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.RegisterQuests(7655, 7291);
                    Core.AddDrop("Rainbow Moonstone");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (!Core.CheckInventory("Rainbow Moonstone", 5))
                        {
                            Core.EquipClass(ClassType.Farm);
                            while (!Bot.ShouldExit && !Core.CheckInventory("Rainbow Moonstone", 5))
                            {
                                Core.HuntMonster("earthstorm", "Diamond Golem", "Chip of Diamond");
                                Core.HuntMonster("earthstorm", "Emerald Golem", "Chip of Emerald");
                                Core.HuntMonster("earthstorm", "Ruby Golem", "Chip of Ruby");
                                Core.HuntMonster("earthstorm", "Sapphire Golem", "Chip of Sapphire");

                                Bot.Wait.ForPickup("Rainbow Moonstone");
                            }
                        }
                        Core.EquipClass(ClassType.Solo);
                        Core.KillMonster("doomwood", "r10", "Right", "Undead Paladin", "Purification Orb", 10, isTemp: false);
                        Core.KillMonster("desolich", "r3", "Left", "Desolich", "Desolich's Dark Horn", 3, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("55484", "Deadtech Necromancer", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer\" ?", false),
        new Option<bool>("55485", "Deadtech Necromancer Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer Hood\" ?", false),
        new Option<bool>("55486", "Deadtech Necromancer Masked Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer Masked Hood\" ?", false),
        new Option<bool>("55487", "Deadtech Necromancer Skull Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer Skull Hood\" ?", false),
        new Option<bool>("55488", "Deadtech Necromancer Tech Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer Tech Hood\" ?", false),
        new Option<bool>("55490", "Deadtech Necromancer Skeleton Arms", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer Skeleton Arms\" ?", false),
        new Option<bool>("55491", "Deadtech Necromancer Hatchet", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer Hatchet\" ?", false),
        new Option<bool>("55492", "Deadtech Necromancer Staff", "Mode: [select] only\nShould the bot buy \"Deadtech Necromancer Staff\" ?", false),
        new Option<bool>("55493", "Deadtech Lich", "Mode: [select] only\nShould the bot buy \"Deadtech Lich\" ?", false),
        new Option<bool>("55494", "Deadtech Lich Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Lich Hood\" ?", false),
        new Option<bool>("55495", "Deadtech Lich Masked Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Lich Masked Hood\" ?", false),
        new Option<bool>("55496", "Deadtech Lich Skull Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Lich Skull Hood\" ?", false),
        new Option<bool>("55497", "Deadtech Lich Borg Hood", "Mode: [select] only\nShould the bot buy \"Deadtech Lich Borg Hood\" ?", false),
        new Option<bool>("55498", "Deadtech Lich Hatchet", "Mode: [select] only\nShould the bot buy \"Deadtech Lich Hatchet\" ?", false),
        new Option<bool>("55499", "Deadtech Lich Staff", "Mode: [select] only\nShould the bot buy \"Deadtech Lich Staff\" ?", false),
        new Option<bool>("55897", "Mechanized Armblades", "Mode: [select] only\nShould the bot buy \"Mechanized Armblades\" ?", false),
        new Option<bool>("55885", "Uni-Blade of Destruction", "Mode: [select] only\nShould the bot buy \"Uni-Blade of Destruction\" ?", false),
        new Option<bool>("55886", "Rainbow Uni-Blade of Destruction", "Mode: [select] only\nShould the bot buy \"Rainbow Uni-Blade of Destruction\" ?", false),
        new Option<bool>("55889", "Ultimate Blinding Light of Destiny", "Mode: [select] only\nShould the bot buy \"Ultimate Blinding Light of Destiny\" ?", false),
        new Option<bool>("55893", "Ultimate Twin Blades of Destiny", "Mode: [select] only\nShould the bot buy \"Ultimate Twin Blades of Destiny\" ?", false),
        new Option<bool>("55892", "Ultimate DragonStaff of Destiny", "Mode: [select] only\nShould the bot buy \"Ultimate DragonStaff of Destiny\" ?", false),
    };
}
