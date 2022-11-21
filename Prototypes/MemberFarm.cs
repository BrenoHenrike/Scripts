//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs
//cs_include Scripts/Nation/Various/ArchfiendDragonEgg[Mem].cs
//cs_include Scripts/Nation/Various/TendurrrTheAssistantQuests.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/TarosPrismaticManslayers.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/Friday13th/CoreFriday13th.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/CruxShip.cs
//cs_include Scripts/Prototypes/OdditiesMerge.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/TrobbolierPet[Member].cs
//cs_include Scripts/Other/MergeShops/DeadflyMerge.cs
//cs_include Scripts/Other/MergeShops/BoneTowersMerge.cs
//cs_include Scripts/Other/MergeShops/TachyonMerge.cs
//cs_include Scripts/Other/MergeShops/GonnaGetchaMerge.cs
//cs_include Scripts/Other/SpellRaiser[Member].cs
//cs_include Scripts/Other/TheLostKnightAndBackupBlade[Member].cs
//cs_include Scripts/Other/CoinCollectorSet[Member].cs
//cs_include Scripts/Other/Classes/Curio-Classes/LegendaryElementalWarrior[mem].cs
//cs_include Scripts/Other/Classes/Members-CLasses/ChronoAssassin[Mem].cs

using Skua.Core.Interfaces;

