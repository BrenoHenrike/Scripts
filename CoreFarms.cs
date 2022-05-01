using RBot;
using RBot.Items;

public class CoreFarms
{
    // [Can Change] Delay between common actions, 700 is the safe number
    public bool canSoloInPvP { get; set; } = true;

    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    //CLASS Boost! (60 min) -- 27555
    //Doom CLASS Boost! (60 min) -- 19761 (can get this from /join Doom merge with Daily XP Boosts)
    //Daily Login Class Boost *20 min* -- 22447

    //REPUTATION Boost! (60 min) -- 27553
    //Doom REP Boost! (60 min) -- 19762 (can get this from /join Doom merge with Daily XP Boosts)
    //REPUTATION Boost! (20 min) -- 22449

    //GOLD Boost! (60 min) -- 27554
    //Doom GOLD Boost! (60 min) -- 19763 (can get this from /join Doom merge with Daily XP Boosts)
    //GOLD Boost! (20 min) -- 22450

    //XP Boost! (60 min) -- 27552
    //Daily XP Boost! (1 hr) -- 19189
    //XP Boost! (20 min) -- 22448

    /// <summary>
    /// Uses a boost with the given ID.
    /// </summary>
    /// <param name="boostID">ID of the Boost</param>
    /// <param name="type">Type of the Boost</param>
    /// <param name="useMultiple">Whether use more than one boost</param>
    public void UseBoost(int boostID, BoostType type, bool useMultiple = true)
    {
        if (useMultiple)
        {
            Bot.RegisterHandler(5, b =>
            {
                if (!b.Player.IsBoostActive(type))
                    b.Player.UseBoost(boostID);
            });
        }
        else
        {
            Bot.Player.UseBoost(boostID);
        }
    }

    /// <summary>
    /// Uses a boost with one of the IDs present in <see cref="BoostIDs"/>
    /// </summary>
    /// <param name="boost">Desired Boost</param>
    /// <param name="type">Type of the Boost</param>
    /// <param name="useMultiple">Whether use more than one boost</param>
    public void UseBoost(BoostIDs boost, BoostType type, bool useMultiple = true) => UseBoost((int)boost, type, useMultiple);

    #region Gold
    public void Gold(int quant = 100000000)
    {
        if (Bot.Player.Gold >= quant)
            return;

        HonorHall(quant);
        BattleGroundE(quant);
        BerserkerBunny(quant);
    }

    /// <summary>
    /// Farms Gold in HonorHall (members) with quests HonorHall Mobs and 61-75
    /// </summary>
    /// <param name="goldQuant">How much gold to farm</param>
    public void HonorHall(int goldQuant = 100000000)
    {
        if (!Core.IsMember)
            return;
        if (Bot.Player.Level < 61)
            return;
        if (Bot.Player.Gold >= goldQuant)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {goldQuant} gold using HonorHall Method");
        while (Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.RegisterQuests(3992, 3993);
            Core.KillMonster("honorhall", "r1", "Center", "*", "Battleground E Opponent Defeated", 10, log: false);
            Core.KillMonster("honorhall", "r1", "Center", "*", "HonorHall Opponent Defeated", 10, log: false);
        }
    }

    /// <summary>
    /// Farms Gold in Battle Ground E with quests Level 46-60 and 61-75
    /// </summary>
    /// <param name="goldQuant">How much gold to farm</param>
    public void BattleGroundE(int goldQuant = 100000000)
    {
        if (Bot.Player.Gold >= goldQuant)
            return;
        if (Bot.Player.Level < 61)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {goldQuant} gold using BattleGroundE Method");
        while (Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.RegisterQuests(3992, 3993);
            Core.KillMonster("battlegrounde", "r2", "Center", "*", "Battleground D Opponent Defeated", 10, log: false);
            Core.KillMonster("battlegrounde", "r2", "Center", "*", "Battleground E Opponent Defeated", 10, log: false);
        }
    }

    /// <summary>
    /// Farms Gold by selling Berserker Bunny
    /// </summary>
    /// <param name="goldQuant">How much gold to farm</param>
    public void BerserkerBunny(int goldQuant = 100000000)
    {
        if (Bot.Player.Gold >= goldQuant)
            return;
        Core.AddDrop("Berserker Bunny");
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {goldQuant}  using BerserkerBunny Method");
        while (Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.RegisterQuests(236);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Were Egg", log: false);
            Bot.Player.Pickup("Berserker Bunny");
            Bot.Sleep(Core.ActionDelay);
            Bot.Shops.SellItem("Berserker Bunny");
        }
    }
    #endregion

    #region Experience
    public void Experience(int level = 100)
    {
        if (Bot.Player.Level >= level)
            return;
        IcestormArena();
    }

