//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class CoreFarms
{
    // [Can Change] Can you solo the boss without killing the ads
    public bool canSoloInPvP { get; set; } = true;

    // [Can Change] Use boosts on Gold farming
    public bool doGoldBoost { get; set; } = false;
    // [Can Change] Use boosts on Class farming
    public bool doClassBoost { get; set; } = false;
    // [Can Change] Use boosts on Reputation farming
    public bool doRepBoost { get; set; } = false;
    // [Can Change] Use boosts on Experience farming
    public bool doExpBoost { get; set; } = false;

    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void ToggleBoost(BoostType type, bool enabled = true)
    {
        if (enabled)
        {
            if (Core.CBOBool("doGoldBoost", out bool _doGoldBoost))
                doGoldBoost = _doGoldBoost;
            if (Core.CBOBool("doClassBoost", out bool _doClassBoost))
                doClassBoost = _doClassBoost;
            if (Core.CBOBool("doRepBoost", out bool _doRepBoost))
                doRepBoost = _doRepBoost;
            if (Core.CBOBool("doExpBoost", out bool _doExpBoost))
                doExpBoost = _doExpBoost;

            switch (type)
            {
                case BoostType.Gold:
                    if (!doGoldBoost || Bot.Boosts.UseGoldBoost)
                        return;
                    Bot.Boosts.SetGoldBoostID();
                    Bot.Boosts.UseGoldBoost = true;
                    break;

                case BoostType.Class:
                    if (!doClassBoost || Bot.Boosts.UseClassBoost)
                        return;
                    Bot.Boosts.SetClassBoostID();
                    Bot.Boosts.UseClassBoost = true;
                    break;

                case BoostType.Reputation:
                    if (!doRepBoost || Bot.Boosts.UseReputationBoost)
                        return;
                    Bot.Boosts.SetReputationBoostID();
                    Bot.Boosts.UseReputationBoost = true;
                    break;

                case BoostType.Experience:
                    if (!doExpBoost || Bot.Boosts.UseExperienceBoost)
                        return;
                    Bot.Boosts.SetExperienceBoostID();
                    Bot.Boosts.UseExperienceBoost = true;
                    break;
            }
            Bot.Boosts.Start();
        }
        else
        {
            switch (type)
            {
                case BoostType.Gold:
                    if (!Bot.Boosts.UseGoldBoost)
                        return;
                    Bot.Boosts.UseGoldBoost = false;
                    break;

                case BoostType.Class:
                    if (!Bot.Boosts.UseClassBoost)
                        return;
                    Bot.Boosts.UseClassBoost = false;
                    break;

                case BoostType.Reputation:
                    if (!Bot.Boosts.UseReputationBoost)
                        return;
                    Bot.Boosts.UseReputationBoost = false;
                    break;

                case BoostType.Experience:
                    if (!Bot.Boosts.UseExperienceBoost)
                        return;
                    Bot.Boosts.UseExperienceBoost = false;
                    break;
            }
            if (new[] { Bot.Boosts.UseGoldBoost, Bot.Boosts.UseClassBoost, Bot.Boosts.UseReputationBoost, Bot.Boosts.UseExperienceBoost }.All(on => !on))
                Bot.Boosts.Stop();
        }
    }

    #region Gold
    public void Gold(int quant = 100000000)
    {
        if (Bot.Player.Gold >= quant)
            return;

        ToggleBoost(BoostType.Gold);

        HonorHall(quant);
        BattleGroundE(quant);
        BerserkerBunny(quant);

        ToggleBoost(BoostType.Gold, false);
    }

    /// <summary>
    /// Farms Gold in HonorHall (members) with quests HonorHall Mobs and 61-75
    /// </summary>
    /// <param name="goldQuant">How much gold to farm</param>
    public void HonorHall(int goldQuant = 100000000)
    {
        if (!Core.IsMember || Bot.Player.Level < 61 || Bot.Player.Gold >= goldQuant)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        Core.Logger($"Farming {goldQuant} gold using HonorHall Method");

        Core.RegisterQuests(3992, 3993);
        while (!Bot.ShouldExit && Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.KillMonster("honorhall", "r1", "Center", "*", "Battleground E Opponent Defeated", 10, log: false);
            Core.KillMonster("honorhall", "r1", "Center", "*", "HonorHall Opponent Defeated", 10, log: false);
        }
        Core.CancelRegisteredQuests();
        Core.SavedState(false);
    }

    /// <summary>
    /// Farms Gold in Battle Ground E with quests Level 46-60 and 61-75
    /// </summary>
    /// <param name="goldQuant">How much gold to farm</param>
    public void BattleGroundE(int goldQuant = 100000000)
    {
        if (Bot.Player.Gold >= goldQuant || Bot.Player.Level < 61)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        Core.Logger($"Farming {goldQuant} gold using BattleGroundE Method");

        Core.RegisterQuests(3991, 3992);
        while (!Bot.ShouldExit && Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.KillMonster("battlegrounde", "r2", "Center", "*", "Battleground D Opponent Defeated", 10, log: false);
            Core.KillMonster("battlegrounde", "r2", "Center", "*", "Battleground E Opponent Defeated", 10, log: false);
        }
        Core.CancelRegisteredQuests();
        Core.SavedState(false);
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
        Core.SavedState();
        Core.Logger($"Farming {goldQuant}  using BerserkerBunny Method");

        Core.RegisterQuests(236);
        while (!Bot.ShouldExit && Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Were Egg", log: false);
            Bot.Drops.Pickup("Berserker Bunny");
            Bot.Sleep(Core.ActionDelay);
            Bot.Shops.SellItem("Berserker Bunny");
        }
        Core.CancelRegisteredQuests();
        Core.SavedState(false);
    }

    // <summary>
    // Farms Gold by Kill mobs in "darkwarlegion" for Badges and turning the quest in. (ignore the missign turning reqs.. its to quick)
    // </summary>
    // <param name="goldQuant">How much gold to farm</param>
    public void DarkWarLegion(int goldQuant = 100000000) //Slower then BattleGroundE
    {
        if (Bot.Player.Gold >= goldQuant)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        Core.Logger($"Farming {goldQuant}  using DarkWarLegion Method");

        Core.RegisterQuests(8584, 8585);
        while (!Bot.ShouldExit && Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
            Core.KillMonster("darkwarlegion", "r2", "Left", "*", "Nation Badge", 5, log: false);
        Core.CancelRegisteredQuests();
        Core.SavedState(false);
    }
    #endregion

    #region Experience
    public void Experience(int level = 100)
    {
        if (Bot.Player.Level >= level)
            return;

        ToggleBoost(BoostType.Experience);

        FireWarxp(level);
        IcestormArena(level);

        ToggleBoost(BoostType.Experience, false);
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
        Core.SavedState();

        //Between level 1 and 5
        while (NotYetLevel(5))
            Core.KillMonster("icestormarena", "r4", "Bottom", "*", log: false, publicRoom: true);

        //Between level 5 and 10
        while (NotYetLevel(10))
            Core.KillMonster("icestormarena", "r5", "Left", "*", log: false, publicRoom: true);

        //Between level 10 and 20
        while (NotYetLevel(20))
            Core.KillMonster("icestormarena", "r6", "Left", "*", log: false, publicRoom: true);

        //Between level 20 and 25
        if (NotYetLevel(25))
        {
            Core.RegisterQuests(6628);
            while (NotYetLevel(25))
            {
                Core.KillMonster("icestormarena", "r7", "Left", "*", "Icewing Grunt Defeated", 3, log: false, publicRoom: true);
            }
            Core.CancelRegisteredQuests();
        }

        //Between level 25 and 30
        while (NotYetLevel(30))
            Core.KillMonster("icestormarena", "r10", "Left", "*", log: false, publicRoom: true);

        //Between level 30 and 35
        if (NotYetLevel(35))
        {
            Core.RegisterQuests(6629);
            while (NotYetLevel(35))
            {
                Core.KillMonster("icestormarena", "r11", "Left", "*", "Icewing Warrior Defeated", 3, log: false, publicRoom: true);
            }
            Core.CancelRegisteredQuests();
        }

        //Between level 35 and 50
        while (NotYetLevel(50))
            Core.KillMonster("icestormarena", "r14", "Left", "*", log: false, publicRoom: true);

        //Between level 50 and 75
        while (NotYetLevel(75))
            Core.KillMonster("icestormarena", "r3b", "Top", "*", log: false, publicRoom: true);

        //Between level 75 and 100
        while (NotYetLevel(100))
            Core.KillMonster("icestormarena", "r3c", "Top", "*", log: false, publicRoom: true);

        Bot.Options.AggroMonsters = OptionRestore;
        Core.SavedState(false);

        bool NotYetLevel(int _level)
        {
            return !Bot.ShouldExit && (Bot.Player.Level < _level && Bot.Player.Level < level) || (Bot.Player.Level <= _level && rankUpClass && Bot.Player.CurrentClassRank != 10);
        }

    }

    /// <summary>
    /// Farms in Seven Circles War for level and items
    /// </summary>
    /// <param name="level">Desired level</param>
    public void SevenCirclesWar(int level = 100, int gold = 100000000)
    {
        if (Bot.Player.Level >= level && Bot.Player.Gold >= gold)
            return;

        if (!Bot.Quests.IsUnlocked(7979))
        {
            Core.Logger("Please use Scripts/Story/Legion/SevenCircles(War).cs in order to use the SevenCircles method");
            return;
        }

        Core.AddDrop("Essence of Wrath", "Souls of Heresy");
        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        Core.Logger($"Farming {gold} gold using SCW Method");

        Core.RegisterQuests(7979, 7980, 7981);
        while (!Bot.ShouldExit && Bot.Player.Level < level || Bot.Player.Gold < gold)
        {
            Core.KillMonster("sevencircleswar", "Enter", "Right", "*", "Wrath Guards Defeated", 12);
            Core.KillMonster("sevencircleswar", "Enter", "Right", "*", "War Medal", 5);
            Core.KillMonster("sevencircleswar", "Enter", "Right", "*", "Mega War Medal", 3);
        }
        Core.CancelRegisteredQuests();
        Core.SavedState(false);
    }

    /// <summary>
    /// Farms level in FireWar Turnins
    /// </summary>
    /// <param name="level">Desired level</param>
    public void FireWarxp(int level = 70)
    {
        if (Bot.Player.Level >= 70)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();

        Core.RegisterQuests(6294, 6295);
        Bot.Options.AggroMonsters = true;
        while (!Bot.ShouldExit && Bot.Player.Level < level)
            Core.KillMonster("Firewar", "r2", "Right", "*", log: false);

        Bot.Options.AggroMonsters = false;
        Core.CancelRegisteredQuests();
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
        Bot.Drops.Pickup("Black Knight Orb");
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
        while (!Bot.ShouldExit && !Core.CheckInventory("The Secret 4"))
        {
            Core.Join("bludrutbrawl", "Enter0", "Spawn", ignoreCheck: true);

            Core.PvPMove(5, "Morale0C");
            Core.PvPMove(4, "Morale0B");
            Core.PvPMove(7, "Morale0A");
            Core.PvPMove(9, "Crosslower");
            Core.PvPMove(14, "Crossupper", 528, 255);
            Core.PvPMove(18, "Resource1A");

            Bot.Kill.Monster("(B) Defensive Restorer");
            Bot.Drops.Pickup("The Secret 4");
            Bot.Kill.Monster("(B) Defensive Restorer");
            Bot.Drops.Pickup("The Secret 4");

            if (Core.CheckInventory("The Secret 4"))
                break;

            Core.PvPMove(20, "Resource1B");

            Bot.Kill.Monster("(B) Defensive Restorer");
            Bot.Drops.Pickup("The Secret 4");
            Bot.Kill.Monster("(B) Defensive Restorer");
            Bot.Drops.Pickup("The Secret 4");
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

        if (Core.CBOBool("PVP_SoloPvPBoss", out bool _canSoloBoss))
            canSoloBoss = _canSoloBoss;

        Core.AddDrop(item);

        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {quant} {item}. SoloBoss = {canSoloBoss}");

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.AddDrop(item);
            Core.Join("bludrutbrawl", "Enter0", "Spawn");

            Core.PvPMove(5, "Morale0C");
            Core.PvPMove(4, "Morale0B");
            Core.PvPMove(7, "Morale0A");
            Core.PvPMove(9, "Crosslower");

            if (!canSoloBoss)
            {
                Core.PvPMove(14, "Crossupper", 528, 255);
                Core.PvPMove(18, "Resource1A");

                Bot.Kill.Monster("(B) Defensive Restorer");
                Bot.Kill.Monster("(B) Defensive Restorer");

                Core.PvPMove(20, "Resource1B");

                Bot.Kill.Monster("(B) Defensive Restorer");
                Bot.Kill.Monster("(B) Defensive Restorer");

                Core.PvPMove(21, "Resource1A", 124);
                Core.PvPMove(19, "Crossupper", 124);
                Core.PvPMove(17, "Crosslower", 488, 483);
            }
            Core.PvPMove(15, "Morale1A");

            if (!canSoloBoss)
                Bot.Kill.Monster("(B) Brawler");
            Core.PvPMove(23, "Morale1B");
            if (!canSoloBoss)
                Bot.Kill.Monster("(B) Brawler");
            Core.PvPMove(25, "Morale1C");
            if (!canSoloBoss)
                Bot.Kill.Monster("(B) Brawler");
            Core.PvPMove(28, "Captain1", 528, 255);

            Bot.Kill.Monster("Team B Captain");
            Bot.Wait.ForDrop(item);

            while (!Bot.ShouldExit && Bot.Map.Name != "battleon")
            {
                Bot.Sleep(5000);
                Core.Join("battleon");
            }
        }
    }

    public void BattleUnderB(string item = "Bone Dust", int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Core.AddDrop(item);
        Core.EquipClass(ClassType.Farm);

        Bot.Options.AggroMonsters = true;
        Core.KillMonster("battleunderb", "Enter", "Spawn", "*", item, quant, false, publicRoom: true, log: false);
        Bot.Options.AggroMonsters = false;
        Bot.Combat.Exit();
    }

    #endregion

    #region Reputation
    public void GetAllRanks()
    {
        ToggleBoost(BoostType.Reputation);

        AegisREP();
        AlchemyREP();
        ArcangroveREP();
        BaconCatREP();
        if (Core.IsMember)
            BeastMasterREP();
        BlacksmithingREP();
        BladeofAweREP(farmBoA: false);
        BrightoakREP();
        ChaosMilitiaREP();
        ChaosREP();
        ChronoSpanREP();
        CraggleRockREP();
        DeathPitArenaREP();
        DeathPitBrawlREP();
        DiabolicalREP();
        DoomWoodREP();
        DreadfireREP();
        DreadrockREP();
        DruidGroveREP();
        DwarfholdREP();
        ElementalMasterREP();
        EmberseaREP();
        EternalREP();
        EtherStormREP();
        EvilREP();
        //FaerieCourtREP();
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
        if (Core.IsMember)
            SkyguardREP();
        SomniaREP();
        SpellCraftingREP();
        SwordhavenREP();
        ThunderForgeREP();
        TreasureHunterREP();
        TrollREP();
        VampireREP();
        YokaiREP();

        ToggleBoost(BoostType.Reputation, false);
    }

    public void AegisREP(int rank = 10)
    {
        if (FactionRank("Aegis") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(4900, 4910, 4914);
        while (!Bot.ShouldExit && FactionRank("Aegis") < rank)
        {
            Core.HuntMonster("skytower", "Seraphic Assassin", "Seraphic Assassin Dueled", 10, log: false);
            Core.HuntMonster("skytower", "Virtuous Warrior", "Warriors Dueled", 10, log: false);
            Core.HuntMonster("skytower", "Seraphic Assassin", "Assassins Handed To Them", 6, log: false);
            Core.HuntMonster("skytower", "Virtuous Warrior", "Warrior Butt Beaten", 6, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
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
    public void AlchemyPacket(string reagent1, string reagent2, AlchemyRunes rune = AlchemyRunes.Gebo, int rank = 0, bool loop = true, string modifier = "Moose", AlchemyTraits trait = AlchemyTraits.APw, bool P2w = false)
    {
        if (rank != 0 && FactionRank("Alchemy") < rank)
            AlchemyREP(rank);

        // Core.Join("Alchemy");
        InventoryItem reagentid1 = Bot.Inventory.GetItem(reagent1);
        InventoryItem reagentid2 = Bot.Inventory.GetItem(reagent2);

        void Packet()
        {
            if (!P2w)
                Bot.Sleep(3500);
            else Bot.Sleep(Core.ActionDelay);
            if (P2w && Core.CheckInventory("Dragon Runestone"))
                Bot.Send.Packet($"%xt%zm%crafting%1%getAlchWait%{reagentid1.ID}%{reagentid2.ID}%true%Ready to Mix%{reagent1}%{reagent2}%{rune}%{trait}%");
            else if (!P2w)
                Bot.Send.Packet($"%xt%zm%crafting%1%getAlchWait%{reagentid1.ID}%{reagentid2.ID}%false%Ready to Mix%{reagent1}%{reagent2}%{rune}%{modifier}%");
            if (P2w)
                Bot.Sleep(1500);
            else Bot.Sleep(11000);
            if (P2w && Core.CheckInventory("Dragon Runestone"))
                Bot.Send.Packet($"%xt%zm%crafting%1%checkAlchComplete%{reagentid1.ID}%{reagentid2.ID}%true%Mix Complete%{reagent1}%{reagent2}%{rune}%{trait}%");
            else if (!P2w)
                Bot.Send.Packet($"%xt%zm%crafting%1%checkAlchComplete%{reagentid1.ID}%{reagentid2.ID}%false%Mix Complete%{reagent1}%{reagent2}%{rune}%{modifier}%");
        }

        Core.Logger($"Reagents: [{reagent1}], [{reagent2}].");
        Core.Logger($"Rune: {rune}.");
        Core.Logger($"Modifier: {modifier}.");
        if (loop)
        {
            int i = 1;
            while (!Bot.ShouldExit && Core.CheckInventory(new[] { reagent1, reagent2 }))
            {
                if (P2w && !Core.CheckInventory("Dragon Runestone"))
                    break;
                Packet();
                Core.Logger($"Completed alchemy x{i++}");
            }
        }
        else Packet();
        //Times alchemy was fixed: TO FUCKING MANY I HATE ARTIX
    }


    /// <summary>
    /// Uses the Jera:hOu in the alchemy packet for starting rep.
    /// to find teh correct trait for a specific pot, goto /join alchemy with the correct reagents
    /// and open packet logger, enable it, start "help me", "use dragon stones"
    /// slect the level/type/kind(atk, int, spell, etc), and start it, grab the packet(copy it)
    /// make request with that and the Potion's name+itemid(from tools > grabber > inventory > grab)
    /// </summary>
    public enum AlchemyTraits
    {
        Dam = 11736, // Potent Honor Potion (trait, itemID)
        APw = 11741, // Potent Battle Elixir
        Luc = 11683, // Fate Tonic
        Int = 11635, // Sage Tonic
        SPw = 11745, // Potent Malevolence Elixir    
        hOu = 11761 // Healer Elixer / Potent Guard Potion / Unstable Healer Elixer // rep spam with jerra
        //more to be added by request
    };

    public void DragonRunestone(int quant = 1)
    {
        if (Core.CheckInventory("Dragon Runestone", quant))
            return;

        Core.FarmingLogger("Dragon Rune", quant);
        Gold(100000 * quant);
        Core.FarmingLogger("Gold Voucher 100k", quant);
        Core.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", quant);
        Core.BuyItem("alchemyacademy", 395, "Dragon Runestone", 1, 1, 8844);
    }

    public void AlchemyREP(int rank = 10, bool goldMethod = true)
    {
        if (FactionRank("Alchemy") >= rank)
            return;

        if (!Bot.Reputation.FactionList.Exists(f => f.Name == "Alchemy"))
        {
            Core.Logger("Getting Pre-Ranking XP");
            if (!Core.CheckInventory(new[] { "Ice Vapor", "Dragon Runestone" }) || !Core.CheckInventory(11475)) //there are 2 dagon scales.
            {
                if (!Core.CheckInventory("Dragon Runestone", 10))
                {
                    Gold(1000000);
                    Core.BuyItem("alchemyacademy", 395, "Gold Voucher 500k", 2);
                }
                Core.BuyItem("alchemyacademy", 395, 7132, 10, 10, 8845);
                Core.BuyItem("alchemyacademy", 397, 11475, 1, 2, 1232);
                Core.BuyItem("alchemyacademy", 397, 11478, 1, 2, 1235);

                AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Jera, loop: false, trait: CoreFarms.AlchemyTraits.hOu, P2w: true);
            }
        }

        Core.AddDrop("Dragon Scale", "Ice Vapor");
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank} Alchemy");

        int i = 1;
        while (!Bot.ShouldExit && FactionRank("Alchemy") < rank)
        {
            if (goldMethod)
            {
                if (!Core.CheckInventory(new[] { "Ice Vapor", "Dragon Runestone" }) || !Core.CheckInventory(11475)) //there are 2 dagon scales.
                {
                    if (!Core.CheckInventory("Dragon Runestone", 10))
                    {
                        Gold(1000000);
                        Core.BuyItem("alchemyacademy", 395, "Gold Voucher 500k", 2);
                    }
                    Core.BuyItem("alchemyacademy", 395, 7132, 10, 10, 8845);
                    Core.BuyItem("alchemyacademy", 397, 11475, 10, 2, 1232);
                    Core.BuyItem("alchemyacademy", 397, 11478, 10, 2, 1235);
                }

                if (FactionRank("Alchemy") < 3)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Jera, trait: CoreFarms.AlchemyTraits.hOu, P2w: true);

                else if (FactionRank("Alchemy") < 5)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Uruz, trait: CoreFarms.AlchemyTraits.hOu, P2w: true);

                else if (FactionRank("Alchemy") < 8)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Fehu, trait: CoreFarms.AlchemyTraits.hOu, P2w: true);

                else if (FactionRank("Alchemy") >= 8)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Gebo, trait: CoreFarms.AlchemyTraits.hOu, P2w: true);
            }
            else
            {
                Core.EquipClass(ClassType.Farm);
                Core.KillMonster("lair", "Enter", "Spawn", "*", "Dragon Scale", 10, false, log: false);
                Core.KillMonster("lair", "Enter", "Spawn", "*", "Ice Vapor", 10, false, log: false);

                if (FactionRank("Alchemy") < 3)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Jera, trait: CoreFarms.AlchemyTraits.hOu, P2w: false);

                if (FactionRank("Alchemy") < 5)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Uruz, trait: CoreFarms.AlchemyTraits.hOu, P2w: false);

                if (FactionRank("Alchemy") < 8)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Fehu, trait: CoreFarms.AlchemyTraits.hOu, P2w: false);

                if (FactionRank("Alchemy") >= 8)
                    AlchemyPacket("Dragon Scale", "Ice Vapor", AlchemyRunes.Gebo, trait: CoreFarms.AlchemyTraits.hOu, P2w: false);
            }
            Core.Logger($"Iteration {i++} completed");
        }
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void ArcangroveREP(int rank = 10)
    {
        if (FactionRank("Arcangrove") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(794, 795, 796, 797, 798, 799, 800, 801);
        while (!Bot.ShouldExit && FactionRank("Arcangrove") < rank)
        {
            Core.HuntMonster("arcangrove", "Seed Spitter", "Spool of Arcane Thread", 10, log: false);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Defeated Seed Spitter", 10, log: false);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Bundle of Thyme", 10, log: false);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Thistle", 5, log: false);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Pretzel Root", 4, log: false);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Lore-Strip Gorillaphant Steak", 8, log: false);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Slain Gorillaphant", 7, log: false);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Gorillaphant Tusk", 6, log: false);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Batch of Mustard Seeds", 3, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void BaconCatREP(int rank = 10)
    {
        if (FactionRank("BaconCat") >= rank)
            return;

        if (!Bot.Quests.IsUnlocked(5120))
            Core.Logger($"Quest [5120] \"Ziri Is Also Tough\", has yet to be completed, please run \"Farm/REP/BaconCatREP.cs\"", stopBot: true, messageBox: true);

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5112, 5120);
        while (!Bot.ShouldExit && FactionRank("BaconCat") < rank)
        {
            Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Moglinberry Ice Cream", 5, log: false);
            Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Shark Teeth", 10, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
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
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");
        Bot.Quests.UpdateQuest(4614);

        Core.RegisterQuests(3757);
        while (!Bot.ShouldExit && FactionRank("BeastMaster") < rank)
        {
            Core.HuntMonster("pyramid", "Golden Scarab", "Gleaming Gems of Containment", 9, log: false);
            Core.HuntMonster("lair", "Golden Draconian", "Bright Binding of Submission", 8, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void BlacksmithingREP(int rank = 10, bool UseGold = false)
    {
        if (FactionRank("Blacksmithing") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation, false);
        Core.Logger($"Farming rank {rank}");

        if (!UseGold)
        {
            Core.Logger("Using Non-Gold Method");
            Core.Logger($"If you can't Solo SlugButter, Either use the Gold method or Run the AP Script (Found in: Good-ArchPaladin) as it can Solo the boss 👍");
        }

        if (UseGold)
        {
            Core.Logger("using Gold Method");
            // Core.RegisterQuests(8737);
            while (!Bot.ShouldExit && FactionRank("Blacksmithing") < rank)
            {
                Core.EnsureAccept(8737);
                Gold(500000);
                Core.BuyItem("alchemyacademy", 2036, "Gold Voucher 500k");
                Bot.Sleep(Core.ActionDelay);
                Core.EnsureComplete(8737);
            }
            Core.CancelRegisteredQuests();
        }

        Core.RegisterQuests(2777);
        while (!Bot.ShouldExit && FactionRank("Blacksmithing") < 4 && !UseGold)
        {
            Core.HuntMonster("greenguardeast", "Wolf", "Furry Lost Sock", 2, log: false);
            Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5, log: false);
        }
        Core.CancelRegisteredQuests();

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(8736);
        Bot.Quests.UpdateQuest(3484);
        while (!Bot.ShouldExit && FactionRank("Blacksmithing") < rank && !UseGold)
        {
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Monster Trophy", 15, isTemp: false, log: false);
            Core.HuntMonster("hydrachallenge", "Hydra Head 25", "Hydra Scale Piece", 75, isTemp: false, log: false);
            Core.HuntMonster("maul", "Creature Creation", "Creature Shard", isTemp: false, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation);
        Core.SavedState(false);
    }

    public void BladeofAweREP(int rank = 10, bool farmBoA = true)
    {
        if (farmBoA && !Core.CheckInventory(17585))
        {
            UnlockBoA();
            RepFarm(6);
            Core.BuyItem("museum", 631, 17585);
        }
        RepFarm(rank);

        void RepFarm(int rank)
        {
            if (FactionRank("Blade of Awe") >= rank)
                return;

            UnlockBoA();

            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            Core.EquipClass(ClassType.Farm);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(2935);
            while (!Bot.ShouldExit && FactionRank("Blade of Awe") < rank)
                Core.HuntMonster("castleundead", "Skeletal Viking", "Hilt Found!", 1, false, log: false);
            Core.CancelRegisteredQuests();
            Core.SavedState(false);
        }

        void UnlockBoA()
        {
            if (Core.isCompletedBefore(2937))
                return;

            if (!Core.isCompletedBefore(2933))
            {
                Core.Logger($"Doing Quest: [2933] - \"Find the Stonewrit!\"");
                Core.EquipClass(ClassType.Solo);
                Core.EnsureAccept(2933);
                Core.HuntMonster("j6", "Sketchy Dragon", "Stonewrit Found!", 1, false, log: false);
                Core.EnsureComplete(2933);
                Core.Logger($"Completed Quest: [2933] - \"Find the Stonewrit!\"");
            }
            else Core.Logger($"Already Completed: [2933] - \"Find the Stonewrit!\"");

            if (!Core.isCompletedBefore(2934))
            {
                Core.Logger($"Doing Quest: [2934] - \"Find the Handle!\"");
                Core.EquipClass(ClassType.Solo);
                Core.EnsureAccept(2934);
                Core.HuntMonster("gilead", "Fire Elemental", "Handle Found!", 1, false, log: false);
                Core.EnsureComplete(2934);
                Core.Logger($"Completed Quest: [2934] - \"Find the Handle!\"");
            }
            else Core.Logger($"Already Completed: [2934] - \"Find the Handle!\"");

            if (!Core.isCompletedBefore(2935))
            {
                Core.Logger($"Doing Quest: [2935] - \"Find the Hilt!\"");
                Core.EquipClass(ClassType.Farm);
                Core.EnsureAccept(2935);
                Core.HuntMonster("castleundead", "Skeletal Viking", "Hilt Found!", 1, false, log: false);
                Core.EnsureComplete(2935);
                Core.Logger($"Completed Quest: [2935] - \"Find the Hilt!\"");
            }
            else Core.Logger($"Already Completed: [2935] - \"Find the Hilt!\"");

            if (!Core.isCompletedBefore(2936))
            {
                Core.Logger($"Doing Quest: [2936] - \"Find the Blade!\"");
                Core.EquipClass(ClassType.Farm);
                Core.EnsureAccept(2936);
                Core.HuntMonster("hydra", "Hydra Head", "Blade Found!", 1, false, log: false);
                Core.EnsureComplete(2936);
                Core.Logger($"Completed Quest: [2936] - \"Find the Blade!\"");
            }
            else Core.Logger($"Already Completed: [2936] - \"Find the Blade!\"");

            if (!Core.isCompletedBefore(2937))
            {
                Core.Logger($"Doing Quest: [2937] - \"Find the Runes!\"");
                Core.EquipClass(ClassType.Solo);
                Core.EnsureAccept(2937);
                Core.KillEscherion("Runes Found!", publicRoom: true, log: false);
                Core.EnsureComplete(2937);
                Core.Logger($"Completed Quest: [2937] - \"Find the Runes!\"");
            }
            else Core.Logger($"Already Completed: [2937] - \"Find the Runes!\"");
        }
    }

    public void BrightoakREP(int rank = 10)
    {
        if (FactionRank("Brightoak") >= rank)
            return;

        if (!Bot.Quests.IsAvailable(4667))
        {
            Core.Logger("Quest not avaible for farm, do Brightoak saga till Elfhame [Unlocking the Guardian's Mouth]");
            return;
        }

        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(4667);
        while (!Bot.ShouldExit && FactionRank("Brightoak") < rank)
            Core.GetMapItem(3984, map: "elfhame");
        Bot.Wait.ForQuestComplete(4667);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void ChaosMilitiaREP(int rank = 10)
    {
        if (FactionRank("Chaos Militia") >= rank)
            return;

        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5775);
        while (!Bot.ShouldExit && FactionRank("Chaos Militia") < rank)
            Core.HuntMonster("citadel", "Inquisitor Guard", "Inquisitor's Tabard", 10, log: false);
        Bot.Wait.ForQuestComplete(5775);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void ChaosREP(int rank = 10)
    {
        if (FactionRank("Chaos") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Chaos, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(3594);
            while (!Bot.ShouldExit && FactionRank("Chaos") < rank)
                Core.KillMonster("mountdoomskull", "b1", "Left", "*", "Chaos Power Increased", 6, log: false);
            Bot.Wait.ForQuestComplete(3594);
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void ChronoSpanREP(int rank = 10)
    {
        if (FactionRank("ChronoSpan") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.ChronoSpan, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(2204);
            while (!Bot.ShouldExit && FactionRank("ChronoSpan") < rank)
            {
                Core.KillMonster("thespan", "r6", "Left", "Moglin Ghost", "Tin of Ghost Dust", 2, log: false);
                Core.KillMonster("thespan", "r4", "Left", "Minx Fairy", "8 oz Fairy Glitter", 3, log: false);
                Core.KillMonster("thespan", "r4", "Left", "Tog", "Tog Fang", 4, log: false);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void CraggleRockREP(int rank = 10)
    {
        if (FactionRank("CraggleRock") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");
        Core.AddDrop("Empowered Voidstone");
        Core.RegisterQuests(7277);
        while (!Bot.ShouldExit && FactionRank("CraggleRock") < rank)
            Core.KillMonster("wanders", "r3", "Down", "Kalestri Worshiper", "Star of the Sandsea", log: false);
        Bot.Wait.ForQuestComplete(7277);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
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
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5153);
        while (!Bot.ShouldExit && FactionRank("Death Pit Arena") < rank)
            Core.HuntMonster("deathpit", "General Hun'Gar", "General Hun'Gar Defeated", log: false);
        Bot.Wait.ForQuestComplete(5153);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void DiabolicalREP(int rank = 10)
    {
        if (FactionRank("Diabolical") >= rank)
            return;

        Bot.Quests.UpdateQuest(3428);
        if (!Bot.Quests.IsUnlocked(7877))
        {
            Core.EnsureAccept(7875, 7876);
            Core.HuntMonster("timevoid", "Unending Avatar", "Everlasting Scale", log: false);
            Core.EnsureComplete(7875);
            Core.HuntMonster($"twilightedge", "ChaosWeaver Warrior", "Chaotic Arachnid’s Flesh", log: false);
            Core.EnsureComplete(7876);
        }

        Core.EquipClass(ClassType.Solo);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(7877);
        while (!Bot.ShouldExit && FactionRank("Diabolical") < rank)
            Core.HuntMonster("mudluk", "Tiger Leech", "Swamped Leech Tooth", log: false);
        Bot.Wait.ForQuestComplete(7877);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void DoomWoodREP(int rank = 10)
    {
        if (FactionRank("DoomWood") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.DoomWood, rank);
        else
        {
            Core.AddDrop("Dark Tower Sword");
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(1151, 1152, 1153);
            while (!Bot.ShouldExit && FactionRank("DoomWood") < rank)
            {
                Core.HuntMonster("shadowfallwar", "*", "To Do List of Doom", log: false);
                Core.HuntMonster("shadowfallwar", "*", "Skeleton Key", log: false);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void DreadfireREP(int rank = 10)
    {
        if (FactionRank("Dreadfire") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5697);
        while (!Bot.ShouldExit && FactionRank("Dreadfire") < rank)
            Core.KillMonster("dreadfire", "r13", "Bottom", "Arcane Crystal", "Perfect Crystal Orb", log: false);
        Bot.Wait.ForQuestComplete(5697);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void DreadrockREP(int rank = 10)
    {
        if (FactionRank("Dreadrock") >= rank)
            return;

        Core.AddDrop("Ghastly Dreadrock Blade");
        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(4863, 4862, 4865, 4868);
        while (!Bot.ShouldExit && FactionRank("Dreadrock") < rank)
            Core.KillMonster("dreadrock", "r3", "Bottom", "*", "Goldfish Companion", log: false);
        Bot.Wait.ForQuestComplete(4868);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void DruidGroveREP(int rank = 10)
    {
        if (FactionRank("Druid Grove") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(3049);
        while (!Bot.ShouldExit && FactionRank("Druid Grove") < rank)
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Geode", 5, log: false);
        Bot.Wait.ForQuestComplete(3049);
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void DwarfholdREP(int rank = 10)
    {
        if (FactionRank("Dwarfhold") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Dwarfhold, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(320, 321);
            while (!Bot.ShouldExit && FactionRank("Dwarfhold") < rank)
            {
                Core.KillMonster("pines", "Enter", "Right", "Pine Grizzly", "Bear Skin", 5, log: false);
                Core.KillMonster("pines", "Enter", "Right", "Red Shell Turtle", "Red Turtle Shell", 5, log: false);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void ElementalMasterREP(int rank = 10)
    {
        if (FactionRank("Elemental Master") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(3050, 3298);
        while (!Bot.ShouldExit && FactionRank("Elemental Master") < rank)
        {
            Core.HuntMonster("gilead", "Water Elemental", "Water Core", log: false);
            Core.HuntMonster("gilead", "Fire Elemental", "Fire Core", log: false);
            Core.HuntMonster("gilead", "Wind Elemental", "Air Core", log: false);
            Core.HuntMonster("gilead", "Earth Elemental", "Earth Core", log: false);
            Core.HuntMonster("gilead", "Mana Elemental", "Mana Core", log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void EmberseaREP(int rank = 10)
    {
        if (FactionRank("Embersea") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Embersea, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(4228);
            while (!Bot.ShouldExit && FactionRank("Embersea") < rank)
                Core.HuntMonster("fireforge", "Blazebinder", "Defeated Blazebinder", 5, log: false);
            Bot.Wait.ForQuestComplete(4228);
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
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
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5198);
        while (!Bot.ShouldExit && FactionRank("Eternal") < rank)
        {
            Core.KillMonster("fourdpyramid", "r11", "Right", 2908, "White Gem", 2, log: false);
            Core.KillMonster("fourdpyramid", "r11", "Right", 2909, "Black Gem", 2, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void EtherStormREP(int rank = 10)
    {
        if (FactionRank("Etherstorm") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Etherstorm, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(1721);
            while (!Bot.ShouldExit && FactionRank("Etherstorm") < rank)
            {
                Core.HuntMonster("etherwardes", "Water Dragon Warrior", "Water Dragon Tears", 3, log: false);
                Core.HuntMonster("etherwardes", "Fire Dragon Warrior", "Fire Dragon Flames", 3, log: false);
                Core.HuntMonster("etherwardes", "Air Dragon Warrior", "Air Dragon Breaths", 3, log: false);
                Core.HuntMonster("etherwardes", "Earth Dragon Warrior", "Earth Dragon Claws", 3, log: false);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void EvilREP(int rank = 10)
    {
        if (FactionRank("Evil") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Evil, rank);
        else
        {
            Core.ChangeAlignment(Alignment.Evil);
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(364);
            while (!Bot.ShouldExit && FactionRank("Evil") < 4)
                Core.HuntMonster("swordhavenbridge", "Slime", "Youthanize", log: false);

            Core.CancelRegisteredQuests();
            Core.RegisterQuests(Core.IsMember ? 366 : 367);

            while (!Bot.ShouldExit && FactionRank("Evil") < rank)
            {
                if (!Core.IsMember)
                {
                    Core.HuntMonster("castleundead", "*", "Replacement Tibia", 6, log: false);
                    Core.HuntMonster("castleundead", "*", "Phalanges", 3, log: false);
                }
                else
                {
                    Core.HuntMonster("sleuthhound", "Chair", "Chair", 4, log: false);
                    Core.HuntMonster("sleuthhound", "Table", "Table", 2, log: false);
                    Core.HuntMonster("sleuthhound", "Bookcase", "Bookcase", log: false);
                }
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void FishingREP(int rank = 10, bool shouldDerp = false)
    {
        if (FactionRank("Fishing") >= rank)
        {
            Core.TrashCan("Fishing Bait", "Fishing Dynamite");
            return;
        }

        Core.AddDrop("Fishing Bait", "Fishing Dynamite");
        Core.EquipClass(ClassType.Farm);
        ToggleBoost(BoostType.Reputation);
        Core.SavedState();
        Core.Logger($"Farming rank {rank}");
        int z = 1;

        Core.Logger("Pre-Ranking XP(This is Required)");
        Core.EnsureAccept(1682);
        Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);
        Core.EnsureComplete(1682);
        Core.Join("fishing");

        while (!Bot.ShouldExit && FactionRank("Fishing") < (rank > 2 ? 2 : rank) && (shouldDerp ? !Core.HasAchievement(14) : true))
        {
            GetBaitandDynamite(20, 0);
            Core.Logger($"Fishing With: Fishing Bait");
            Core.Logger($"0 Xp means a Failed Catch, common at lower Fishing (non)Faction ranks");

            while (!Bot.ShouldExit && Core.CheckInventory("Fishing Bait"))
            {
                Bot.Send.Packet("%xt%zm%FishCast%1%Net%30%");
                Bot.Sleep(10000);
                Core.Logger($"Fished {z++} Times");
            }
        }

        while (!Bot.ShouldExit && FactionRank("Fishing") < rank && (shouldDerp ? !Core.HasAchievement(14) : true))
        {
            GetBaitandDynamite(0, 20);
            Core.Logger($"Fishing With: Dynamite");

            while (!Bot.ShouldExit && Core.CheckInventory("Fishing Dynamite") && FactionRank("Fishing") < rank && (shouldDerp ? !Core.HasAchievement(14) : true))
            {
                Bot.Send.Packet($"%xt%zm%FishCast%1%Dynamite%30%");
                Bot.Sleep(3500);
                Core.SendPackets("%xt%zm%getFish%1%false%");
                Core.Logger($"Fished {z++} Times");
            }
        }
        Core.TrashCan(new[] { "Fishing Bait", "Fishing Dynamite" });
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);

        void GetBaitandDynamite(int FishingBaitQuant, int FishingDynamiteQuant)
        {
            if (Core.CheckInventory("Fishing Bait", FishingBaitQuant) && Core.CheckInventory("Fishing Dynamite", FishingDynamiteQuant))
                return;

            Core.RegisterQuests(1682);

            Core.FarmingLogger("Fishing Bait", FishingBaitQuant);
            while (!Bot.ShouldExit && FishingBaitQuant != 0 && !Core.CheckInventory("Fishing Bait", FishingBaitQuant))
                Core.KillMonster("greenguardwest", "West3", "Right", "Frogzard", "Fishing Bait", FishingBaitQuant, isTemp: false, log: false);

            Core.FarmingLogger("Fishing Dynamite", FishingDynamiteQuant);
            while (!Bot.ShouldExit && FishingDynamiteQuant != 0 && !Core.CheckInventory("Fishing Dynamite", FishingDynamiteQuant))
                Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);

            Core.CancelRegisteredQuests();
            Core.Logger("Returing to Fishing Map");
            Core.Join("fishing");
        }
    }

    public void DeathPitBrawlREP(int rank = 10)
    {
        if (FactionRank("Death Pit Brawl") >= rank)
            return;
        if (Core.isCompletedBefore(5165))
            return;

        Core.EquipClass(ClassType.Solo);
        ToggleBoost(BoostType.Reputation);
        Core.SavedState();
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5155, 5156, 5157, 5165);
        while (!Bot.ShouldExit && FactionRank("Death Pit Brawl") < rank)
            DeathPitToken();
        Core.CancelRegisteredQuests();

        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);


        void DeathPitToken(string item = "Death Pit Token", int quant = 30, bool temp = false)
        {
            if (Core.CheckInventory(item, quant))
                return;

            Core.EquipClass(ClassType.Solo);
            Core.FarmingLogger(item, quant);

            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                Core.AddDrop(item);
                Core.Join("DeathPitbrawl", "Enter0", "Spawn");

                Core.PvPMove(5, "Morale0C", 228, 291);
                Core.PvPMove(4, "Morale0B", 936, 397);
                Core.PvPMove(7, "Morale0A", 946, 394);
                Core.PvPMove(9, "Crosslower", 948, 400);
                Core.PvPMove(14, "Crossupper", 903, 324);
                Core.PvPMove(18, "Resource1A", 482, 295);
                Bot.Kill.Monster("Velm's Restorer");
                Bot.Kill.Monster("Velm's Restorer");
                Core.PvPMove(20, "Resource1B", 938, 400);
                Bot.Kill.Monster("Velm's Restorer");
                Bot.Kill.Monster("Velm's Restorer");
                Core.PvPMove(21, "Resource1A", 9, 435);
                Core.PvPMove(19, "Crossupper", 461, 315);
                Core.PvPMove(17, "Crosslower", 54, 339);
                Core.PvPMove(15, "Morale1A", 522, 286);
                Bot.Kill.Monster("Velm's Brawler");
                Core.PvPMove(23, "Morale1B", 948, 403);
                Bot.Kill.Monster("Velm's Brawler");
                Core.PvPMove(25, "Morale1C", 945, 397);
                Bot.Kill.Monster("Velm's Brawler");
                Core.PvPMove(28, "Captain1", 943, 404);
                Bot.Kill.Monster("General Velm (B)");
                Bot.Wait.ForDrop(item);
                Bot.Sleep(Core.ActionDelay);
                Bot.Send.Packet($"%xt%zm%house%1%{Bot.Player.Username}%");
            }
        }
    }

    public void FaerieCourtREP(int rank = 10) // Seasonal
    {
        if (FactionRank("Faerie Court") >= rank)
            return;

        Core.JumpWait();
        Bot.Map.Join("rainbow");
        if (Bot.Map.Name != "rainbow")
        {
            Core.Logger("Can't level FaerieCourt, as it's seasonal");
            return;
        }

        Core.Logger($"Farming rank {rank}");
        ToggleBoost(BoostType.Reputation);
        Core.SavedState();

        Core.RegisterQuests(6775, 6779);
        while (!Bot.ShouldExit && FactionRank("Faerie Court") < rank)
        {
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
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void GlaceraREP(int rank = 10)
    {
        if (FactionRank("Glacera") >= rank)
            return;

        if (!Core.isCompletedBefore(5601))
            Core.Logger("Farming Quests are not unlocked, Please run: \"Story/Glacera.cs\"", stopBot: true);

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5597, 5598, 5599, 5600);
        Bot.Events.CellChanged += CutSceneFixer;

        while (!Bot.ShouldExit && FactionRank("Glacera") < rank)
        {
            Core.KillMonster("icewindwar", "r2", "Left", "*", "World Ender Medal", 10, log: false);
            Bot.Wait.ForQuestComplete(5597);
            Bot.Events.CellChanged -= CutSceneFixer;
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);

        void CutSceneFixer(string map, string cell, string pad)
        {
            if (map == "icewindwar" && cell != "r2")
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != "r2")
                {
                    Bot.Sleep(2500);
                    Core.Jump("r2", "Left");
                    Bot.Sleep(2500);
                }
            }
            //if more maps get stuck, just fillin the bit below.
            if (map == "Map" && cell != "Cell")
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != "InsertCell")
                {
                    Bot.Sleep(2500);
                    Core.Jump("Cell", "pad");
                    Bot.Sleep(2500);
                }
            }
        }
    }

    public void GoodREP(int rank = 10)
    {
        if (FactionRank("Good") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Good, rank);
        else
        {
            Core.ChangeAlignment(Alignment.Good);
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(369);
            while (!Bot.ShouldExit && FactionRank("Good") < 4)
            {
                Core.KillMonster("swordhavenbridge", "Bridge", "Left", "Slime", "Slime in a Jar", 6, log: false);
                Bot.Wait.ForQuestComplete(369);
            }

            Core.CancelRegisteredQuests();
            Core.RegisterQuests(Core.IsMember ? 371 : 372);
            while (!Bot.ShouldExit && FactionRank("Good") < rank)
            {
                if (!Core.IsMember)
                {
                    Core.KillMonster("castleundead", "Enter", "Spawn", "*", "Chaorrupted Skull", 5, log: false);
                    Bot.Wait.ForQuestComplete(372);
                }
                else
                {
                    Core.HuntMonster("sewer", "Grumble", "Grumble's Fang", log: false);
                    Bot.Wait.ForQuestComplete(371);
                }
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void HollowbornREP(int rank = 10)
    {
        if (FactionRank("Hollowborn") >= rank)
            return;

        Core.AddDrop("Hollow Soul");
        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(7553, 7555);
        while (!Bot.ShouldExit && FactionRank("Hollowborn") < rank)
        {
            Core.KillMonster("shadowrealm", "r2", "Down", "*", "Darkseed", 8, log: false);
            Core.KillMonster("shadowrealm", "r2", "Down", "*", "Shadow Medallion", 5, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void HorcREP(int rank = 10)
    {
        if (FactionRank("Horc") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Horc, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(1265);
            while (!Bot.ShouldExit && FactionRank("Horc") < rank)
            {
                Core.HuntMonster("bloodtuskwar", "Chaotic Lemurphant", "Chaorrupted Eye", 3, log: false);
                Core.HuntMonster("bloodtuskwar", "Chaotic Horcboar", "Chaorrupted Tentacle", 5, log: false);
                Core.HuntMonster("bloodtuskwar", "Chaotic Chinchilizard", "Chaorrupted Tusk", 5, log: false);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void LoremasterREP(int rank = 10)
    {
        if (FactionRank("Loremaster") >= rank)
            return;

        Experience(15);
        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        while (!Bot.ShouldExit && FactionRank("Loremaster") < rank)
        {
            if (!Core.IsMember ? FactionRank("Loremaster") < 10 : FactionRank("Loremaster") < rank)
            {
                Experience(15);
                Core.EquipClass(ClassType.Farm);
                Core.RegisterQuests(7505);
                while (!Bot.ShouldExit && Core.IsMember ? FactionRank("Loremaster") < 10 : FactionRank("Loremaster") < rank)
                {
                    Core.HuntMonster("uppercity", "Drow Assassin", "Poisoned Dagger", 4, log: false);
                    Core.HuntMonster("wardwarf", "D'wain Jonsen", "Scroll: Opportunity's Strike", log: false);
                }
                Core.CancelRegisteredQuests();
            }
            else if (Core.IsMember ? FactionRank("Loremaster") < 3 : FactionRank("Loremaster") < rank)
            {
                Core.RegisterQuests(7505);
                Core.EquipClass(ClassType.Farm);
                while (!Bot.ShouldExit && Core.IsMember ? FactionRank("Loremaster") < 3 : FactionRank("Loremaster") < rank)
                {
                    Core.HuntMonster("wardwarf", "Drow Assassin", "Poisoned Dagger", 4, log: false);
                    Core.HuntMonster("wardwarf", "D'wain Jonsen", "Scroll: Opportunity's Strike", 1, log: false);
                }
                Core.CancelRegisteredQuests();
            }
            else if (Core.IsMember && FactionRank("Loremaster") >= 3)
            {
                if (!Bot.Quests.IsUnlocked(3032))
                {
                    // Rosetta Stones
                    Core.EnsureAccept(3029);
                    Core.HuntMonster("druids", "Void Bear", "Voidstone ", 6);
                    Core.EnsureComplete(3029);

                    // Cull the Foot Soldiers
                    Core.EnsureAccept(3030);
                    Core.HuntMonster("druids", "Void Larva", "Void Larvae Death Cry", 4);
                    Core.EnsureComplete(3030);

                    // Bad Vibes
                    Core.EnsureAccept(3031);
                    Core.HuntMonster("druids", "Void Ghast", "Ghast's Death Cry", 4);
                    Core.EnsureComplete(3031);
                }
                // Quite the Problem
                Core.RegisterQuests(3032);
                Core.EquipClass(ClassType.Solo);
                while (!Bot.ShouldExit && FactionRank("Loremaster") < rank)
                {
                    Core.HuntMonster("druids", "Young Void Giant", "Void Giant Death Knell", log: false);
                }
                Core.CancelRegisteredQuests();
            }
        }
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void LycanREP(int rank = 10)
    {
        if (FactionRank("Lycan") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Lycan, rank);
        else
        {
            if (!Bot.Quests.IsAvailable(537))
            {
                Core.Logger("Can't do farming quest [Sanguine] (/lycan)", messageBox: true);
                return;
            }

            Core.EquipClass(ClassType.Solo);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(537);
            while (!Bot.ShouldExit && FactionRank("Lycan") < rank)
            {
                Core.HuntMonster("lycan", "Sanguine", "Sanguine Mask", log: false);
                Bot.Wait.ForQuestComplete(537);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void InfernalArmyREP(int rank = 10)
    {
        if (FactionRank("Infernal Army") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5707);
        while (!Bot.ShouldExit && FactionRank("Infernal Army") < rank)
        {
            Core.KillMonster("dreadfire", "r10", "Left", "Living Brimstone", "Living Brimstone Defeated", log: false);
            Bot.Wait.ForQuestComplete(5707);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void MonsterHunterREP(int rank = 10)
    {
        if (FactionRank("Monster Hunter") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5849, 5850);
        if (!Bot.Quests.IsAvailable(5850))
        {
            Core.KillMonster("pilgrimage", "r5", "Left", "SpiderWing", "Spiderwing Captured", 4, log: false);
            Core.KillMonster("pilgrimage", "r5", "Left", "Urstrix", "Urstrix Captured", 4, log: false);
            Core.Logger($"Completed Quest Capture the Misshapen");
        }

        while (!Bot.ShouldExit && FactionRank("Monster Hunter") < rank)
        {
            Core.KillMonster("pilgrimage", "r5", "Left", "SpiderWing", "Spiderwing Captured", 4, log: false);
            Core.KillMonster("pilgrimage", "r5", "Left", "Urstrix", "Urstrix Captured", 4, log: false);
            Core.KillMonster("pilgrimage", "r5", "Left", "Ravenous Parasite", "Ravenous Parasites Slain", 7, log: false);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void MysteriousDungeonREP(int rank = 10)
    {
        if (FactionRank("Mysterious Dungeon") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        if (!Bot.Quests.IsAvailable(5429))
        {
            Core.Join("cursedshop");
            Core.EnsureAccept(5428);
            Bot.Map.GetMapItem(4803);
            Bot.Sleep(2500);
            if (Bot.Quests.CanComplete(5428))
                Core.EnsureComplete(5428);
            Bot.Map.Jump("Enter", "Spawn");
        }

        Core.RegisterQuests(5429);
        while (!Bot.ShouldExit && FactionRank("Mysterious Dungeon") < rank)
        {
            Core.HuntMonster("cursedshop", "Antique Chair", "Antique Chair Defeated", log: false);
            Bot.Wait.ForQuestComplete(5429);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void MythsongREP(int rank = 10)
    {
        if (FactionRank("Mythsong") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Mythsong, rank);
        else
        {
            if (!Bot.Quests.IsUnlocked(4829))
            {
                Core.Logger("Can't do farming quest [Sugar, Sugar] (/beehive)", messageBox: true);
                return;
            }
            Core.EquipClass(ClassType.Solo);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(4829);
            while (!Bot.ShouldExit && FactionRank("Mythsong") < rank)
            {
                Core.HuntMonster("beehive", "Stinger", "Honey Gathered", 10, log: false);
                Bot.Wait.ForQuestComplete(4829);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void NecroCryptREP(int rank = 10)
    {
        if (FactionRank("Necro Crypt") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(3048);
        while (!Bot.ShouldExit && FactionRank("Necro Crypt") < rank)
        {
            Core.HuntMonster("castleundead", "Skeletal Viking", "Old Bone", 5, log: false);
            Bot.Wait.ForQuestComplete(3048);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void NorthpointeREP(int rank = 10)
    {
        if (FactionRank("Northpointe") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Northpointe, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            ToggleBoost(BoostType.Reputation);
            Core.SavedState();
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(4027);
            while (!Bot.ShouldExit && FactionRank("Northpointe") < rank)
                Core.HuntMonster("northpointe", "Grim Stalker", "Bunch of Sage", 10, log: false);
            Bot.Wait.ForQuestComplete(4027);
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void PetTamerREP(int rank = 10)
    {
        if (FactionRank("Pet Tamer") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(5261);
        while (!Bot.ShouldExit && FactionRank("Pet Tamer") < rank)
        {
            Core.KillMonster("greenguardwest", "West7", "Down", "Mogzard", "Mogzard Captured", log: false);
            Bot.Wait.ForQuestComplete(5261);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void RavenlossREP(int rank = 10)
    {
        if (FactionRank("Ravenloss") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Ravenloss, rank);
        else
        {
            if (!Bot.Quests.IsAvailable(3445))
            {
                Core.Logger("Quest Locked \"Slay the Spiderkin\" (/twilightedge)", messageBox: true);
                return;
            }
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(3445);
            while (!Bot.ShouldExit && FactionRank("Ravenloss") < rank)
            {
                Core.HuntMonster("twilightedge", "ChaosWeaver Mage", "ChaosWeaver Slain", 10);
                Bot.Wait.ForQuestComplete(5443);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void SandseaREP(int rank = 10)
    {
        if (FactionRank("Sandsea") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Sandsea, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(916, 917, 919, 921, 922);
            while (!Bot.ShouldExit && FactionRank("Sandsea") < rank)
            {
                Core.HuntMonster("sandsea", "Bupers Camel", "Bupers Camel Document", 10);
                Core.HuntMonster("sandsea", "Bupers Camel", "Barrel of Desert Water", 10);
                Core.HuntMonster("sandsea", "Bupers Camel", "Flexible Camel Spit", 7);
                Core.HuntMonster("sandsea", "Bupers Camel", "Oasis Jewelry Piece", 4);
                Core.HuntMonster("sandsea", "Bupers Camel", "Camel Skull", 2);
                Core.HuntMonster("sandsea", "Cactus Creeper", "Sandsea Cotton", 8);
                Core.HuntMonster("sandsea", "Cactus Creeper", "Cactus Creeper Head", 8);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
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

        MembershipDues(MemberShipsIDS.Skyguard, rank);

        //Core.RegisterQuests(1016);
        //while (!Bot.ShouldExit && FactionRank("Skyguard") < rank)
        //{
        //    Core.HuntMonster("gilead", "Water Elemental", "Bucket of Water", 5);
        //    Core.HuntMonster("gilead", "Wind Elemental", "Beaker of Air", 5);
        //}
        //Core.CancelRegisteredQuests();
    }

    public void SomniaREP(int rank = 10)
    {
        if (FactionRank("Somnia") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(7665, 7666, 7669);
        while (!Bot.ShouldExit && FactionRank("Somnia") < rank)
        {
            Core.HuntMonster("somnia", "Nightspore", "Dream Truffle", 8);
            Core.HuntMonster("somnia", "Orpheum Elemental", "Orphium Ore", 8);
            Core.HuntMonster("somnia", "Dream Larva", "Dreamsilk", 5);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void SpellCraftingREP(int rank = 10)
    {
        if (FactionRank("SpellCrafting") >= rank)
            return;

        Core.AddDrop("Mystic Quills", "Mystic Parchment");
        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        if (FactionRank("SpellCrafting") == 0)
        {
            Core.EnsureAccept(2260);
            Core.Join("dragonrune");
            Bot.Map.GetMapItem(1920);
            Core.HuntMonster("castleundead", "Skeletal Warrior", "Arcane Parchment", 13);
            Core.JumpWait();
            Bot.Quests.EnsureComplete(2260);
        }

        if (FactionRank("SpellCrafting") < 4)
        {
            Core.HuntMonster("mobius", "Slugfit", "Mystic Quills", 10, false);
            Core.BuyItem("dragonrune", 549, "Ember Ink", 50, 5);
            while (!Bot.ShouldExit && FactionRank("SpellCrafting") < (rank > 4 ? rank : 4))
            {
                Core.ChainComplete(2299);
            }
        }
        while (!Bot.ShouldExit && FactionRank("SpellCrafting") < rank)
        {
            Core.HuntMonster("underworld", "Skull Warrior", "Mystic Parchment", 10, false);
            Core.BuyItem("dragonrune", 549, "Hallow Ink", 50, 5);
            while (!Bot.ShouldExit && Core.CheckInventory("Hallow Ink") && FactionRank("SpellCrafting") < rank)
            {
                Core.ChainComplete(2322);
            }
        }
        Core.SellItem("Ember Ink", all: true);
        Core.SellItem("Hallow Ink", all: true);

        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void SwordhavenREP(int rank = 10)
    {
        if (FactionRank("Swordhaven") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Swordhaven, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(3065, 3066, 3067, 3070, 3085, 3086, 3087);
            while (!Bot.ShouldExit && FactionRank("Swordhaven") < rank)
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
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void ThunderForgeREP(int rank = 10)
    {
        if (FactionRank("ThunderForge") >= rank)
            return;
        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Thunderforge, rank);
        else
        {
            if (!Bot.Quests.IsAvailable(2733))
            {
                Core.Logger("Quest not avaible for farm, do ThunderForge saga till Deathpits [The Chaos Eye of Vestis]");
                return;
            }
            Core.EquipClass(ClassType.Solo);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(2733);
            while (!Bot.ShouldExit && FactionRank("ThunderForge") < rank)
            {
                Core.HuntMonster("deathpits", "Wrathful Vestis", "Vestis's Chaos Eye");
                Bot.Wait.ForQuestComplete(2733);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void TreasureHunterREP(int rank = 10)
    {
        if (FactionRank("TreasureHunter") >= rank)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        Core.Logger($"Farming rank {rank}");

        Core.RegisterQuests(6593);
        while (!Bot.ShouldExit && FactionRank("TreasureHunter") < rank)
        {
            Core.HuntMonster("stalagbite", "Balboa", "Super Specific Rock");
            Bot.Wait.ForQuestComplete(6593);
        }
        Core.CancelRegisteredQuests();
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public void TrollREP(int rank = 10)
    {
        if (FactionRank("Troll") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Troll, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(1263);
            while (!Bot.ShouldExit && FactionRank("Troll") < rank)
            {
                Core.HuntMonster("bloodtuskwar", "Chaotic Lemurphant", "Chaorrupted Eye", 3);
                Core.HuntMonster("bloodtuskwar", "Chaotic Horcboar", "Chaorrupted Tentacle", 5);
                Core.HuntMonster("bloodtuskwar", "Chaotic Chinchilizard", "Chaorrupted Tusk", 5);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void VampireREP(int rank = 10)
    {
        if (FactionRank("Vampire") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Vampire, rank);
        else
        {
            if (!Bot.Quests.IsUnlocked(522))
            {
                Core.Logger("Can't do farming quest [Twisted Paw] (/safiria)", messageBox: true);
                return;
            }
            Core.EquipClass(ClassType.Solo);
            Core.SavedState();
            ToggleBoost(BoostType.Reputation);
            Core.Logger($"Farming rank {rank}");

            Core.RegisterQuests(522);
            while (!Bot.ShouldExit && FactionRank("Vampire") < rank)
            {
                Core.HuntMonster("safiria", "Twisted Paw", "Twisted Paw's Head");
                Bot.Wait.ForQuestComplete(522);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void YokaiREP(int rank = 10)
    {
        if (FactionRank("Yokai") >= rank)
            return;

        if (Core.IsMember)
            MembershipDues(MemberShipsIDS.Yokai, rank);
        else
        {
            Core.EquipClass(ClassType.Farm);
            ToggleBoost(BoostType.Reputation);
            Core.SavedState();
            Core.Logger($"Farming rank {rank}");
            Bot.Quests.UpdateQuest(488);

            Core.RegisterQuests(383);
            while (!Bot.ShouldExit && FactionRank("Yokai") < rank)
            {
                Core.HuntMonster("dragonkoiz", "Pockey Chew", "Piece of Pockey", 3);
                Bot.Wait.ForQuestComplete(383);
            }
            Core.CancelRegisteredQuests();
            ToggleBoost(BoostType.Reputation, false);
            Core.SavedState(false);
        }
    }

    public void SwagTokenA(int quant = 100)
    {
        if (!Core.IsMember || Core.CheckInventory("Super-Fan Swag Token A", quant))
            return;

        Core.AddDrop("Super-Fan Swag Token A", "Super-Fan Swag Token B", "Super-Fan Swag Token C");
        Core.EquipClass(ClassType.Farm);

        while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token A", quant))
        {
            Core.FarmingLogger($"Super-Fan Swag Token A", quant);

            Core.RegisterQuests(1310);
            while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token C", 200))
                Core.KillMonster("collectorlab", "r2", "Right", "*", "Doppelganger Documents", log: false);
            Core.CancelRegisteredQuests();

            Core.Join("Collection", "Begin", "Spawn");
            Core.BuyItem("collection", 325, "Super-Fan Swag Token B", 20);
            Bot.Shops.Load(325);
            Bot.Shops.BuyItem("Super-Fan Swag Token A");
        }
    }


    public void MembershipDues(MemberShipsIDS faction, int rank = 10)
    {
        if (!Core.IsMember || FactionRank(faction.ToString()) >= rank)
            return;

        Core.SavedState();
        ToggleBoost(BoostType.Reputation);
        int i = 1;
        while (FactionRank($"{faction}") < 10)
        {
            SwagTokenA(1);
            Core.ChainComplete((int)faction);
            Core.Logger($"Completed x{i++}");
        }
        ToggleBoost(BoostType.Reputation, false);
        Core.SavedState(false);
    }

    public int FactionRank(string faction) => Bot.Reputation.GetRank(faction);

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
    Skyguard = 1326,
    DoomWood = 1327,
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
