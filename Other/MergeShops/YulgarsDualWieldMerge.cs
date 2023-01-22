/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Other/WeaponReflection.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YulgarsDualWieldMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreDailies Daily = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();
    public static CoreAdvanced sAdv = new();
    public DualWield DW = new();
    public DragonBladeofNulgath DBoN = new();
    public SRoD SRoD = new();
    public JuggernautItemsofNulgath juggernaut = new();
    public CoreLegion Legion = new();

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

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.CheckInventory("Golden 8th Birthday Candle"))
            Core.BuyItem(Bot.Map.Name, 1317, "Golden 8th Birthday Candle");
        Bot.Sleep(1500);
        if (!Core.CheckInventory("Golden 8th Birthday Candle"))
        {
            Core.Logger("Golden Candle not found - stopping bot.", messageBox: true);
            return;
        }
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("nostalgiaquest", 1311, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Weapon Reflection":
                    Core.AddDrop(req.Name);
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.EnsureAccept(5518);
                        Core.HuntMonster("nostalgiaquest", "Skeletal Viking", "Reflected Glory", 5);
                        Core.HuntMonster("nostalgiaquest", "Skeletal Warrior", "Divided Light", 5);
                        Core.EnsureComplete(5518);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Boom Went The Dynamite":
                    if (!Core.IsMember)
                        return;
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("banished", "Desterrat Moya", req.Name, quant, false);
                    break;

                case "TheWicked":
                    Core.Logger($"You don't own {req.Name} (Rare)");
                    break;

                case "Oblivion of Nulgath":
                    juggernaut.JuggItems(reward: JuggernautItemsofNulgath.RewardsSelection.Oblivion_of_Nulgath);
                    break;

                case "Overlord's DoomBlade":
                    if (Core.HasAchievement(27, "ip0"))
                        Core.BuyItem(Bot.Map.Name, 340, req.Name);
                    else
                        Core.Logger($"You don't have access to this shop for {req.Name}");
                    break;

                case "Blessed Coffee Cup":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5405);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("sandsea", "Oasis Monkey", "Pally Luwak Beans");
                    Core.CancelRegisteredQuests();
                    break;

                case "Party Slasher Birthday Sword":
                case "Rapier of Skulls":
                    Core.Logger($"You don't own {req.Name} (Rare)");
                    break;

                case "Unarmed":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to Obtain");
                        return;
                    }
                    Adv.BuyItem(Bot.Map.Name, 1536, req.Name);
                    break;

                case "Frostbite":
                    if (!Core.IsMember || (!Core.isCompletedBefore(793)))
                    {
                        Core.Logger($"You require Membership for {req.Name}, or you're not part of the Legion");
                        return;
                    }
                    if (!Core.CheckInventory("Frosted Falchion"))
                        Adv.BuyItem("blindingsnow", 236, "Frosted Falchion");
                    Legion.FarmLegionToken(70);
                    Adv.BuyItem("underworld", 238, req.Name);
                    break;

                case "A Rock":
                    if (!Bot.Inventory.Contains(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Phoenix Blade of Nulgath":
                    //  5373 = Oblivion Blade of Nulgath (Pet) ---- 4809 = Oblivion Blade of Nulgath Pet (Rare)
                    if (!Core.IsMember || (!Core.CheckInventory(5373)) && (!Core.CheckInventory(4809)))
                    {
                        Core.Logger($"You don't own any of the pets/Membership to get {req.Name}");
                        return;
                    }

                    if (Core.CheckInventory(5373))
                        Core.EnsureAccept(2558);
                    else if (Core.CheckInventory(4809))
                        Core.EnsureAccept(558);
                    Core.AddDrop(Nation.bagDrops);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("lair", "Red Dragon", "Phoenix Blade", isTemp: false);
                    Nation.FarmDarkCrystalShard(5);
                    Nation.FarmDiamondofNulgath(10);
                    Nation.SwindleBulk(5);
                    Nation.FarmUni13(1);
                    Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Sigil");
                    Core.AddDrop(req.Name);
                    if (Core.CheckInventory(5373))
                        Core.EnsureComplete(2558);
                    else if (Core.CheckInventory(4809))
                        Core.EnsureComplete(558);
                    Core.RemoveDrop(Nation.bagDrops);
                    Core.ToBank(Nation.bagDrops);
                    break;

                case "Shadow Spear of Nulgath":
                case "Guardian of Virtue":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Leviasea Sword":
                    if (!Core.CheckInventory(req.Name) && !Core.IsMember)
                    {
                        Core.Logger($"You require Membership for {req.Name}");
                        return;
                    }
                    if (!Core.CheckInventory(req.Name) && Core.IsMember)
                        Adv.BuyItem("yulgar", 69, req.Name);
                    break;

                case "Iron Dreadsaw":
                    if (!Core.CheckInventory(req.Name))
                    {
                        if (!Core.CheckInventory("Raw Dreadsaw"))
                        {
                            Core.Logger($"You dont own Raw Dreadsaw, getting it first");
                            Nation.ApprovalAndFavor(10, 0);
                            Nation.FarmDiamondofNulgath(5);
                            Nation.SwindleBulk(10);
                            Adv.BuyItem("archportal", 1211, "Raw Dreadsaw");
                        }
                        Nation.ApprovalAndFavor(40, 20);
                        Nation.FarmGemofNulgath(10);
                        Adv.BuyItem("archportal", 1211, req.Name);
                    }
                    break;

                case "Blood Axe Of Destruction":
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.CheckInventory(req.Name))
                        Core.KillMonster("infernalspire", "r2", "Left", "*", req.Name, isTemp: false);
                    break;

                case "PainSaw of Eidolon":
                    if (!Core.CheckInventory("Undead Champion"))
                    {
                        Core.Logger($"You don't own Undead Champion - go and complete the Legion intro (requires 1200 AC)");
                        return;
                    }
                    Core.AddDrop("PainSaw of Eidolon", "Judgement Scythe", "Soul Eater Advanced", "Legion Token");
                    Core.RegisterQuests(824);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && (!Core.CheckInventory(req.Name)))
                    {
                        Core.KillMonster("marsh2", "End", "Left", 72, "Soul Scythe", 1, false);
                        Core.KillMonster("marsh2", "End", "Left", "Lesser Shadow Serpent", "Potent Viper's Blood");
                        Core.HuntMonster("battleundera", "Skeletal Ice Mage", "Frostbit Skull", 15);
                    }
                    if (Core.CheckInventory("Judgement Scythe"))
                        Core.ToBank("Judgement Scythe");
                    if (Core.CheckInventory("Soul Eater Advanced"))
                        Core.ToBank("Soul Eater Advanced");
                    Core.ToBank("Legion Token");
                    break;

                case "Hanzamune Dragon Koi Blade":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("kitsune", "Kitsune", req.Name, isTemp: false);
                    break;

                case "Ugly Stick":
                    if (!Core.CheckInventory(req.Name))
                        Adv.BuyItem("newbie", 39, req.Name);
                    break;

                case "Balrog Blade":
                    if (Core.HasAchievement(5))
                        Adv.BuyItem(Bot.Map.Name, 5, req.Name);
                    else
                        Core.Logger($"You don't have access to this shop for {req.Name}");

                    break;

                case "Legendary Magma Sword":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Dragon Saw":
                    if (!Core.CheckInventory(req.Name))
                        Adv.BuyItem("yulgar", 16, req.Name);
                    break;

                case "Overfiend Blade of Nulgath":
                    if (!Core.CheckInventory(req.Name))
                        juggernaut.JuggItems(reward: JuggernautItemsofNulgath.RewardsSelection.Overfiend_Blade_of_Nulgath);
                    break;

                case "Bone Sword":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.EnsureAccept(7);
                        Core.RegisterQuests(5);
                        Core.AddDrop(req.Name);
                        while (!Bot.ShouldExit && !Bot.TempInv.Contains("Small Skull", 8))
                            Core.HuntMonster("graveyard", "Big Jack Sprat");
                        Core.EnsureComplete(7);
                    }
                    break;

                case "Honor Guard's Blade":
                case "Ceremonial Legion Blade":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Alteon's Pride":
                    if (!Core.CheckInventory(req.Name))
                    {
                        if (Farm.FactionRank("Good") < 7)
                            Farm.GoodREP(7);
                        Adv.BuyItem("castle", 88, req.Name);
                    }
                    break;

                case "Ddog Sea Serpent Sword":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to obtain");
                        return;
                    }
                    Core.EnsureAccept(554);
                    Nation.FarmUni13(1);
                    Core.HuntMonster("underworld", "Undead Legend", "Undead Legend Rune", log: false);
                    Core.EnsureCompleteChoose(554, new[] { "Ddog Sea Serpent Sword" });
                    break;

                case "Eternity Blade":
                    Core.EquipClass(ClassType.Solo);
                    Core.EnsureAccept(3485);
                    Bot.Quests.UpdateQuest(3484);
                    Core.HuntMonster("towerofdoom10", "Slugbutter", "Eternity Blade");
                    Core.EnsureComplete(3485);
                    break;

                case "Blinding Light of Destiny":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"Go and get {req.Name} yourself.");
                        return;
                    }
                    break;

                case "Crystal Claymore":
                    Adv.BuyItem("castle", 48, req.Name);
                    break;

                case "Dark Crystal Claymore":
                    Adv.BuyItem("shadowfall", 47, req.Name);
                    break;

                case "Soulreaper of Nulgath":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to obtain");
                        return;
                    }
                    Core.AddDrop(req.Name);
                    Core.EnsureAccept(571);
                    if (!Core.CheckInventory("Godly Golden Dragon Axe"))
                    {
                        Core.EnsureAccept(554);
                        Nation.FarmUni13(1);
                        Core.HuntMonster("underworld", "Undead Legend", "Undead Legend Rune", log: false);
                        Core.EnsureCompleteChoose(554, new[] { "Godly Golden Dragon Axe" });
                    }
                    Nation.FarmDiamondofNulgath(10);
                    Nation.FarmDarkCrystalShard(5);
                    Nation.SwindleBulk(5);
                    Nation.FarmUni13(1);
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.CheckInventory("Abaddon's Terror"))
                        Core.HuntMonster("twilight", "Abaddon", "Abaddon's Terror", isTemp: false);
                    Core.EnsureComplete(571);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Grumpy Warhammer":
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.CheckInventory(req.Name))
                        Core.HuntMonster("boxes", "Sneeviltron", req.Name, isTemp: false);
                    break;

                case "Crystal Phoenix Blade of Nulgath":
                    if (!Core.CheckInventory(req.Name))
                        juggernaut.JuggItems(reward: JuggernautItemsofNulgath.RewardsSelection.Crystal_Phoenix_Blade_of_Nulgath);
                    break;

                case "Maximillian's Whip":
                case "Mystic Pencil of Endless Scribbles":
                case "WarpForce War Shovel 20K":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Godly Mace of the Ancients":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to obtain");
                        return;
                    }
                    if (!Core.CheckInventory(req.Name))
                        Adv.BuyItem("citadel", 44, req.Name);
                    break;

                case "Mace of the Grand Inquisitor":
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.CheckInventory(req.Name))
                        Core.HuntMonster("citadel", "Grand Inquisitor", req.Name, isTemp: false);
                    break;

                case "KneeCapper":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Morning Star":
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.CheckInventory(req.Name))
                        Core.HuntMonster("forest", "Boss Zardman", req.Name, isTemp: false);
                    break;

                case "Axe of the Black Knight":
                case "Cruel Axe of Midnight":
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.CheckInventory(req.Name))
                        Core.KillMonster("greenguardwest", "BKWest15", "Down", "Black Knight", req.Name, isTemp: false);
                    break;

                case "Platinum Axe of Destiny":
                case "Star Sword":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Big 100K":
                    if (!Core.CheckInventory(req.Name))
                        Adv.BuyItem("swordhaven", 3, req.Name);
                    break;

                case "Blister's Chainsaw 08":
                case "Golden Phoenix Sword":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "Hydra Blade":
                    if (!Core.CheckInventory(req.Name))
                        Adv.BuyItem("swordhaven", 4, req.Name);
                    break;

                case "Crusader Sword":
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.CheckInventory(req.Name))
                        Core.HuntMonster("citadel", "Crusader", req.Name, isTemp: false);
                    break;

                case "Bloodriver":
                case "Star Sword Breaker":
                    if (!Core.CheckInventory(req.Name))
                    {
                        Core.Logger($"You don't own {req.Name} (Rare)");
                        return;
                    }
                    break;

                case "ReignBringer":
                    if (!Core.CheckInventory(req.Name))
                        Adv.BuyItem("swordhaven", 4, req.Name);
                    break;

                case "Balor's Cruelty":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to obtain");
                        return;
                    }
                    Core.EquipClass(ClassType.Solo);
                    if (!Core.CheckInventory(req.Name))
                        Core.HuntMonster("twilight", "Abaddon", req.Name, isTemp: false);
                    break;

                case "Default Sword":
                case "Iron Spear":
                    Adv.BuyItem("yulgar", 16, req.Name);
                    break;

                case "Undead Plague Spear":
                    if (Core.HasAchievement(5))
                        Adv.BuyItem(Bot.Map.Name, 5, req.Name);
                    else
                        Core.HuntMonster("graveyard", "Big Jack Sprat", req.Name, isTemp: false);
                    break;

                case "Mighty Sword Of The Dragons":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to obtain");
                        return;
                    }
                    Core.EquipClass(ClassType.Solo);
                    Core.AddDrop("Zellare's Death Scale", "Moganth's Death Scale", "Udaroth's Death Scale", "Cellot's Death Scale", "Mighty Sword Of The Dragons");
                    Core.RegisterQuests(3343);
                    Bot.Quests.UpdateQuest(1416);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name))
                    {
                        Core.HuntMonster("wind", "Cellot", "Cellot's Death Scale", isTemp: false);
                        Core.HuntMonster("fire", "Zellare", "Zellare's Death Scale", isTemp: false);
                        Core.HuntMonster("water", "Udaroth", "Udaroth's Death Scale", isTemp: false);
                        Core.HuntMonster("dragonplane", "Moganth", "Moganth's Death Scale", isTemp: false);
                    }
                    break;

                case "Necrotic Sword of Doom":
                    Core.Logger($"Go and get {req.Name} yourself.");
                    break;

                case "Burning Blade Of Abezeth":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("celestialarenad", "Aranx", req.Name, isTemp: false);
                    break;

                case "Blade of Awe":
                    Farm.BladeofAweREP(6, true);
                    break;

                case "Abaddon's Terror":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to obtain");
                        return;
                    }
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("twilight", "Abaddon", req.Name, isTemp: false);
                    break;

                case "Krom's Brutality":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("forest", "Boss Zardman", req.Name, isTemp: false);
                    break;

                case "Phoenix Blade":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("lair", "Red Dragon", "Phoenix Blade", isTemp: false);
                    break;

                case "Burn it Down":
                    if (!Daily.CheckDaily(187, true, req.Name))
                    {
                        Core.Logger($"{req.Name} owned, or daily unavailable");
                        return;
                    }
                    Core.AddDrop(req.Name);
                    Core.EnsureAccept(187);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("portalundead", "Enter", "Spawn", "*", "Fire Gem");
                    Core.EnsureComplete(187);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Shadow Terror Axe":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("battleundera", "Bone Terror", req.Name, isTemp: false);
                    break;

                case "DragonBlade of Nulgath":
                    if (!Core.IsMember)
                    {
                        Core.Logger($"{req.Name} requires Membership to obtain");
                        return;
                    }
                    DBoN.GetDragonBlade();
                    break;

                case "ShadowReaper Of Doom":
                    SRoD.ShadowReaperOfDoom();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("37250", "Dual Boom Went The Dynamite", "Mode: [select] only\nShould the bot buy \"Dual Boom Went The Dynamite\" ?", false),
        new Option<bool>("37249", "Dual TheWicked", "Mode: [select] only\nShould the bot buy \"Dual TheWicked\" ?", false),
        new Option<bool>("37248", "Dual Oblivion of Nulgath Maces", "Mode: [select] only\nShould the bot buy \"Dual Oblivion of Nulgath Maces\" ?", false),
        new Option<bool>("37244", "Dual Overlord's DoomBlade", "Mode: [select] only\nShould the bot buy \"Dual Overlord's DoomBlade\" ?", false),
        new Option<bool>("37241", "Dual Blessed Coffee Cup", "Mode: [select] only\nShould the bot buy \"Dual Blessed Coffee Cup\" ?", false),
        new Option<bool>("37240", "Dual Party Slasher Birthday Sword", "Mode: [select] only\nShould the bot buy \"Dual Party Slasher Birthday Sword\" ?", false),
        new Option<bool>("37239", "Dual Rapier of Skulls", "Mode: [select] only\nShould the bot buy \"Dual Rapier of Skulls\" ?", false),
        new Option<bool>("37238", "Dual Unarmed", "Mode: [select] only\nShould the bot buy \"Dual Unarmed\" ?", false),
        new Option<bool>("37236", "Dual Frostbite", "Mode: [select] only\nShould the bot buy \"Dual Frostbite\" ?", false),
        new Option<bool>("37235", "Dual Rocks", "Mode: [select] only\nShould the bot buy \"Dual Rocks\" ?", false),
        new Option<bool>("37234", "Dual Phoenix Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Dual Phoenix Blade of Nulgath\" ?", false),
        new Option<bool>("37233", "Dual Shadow Spear of Nulgath", "Mode: [select] only\nShould the bot buy \"Dual Shadow Spear of Nulgath\" ?", false),
        new Option<bool>("37230", "Dual Guardian of Virtue", "Mode: [select] only\nShould the bot buy \"Dual Guardian of Virtue\" ?", false),
        new Option<bool>("37228", "Dual Leviasea Sword", "Mode: [select] only\nShould the bot buy \"Dual Leviasea Sword\" ?", false),
        new Option<bool>("37227", "Dual Iron Dreadsaw", "Mode: [select] only\nShould the bot buy \"Dual Iron Dreadsaw\" ?", false),
        new Option<bool>("37226", "Dual Blood Axe Of Destruction", "Mode: [select] only\nShould the bot buy \"Dual Blood Axe Of Destruction\" ?", false),
        new Option<bool>("37224", "Dual PainSaw of Eidolon", "Mode: [select] only\nShould the bot buy \"Dual PainSaw of Eidolon\" ?", false),
        new Option<bool>("37223", "Dual Hanzamune Dragon Koi Blade", "Mode: [select] only\nShould the bot buy \"Dual Hanzamune Dragon Koi Blade\" ?", false),
        new Option<bool>("37222", "Dual Ugly Stick", "Mode: [select] only\nShould the bot buy \"Dual Ugly Stick\" ?", false),
        new Option<bool>("37221", "Dual Balrog Blade", "Mode: [select] only\nShould the bot buy \"Dual Balrog Blade\" ?", false),
        new Option<bool>("37219", "Dual Legendary Magma Sword", "Mode: [select] only\nShould the bot buy \"Dual Legendary Magma Sword\" ?", false),
        new Option<bool>("37218", "Dual Dragon Saw", "Mode: [select] only\nShould the bot buy \"Dual Dragon Saw\" ?", false),
        new Option<bool>("37217", "Dual Overfiend Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Dual Overfiend Blade of Nulgath\" ?", false),
        new Option<bool>("37216", "Dual Bone Sword", "Mode: [select] only\nShould the bot buy \"Dual Bone Sword\" ?", false),
        new Option<bool>("37215", "Dual Honor Guard's Blade", "Mode: [select] only\nShould the bot buy \"Dual Honor Guard's Blade\" ?", false),
        new Option<bool>("37214", "Dual Ceremonial Legion Blade", "Mode: [select] only\nShould the bot buy \"Dual Ceremonial Legion Blade\" ?", false),
        new Option<bool>("37213", "Dual Alteon's Pride", "Mode: [select] only\nShould the bot buy \"Dual Alteon's Pride\" ?", false),
        new Option<bool>("37211", "Dual Ddog Sea Serpent Sword", "Mode: [select] only\nShould the bot buy \"Dual Ddog Sea Serpent Sword\" ?", false),
        new Option<bool>("37208", "Dual Eternity Blade", "Mode: [select] only\nShould the bot buy \"Dual Eternity Blade\" ?", false),
        new Option<bool>("37207", "Dual Blinding Light of Destiny", "Mode: [select] only\nShould the bot buy \"Dual Blinding Light of Destiny\" ?", false),
        new Option<bool>("37205", "Dual Crystal Claymore", "Mode: [select] only\nShould the bot buy \"Dual Crystal Claymore\" ?", false),
        new Option<bool>("37204", "Dual Dark Crystal Claymore", "Mode: [select] only\nShould the bot buy \"Dual Dark Crystal Claymore\" ?", false),
        new Option<bool>("37203", "Dual Soulreaper of Nulgath", "Mode: [select] only\nShould the bot buy \"Dual Soulreaper of Nulgath\" ?", false),
        new Option<bool>("37201", "Dual Grumpy Warhammer", "Mode: [select] only\nShould the bot buy \"Dual Grumpy Warhammer\" ?", false),
        new Option<bool>("37200", "Dual Crystal Phoenix Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Dual Crystal Phoenix Blade of Nulgath\" ?", false),
        new Option<bool>("37197", "Dual Maximillian's Whip", "Mode: [select] only\nShould the bot buy \"Dual Maximillian's Whip\" ?", false),
        new Option<bool>("37196", "Dual Pencil of Endless Scribbles", "Mode: [select] only\nShould the bot buy \"Dual Pencil of Endless Scribbles\" ?", false),
        new Option<bool>("37195", "Dual WarpForce War Shovel 20K", "Mode: [select] only\nShould the bot buy \"Dual WarpForce War Shovel 20K\" ?", false),
        new Option<bool>("37194", "Dual Godly Mace of the Ancients", "Mode: [select] only\nShould the bot buy \"Dual Godly Mace of the Ancients\" ?", false),
        new Option<bool>("37193", "Dual Mace of the Grand Inquisitor", "Mode: [select] only\nShould the bot buy \"Dual Mace of the Grand Inquisitor\" ?", false),
        new Option<bool>("37192", "Dual KneeCappers", "Mode: [select] only\nShould the bot buy \"Dual KneeCappers\" ?", false),
        new Option<bool>("37191", "Dual Morning Stars", "Mode: [select] only\nShould the bot buy \"Dual Morning Stars\" ?", false),
        new Option<bool>("37190", "Dual Axe of the Black Knight", "Mode: [select] only\nShould the bot buy \"Dual Axe of the Black Knight\" ?", false),
        new Option<bool>("37189", "Dual Cruel Axe of Midnight", "Mode: [select] only\nShould the bot buy \"Dual Cruel Axe of Midnight\" ?", false),
        new Option<bool>("37188", "Dual Platinum Axe of Destiny", "Mode: [select] only\nShould the bot buy \"Dual Platinum Axe of Destiny\" ?", false),
        new Option<bool>("37186", "Dual Star Sword", "Mode: [select] only\nShould the bot buy \"Dual Star Sword\" ?", false),
        new Option<bool>("37185", "Dual Big 100K", "Mode: [select] only\nShould the bot buy \"Dual Big 100K\" ?", false),
        new Option<bool>("37184", "Dual Blister's Chainsaw 08", "Mode: [select] only\nShould the bot buy \"Dual Blister's Chainsaw 08\" ?", false),
        new Option<bool>("37183", "Dual Golden Phoenix Sword", "Mode: [select] only\nShould the bot buy \"Dual Golden Phoenix Sword\" ?", false),
        new Option<bool>("37182", "Dual Hydra Blades", "Mode: [select] only\nShould the bot buy \"Dual Hydra Blades\" ?", false),
        new Option<bool>("37181", "Dual Crusader Sword", "Mode: [select] only\nShould the bot buy \"Dual Crusader Sword\" ?", false),
        new Option<bool>("37180", "Dual Bloodrivers", "Mode: [select] only\nShould the bot buy \"Dual Bloodrivers\" ?", false),
        new Option<bool>("37178", "Dual Star Sword Breaker", "Mode: [select] only\nShould the bot buy \"Dual Star Sword Breaker\" ?", false),
        new Option<bool>("37177", "Dual ReignBringers", "Mode: [select] only\nShould the bot buy \"Dual ReignBringers\" ?", false),
        new Option<bool>("37176", "Dual Balor's Cruelty", "Mode: [select] only\nShould the bot buy \"Dual Balor's Cruelty\" ?", false),
        new Option<bool>("37175", "Dual Default Sword", "Mode: [select] only\nShould the bot buy \"Dual Default Sword\" ?", false),
        new Option<bool>("41805", "Dual Iron Spears", "Mode: [select] only\nShould the bot buy \"Dual Iron Spears\" ?", false),
        new Option<bool>("41806", "Dual Undead Plague Spear", "Mode: [select] only\nShould the bot buy \"Dual Undead Plague Spear\" ?", false),
        new Option<bool>("41803", "Dual Mighty Sword Of The Dragons", "Mode: [select] only\nShould the bot buy \"Dual Mighty Sword Of The Dragons\" ?", false),
        new Option<bool>("45359", "Dual Necrotic Swords of Doom", "Mode: [select] only\nShould the bot buy \"Dual Necrotic Swords of Doom\" ?", false),
        new Option<bool>("45360", "Dual Burning Blades Of Abezeth", "Mode: [select] only\nShould the bot buy \"Dual Burning Blades Of Abezeth\" ?", false),
        new Option<bool>("45361", "Dual Blades of Awe", "Mode: [select] only\nShould the bot buy \"Dual Blades of Awe\" ?", false),
        new Option<bool>("45552", "Dual Abaddon's Terrors", "Mode: [select] only\nShould the bot buy \"Dual Abaddon's Terrors\" ?", false),
        new Option<bool>("45553", "Dual Krom's Brutalities", "Mode: [select] only\nShould the bot buy \"Dual Krom's Brutalities\" ?", false),
        new Option<bool>("37000", "Phoenix Blades", "Mode: [select] only\nShould the bot buy \"Phoenix Blades\" ?", false),
        new Option<bool>("50926", "Dual Burn it Down Staves", "Mode: [select] only\nShould the bot buy \"Dual Burn it Down Staves\" ?", false),
        new Option<bool>("50927", "Dual Shadow Terror Axes", "Mode: [select] only\nShould the bot buy \"Dual Shadow Terror Axes\" ?", false),
        new Option<bool>("50928", "Dual DragonBlades of Nulgath", "Mode: [select] only\nShould the bot buy \"Dual DragonBlades of Nulgath\" ?", false),
        new Option<bool>("50929", "Dual ShadowReapers Of Doom", "Mode: [select] only\nShould the bot buy \"Dual ShadowReapers Of Doom\" ?", false),
    };
}
