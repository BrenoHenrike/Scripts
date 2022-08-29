//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Story/ArtixWedding.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BeleensMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreLegion CoreLegion = new();
    public LegionExercise3 LegionExercise3 = new();
    public LegionExercise4 LegionExercise4 = new();
    public CoreNation CoreNation = new();
    public TarosManslayer TarosManslayer = new();
    public ArtixWedding ArtixWedding = new();

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
        Adv.StartBuyAllMerge("tower", 347, findIngredients);

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

                case "Platinum Wings":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    if (Farm.FactionRank("Good") < 7)
                    {
                        Core.Logger("This item requires Good Faction Rank 7. starting GoodREP script");
                        Farm.GoodREP(7);
                    }
                    Adv.BuyItem("Castle", 88, req.Name);
                    break;

                case "Fuchsia Dye":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.AddDrop(new[] { "Fuchsia Dye", "Magenta Dye" });
                    if (Core.IsMember)
                    {
                        Core.RegisterQuests(1491);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            //Dyeing for Gemstones [Membership] 1491
                            Core.HuntMonster("DarkoviaForest", "Lich Of The Stone", "Garnet Gem", 2);
                            Core.HuntMonster("Cornelis", "Gargoyle", "Spinel Gem", 6);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    else
                    {
                        Core.RegisterQuests(1489);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            //Flowers for the Pink Gal 1489
                            Core.HuntMonster("Sandsea", "Cactus Creeper", "Fandango Flower", 5);
                            Core.HuntMonster("Wanders", "Lotus Spider", "Lotus Flower", 4);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    break;

                case "Plague Strike Scythe":
                    if (Farm.FactionRank("Evil") < 8)
                    {
                        Core.Logger("This item requires Evil Faction Rank 7. starting EvilREP script");
                        Farm.EvilREP(8);
                    }
                    Adv.BuyItem("ShadowFall", 89, req.Name);
                    break;

                case "Baby Red Dragon":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem("AriaPet", 12, req.Name);
                    break;

                case "Zealith Reavers":
                    Core.FarmingLogger($"{req.Name}", quant);
                    if (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        LegionExercise3.Exercise(new[] { "Judgement Hammer", "Legion Token" });
                        LegionExercise4.Exercise(new[] { "Judgement Scythe", "Legion Token" });
                        CoreLegion.FarmLegionToken(50);
                        Adv.BuyItem("Underworld", 238, req.Name);
                    }
                    break;

                case "Great Astral Wings":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Elemental", "Mana Falcon", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Wave Cutter":
                    Core.FarmingLogger($"{req.Name}", quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Pirates", "Shark Bait", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Star Caster Staff":
                    Adv.BuyItem("Castle", 48, req.Name);
                    break;

                case "Scarlet's Costume":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem("Sleuthhound", 65, req.Name);
                    break;

                case "Infernal Dark Blade of Cruelty":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem("Battleon", 10, req.Name);
                    break;
                case "Chaos Dragonlord Helm":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Stalagbite", "Vath", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Rose Aura of the Ascended":
                    Core.Logger($"{req.Name} is seasonal AC item and only available Beleen's Birthday event.");
                    Core.FarmingLogger($"{req.Name}", quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Logger("This item is not setup yet"); //needs to be edited later as i couldn't find ShopID for it
                    }
                    break;

                case "Prismatic Dye":
                    Adv.BuyItem("Tower", 1966, req.Name);
                    break;

                case "Iron Dreadsaw":
                    Core.FarmingLogger($"{req.Name}", quant);
                    if (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (!Core.CheckInventory("Raw Dreadsaw"))
                        {
                            CoreNation.ApprovalAndFavor(10, 0);
                            CoreNation.FarmDiamondofNulgath(5);
                            CoreNation.SwindleBulk(10);
                            Adv.BuyItem("ArchPortal", 1211, "Raw Dreadsaw");
                        }
                        CoreNation.ApprovalAndFavor(40, 20);
                        CoreNation.FarmGemofNulgath(10);
                        Adv.BuyItem("ArchPortal", 1211, req.Name);
                    }
                    break;

                case "Taro's Manslayer":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    else
                    {
                        Core.FarmingLogger($"{req.Name}", quant);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Core.IsMember)
                        {
                            TarosManslayer.GuardianTaro(ManslayerOnly: true);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    break;

                case "Dual Manslayer of Taro":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    else
                    {
                        Core.FarmingLogger($"{req.Name}", quant);
                        Core.EquipClass(ClassType.Farm);
                        Core.RegisterQuests(625);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Core.IsMember)
                        {
                            TarosManslayer.GuardianTaro(ManslayerOnly: true);
                            CoreNation.FarmDiamondofNulgath(7);
                            CoreNation.FarmDarkCrystalShard(13);
                            CoreNation.SwindleBulk(13);
                            CoreNation.FarmUni13(1);
                            CoreNation.FarmVoucher(member: true);
                            Core.HuntMonster("Underworld", "Undead Bruiser", "Undead Bruiser Rune");
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    break;

                case "Demonhuntress Horns":
                    if (Core.CheckInventory("Blindfolded Pink Demonhuntress Horns") || Core.CheckInventory("Pink Demonhuntress Horns"))
                        Adv.BuyItem("Curio", 1070, req.Name);
                    else
                    {
                        Core.Logger($"{req.Name} is pseudo-Rare, you don't have the Rare item to merge this material");
                        return;
                    }
                    break;

                case "Demonhunter Horns":
                    if (Core.CheckInventory("Blindfolded Pink Demonhunter Horns") || Core.CheckInventory("Pink Demonhunter Horns"))
                    {
                        if (Core.CheckInventory("Blindfolded Pink Demonhunter Horns"))
                            Adv.BuyItem("Curio", 1214, req.Name);
                        else
                            Adv.BuyItem("Curio", 52, req.Name);
                    }
                    else
                    {
                        Core.Logger($"{req.Name} is pseudo-Rare, you don't have the Rare item to merge this material");
                        return;
                    }
                    break;

                case "DOOMFire Warrior":
                    if (Core.HasAchievement(19, "ip6"))
                        Adv.BuyItem("Battleon", 1306, req.Name);
                    else
                        return;
                    break;

                case "Fire Imp Tail":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    else
                    {
                        Core.FarmingLogger($"{req.Name}", quant);
                        Core.EquipClass(ClassType.Farm);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Core.IsMember)
                        {
                            Core.HuntMonster("Mobius", "Fire Imp", req.Name, isTemp: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        break;
                    }

                case "Unarmed":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem(Bot.Map.Name, 1536, req.Name);
                    break;

                case "Scarbucks Latte":
                    Core.Logger($"{req.Name} is Member & Seasonal item");
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem("FearFeast", 1190, req.Name);
                    break;

                case "Valor High Halo":
                    if (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Logger($"{req.Name} is acquired from 'Open Treasure Chests! quest from Twilly");
                        return;
                    }
                    break;

                case "Doge the Evil":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    else
                    {
                        Core.FarmingLogger($"{req.Name}", quant);
                        Core.EquipClass(ClassType.Farm);
                        Core.EnsureAccept(2951);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Core.IsMember)
                        {
                            //Legion Armored Daimyo [Member] 2951
                            Core.HuntMonster("Ruins", "Dark Elemental", "Souls of the Destroyed", 15);
                            Core.HuntMonster("DarkDungeon", "Shadow Serpent", "Shadow Essence", 4);
                            Core.HuntMonster("GreenguardWest", "Black Knight", "Black Metal Armor", 4);
                        }
                        Core.EnsureCompleteChoose(2951, new[] { req.Name });
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;


                case "Shimmering Flakes":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem("BlindingSnow", 236, req.Name);
                    break;

                case "Red Rose":
                    Core.FarmingLogger($"{req.Name}", quant);
                    ArtixWedding.ArtixWeddingComplete();
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("BattleWedding", "EbilCorp Ninja", "Love Token", 10, isTemp: false);
                        Bot.Wait.ForPickup("Love Token");
                        Adv.BuyItem("ArtixWedding", 788, req.Name);
                    }
                    break;

                case "Scarbucks Espresso Cup":
                    Core.Logger($"{req.Name} is Member & Seasonal item");
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem("FearFeast", 1190, req.Name);
                    break;

                case "Shadowslayer Armor":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem("DarkoviaForest ", 138, req.Name);
                    break;

                case "ShadowSlayer Hat":
                case "Shadow Z Hat":
                    Adv.BuyItem("DarkoviaForest ", 138, req.Name);
                    break;

                case "Reavers Of Good":
                    Core.FarmingLogger($"{req.Name}", quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("BrightFortress", "Dark Assassin", "Mirror Token", 35, isTemp: false);
                        Bot.Wait.ForPickup("Mirror Token");
                        Adv.BuyItem("BrightFortress", 795, req.Name);
                    }
                    break;
                case "Slayer's Neophyte Broadsword":
                case "Slithering ShadowSlayer":
                case "Slithering Hunter's Hat":
                case "Slithering Hunter's Hat + Locks":
                case "Slayer's Wooden Pistol":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("DarkoviaForest", "Lich of the Stone", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Slithering Hunter's Knife":
                case "Slayer's Wooden Rifle":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Core.IsMember)
                    {
                        Core.HuntMonster("DarkoviaForest", "Lich of the Stone", req.Name, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("10177", "Pinkest Platinum Wings", "Mode: [select] only\nShould the bot buy \"Pinkest Platinum Wings\" ?", false),
        new Option<bool>("10178", "Pink Strike Scythe", "Mode: [select] only\nShould the bot buy \"Pink Strike Scythe\" ?", false),
        new Option<bool>("10179", "Baby Pink Dragon", "Mode: [select] only\nShould the bot buy \"Baby Pink Dragon\" ?", false),
        new Option<bool>("10319", "Amaranth Reavers", "Mode: [select] only\nShould the bot buy \"Amaranth Reavers\" ?", false),
        new Option<bool>("10320", "Astralnomically Pink Wings", "Mode: [select] only\nShould the bot buy \"Astralnomically Pink Wings\" ?", false),
        new Option<bool>("10321", "Carmine Cutter", "Mode: [select] only\nShould the bot buy \"Carmine Cutter\" ?", false),
        new Option<bool>("10322", "Shocking Pink Caster Staff", "Mode: [select] only\nShould the bot buy \"Shocking Pink Caster Staff\" ?", false),
        new Option<bool>("10323", "Magenta's Costume", "Mode: [select] only\nShould the bot buy \"Magenta's Costume\" ?", false),
        new Option<bool>("10324", "Adorable Pink Blade of Cuteness", "Mode: [select] only\nShould the bot buy \"Adorable Pink Blade of Cuteness\" ?", false),
        new Option<bool>("10353", "Chaotically Cute DragonLord Helm", "Mode: [select] only\nShould the bot buy \"Chaotically Cute DragonLord Helm\" ?", false),
        new Option<bool>("46826", "Prismatic Aura of the Ascended", "Mode: [select] only\nShould the bot buy \"Prismatic Aura of the Ascended\" ?", false),
        new Option<bool>("59032", "Pretty Pink DreadSaw", "Mode: [select] only\nShould the bot buy \"Pretty Pink DreadSaw\" ?", false),
        new Option<bool>("59266", "Taro's Pretty Manslayer", "Mode: [select] only\nShould the bot buy \"Taro's Pretty Manslayer\" ?", false),
        new Option<bool>("59352", "Taro's Dual Pretty Manslayers", "Mode: [select] only\nShould the bot buy \"Taro's Dual Pretty Manslayers\" ?", false),
        new Option<bool>("59267", "Pink Demonhuntress Horns", "Mode: [select] only\nShould the bot buy \"Pink Demonhuntress Horns\" ?", false),
        new Option<bool>("59268", "Blindfolded Pink Demonhuntress Horns", "Mode: [select] only\nShould the bot buy \"Blindfolded Pink Demonhuntress Horns\" ?", false),
        new Option<bool>("59269", "Pink Demonhunter Horns", "Mode: [select] only\nShould the bot buy \"Pink Demonhunter Horns\" ?", false),
        new Option<bool>("59270", "Blindfolded Pink Demonhunter Horns", "Mode: [select] only\nShould the bot buy \"Blindfolded Pink Demonhunter Horns\" ?", false),
        new Option<bool>("59271", "Pretty DOOMFire Warrior", "Mode: [select] only\nShould the bot buy \"Pretty DOOMFire Warrior\" ?", false),
        new Option<bool>("59272", "Pink Imp Tail", "Mode: [select] only\nShould the bot buy \"Pink Imp Tail\" ?", false),
        new Option<bool>("59347", "Viole(n)t Unarmed", "Mode: [select] only\nShould the bot buy \"Viole(n)t Unarmed\" ?", false),
        new Option<bool>("59359", "Pretty Pink Scarbucks Drink", "Mode: [select] only\nShould the bot buy \"Pretty Pink Scarbucks Drink\" ?", false),
        new Option<bool>("59360", "Vibrant Valor High Halo", "Mode: [select] only\nShould the bot buy \"Vibrant Valor High Halo\" ?", false),
        new Option<bool>("38658", "Doge the Pink", "Mode: [select] only\nShould the bot buy \"Doge the Pink\" ?", false),
        new Option<bool>("59354", "Pretty Shimmering Flakes", "Mode: [select] only\nShould the bot buy \"Pretty Shimmering Flakes\" ?", false),
        new Option<bool>("59355", "Pink Rose", "Mode: [select] only\nShould the bot buy \"Pink Rose\" ?", false),
        new Option<bool>("59359", "Pretty Pink Scarbucks Drink", "Mode: [select] only\nShould the bot buy \"Pretty Pink Scarbucks Drink\" ?", false),
        new Option<bool>("59531", "Bright Pink ShadowSlayer", "Mode: [select] only\nShould the bot buy \"Bright Pink ShadowSlayer\" ?", false),
        new Option<bool>("59534", "Pink ShadowSlayer's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Pink ShadowSlayer's Hat + Locks\" ?", false),
        new Option<bool>("59535", "Pink ShadowSlayer's Hat", "Mode: [select] only\nShould the bot buy \"Pink ShadowSlayer's Hat\" ?", false),
        new Option<bool>("59541", "Pink ShadowSlayer's Broadsword", "Mode: [select] only\nShould the bot buy \"Pink ShadowSlayer's Broadsword\" ?", false),
        new Option<bool>("59532", "Reavers of Sakura", "Mode: [select] only\nShould the bot buy \"Reavers of Sakura\" ?", false),
        new Option<bool>("59595", "Cool PinkSlayer", "Mode: [select] only\nShould the bot buy \"Cool PinkSlayer\" ?", false),
        new Option<bool>("59596", "PinkSlayer", "Mode: [select] only\nShould the bot buy \"PinkSlayer\" ?", false),
        new Option<bool>("59597", "PinkSlayer's Hat", "Mode: [select] only\nShould the bot buy \"PinkSlayer's Hat\" ?", false),
        new Option<bool>("59598", "PinkSlayer's Hat + Locks", "Mode: [select] only\nShould the bot buy \"PinkSlayer's Hat + Locks\" ?", false),
        new Option<bool>("59601", "PinkSlayer's Knives", "Mode: [select] only\nShould the bot buy \"PinkSlayer's Knives\" ?", false),
        new Option<bool>("59602", "PinkSlayer's Knife", "Mode: [select] only\nShould the bot buy \"PinkSlayer's Knife\" ?", false),
        new Option<bool>("59600", "PinkSlayer's Pistol", "Mode: [select] only\nShould the bot buy \"PinkSlayer's Pistol\" ?", false),
        new Option<bool>("59599", "PinkSlayer's Rifle", "Mode: [select] only\nShould the bot buy \"PinkSlayer's Rifle\" ?", false),
    };
}