public class MemberFarm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreNation Nation = new();
    public CoreFriday13th C13F = new();
    public TrobbolierPet Trobbolier = new();
    public CoinCollectorSet CoinCollector = new();
    public TheLostKnightAndBackupBlade LostKnight = new();
    public DeadflyMerge Deadfly = new();
    public OdditiesMerge Oddities = new();
    public CoreSDKA SDKA = new();
    public SpellRaiser SpellRaiser = new();
    public ArchfiendDragonEgg ArchfiendDragonPet = new();
    public DragonBladeofNulgath DBoN = new();
    public TowersMerge DeathKnight = new();
    public LegendaryElementalWarrior LegendaryElementalWarrior = new();
    public ChronoAssassin ChronoAssassin = new();
    public TachyonMerge Tachyon = new();
    public CoreQOM QOM = new();
    public DragonFableOrigins DFO = new();
    public CruxShip Crux = new();
    public TendurrrTheAssistantQuests Tendurr = new();
    public TarosPrismaticManslayers TarosItems = new();
    public GonnaGetchaMerge GonnaGetcha = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (!Core.IsMember)
        {
            Core.Logger("This script is Member-Only", messageBox: true);
            return;
        }

        SDKA.DoAll();
        Core.ToBank("Sepulchure's DoomKnight Armor");

        DBoN.GetDragonBlade();
        Core.ToBank("DragonBlade of Nulgath");

        ChronoAssassin.GetChronoAss();
        Core.ToBank("Chrono Assassin");

        LegendaryElementalWarrior.GetLEW();
        Core.ToBank("Legendary Elemental Warrior");

        TarosItems.TemptationTest();
        Core.ToBank("Taro's Prismatic Manslayer", "Taro's Dual Prismatic Manslayers", "Taro's BattleBlade");

        ArchfiendDragonPet.GetAFDE();
        Core.ToBank("ArchFiend Baby Dragon Pet");

        CoinCollector.GetItems();
        DualWield();
        FireWar();
        CruxVIPWeapon();
        DeepForestItems();
        Tendurr.TendurrItems();
        Core.ToBank(Nation.bagDrops);
        Core.ToBank(Tendurr.Rewards);
        HuntingMonster();
        SpellRaiser.GetAll();
        LostKnight.GetAll();
        Trobbolier.GetAll();
        DeathKnight.BuyAllMerge();
        Tachyon.BuyAllMerge();
        Deadfly.BuyAllMerge();
        GonnaGetcha.BuyAllMerge();
        OdditiesMergeShop();
        //Dark Orb Quest (Need ACs for this quest)
    }


    public void CruxVIPWeapon()
    {
        if (Core.CheckInventory("Darkwave Khopesh"))
        {
            Core.ToBank("Darkwave Khopesh");
            return;
        }

        Crux.StoryLine();

        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Darkwave Khopesh")))
        {
            Core.EnsureAccept(4618);
            Core.HuntMonster("cruxship", "Apephryx", "Khopesh Shard", 1, isTemp: false);
            Core.EnsureComplete(4618);
            Bot.Drops.Pickup("Darkwave Khopesh");
        }
        Core.ToBank("Darkwave Khopesh");
    }


    public void FireWar()
    {
        if (Core.CheckInventory("Ignited Guardian's Accoutrements"))
        {
            Core.ToBank("Ignited Guardian's Accoutrements");
            return;
        }

        DFO.GreatFireWar();
        Core.BuyItem("firewar", 1586, "Flame Guardian's Accoutrements");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("firewar", "Uriax", "Dragon Eye", 2, isTemp: false);

        while (!Bot.ShouldExit && (!Core.CheckInventory("Dragon Flame", 25)))
        {
            Core.EquipClass(ClassType.Farm);
            Core.AddDrop("Dragon Flame");
            Core.EnsureAccept(6300);
            Core.HuntMonster("firewar", "Fire Dragon", "Fire Dragon Slain", 3);
            Core.KillMonster("firewar", "r8", "Left", "Inferno Dragon", "Inferno Dragon Slain", 2);
            Core.EnsureComplete(6300);
        }
        Core.BuyItem("firewar", 1587, "Ignited Guardian's Accoutrements");
        Core.ToBank("Ignited Guardian's Accoutrements");
    }


    public void DeepForestItems()
    {
        QOM.TheBook();

        if (!Core.CheckInventory("Polished Necrotic Blade of Chaos"))
        {
            Core.BuyItem("castleundead", 45, "Necrotic Blade of Chaos");
            Adv.BuyItem("deepforest", 1999, "Gold Voucher 500k", 4);
            Core.BuyItem("deepforest", 1999, "Polished Necrotic Blade of Chaos");
        }
        Core.ToBank("Polished Necrotic Blade of Chaos");

        if (!Core.CheckInventory("Polished Dragon Sword of Chaos"))
        {
            Core.BuyItem("castleundead", 45, "Dragon Sword of Chaos");
            Adv.BuyItem("deepforest", 1999, "Gold Voucher 500k", 4);
            Core.BuyItem("deepforest", 1999, "Polished Dragon Sword of Chaos");
        }
        Core.ToBank("Polished Dragon Sword of Chaos");
    }


    public void OdditiesMergeShop()
    {
        C13F.Oddities();

        Oddities.MergeShopFabyo("all");
        Oddities.MergeShopOddities("all");

        Core.ToBank(Oddities.MergeShop1Items);
        Core.ToBank(Oddities.MergeShop2Items);
    }


    public void DualWield()
    {
        if (!Core.CheckInventory("Golden 8th Birthday Candle"))
            Core.BuyItem(Bot.Map.Name, 1317, "Golden 8th Birthday Candle");
        Bot.Sleep(1500);
        if (!Core.CheckInventory("Golden 8th Birthday Candle"))
        {
            Core.Logger("Golden Candle not found - stopping bot.", messageBox: true);
            return;
        }

        while (!Bot.ShouldExit && (!Core.CheckInventory("Weapon Reflection", 12)))
        {
            Core.EnsureAccept(5518);
            Core.HuntMonster("nostalgiaquest", "Skeletal Viking", "Reflected Glory", 5);
            Core.HuntMonster("nostalgiaquest", "Skeletal Warrior", "Divided Light", 5);
            Core.EnsureComplete(5518);
            Bot.Wait.ForPickup("Weapon Reflection");
        }

        //Boom Went The Dynamite
        if (!Core.CheckInventory("Dual Boom Went The Dynamite"))
        {
            if (!Core.CheckInventory("Boom Went The Dynamite", toInv: false))
            {
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("banished", "Desterrat Moya", "Boom Went The Dynamite", isTemp: false);
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Boom Went The Dynamite");
        }
        Core.ToBank("Dual Boom Went The Dynamite");

        //Unarmed
        if (!Core.CheckInventory("Dual Unarmed"))
        {
            if (!Core.CheckInventory("Unarmed", toInv: false))
            {
                Adv.BuyItem(Bot.Map.Name, 1536, "Unarmed");
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Unarmed");
        }
        Core.ToBank("Dual Unarmed");

        //Leviasea Sword
        if (!Core.CheckInventory("Dual Leviasea Sword"))
        {
            if (!Core.CheckInventory("Leviasea Sword", toInv: false))
            {
                Adv.BuyItem("yulgar", 69, "Leviasea Sword");
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Leviasea Sword");
        }
        Core.ToBank("Dual Leviasea Sword");

        //Ddog Sea Serpent Sword
        if (!Core.CheckInventory("Dual Ddog Sea Serpent Sword"))
        {
            if (!Core.CheckInventory("Ddog Sea Serpent Sword", toInv: false))
            {
                Core.EnsureAccept(554);
                Nation.FarmUni13(1);
                Core.HuntMonster("underworld", "Undead Legend", "Undead Legend Rune", log: false);
                Core.EnsureCompleteChoose(554, new[] { "Ddog Sea Serpent Sword" });
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Leviasea Sword");
        }
        Core.ToBank("Dual Ddog Sea Serpent Sword");

        //Soulreaper of Nulgath
        if (!Core.CheckInventory("Dual Soulreaper of Nulgath"))
        {
            if (!Core.CheckInventory("Soulreaper of Nulgath"))
            {
                Core.AddDrop("Soulreaper of Nulgath");
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
                Bot.Wait.ForPickup("Soulreaper of Nulgath");
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Soulreaper of Nulgath");
        }
        Core.ToBank("Dual Soulreaper of Nulgath");

        //Godly Mace of the Ancients
        if (!Core.CheckInventory("Dual Godly Mace of the Ancients"))
        {
            if (!Core.CheckInventory("Godly Mace of the Ancients"))
            {
                Adv.BuyItem("citadel", 44, "Godly Mace of the Ancients");
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Godly Mace of the Ancients");
        }
        Core.ToBank("Dual Godly Mace of the Ancients");

        //Balor's Cruelty
        if (!Core.CheckInventory("Dual Balor's Cruelty"))
        {
            if (!Core.CheckInventory("Balor's Cruelty", toInv: false))
            {
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("twilight", "Abaddon", "Balor's Cruelty", isTemp: false);
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Balor's Cruelty");
        }
        Core.ToBank("Dual Balor's Cruelty");

        //Abaddon's Terror
        if (!Core.CheckInventory("Dual Abaddon's Terrors"))
        {
            if (!Core.CheckInventory("Abaddon's Terror"))
            {
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("twilight", "Abaddon", "Abaddon's Terror", isTemp: false);
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Abaddon's Terrors");
        }
        Core.ToBank("Dual Abaddon's Terrors");

        //Mighty Sword Of The Dragons
        if (!Core.CheckInventory("Dual Mighty Sword Of The Dragons"))
        {
            if (!Core.CheckInventory("Mighty Sword Of The Dragons"))
            {
                Core.EquipClass(ClassType.Solo);
                Core.AddDrop("Zellare's Death Scale", "Moganth's Death Scale", "Udaroth's Death Scale", "Cellot's Death Scale", "Mighty Sword Of The Dragons");
                Core.RegisterQuests(3343);
                Bot.Quests.UpdateQuest(1416);
                while (!Bot.ShouldExit && !Core.CheckInventory("Mighty Sword Of The Dragons"))
                {
                    Core.HuntMonster("wind", "Cellot", "Cellot's Death Scale", isTemp: false);
                    Core.HuntMonster("fire", "Zellare", "Zellare's Death Scale", isTemp: false);
                    Core.HuntMonster("water", "Udaroth", "Udaroth's Death Scale", isTemp: false);
                    Core.HuntMonster("dragonplane", "Moganth", "Moganth's Death Scale", isTemp: false);
                }
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Mighty Sword Of The Dragons");
        }
        Core.ToBank("Dual Mighty Sword Of The Dragons");

        //Frostbite
        if (!Core.CheckInventory("Dual Frostbite"))
        {
            if (!Core.CheckInventory("Frostbite"))
            {
                Adv.BuyItem("blindingsnow", 236, "Frosted Falchion");
                Adv.BuyItem("underworld", 238, "Frostbite");
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual Frostbite");
        }
        Core.ToBank("Dual Frostbite");

        //DragonBlade of Nulgath
        if (!Core.CheckInventory("Dual DragonBlades of Nulgath"))
        {
            if (!Core.CheckInventory("DragonBlade of Nulgath"))
            {
                Core.Logger("You don't have DBoN on your inventory/bank, please check your buyback menu");
                return;
            }
            Adv.BuyItem("nostalgiaquest", 1311, "Dual DragonBlades of Nulgath");
        }
        Core.ToBank("Dual DragonBlades of Nulgath");

        //Phoenix Blade of Nulgath (Pseudo-Rare Item)
    }

    public void HuntingMonster()
    {
        GetItems("rotfinger", "rotfinger", "Horned Meat Horror Helm", "Macabre Horror Hammer", "Macabre Meat Horror", "Macabre Meat Ripper", "Macabre Meat Slicer", "Rotfinger's ArmBlades", "Rotfinger's Bow", "Rotfinger's Scythe", "Rotfinger's Staff", "Scream of Agony");
        GetItems("bonebreak", "Killek BoneBreaker", "Axe of Boneshearing", "Dark BonePiercer Spikes", "Killek BoneBreaker");
        GetItems("bonebreak", "Unbroken Minion", "Berserker Minion Mace");
        GetItems("bonebreak", "Undead Berserker", "Berserker Minion Skull Mace");
        GetItems("bonebreak", "Bonebreaker", "Undead Berserker Guard", "Undead Berserker Guard Helm");
        GetItems("deadfly", "Deadfly", "BlackSkulls Knuckle", "Deadfly Morph", "Deadfly's Armor", "Dual BlackSkulls Knuckles");
        GetItems("oddities", "Cursed Spirit", "Cursed Spirit Hunter", "Reaver of Wrath", "Scary Machete", "Scary Machetes", "Spirit Scythe of Wrath", "Spooky Spirit Hunter", "Spooky Spirit Hunter Hat", "Spooky Spirit Hunter Hat + Locks", "Spooky Spirit Hunter Hood", "Unlucky Farmer", "Unlucky Farmer's Hood", "Unlucky Portal Cape");
        GetItems("wormhole", "Trobbolegion", "Blue Trobbolier Morph", "Gold Trobbolier Morph", "Mutated Pink Trobbolier Morph", "Silver Trobbolier Morph");
        GetItems("gonnagetcha", "Shrade Cultist", "Cultist Knife", "Dual Cultist Knife", "Missing Keys Plaque");
        GetItems("gonnagetcha", "Murkonian", "GonnaGetcha Trident");
        GetItems("gonnagetcha", "Shrade", "DeathHunter Hair", "DeathHunter Hood", "DeathHunter Locks", "Fanged Cultist Mask", "Feral Cultist Mask", "Malevolent Cultist Mask", "Shadow Cultist Armor");
        GetItems("splatterwardage", "Shrade", "Celtic Hunter Blade", "Underworld Shrade", "Underworld Shrade Axe", "Underworld Shrade Helm", "Underworld Shrade Minion", "Well-wet Hair");
        GetItems("greymoor", "Shrade", "Necrotic Caster", "Necrotic Caster Cross Back", "Necrotic Caster Grave Spade", "Necrotic Caster Hair", "Necrotic Caster Locks", "Necrotic Caster Locks Morph", "Necrotic Caster Mask", "Necrotic Caster Mask Morph", "Necrotic Caster Masked Locks", "Necrotic Caster Scroll");
        GetItems("greymoor", "Ultra Shrade", "Shrade Armor", "Shrade Helm");
        GetItems("battledoom", "13th Doom Lord", "Doom Lord Vaal and Vayle", "Dual Skull Half-Axes", "Skulled Half-Axe", "SkullBorne Dagger", "Vaal's Doom Visage", "Vayle's Doom Hood", "Weeping Axe of DOOM");
        GetItems("crownsreachfxiii", "Shub-Hathrys", "Tentacled Tophat and Beard", "Tentacled Tophat and Locks");
    }

    private void GetItems(string map, string monster, params string[] items)
    {
        if (Core.CheckInventory(items, toInv: false))
            return;

        Bot.Drops.Add(items);
        foreach (string item in items)
        {
            Core.HuntMonster(map, monster, item, 1, false, log: false);
            Core.ToBank(item);
        }
    }
}