    /// <summary>
    /// Farms level in Ice Storm Arena
    /// </summary>
    /// <param name="level">Desired level</param>
    public void IcestormArena(int level = 100, bool rankUpClass = false)
    {
        if (Bot.Player.Level >= level && !rankUpClass)
            return;
        if (!rankUpClass)
            Core.EquipClass(ClassType.Farm);

        bool OptionRestore = Bot.Options.AggroMonsters;
        Bot.Options.AggroMonsters = true;

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 5 && Bot.Player.Level < level) || (Bot.Player.Level < 5 && rankUpClass && Bot.Player.Rank != 10)))
            Core.KillMonster("icestormarena", "r4", "Bottom", "*", log: false, publicRoom: true);

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 10 && Bot.Player.Level < level) || (Bot.Player.Level < 10 && rankUpClass && Bot.Player.Rank != 10)))
            Core.KillMonster("icestormarena", "r5", "Left", "*", log: false, publicRoom: true);

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 20 && Bot.Player.Level < level) || (Bot.Player.Level < 20 && rankUpClass && Bot.Player.Rank != 10)))
            Core.KillMonster("icestormarena", "r6", "Left", "*", log: false, publicRoom: true);

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 25 && Bot.Player.Level < level) || (Bot.Player.Level < 25 && rankUpClass && Bot.Player.Rank != 10)))
        {
            Core.RegisterQuests(6628);
            Core.KillMonster("icestormarena", "r7", "Left", "*", "Icewing Grunt Defeated", 3, log: false, publicRoom: true);
        }

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 30 && Bot.Player.Level < level) || (Bot.Player.Level < 30 && rankUpClass && Bot.Player.Rank != 10)))
            Core.KillMonster("icestormarena", "r10", "Left", "*", log: false, publicRoom: true);

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 35 && Bot.Player.Level < level) || (Bot.Player.Level < 35 && rankUpClass && Bot.Player.Rank != 10)))
        {
            Core.RegisterQuests(6629);
            Core.KillMonster("icestormarena", "r11", "Left", "*", "Icewing Warrior Defeated", 3, log: false, publicRoom: true);
        }

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 50 && Bot.Player.Level < level) || (Bot.Player.Level < 50 && rankUpClass && Bot.Player.Rank != 10)))
            Core.KillMonster("icestormarena", "r14", "Left", "*", log: false, publicRoom: true);

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 75 && Bot.Player.Level < level) || (Bot.Player.Level < 75 && rankUpClass && Bot.Player.Rank != 10)))
            Core.KillMonster("icestormarena", "r3b", "Top", "*", log: false, publicRoom: true);

        while (!Bot.ShouldExit() && ((Bot.Player.Level < 100 && Bot.Player.Level < level) || (Bot.Player.Level <= 100 && rankUpClass && Bot.Player.Rank != 10)))
            Core.KillMonster("icestormarena", "r3c", "Top", "*", log: false, publicRoom: true);

        Bot.Options.AggroMonsters = OptionRestore;
    }

    /// <summary>
    /// Farms in Seven Circles War for level and items
    /// </summary>
    /// <param name="level">Desired level</param>
    public void SevenCirclesWar(int level = 100, int gold = 100000000)
    {
        if (Bot.Player.Level >= level && Bot.Player.Gold >= gold)
            return;      
       
       if (!Bot.Quests.IsAvailable(7979))
        {
            Core.Logger("Please use Scripts/Story/Legion/SevenCircles(War).cs in order to use the SevenCircles method");
            return;
        }

        Core.AddDrop("Essence of Wrath", "Souls of Heresy");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {gold} gold using SCW Method");
        Core.RegisterQuests(7979, 7980, 7981);
        while (Bot.Player.Level < level && Bot.Player.Gold < gold && !Bot.ShouldExit())
        {
            Core.KillMonster("sevencircleswar", "Enter", "Right", "*", "Wrath Guards Defeated", 12);
            Core.KillMonster("sevencircleswar", "Enter", "Right", "*", "War Medal", 5);
            Core.KillMonster("sevencircleswar", "Enter", "Right", "*", "Mega War Medal", 3);
        }
    }
    #endregion

    #region Misc
    /// <summary>
    /// Farms the Black Knight Orb
    /// </summary>
    public void BlackKnightOrb()
    {
        if (Core.CheckInventory("Black Knight Orb"))
            return;
        Core.AddDrop("Black Knight Orb");
        Core.EnsureAccept(318);
        Core.HuntMonster("well", "Gell Oh No", "Black Knight Leg Piece");
        Core.HuntMonster("greendragon", "Greenguard Dragon", "Black Knight Chest Piece");
        Core.HuntMonster("deathgazer", "DeathGazer", "Black Knight Shoulder Piece");
        Core.HuntMonster("trunk", "Greenguard Basilisk", "Black Knight Arm Piece");
        Core.EnsureComplete(318);
        Bot.Player.Pickup("Black Knight Orb");
    }

    /// <summary>
    /// Kills the Restorers from /BludrutBrawl for "The Secret 4" item
    /// </summary>
    public void TheSecret4()
    {
        if (Core.CheckInventory("The Secret 4"))
            return;
        Core.EquipClass(ClassType.Solo);
        Core.JumpWait();
        while (!Core.CheckInventory("The Secret 4"))
        {
            Core.Join("bludrutbrawl", "Enter0", "Spawn", ignoreCheck: true);
            Bot.Wait.ForMapLoad("bludrutbrawl");
            Core.BludrutMove(5, "Morale0C");
            Core.BludrutMove(4, "Morale0B");
            Core.BludrutMove(7, "Morale0A");
            Core.BludrutMove(9, "Crosslower");
            Core.BludrutMove(14, "Crossupper", 528, 255);
            Core.BludrutMove(18, "Resource1A");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
            if (Core.CheckInventory("The Secret 4"))
                break;
            Core.BludrutMove(20, "Resource1B");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
            Bot.Player.Kill("Team B Restorer");
            Bot.Player.Pickup("The Secret 4");
        }
    }

    /// <summary>
    /// Kills the Team B Captain in /BludrutBrawl for the desired item (Combat Trophy or Yoshino's Citrine)
    /// </summary>
    /// <param name="item">Name of the desired item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="canSoloBoss">Whether you can solo the Boss without killing Restorers and Brawlers</param>
    public void BludrutBrawlBoss(string item = "Combat Trophy", int quant = 500, bool canSoloBoss = true)
    {
        if (Core.CheckInventory(item, quant))
            return;

        if (Core.CBO_Active)
            canSoloBoss = !Core.CBOBool("PVP_SoloPvPBoss");

        Core.AddDrop(item);

        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {quant} {item}. SoloBoss = {canSoloBoss}");

        while (!Core.CheckInventory(item, quant))
        {
            Core.AddDrop(item);
            Core.Join("bludrutbrawl", "Enter0", "Spawn");
            Bot.Wait.ForMapLoad("bludrutbrawl");
            Core.BludrutMove(5, "Morale0C");
            Core.BludrutMove(4, "Morale0B");
            Core.BludrutMove(7, "Morale0A");
            Core.BludrutMove(9, "Crosslower");
            if (!canSoloBoss)
            {
                Core.BludrutMove(14, "Crossupper", 528, 255);
                Core.BludrutMove(18, "Resource1A");
                Bot.Player.Kill("Team B Restorer");
                Bot.Player.Kill("Team B Restorer");
                Core.BludrutMove(20, "Resource1B");
                Bot.Player.Kill("Team B Restorer");
                Bot.Player.Kill("Team B Restorer");
                Core.BludrutMove(21, "Resource1A", 124);
                Core.BludrutMove(19, "Crossupper", 124);
                Core.BludrutMove(17, "Crosslower", 488, 483);
            }
            Core.BludrutMove(15, "Morale1A");
            if (!canSoloBoss)
                Bot.Player.Kill("Team B Brawler");
            Core.BludrutMove(23, "Morale1B");
            if (!canSoloBoss)
                Bot.Player.Kill("Team B Brawler");
            Core.BludrutMove(25, "Morale1C");
            if (!canSoloBoss)
                Bot.Player.Kill("Team B Brawler");
            Core.BludrutMove(28, "Captain1", 528, 255);
            Bot.Player.Kill("Team B Captain");
            Bot.Wait.ForPickup(item);
            while (Bot.Map.Name != "battleon")
            {
                Bot.Sleep(5000);
                Core.Join("battleon");
                Bot.Wait.ForMapLoad("battleon");
            }

        }
    }

    public void BattleUnderB(string item = "Bone Dust", int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Core.AddDrop(item);
        Core.EquipClass(ClassType.Farm);

        Core.JumpWait();
        Core.Join("battleunderb", "Enter", "Spawn", ignoreCheck: true);
        Bot.Wait.ForMapLoad("battleunderb".ToLower());

        Bot.Options.AggroMonsters = true;
        Core.KillMonster("battleunderb", "Enter", "Spawn", "*", item, quant, false, publicRoom: true, log: false);
        Bot.Options.AggroMonsters = false;
        Bot.Player.ExitCombat();
    }

    #endregion

    #region Reputation
    public void GetAllRanks()
    {                           // Commented out functions dont excist yet
        AegisREP();
        AlchemyREP();
        ArcangroveREP();
        BaconCatREP();
        BeastMasterREP();
        BlacksmithingREP();
        BladeofAweREP();
        BrightoakREP();
        ChaosMilitiaREP();
        ChaosREP();
        ChronoSpanREP();
        CraggleRockREP();
        DeathPitArenaREP();
        //DeathPitBrawlREP();
        DiabolicalREP();
        DoomwoodREP();
        DreadFireREP();
        DreadrockREP();
        DruidGroveREP();
        DwarfholdREP();
        ElementalMasterREP();
        EmberseaREP();
        EternalREP();
        EtherStormREP();
        EvilREP();
        //FaerieCourtREP(); <- seasonal
        FishingREP();
        GlaceraREP();
        GoodREP();
        HollowbornREP();
        HorcREP();
        InfernalArmyREP();
        LoremasterREP();
        LycanREP();
        MonsterHunterREP();
        MysteriousDungeonREP();
        MythsongREP();
        NecroCryptREP();
        NorthpointeREP();
        PetTamerREP();
        RavenlossREP();
        SandseaREP();
        SkyguardREP();
        SomniaREP();
        SpellCraftingREP();
        SwordhavenREP();
        ThunderForgeREP();
        TreasureHunterREP();
        TrollREP();
        VampireREP();
        YokaiREP();
    }

    public void AegisREP(int rank = 10)
    {
        if (FactionRank("Aegis") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(4900, 4910, 4914);
        while (FactionRank("Aegis") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("skytower", "Seraphic Assassin", "Seraphic Assassin Dueled", 10);
            Core.HuntMonster("skytower", "Virtuous Warrior", "Warriors Dueled", 10);
            Core.HuntMonster("skytower", "Seraphic Assassin", "Assassins Handed To Them", 6);
            Core.HuntMonster("skytower", "Virtuous Warrior", "Warrior Butt Beaten", 6);
        }
    }

    /// <summary>
    /// Uses the specified parameters to make an Alchemy misture
    /// </summary>
    /// <param name="reagent1">The first reagent.</param>
    /// <param name="reagent2">The second reagent</param>
    /// <param name="rune">The rune to be used (AlchemyRunes.Gebo by default).</param>
    /// <param name="rank">The minimum rank to make the misture, use 0 for any rank.</param>
    /// <param name="loop">Whether loop till you run out of reagents</param>
    /// <param name="modifier">Some mistures have specific packet modifiers, default is Moose but you can find Man, mRe and others.</param>
    public void AlchemyPacket(string reagent1, string reagent2, AlchemyRunes rune = AlchemyRunes.Gebo, int rank = 0, bool loop = true, string modifier = "Moose")
    {
        if (rank != 0 && FactionRank("Alchemy") < rank && !Bot.ShouldExit())
            AlchemyREP(rank);

        void Packet()
        {
            Bot.Sleep(3500);
            Bot.SendPacket($"%xt%zm%crafting%1%getAlchWait%11475%11478%true%Ready to Mix%{reagent1}%{reagent2}%{rune}%{modifier}%");
            Bot.Sleep(11000);
            Bot.SendPacket($"%xt%zm%crafting%1%checkAlchComplete%11475%11478%true%Mix Complete%{reagent1}%{reagent2}%{rune}%{modifier}%");
        }

        Core.Logger($"Reagents: [{reagent1}], [{reagent2}].");
        Core.Logger($"Rune: {rune}.");
        Core.Logger($"Modifier: {modifier}.");
        if (loop)
        {
            int i = 1;
            while (Core.CheckInventory(new[] { reagent1, reagent2 }))
            {
                Packet();
                Core.Logger($"Completed alchemy x{i++}");
            }
        }
        else
            Packet();
    }

    public void AlchemyREP(int rank = 10, bool goldMethod = true)
    {
        if (FactionRank("Alchemy") >= rank)
            return;

        if (!Bot.Player.Factions.Exists(f => f.Name == "Alchemy"))
        {
            Core.Logger("Getting Pre-Ranking XP");
            if (!Core.CheckInventory(new[] { "Ice Vapor", "Dragon Scale", "Dragon Runestone" }))
            {
                if (!Core.CheckInventory("Dragon Runestone", 10))
                {
                    Gold(1000000);
                    Core.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 10);
                }
                Core.BuyItem("alchemyacademy", 395, 7132, 3);
                Core.BuyItem("alchemyacademy", 397, 11475, 1, 2);
                Core.BuyItem("alchemyacademy", 397, 11478, 1, 2);
                AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Jera, loop: false);
            }
        }


        Core.AddDrop("Dragon Scale", "Ice Vapor");
        Core.Logger($"Farming rank {rank} Alchemy");
        int i = 1;
        while (FactionRank("Alchemy") < rank && !Bot.ShouldExit())
        {
            if (goldMethod)
            {
                if (!Core.CheckInventory(new[] { "Ice Vapor", "Dragon Scale", "Dragon Runestone" }))
                {
                    if (!Core.CheckInventory("Dragon Runestone", 10))
                    {
                        Gold(1000000);
                        Core.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 10);
                    }
                    Core.BuyItem("alchemyacademy", 395, 7132, 10);
                    Core.BuyItem("alchemyacademy", 397, 11475, 10, 2);
                    Core.BuyItem("alchemyacademy", 397, 11478, 10, 2);
                }
                AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Gebo);
            }
            else
            {
                Core.KillMonster("lair", "Enter", "Spawn", "*", "Dragon Scale", 10, false);
                Core.KillMonster("lair", "Enter", "Spawn", "*", "Ice Vapor", 10, false);
                AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Gebo);
            }
            Core.Logger($"Iteration {i++} completed");
        }
    }

    public void ArcangroveREP(int rank = 10)
    {
        if (FactionRank("Arcangrove") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(794, 795, 796, 797, 798, 799, 800, 801);
        while (FactionRank("Arcangrove") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("arcangrove", "Seed Spitter", "Spool of Arcane Thread", 10);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Defeated Seed Spitter", 10);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Bundle of Thyme", 10);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Thistle", 5);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Pretzel Root", 4);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Lore-Strip Gorillaphant Steak", 8);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Slain Gorillaphant", 7);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Gorillaphant Tusk", 6);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Batch of Mustard Seeds", 3);
        }
    }

    public void BaconCatREP(int rank = 10)
    {
        if (FactionRank("BaconCat") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.EquipClass(ClassType.Farm);

        if (!Bot.Quests.IsUnlocked(5120))
            Core.Logger($"Quest [5120] \"Ziri Is Also Tough\", has yet to be completed, please run \"Farm/REP/BaconCatREP.cs\"", stopBot: true, messageBox: true);

        Core.RegisterQuests(5112, 5120);
        while (FactionRank("BaconCat") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Moglinberry Ice Cream", 5);
            Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Shark Teeth", 10);
        }
    }

    public void BeastMasterREP(int rank = 10)
    {
        if (FactionRank("BeastMaster") >= rank)
            return;
        if (!Core.IsMember)
        {
            Core.Logger("Beast Master REP is Member-Only", messageBox: true);
            return;
        }

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(3757);
        while (FactionRank("BeastMaster") < rank && !Bot.ShouldExit())
        {
            Bot.Quests.UpdateQuest(4614);
            Core.HuntMonster("pyramid", "Golden Scarab", "Gleaming Gems of Containment", 9);
            Core.HuntMonster("lair", "Golden Draconian", "Bright Binding of Submission", 8);
        }
    }

    public void BlacksmithingREP(int rank = 4)
    {
        if (FactionRank("Blacksmithing") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(2777);
        while (FactionRank("Blacksmithing") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("greenguardeast", "Wolf", "Furry Lost Sock", 2);
            Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5);
        }
    }

    public void BladeofAweREP(int rank = 10, bool farmBoA = true)
    {
        if (FactionRank("Blade of Awe") >= rank && !farmBoA)
            return;
        if (farmBoA && Core.CheckInventory("Blade of Awe"))
            farmBoA = false;
        if (farmBoA)
            Core.AddDrop("Legendary Stonewrit", "Legendary Handle", "Legendary Hilt", "Legendary Blade", "Legendary Runes");
        Core.AddDrop("Stonewrit Found!", "Handle Found!", "Hilt Found!", "Blade Found!", "Runes Found!");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(2933, 2934);
        int i = 1;
        if (!Core.CheckInventory("Legendary Stonewrit", toInv: false) && (!Bot.Quests.IsUnlocked(2934) || farmBoA))
        {
            Core.HuntMonster("j6", "Sketchy Dragon", "Stonewrit Found!", 1, false);
            if (farmBoA)
                Bot.Player.Pickup("Legendary Stonewrit");
            Core.Logger("Find the Stonewrit! completed");
        }
        if (!Core.CheckInventory("Legendary Handle", toInv: false) && (!Bot.Quests.IsUnlocked(2935) || farmBoA))
        {
            Core.HuntMonster("gilead", "Fire Elemental", "Handle Found!", 1, false);
            if (farmBoA)
                Bot.Player.Pickup("Legendary Handle");
            Core.Logger("Find the Handle! completed");
        }
        Core.RegisterQuests(2933, 2934);
        Core.RegisterQuests(2935);
        while (FactionRank("Blade of Awe") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("castleundead", "Skeletal Viking", "Hilt Found!", 1, false);
            if (farmBoA)
            {
                Bot.Player.Pickup("Legendary Hilt");
                if (FactionRank("Blade of Awe") >= 6)
                    break;
            }
            Core.Logger($"Completed Find the Hilt! x{i++}");
        }
        Core.RegisterQuests(2935);
        if (farmBoA)
        {
            Core.RegisterQuests(2936, 2937);
            if (FactionRank("Blade of Awe") < 6 || !Bot.Quests.IsAvailable(2939))
            {
                if (!Core.CheckInventory("Legendary Blade", toInv: false))
                {
                    Core.HuntMonster("hydra", "Hydra Head", "Blade Found!", 1, false);
                    Bot.Player.Pickup("Legendary Blade");
                    Core.Logger("Find the Blade! completed");
                }
                if (!Core.CheckInventory("Legendary Runes", toInv: false))
                {
                    Core.KillEscherion("Runes Found!", publicRoom: true);
                    Bot.Player.Pickup("Legendary Runes");
                    Core.Logger("Find the Runes! completed");
                }
                Core.Unbank("Legendary Stonewrit", "Legendary Handle", "Legendary Hilt", "Legendary Blade", "Legendary Runes");
                Core.BuyItem("museum", 630, "Blade of Awe");
            }
            if (FactionRank("Blade of Awe") >= 6 && Bot.Quests.IsAvailable(2939))
                Core.BuyItem("museum", 631, "Blade of Awe");
        }
    }

    public void BrightoakREP(int rank = 11)
    {
        if (FactionRank("Brightoak") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(4667))
        {
            Core.Logger("Quest not avaible for farm, do Brightoak saga till Elfhame [Unlocking the Guardian's Mouth]");
            return;
        }
        Core.Logger($"Farming rank {rank}");
        Core.Join("elfhame");
        Core.RegisterQuests(4667);
        while (FactionRank("Brightoak") < rank && !Bot.ShouldExit())
        {
            Bot.Map.GetMapItem(3984);
        }
    }

    public void ChaosMilitiaREP(int rank = 10)
    {
        if (FactionRank("Chaos Militia") >= rank)
            return;
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(5775);
        while (FactionRank("Chaos Militia") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("citadel", "Inquisitor Guard", "Inquisitor's Tabard", 10);
        }
    }

    public void ChaosREP(int rank = 10)
    {
        if (FactionRank("Chaos") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Chaos);
        Core.RegisterQuests(3594);
        while (FactionRank("Chaos") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("mountdoomskull", "b1", "Left", "*", "Chaos Power Increased", 6);
        }
    }

    public void ChronoSpanREP(int rank = 10)
    {
        if (FactionRank("ChronoSpan") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.ChronoSpan);

        Core.RegisterQuests(2204);
        while (FactionRank("ChronoSpan") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("thespan", "Moglin Ghost", "Tin of Ghost Dust", 2);
            Core.HuntMonster("thespan", "Minx Fairy", "8 oz Fairy Glitter", 3);
            Core.HuntMonster("thespan", "Tog", "Tog Fang", 4);
        }
    }

    public void CraggleRockREP(int rank = 10)
    {
        if (FactionRank("CraggleRock") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(7277);
        while (FactionRank("CraggleRock") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("wanders", "r3", "Down", "Kalestri Worshiper", "Star of the Sandsea");
        }
    }

    public void DeathPitArenaREP(int rank = 10)
    {
        if (FactionRank("Death Pit Arena") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(5154))
        {
            Core.Logger("Quest not available for farm, do the Death Pit Arena saga and unlock the quest [Pax Defeated]");
            return;
        }
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(5153);
        while (FactionRank("Death Pit Arena") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("deathpit", "General Hun'Gar", "General Hun'Gar Defeated", 1);
        }
    }

    public void DiabolicalREP(int rank = 10)
    {
        if (FactionRank("Diabolical") >= rank)
            return;

        Core.RegisterQuests(7877);
        while (FactionRank("Diabolical") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("mudluk", "Tiger Leech", "Swamped Leech Tooth");
        }
    }

    public void DoomwoodREP(int rank = 10)
    {
        if (FactionRank("Doomwood") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Doomwood);

        Core.AddDrop("Dark Tower Sword", "Light Tower Sword");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(1151, 1152, 1153, 2100, 2101, 2012);
        while (FactionRank("Doomwood") < rank && !Bot.ShouldExit())
        {
            if (!Core.IsMember)
            {
                Core.HuntMonster("shadowfallwar", "*", "To Do List of Doom");
                Core.HuntMonster("shadowfallwar", "*", "Skeleton Key");
            }
            else
            {
                if (!Core.CheckInventory("Light Tower Sword"))
                {
                    Core.HuntMonster("battleunderb", "Skeleton Warrior", "Battered Dark Tower Sword");
                    Bot.Player.Pickup("Dark Tower Sword");
                    Core.HuntMonster("doomwar", "Bronze DracoZombie", "Dracozombies' Spirits", 13);
                    Bot.Player.Pickup("Dark Tower Sword");
                }
                Core.HuntMonster("doomwar", "Dark DracoZombie", "Bones of the Dracozombie");
            }
        }
        if (Core.IsMember)
            Bot.Shops.SellItem("Light Tower Sword");
    }

    public void DreadFireREP(int rank = 10)
    {
        if (FactionRank("Dreadfire") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(5697);
        while (FactionRank("Dreadfire") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("dreadfire", "r13", "Bottom", "Arcane Crystal", "Perfect Crystal Orb");
        }
    }

    public void DruidGroveREP(int rank = 10)
    {
        if (FactionRank("Druid Grove") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(3049);
        while (FactionRank("Druid Grove") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Geode", 5);
        }
    }

    public void DwarfholdREP(int rank = 10)
    {
        if (FactionRank("Dwarfhold") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Dwarfhold);

        Core.RegisterQuests(320, 321);
        while (FactionRank("Dwarfhold") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("pines", "Enter", "Right", "Pine Grizzly", "Bear Skin", 5);
            Core.KillMonster("pines", "Enter", "Right", "Red Shell Turtle", "Red Turtle Shell", 5);
        }
    }

    public void ElementalMasterREP(int rank = 10)
    {
        if (FactionRank("Elemental Master") >= rank)
            return;
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(3050, 3298);
        while (FactionRank("Elemental Master") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("gilead", "Water Elemental", "Water Core");
            Core.HuntMonster("gilead", "Fire Elemental", "Fire Core");
            Core.HuntMonster("gilead", "Wind Elemental", "Air Core");
            Core.HuntMonster("gilead", "Earth Elemental", "Earth Core");
            Core.HuntMonster("gilead", "Mana Elemental", "Mana Core");
        }
    }

    public void EmberseaREP(int rank = 10)
    {
        if (FactionRank("Embersea") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Embersea);

        Core.RegisterQuests(4228);
        while (FactionRank("Embersea") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("fireforge", "Blazebinder", "Defeated Blazebinder", 5);
        }
    }

    public void EternalREP(int rank = 10)
    {
        if (FactionRank("Eternal") >= rank)
            return;
        if (!Bot.Quests.IsAvailable(5198))
        {
            Core.Logger("Can't do farming quest [Sphynxes are Riddled with Gems] (/fourdpyramid)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(5198);
        while (FactionRank("Eternal") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("fourdpyramid", "r11", "Right", 2908, "White Gem", 2);
            Core.KillMonster("fourdpyramid", "r11", "Right", 2909, "Black Gem", 2);
        }
    }

    public void EtherStormREP(int rank = 10)
    {
        if (FactionRank("Etherstorm") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Etherstorm);

        Core.RegisterQuests(1721);
        while (FactionRank("Etherstorm") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("etherwardes", "Water Dragon Warrior", "Water Dragon Tears", 3);
            Core.HuntMonster("etherwardes", "Fire Dragon Warrior", "Fire Dragon Flames", 3);
            Core.HuntMonster("etherwardes", "Air Dragon Warrior", "Air Dragon Breaths", 3);
            Core.HuntMonster("etherwardes", "Earth Dragon Warrior", "Earth Dragon Claws", 3);
        }
    }

    public void EvilREP(int rank = 10)
    {
        if (FactionRank("Evil") >= rank)
            return;
        Core.ChangeAlignment(Alignment.Evil);
        Core.Logger($"Farming rank {rank}");
        Core.EquipClass(ClassType.Farm);
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Evil);

        Core.RegisterQuests(364);
        while (FactionRank("Evil") < 4)
        {
            Core.HuntMonster("newbie", "Slime", "Youthanize");
        }
        Core.RegisterQuests(366, 367);
        while (FactionRank("Evil") < rank && !Bot.ShouldExit())
        {
            if (!Core.IsMember)
            {
                Core.HuntMonster("castleundead", "*", "Replacement Tibia", 6);
                Core.HuntMonster("castleundead", "*", "Phalanges", 3);
            }
            else
            {
                Core.HuntMonster("sleuthhound", "Chair", "Chair", 4);
                Core.HuntMonster("sleuthhound", "Table", "Table", 2);
                Core.HuntMonster("sleuthhound", "Bookcase", "Bookcase");
            }
        }
    }

    public void FaerieCourtREP(int rank = 10) // Seasonal
    {
        if (FactionRank("Faerie Court") >= rank)
            return;
        Core.Logger($"Farming rank {rank}");
        while (FactionRank("Faerie Court") < rank && !Bot.ShouldExit())
        {
            Core.RegisterQuests(6775, 6779);
            if (FactionRank("Faerie Court") < 8)
            {
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("rainbow", "Lucky Harms", "Four Leaf Clover", 3);
            }
            if (FactionRank("Faerie Court") >= 8)
            {
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("faegrove", "Dark Sylphdrake", "Silver Sylph Feather");
            }
        }
    }

    public void GlaceraREP(int rank = 10)
    {
        if (FactionRank("Glacera") >= rank)
            return;

        if (!Core.isCompletedBefore(5601))
            Core.Logger("Farming Quests are not unlocked, Please run: \"Story/Glacera.cs\"", stopBot: true);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(5597, 5598, 5599, 5600);
        while (FactionRank("Glacera") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("icewindwar", "r2", "Left", "*", "World Ender Medal", 10, log: false);
        }
    }

    public void GoodREP(int rank = 10)
    {
        if (FactionRank("Good") >= rank)
            return;
        Core.ChangeAlignment(Alignment.Good);
        Core.Logger($"Farming rank {rank}");
        Core.EquipClass(ClassType.Farm);
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Good);

        Core.RegisterQuests(369, 372, 371);
        while (FactionRank("Good") < 4)
        {
            Core.HuntMonster("swordhavenbridge", "Slime", "Slime in a Jar", 6);
        }
        Core.RegisterQuests(369, 371, 372);
        while (FactionRank("Good") < rank && !Bot.ShouldExit())
        {
            if (!Core.IsMember)
            {
                Core.HuntMonster("castleundead", "*", "Chaorrupted Skull", 5);
            }
            else
            {
                Core.HuntMonster("sewer", "Grumble", "Grumble's Fang");
            }
        }
    }

    public void LoremasterREP(int rank = 10)
    {
        if (FactionRank("Loremaster") >= rank)
            return;

        Experience(15);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        while (FactionRank("Loremaster") < rank && !Bot.ShouldExit())
        {
            if (!Core.IsMember ? FactionRank("Loremaster") < 10 : FactionRank("Loremaster") < rank && !Bot.ShouldExit())
            {
                Experience(15);
                Core.EquipClass(ClassType.Farm);
                Core.RegisterQuests(7505);
                while (Core.IsMember ? FactionRank("Loremaster") < 10 : FactionRank("Loremaster") < rank && !Bot.ShouldExit())
                {
                    Core.HuntMonster("uppercity", "Drow Assassin", "Poisoned Dagger", 4);
                    Core.HuntMonster("wardwarf", "D'wain Jonsen", "Scroll: Opportunity's Strike");
                }
            }
            else if (Core.IsMember ? FactionRank("Loremaster") < 3 : FactionRank("Loremaster") < rank && !Bot.ShouldExit())
            {
                Core.EquipClass(ClassType.Farm);
                while (Core.IsMember ? FactionRank("Loremaster") < 3 : FactionRank("Loremaster") < rank && !Bot.ShouldExit())
                {
                    Core.HuntMonster("wardwarf", "Drow Assassin", "Poisoned Dagger", 4);
                    Core.HuntMonster("wardwarf", "D'wain Jonsen", "Scroll: Opportunity's Strike", 1);
                }
            }
            else if (Core.IsMember && FactionRank("Loremaster") >= 3)
            {
                Core.EquipClass(ClassType.Solo);
                while (FactionRank("Loremaster") < rank && !Bot.ShouldExit())
                {
                    Core.RegisterQuests(3032);
                    Core.HuntMonster("druids", "Young Void Giant", "Void Giant Death Knell", 1);
                }
            }
        }
    }

    public void LycanREP(int rank = 10)
    {
        if (FactionRank("Lycan") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Lycan);
        else
        if (!Bot.Quests.IsAvailable(537))
        {
            Core.Logger("Can't do farming quest [Sanguine] (/lycan)", messageBox: true);
            return;
        }
        Core.Logger($"Farming rank {rank}");

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(537);
        while (FactionRank("Lycan") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("lycan", "Sanguine", "Sanguine Mask");
        }
    }

    public void HollowbornREP(int rank = 10)
    {
        if (FactionRank("Hollowborn") >= rank)
            return;
        Core.AddDrop("Hollow Soul");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(7553, 7555);
        while (FactionRank("Hollowborn") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("shadowrealm", "r2", "Down", "*", "Darkseed", 8);
            Core.KillMonster("shadowrealm", "r2", "Down", "*", "Shadow Medallion", 5);
        }
    }

    public void HorcREP(int rank = 10)
    {
        if (FactionRank("Horc") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Horc);

        Core.RegisterQuests(1265);
        while (FactionRank("Horc") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("bloodtuskwar", "Chaotic Lemurphant", "Chaorrupted Eye", 3);
            Core.HuntMonster("bloodtuskwar", "Chaotic Horcboar", "Chaorrupted Tentacle", 5);
            Core.HuntMonster("bloodtuskwar", "Chaotic Chinchilizard", "Chaorrupted Tusk", 5);
        }
    }

    public void InfernalArmyREP(int rank = 10)
    {
        if (FactionRank("Infernal Army") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        while (FactionRank("Infernal Army") < rank && !Bot.ShouldExit())
        {
            Core.RegisterQuests(5707);
            Core.KillMonster("dreadfire", "r10", "Left", "Living Brimstone", "Living Brimstone Defeated");
        }
        Core.RegisterQuests(5707);
    }

    public void MonsterHunterREP(int rank = 10)
    {
        if (FactionRank("Monster Hunter") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(5849, 5850);
        if (!Bot.Quests.IsAvailable(5850))
        {
            Core.KillMonster("pilgrimage", "r5", "Left", "SpiderWing", "Spiderwing Captured", 4);
            Core.KillMonster("pilgrimage", "r5", "Left", "Urstrix", "Urstrix Captured", 4);
            Core.Logger($"Completed Quest Capture the Misshapen");

        }
        Core.Logger($"Farming rank {rank}");
        while (FactionRank("Monster Hunter") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("pilgrimage", "r5", "Left", "SpiderWing", "Spiderwing Captured", 4);
            Core.KillMonster("pilgrimage", "r5", "Left", "Urstrix", "Urstrix Captured", 4);
            Core.KillMonster("pilgrimage", "r5", "Left", "Ravenous Parasite", "Ravenous Parasites Slain", 7);
        }
    }

    public void MysteriousDungeonREP(int rank = 10)
    {
        if (FactionRank("Mysterious Dungeon") >= rank)
            return;
        Core.Logger($"Farming rank {rank}");
        Core.EquipClass(ClassType.Farm);
        if (!Bot.Quests.IsAvailable(5429))
        {
            Core.Join("cursedshop");
            Core.EnsureAccept(5428);
            Bot.Map.GetMapItem(4803);
            Bot.Sleep(2500);
            if (Bot.Quests.CanComplete(5428))
                Core.EnsureComplete(5428);
            Bot.Player.Jump("Enter", "Spawn");
        }
        Core.RegisterQuests(5429);
        while (FactionRank("Mysterious Dungeon") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("cursedshop", "Antique Chair", "Antique Chair Defeated");
        }
    }

    public void MythsongREP(int rank = 10)
    {
        if (FactionRank("Mythsong") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Mythsong);

        if (!Bot.Quests.IsAvailable(710))
        {
            Core.Logger("Can't do farming quest [Kimberly] (/palooza)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(710);
        while (FactionRank("Mythsong") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("palooza", "Kimberly", "Kimberly Defeated");
        }
    }

    public void NecroCryptREP(int rank = 10)
    {
        if (FactionRank("Necro Crypt") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(3048);
        while (FactionRank("Necro Crypt") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("castleundead", "Skeletal Viking", "Old Bone", 5);
        }
    }

    public void NorthpointeREP(int rank = 10)
    {
        if (FactionRank("Northpointe") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Northpointe);

        Core.RegisterQuests(4027);
        while (FactionRank("Northpointe") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("northpointe", "Grim Stalker", "Bunch of Sage", 10);
        }
    }

    public void PetTamerREP(int rank = 10)
    {
        if (FactionRank("Pet Tamer") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(5261);
        while (FactionRank("Pet Tamer") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("greenguardwest", "West7", "Down", "Mogzard", "Mogzard Captured");
        }
    }

    public void RavenlossREP(int rank = 10)
    {
        if (FactionRank("Ravenloss") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Ravenloss);

        if (!Bot.Quests.IsAvailable(3445))
        {
            Core.Logger("Can't do farming quest [Slay the Spiderkin] (/twilightedge)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Core.RegisterQuests(3445);
        while (FactionRank("Ravenloss") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("twilightedge", "ChaosWeaver Mage", "ChaosWeaver Slain", 10);
        }
    }

    public void SandseaREP(int rank = 10)
    {
        if (FactionRank("Sandsea") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Sandsea);
            Core.RegisterQuests(916, 917, 919, 921, 922);
        while (FactionRank("Sandsea") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("sandsea", "Bupers Camel", "Bupers Camel Document", 10);
            Core.HuntMonster("sandsea", "Bupers Camel", "Barrel of Desert Water", 10);
            Core.HuntMonster("sandsea", "Bupers Camel", "Flexible Camel Spit", 7);
            Core.HuntMonster("sandsea", "Bupers Camel", "Oasis Jewelry Piece", 4);
            Core.HuntMonster("sandsea", "Bupers Camel", "Camel Skull", 2);
            Core.HuntMonster("sandsea", "Cactus Creeper", "Sandsea Cotton", 8);
            Core.HuntMonster("sandsea", "Cactus Creeper", "Cactus Creeper Head", 8);
        }
    }

    public void SkyguardREP(int rank = 10)
    {
        if (FactionRank("Skyguard") >= rank)
            return;
        if (!Core.IsMember)
        {
            Core.Logger("Skyguard REP is Member-Only", messageBox: true);
            return;
        }

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Skygaurd);

            Core.RegisterQuests(1016);
        while (FactionRank("Skyguard") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("gilead", "Water Elemental", "Bucket of Water", 5);
            Core.HuntMonster("gilead", "Wind Elemental", "Beaker of Air", 5);
        }
    }

    public void SomniaREP(int rank = 10)
    {
        if (FactionRank("Somnia") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
            Core.RegisterQuests(7665, 7666, 7669);
        while (FactionRank("Somnia") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("somnia", "Nightspore", "Dream Truffle", 8);
            Core.HuntMonster("somnia", "Orpheum Elemental", "Orphium Ore", 8);
            Core.HuntMonster("somnia", "Dream Larva", "Dreamsilk", 5);
        }
    }

    public void SpellCraftingREP(int rank = 10)
    {
        if (FactionRank("SpellCrafting") >= rank)
            return;
        Core.AddDrop("Mystic Quills", "Mystic Parchment");
        Core.Logger($"Farming rank {rank}");
        Core.EquipClass(ClassType.Farm);

        if (FactionRank("SpellCrafting") == 0)
        {
            Core.EnsureAccept(2260);
            Core.Join("dragonrune");
            Bot.Map.GetMapItem(1920);
            Core.HuntMonster("castleundead", "Skeletal Warrior", "Arcane Parchment", 13);
            Core.JumpWait();
            Bot.Quests.EnsureComplete(2260, tries: 1);
        }

        if (FactionRank("SpellCrafting") < 4)
        {
            Core.HuntMonster("mobius", "Slugfit", "Mystic Quills", 10, false);
            Core.BuyItem("dragonrune", 549, "Ember Ink", 50, 5);
            while (FactionRank("SpellCrafting") < 4)
            {
                Core.ChainComplete(2299);
            }
        }
        while (FactionRank("SpellCrafting") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("underworld", "Skull Warrior", "Mystic Parchment", 10, false);
            Core.BuyItem("dragonrune", 549, "Hallow Ink", 50, 5);
            while (Core.CheckInventory("Hallow Ink") && FactionRank("SpellCrafting") < rank && !Bot.ShouldExit())
            {
                Core.ChainComplete(2322);
            }
        }
        Core.SellItem("Ember Ink", all: true);
        Core.SellItem("Hallow Ink", all: true);
    }

    public void SwordhavenREP(int rank = 10)
    {
        if (FactionRank("Swordhaven") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Swordhaven);

            Core.RegisterQuests(3065, 3066, 3067, 3070, 3085, 3086, 3087);
        while (FactionRank("Swordhaven") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("castle", "Castle Spider", "Eradicated Arachnid", 10);
            Core.HuntMonster("castle", "Castle Spider", "Castle Spider Silk", 8);
            Core.HuntMonster("castle", "Castle Spider", "Castle Spider Silk Yarn", 2);
            Core.HuntMonster("castle", "Castle Wraith", "Castle Wraith Defeated", 10);
            Core.HuntMonster("castle", "Castle Wraith", "Jarred Wraith", 5);
            Core.HuntMonster("castle", "Castle Wraith", "Castle Wraith Wool", 2);
            Core.HuntMonster("castle", "Gargoyle", "Stony Plating", 6);
            Core.HuntMonster("castle", "Gargoyle", "Gargoyle Gems", 2);
            Core.HuntMonster("castle", "Dungeon Fiend", "Dungeon Fiend Hair Bow", 5);
            Core.HuntMonster("castle", "Dungeon Fiend", "Dungeon Fiend Bow Tie", 5);
            Core.HuntMonster("castle", "Dungeon Fiend", "Dungeon Fiend Textiles", 2);
        }
    }

    public void ThunderForgeREP(int rank = 10)
    {
        if (FactionRank("ThunderForge") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Thunderforge);
        else
        if (!Bot.Quests.IsAvailable(2733))
        {
            Core.Logger("Quest not avaible for farm, do ThunderForge saga till Deathpits [The Chaos Eye of Vestis]");
            return;
        }
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming rank {rank}");
            Core.RegisterQuests(2733);
        while (FactionRank("ThunderForge") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("deathpits", "Wrathful Vestis", "Vestis's Chaos Eye");
        }
    }

    public void TreasureHunterREP(int rank = 10)
    {
        if (FactionRank("TreasureHunter") >= rank)
            return;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
            Core.RegisterQuests(6593);
        while (FactionRank("TreasureHunter") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("stalagbite", "Balboa", "Super Specific Rock");
        }
    }

    public void TrollREP(int rank = 10)
    {
        UseBoost(0, RBot.Items.BoostType.Reputation, false);
        if (FactionRank("Troll") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Troll);
        else
            Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
            Core.RegisterQuests(1263);
        while (FactionRank("Troll") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("bloodtuskwar", "Chaotic Lemurphant", "Chaorrupted Eye", 3);
            Core.HuntMonster("bloodtuskwar", "Chaotic Horcboar", "Chaorrupted Tentacle", 5);
            Core.HuntMonster("bloodtuskwar", "Chaotic Chinchilizard", "Chaorrupted Tusk", 5);
        }
    }

    public void VampireREP(int rank = 10)
    {
        if (FactionRank("Vampire") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Vampire);
        else
        if (!Bot.Quests.IsAvailable(522))
        {
            Core.Logger("Can't do farming quest [Twisted Paw] (/safiria)", messageBox: true);
            return;
        }
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming rank {rank}");
            Core.RegisterQuests(522);
        while (FactionRank("Vampire") < rank && !Bot.ShouldExit())
        {
            Core.HuntMonster("safiria", "Twisted Paw", "Twisted Paw's Head");
        }
    }

    public void YokaiREP(int rank = 10)
    {
        if (FactionRank("Yokai") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Yokai);
            
            Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        Bot.Quests.UpdateQuest(488);
            Core.RegisterQuests(383);
        while (FactionRank("Yokai") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("dragonkoiz", "t1", "Left", "Pockey Chew", "Piece of Pockey", 3);
            Bot.Player.Jump("Enter", "Spawn");
        }
    }

    public void DreadrockREP(int rank = 10)
    {
        if (FactionRank("Dreadrock") >= rank)
            return;
        Core.AddDrop("Ghastly Dreadrock Blade");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
            Core.RegisterQuests(4863, 4862, 4865, 4868);
        while (FactionRank("Dreadrock") < rank && !Bot.ShouldExit())
        {
            Core.KillMonster("dreadrock", "r3", "Bottom", "*", "Goldfish Companion", 1);
        }
    }

    public void FishingREP(int rank = 10)
    {
        if (FactionRank("Fishing") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming rank {rank}");
        int z = 1;
        Core.AddDrop("Fishing Bait", "Fishing Dynamite");

        Core.Logger("Pre-Ranking XP");
        Core.EnsureAccept(1682);
        Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);
        Core.EnsureComplete(1682);

        while (FactionRank("Fishing") < 2)
        {
            Core.Logger("Farming Bait");
                Core.RegisterQuests(1682);
            while (!Core.CheckInventory("Fishing Bait", 10))
            {
                Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);
            }

            Core.Join("fishing");
            Core.Logger($"Bait Fishing");

            while (Core.CheckInventory("Fishing Bait"))
            {
                while (!Core.CheckInventory("Fishing Bait"))
                    return;

                Bot.SendPacket("%xt%zm%FishCast%1%Net%30%");
                Bot.Sleep(10000);
                Core.Logger($"Fished {z++} Times");
            }
        }


        while (FactionRank("Fishing") < rank && !Bot.ShouldExit())
        {
            Core.Logger("Farming Dynamite");
            Core.RegisterQuests(1682);
            while (!Core.CheckInventory("Fishing Dynamite", 10) && Core.CheckInventory("Fishing Bait", 1))
            {
                Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);
            }

            Core.Logger($"Dynamite Fishing");

            while (Core.CheckInventory("Fishing Dynamite", 1))
            {
                Bot.SendPacket($"%xt%zm%FishCast%1%Dynamite%30%");
                Bot.Sleep(3500);
                Core.SendPackets("%xt%zm%getFish%1%false%");
                Core.Logger($"Fished {z++} Times");
            }
        }
    }

    public void SwagTokenA(int quant = 100)
    {
        if (!Core.IsMember || Core.CheckInventory("Super-Fan Swag Token A", quant))
            return;

        Core.AddDrop("Super-Fan Swag Token A", "Super-Fan Swag Token B", "Super-Fan Swag Token C");
        Core.EquipClass(ClassType.Farm);
        int i = quant - Bot.Inventory.GetQuantity("Super-Fan Swag Token A");

        while (!Core.CheckInventory("Super-Fan Swag Token A", quant))
        {
            Core.Logger($"Farming Token A x {i}");

            Core.Logger($"Farming Token C x {200 - Bot.Inventory.GetQuantity("Super-Fan Swag Token C")}");

            Core.RegisterQuests(1310);
            while (!Core.CheckInventory("Super-Fan Swag Token C", 200))
            {
                Core.KillMonster("collectorlab", "r2", "Right", "*", "Doppelganger Documents", log: false);
            }

            if (!Core.CheckInventory("Super-Fan Swag Token A", quant))
            {
                Core.BuyItem("collection", 325, "Super-Fan Swag Token B", 20);
                Bot.Sleep(1500);
                Core.BuyItem("collection", 325, "Super-Fan Swag Token A", Bot.Inventory.GetQuantity("Super-Fan Swag Token A") + 1);
            }
            Core.Logger($"Token A {quant - Bot.Inventory.GetQuantity("Super-Fan Swag Token A")} Left to Farm");
        }
    }

    public void MembershipDues(MemberShipsIDS faction, int rank = 10)
    {
        if (!Core.IsMember)
            return;

        int i = 1;
        while (FactionRank($"{faction}") < 10)
        {
            SwagTokenA(1);
            Core.ChainComplete((int)faction);
            Core.Logger($"Completed x{i++}");
        }
    }

    public int FactionRank(string faction) => Bot.Player.GetFactionRank(faction);

    #endregion
}

public enum BoostIDs
{
    DailyXP60 = 19189,
    XP20 = 22448,
    XP60 = 27552,
    DoomClass60 = 19761,
    Class20 = 22447,
    Class60 = 27555,
    DoomREP60 = 19762,
    REP20 = 22449,
    REP60 = 27553,
    DoomGold60 = 19763,
    Gold20 = 22450,
    Gold60 = 27554
}

public enum AlchemyRunes
{
    Dragon,
    Jera,
    Uruz,
    Fehu,
    Gebo
}

public enum MemberShipsIDS
{
    Dwarfhold = 1317,
    Good = 1318,
    Evil = 1319,
    Yokai = 1320,
    Vampire = 1321,
    Lycan = 1322,
    Mythsong = 1323,
    Arcangrove = 1324,
    Sandsea = 1325,
    Skygaurd = 1326,
    Doomwood = 1327,
    Troll = 1328,
    Horc = 1329,
    Etherstorm = 4340,
    ChronoSpan = 4341,
    Thunderforge = 4342,
    Swordhaven = 4343,
    Chaos = 4344,
    Northpointe = 4345,
    Embersea = 4346,
    Ravenloss = 4347

}
