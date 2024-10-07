/*
name: All Classes
description: This script will get all of the classes that are currently farmable.
tags: all classes, class, farm, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Dailies/Cryomancer.cs
//cs_include Scripts/Other/Classes/Daily-Classes/BlazeBinder.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/RavenlossSaga.cs
//cs_include Scripts/Other/Classes/REP-based/Arachnomancer.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Other/Classes/REP-based/Bard.cs
//cs_include Scripts/Other/Classes/REP-based/ChaosSlayer.cs
//cs_include Scripts/Other/Classes/REP-based/DarkbloodStormKing.cs
//cs_include Scripts/Other/Classes/REP-based/DeathKnight.cs
//cs_include Scripts/Other/Classes/REP-based/ElementalDracomancer.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Other/Classes/REP-based/EternalInversionist.cs
//cs_include Scripts/Other/Classes/REP-based/EvolvedShaman.cs
//cs_include Scripts/Other/Classes/REP-based/GlacialBerserker.cs
//cs_include Scripts/Other/Classes/REP-based/HorcEvader.cs
//cs_include Scripts/Other/Classes/REP-based/ImperialChunin.cs
//cs_include Scripts/Other/Classes/REP-based/Lycan.cs
//cs_include Scripts/Other/Classes/REP-based/MasterRanger.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Other/Classes/REP-based/RoyalBattleMage.cs
//cs_include Scripts/Other/Classes/REP-based/Shaman.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Other/Classes/REP-based/StoneCrusher.cs
//cs_include Scripts/Other/Classes/REP-based/ThiefOfHours.cs
//cs_include Scripts/Other/Classes/REP-based/TrollSpellsmith.cs
//cs_include Scripts/Other/Classes/REP-based/BeastMaster[Mem].cs
//cs_include Scripts/Other/Classes/REP-based/UndeadSlayer[Mem].cs
//cs_include Scripts/Evil/DoomKnight[Mem].cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Other/Classes/LegionDoomKnight[Mem].cs
//cs_include Scripts/Other/Classes/Curio-Classes/LegendaryElementalWarrior[mem].cs
//cs_include Scripts/Other/Classes/Members-CLasses/Acolyte[Mem].cs
//cs_include Scripts/Other/Classes/Members-CLasses/AlphaOmega[Mem].cs
//cs_include Scripts/Story/Safiria[Member].cs
//cs_include Scripts/Other/MergeShops/BloodAncientMerge.cs
//cs_include Scripts/Other/Classes/Members-CLasses/BloodAncient[Mem].cs
//cs_include Scripts/Other/Classes/Members-CLasses/BloodTitan[Mem].cs
//cs_include Scripts/Other/MergeShops/BloodTitanMerge[Mem].cs
//cs_include Scripts/Other/Classes/Members-CLasses/ChronoAssassin[Mem].cs
//cs_include Scripts/Other/MergeShops/DeathPitArenaRepMerge.cs
//cs_include Scripts/Other/Classes/Members-CLasses/DrakelWarlord[Mem].cs
//cs_include Scripts/Other/Classes/Members-CLasses/Renegade[Mem].cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowDragonShinobiMerge.cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowDragonShinobi.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
//cs_include Scripts/Seasonal/Frostvale/FrostvalBarbarian.cs
//cs_include Scripts/Seasonal/Frostvale/NorthlandsMonk.cs
//cs_include Scripts/Seasonal/LuckyDay/LuckyDayShamrockFairMerge.cs
//cs_include Scripts/Seasonal/LuckyDay/EvolvedLeprechaun.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/DarkBirthdayTokenMerge.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/ExaltedHarbinger.cs
//cs_include Scripts/Seasonal/MayThe4th/DarkLord.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonMerge.cs
//cs_include Scripts/Seasonal/Mogloween/PumpkinLord(Class).cs
//cs_include Scripts/Seasonal/Mogloween/VampireLord(Class).cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/LegionSwordMasterAssassin.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/BlazeBeardStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/MergeShops/BlazeBeardMerge.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/AlphaPirate.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/PirateClass.cs

//cs_include Scripts/Evil/VordredsArmor.cs
//cs_include Scripts/Other/Concerts/BattleConcert2023.cs
//cs_include Scripts/Other/Concerts/NeoMetalNecro.cs
//cs_include Scripts/Other/Concerts/DoomMetalNecro.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Legion/MergeShops/UndeadLegionMerge.cs
//cs_include Scripts/Hollowborn/Materials/HollowSoul.cs
//cs_include Scripts/Legion/Various/ExaltedSoulCleaver.cs
//cs_include Scripts/Nation/Various/Archfiend.cs
//cs_include Scripts/Other/Classes/BloodSorceress.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Other/Classes/DragonShinobi.cs
//cs_include Scripts/Story/Lair.cs
//cs_include Scripts/Other/Classes/Dragonslayer.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Classes/Enforcer.cs
//cs_include Scripts/Other/Classes/FrostSpiritReaver.cs
//cs_include Scripts/Other/Classes/GrimNecromancer[600kAC].cs
//cs_include Scripts/Other/Classes/HighSeasCommander[10y].cs
//cs_include Scripts/Other/Classes/LightMage.cs
//cs_include Scripts/Other/Classes/MechaJouster.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Other/Classes/ProtoSartorium.cs
//cs_include Scripts/Other/Classes/Rustbucket.cs
//cs_include Scripts/Other/Classes/ScarletSorceress.cs
//cs_include Scripts/Other/Classes/SkyChargedGrenadier[9yMem].cs
//cs_include Scripts/Other/Classes/Curio-Classes/AbyssalAngelsShadow.cs
//cs_include Scripts/Chaos/ChaosAvengerPreReqs.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Other/Classes/ArchMage/CoreArchMage.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Other/MergeShops/TerminaTempleMerge.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DoomPirateStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/MergeShops/DoomPirateHaulMerge.cs
//cs_include Scripts/Other/Classes/VerusDoomKnight.cs
//cs_include Scripts/Other/Weapons/AvatarOfDeathsScythe.cs
//cs_include Scripts/Other/Weapons/GuardianOfSpiritsBlade.cs
//cs_include Scripts/Other/Weapons/LanceOfTime.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAbezeth.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Other/MergeShops/CelestialChampMerge.cs
//cs_include Scripts/Other/Classes/LightCaster.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Other/MergeShops/GooseMerge.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
//cs_include Scripts/Other/MergeShops/BrightForestMerge.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/InfernalArena.cs
//cs_include Scripts/Other/MergeShops/DoomLegacyMerge.cs
//cs_include Scripts/Other/MergeShops/CelestialChallengerMerge.cs
//cs_include Scripts/Other/MergeShops/SpoilsofLightMerge.cs
//cs_include Scripts/Seasonal/NewYear/ArchiveofTimeMerge.cs
//cs_include Scripts/Other/MergeShops/CrocriverMerge.cs
//cs_include Scripts/Other/MergeShops/SuperSlayinMerge.cs
//cs_include Scripts/Story/DreamPalace.cs
//cs_include Scripts/Other/MergeShops/DreampalaceMerge.cs
//cs_include Scripts/Other/MergeShops/BonecastleMerge.cs
//cs_include Scripts/Other/MergeShops/CelestialRealmMerge.cs
//cs_include Scripts/Other/MergeShops/3LittleWolvesHousesMerge.cs
//cs_include Scripts/Other/Various/Potions.cs
//cs_include Scripts/Story/CruxShip.cs
//cs_include Scripts/Seasonal/Mogloween/MoonlightKhopeshMerge.cs
//cs_include Scripts/Other/MergeShops/ThirdspellMerge.cs
//cs_include Scripts/Seasonal/Friday13th/MergeShops/ShadowMerge.cs
//cs_include Scripts/Darkon/MergeShops/ArcanaInvokerResourceMerge.cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowofDoom/CoreShadowofDoom.cs
//cs_include Scripts/Story/FableForest.cs
//cs_include Scripts/Other/Classes/ArcanaInvoker[Non-Insignia].cs
//cs_include Scripts/Story/Nation/VoidRefuge.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
//cs_include Scripts/Other/MergeShops/FelixsGildedGearMerge.cs
//cs_include Scripts/Other/MergeShops/LoughshineLootMerge.cs
//cs_include Scripts/Other/MergeShops/LiaTaraHillLootMerge.cs
//cs_include Scripts/Other/MergeShops/ColdThunderMerge.cs
//cs_include Scripts/Other/MergeShops/LothianTreasuryMerge.cs
//cs_include Scripts/Other/Classes/SovereignOfStorms.cs
//cs_include Scripts/Other/Classes/Sentinal.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AllClasses
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();

    #region Dailies
    private CoreDailies Daily = new();
    private BlazeBinder BB = new();
    private Cryomancer Cryo = new();
    private LordOfOrder LOO = new();
    #endregion Dailies

    #region Rep
    private Arachnomancer Arach = new();
    private ChaosSlayer CS = new();
    private DarkbloodStormKing DBSK = new();
    private ElementalDracomancer ED = new();
    private EternalInversionist EI = new();
    private EvolvedShaman ES = new();
    private GlacialBerserker GB = new();
    private HorcEvader HE = new();
    private ImperialChunin IC = new();
    private Lycan Lycan = new();
    private MasterRanger MR = new();
    private Paladin Pal = new();
    private RoyalBattleMage RBM = new();
    private Shaman Shaman = new();
    private StoneCrusher SC = new();
    private ThiefOfHours TOH = new();
    private TrollSpellsmith TS = new();
    #endregion Rep

    #region Member
    private AlphaOmega AO = new();
    private Acolyte Acolyte = new();
    private Bard Bard = new();
    private BeastMaster BM = new();
    private BloodAncient BA = new();
    private BloodTitan BT = new();
    private ChronoAssassin CA = new();
    private DeathKnight DK = new();
    private DoomKnight DoomK = new();
    private DrakelWarlord DW = new();
    private LegionDoomKnight LDK = new();
    private LegendaryElementalWarrior LEW = new();
    private Renegade Ren = new();
    private UndeadSlayer US = new();
    #endregion Member

    #region Seasonal
    private AlphaPirate APir = new();
    private DarkLord DL = new();
    private EvolvedLeprechaun EL = new();
    private ExaltedHarbinger EH = new();
    private FrostvalBarbarian FB = new();
    private LegionSwordMasterAssassin LSMA = new();
    private NorthlandsMonk NM = new();
    private PirateClass Pirate = new();
    private ShadowDragonShinobi SDS = new();
    private PumpkinLord PL = new();
    private VampireLord VL = new();
    #endregion Seasonal

    #region Various
    private AbyssalAngelsShadow AAS = new();
    private ArchFiend AF = new();
    private BloodSorceress BS = new();
    private DoomMetalNecro DMN = new();
    private Dragonslayer DS = new();
    private DragonslayerGeneral DSG = new();
    private DragonShinobi DSS = new();
    private Enforcer Enf = new();
    private ExaltedSoulCleaver ESC = new();
    private FrostSpiritReaver FSR = new();
    private GrimNecromancer GN = new();
    private HighSeasCommander HSC = new();
    private InfiniteLegionDC ILDC = new();
    private LightMage LM = new();
    private MechaJouster MJ = new();
    private Necromancer Necro = new();
    private NeoMetalNecro NMN = new();
    private ProtoSartorium PS = new();
    private Rustbucket RB = new();
    private ScarletSorceress SS = new();
    private SkyChargedGrenadier SCG = new();
    private SwordMaster SM = new();
    private Sentinel Sentinel = new();
    #endregion Various

    #region End game
    private ArcanaInvoker AI = new();
    private CoreArchMage AM = new();
    private ArchPaladin AP = new();
    private ChaosAvengerClass CAV = new();
    private DragonOfTime DOT = new();
    private LightCaster LC = new();
    private CoreLR LR = new();
    private SovereignOfStorms SOS = new();
    private VerusDoomKnightClass VDK = new();
    private CoreVHL VHL = new();
    private CoreYnR YNR = new();
    #endregion End game


    public string OptionsStorage = "GetAllClasses";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("RankALL", "Rankup All Classes", "wether to Rankup the class to 10 after acquiring it", true),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        int GoldBoostID = Bot.Boosts.GetBoostID(BoostType.Gold, true);
        int ClassBoostID = Bot.Boosts.GetBoostID(BoostType.Class, true);
        int ExperienceBoostID = Bot.Boosts.GetBoostID(BoostType.Experience, true);
        int ReputationBoostID = Bot.Boosts.GetBoostID(BoostType.Reputation, true);

        Core.BankingBlackList.AddRange(
            Bot.Inventory.Items
                .Where(x => x.ID == GoldBoostID || x.ID == ClassBoostID || x.ID == ExperienceBoostID || x.ID == ReputationBoostID)
                .Cast<ItemBase>()
                .Select(item => item.Name)
        );


        Core.SetOptions();

        GetAllClasses();

        Core.SetOptions(false);
    }

    public void GetAllClasses()
    {
        bool rankUpClass = Bot.Config!.Get<bool>("RankALL");

        //some of these are required for forge enhancements
        MoreClassesToGet(rankUpClass);

        // then we start the rest.
        DailyClasses(rankUpClass);
        RepClasses(rankUpClass);
        MemClasses(rankUpClass);
        SeasonalClasses(rankUpClass);
        VariousClasses(rankUpClass);
        EndGameClasses(rankUpClass);
        ACorToHardtoGetClasses(rankUpClass);

    }

    public void MoreClassesToGet(bool rankUpClass)
    {
        Core.Logger("=== Buying `beginner` classes start (will help with forge enhancements later)===");
        Adv.GearStore();
        Core.BuyItem("trainers", 170, "Warrior");
        Core.BuyItem("trainers", 174, "Mage");
        Core.BuyItem("trainers", 176, "Healer");
        Core.BuyItem("trainers", 172, "Rogue");
        Core.BuyItem("classhalla", 178, "Ninja");
        Core.BuyItem(Bot.Map.Name, 299, "Barber");
        Core.BuyItem(Bot.Map.Name, 299, "Oracle");
        Core.BuyItem(Bot.Map.Name, 222, "Battle Warrior");
        Core.BuyItem(Bot.Map.Name, 222, "Battle Healer");
        Core.BuyItem(Bot.Map.Name, 222, "No Class");
        Adv.GearStore(true);
        Core.ToBank(new[] { "Warrior", "Mage", "Healer", "Rogue", "Ninja", "Barber", "Oracle", "Battle Warrior", "Battle Healer", "No Class" });
        Core.Logger("=== `beginner` classes - Bought! ===");
    }

    public void DailyClasses(bool rankUpClass)
    {
        Core.Logger("=== Doing Daily Classes ===");

        Adv.GearStore();
        CheckAndExecute("Blaze Binder", () => BB.GetClass(rankUpClass));
        CheckAndExecute("The Collector", Daily.CollectorClass);
        CheckAndExecute("Cryomancer", () => Cryo.DoCryomancer(rankUpClass));
        CheckAndExecute("Death KnightLord", Daily.DeathKnightLord);
        CheckAndExecute("Lord of Order", () => LOO.GetLoO(rankUpClass));
        Adv.GearStore(true);
        Core.ToBank(new[] { "Blaze Binder", "The Collector", "Cryomancer", "Death KnightLord", "Lord of Order" });

        Core.Logger("=== Daily Classes - Completed! ===");
    }

    public void RepClasses(bool rankUpClass)
    {
        Core.Logger("=== Doing Reputation Classes ===");

        Adv.GearStore();
        CheckAndExecute("Arachnomancer", () => Arach.GetArach(rankUpClass));
        CheckAndExecute("Darkblood StormKing", () => DBSK.GetDSK(rankUpClass));
        CheckAndExecute("Elemental Dracomancer", () => ED.GetClass(rankUpClass));
        CheckAndExecute("Eternal Inversionist", () => EI.GetEI(rankUpClass));
        CheckAndExecute("Evolved Shaman", () => ES.GetES(rankUpClass));
        CheckAndExecute("Glacial Berserker", () => GB.GetGB(rankUpClass));
        CheckAndExecute("Horc Evader", () => HE.GetHE(rankUpClass));
        CheckAndExecute("Imperial Chunin", () => IC.GetIC(rankUpClass));
        CheckAndExecute("Lycan", () => Lycan.GetLycan(rankUpClass));
        CheckAndExecute("Master Ranger", () => MR.GetMR(rankUpClass));
        CheckAndExecute("Paladin", () => Pal.GetPaladin(rankUpClass));
        CheckAndExecute("Royal BattleMage", () => RBM.GetRBM(rankUpClass));
        CheckAndExecute("Shaman", () => Shaman.GetShaman(rankUpClass));
        CheckAndExecute("StoneCrusher", () => SC.GetSC(rankUpClass));
        CheckAndExecute("Thief of Hours", () => TOH.GetToH(rankUpClass));
        CheckAndExecute("Troll Spellsmith", () => TS.GetTS(rankUpClass));

        // Chaos Slayer variants
        CheckAndExecute("Chaos Slayer Mystic", () => CS.GetCS(CSvariant.Mystic, rankUpClass));
        CheckAndExecute("Chaos Slayer Berserker", () => CS.GetCS(CSvariant.Berserker, rankUpClass));
        CheckAndExecute("Chaos Slayer Cleric", () => CS.GetCS(CSvariant.Cleric, rankUpClass));
        CheckAndExecute("Chaos Slayer Thief", () => CS.GetCS(CSvariant.Thief, rankUpClass));
        Adv.GearStore(true);
        Core.ToBank(new[] { "Arachnomancer", "Darkblood StormKing", "Elemental Dracomancer", "Eternal Inversionist", "Evolved Shaman", "Glacial Berserker", "Horc Evader", "Imperial Chunin", "Lycan", "Master Ranger", "Paladin", "Royal BattleMage", "Shaman", "StoneCrusher", "Thief of Hours", "Troll Spellsmith", "Chaos Slayer Mystic", "Chaos Slayer Berserker", "Chaos Slayer Cleric", "Chaos Slayer Thief" });


        Core.Logger("=== Reputation Classes - Completed! ===");
    }

    private void MemClasses(bool rankUpClass)
    {
        if (!Core.IsMember)
            return;

        Core.Logger("=== Doing Member Classes ===");

        Adv.GearStore();
        CheckAndExecute("Alpha Omega", () => AO.GetAlphaOmega(rankUpClass));
        CheckAndExecute("Acolyte", () => Acolyte.GetAcolyte(rankUpClass));
        CheckAndExecute("Bard", () => Bard.GetBard(rankUpClass));
        CheckAndExecute("BeastMaster", () => BM.GetBM(rankUpClass));
        CheckAndExecute("Blood Ancient", () => BA.GetBAnc(rankUpClass));
        CheckAndExecute("Blood Titan", () => BT.Getclass(rankUpClass));
        CheckAndExecute("Chrono Assassin", () => CA.GetChronoAss(rankUpClass));
        CheckAndExecute("DeathKnight", () => DK.GetDK(rankUpClass));
        CheckAndExecute("DoomKnight", () => DoomK.GetDoomKnight(rankUpClass));
        CheckAndExecute("Drakel Warlord", () => DW.GetClass(rankUpClass));
        CheckAndExecute("Legion DoomKnight", () => LDK.GetLDK(rankUpClass));
        CheckAndExecute("Legendary Elemental Warrior", () => LEW.GetLEW(rankUpClass));
        CheckAndExecute("Renegade", () => Ren.Getclass(rankUpClass));
        CheckAndExecute("UndeadSlayer", () => US.GetUS(rankUpClass));
        Adv.GearStore(true);
        Core.ToBank(new[] { "Alpha Omega", "Acolyte", "Bard", "BeastMaster", "Blood Ancient", "Blood Titan", "Chrono Assassin", "DeathKnight", "DoomKnight", "Drakel Warlord", "Legion DoomKnight", "Legendary Elemental Warrior", "Renegade", "UndeadSlayer" });

        Core.Logger("=== Member Classes - Completed! ===");
    }

    private void SeasonalClasses(bool rankUpClass)
    {
        Core.Logger("=== Doing Seasonal Classes ===");

        Adv.GearStore();
        CheckAndExecute("Alpha Pirate", () => APir.GetAlphaPirate(rankUpClass));
        CheckAndExecute("Dark Lord", () => DL.GetDL(rankUpClass));
        CheckAndExecute("Evolved Leprechaun", () => EL.GetClass(rankUpClass));
        CheckAndExecute("Exalted Harbinger", () => EH.GetEH(rankUpClass));
        CheckAndExecute("Frostval Barbarian", () => FB.GetFB(rankUpClass));
        CheckAndExecute("Legion SwordMaster Assassin", () => LSMA.GetClass(rankUpClass));
        CheckAndExecute("Northlands Monk", () => NM.GetNlMonk(rankUpClass));
        CheckAndExecute("Pirate", () => Pirate.GetPirate(rankUpClass));
        CheckAndExecute("Shadow Dragon Shinobi", () => SDS.GetClass(rankUpClass));
        CheckAndExecute("Pumpkin Lord", () => PL.GetClass(rankUpClass));
        CheckAndExecute("Vampire Lord", () => VL.GetClass(rankUpClass));
        Adv.GearStore(true);
        Core.ToBank(new[] { "Alpha Pirate", "Dark Lord", "Evolved Leprechaun", "Exalted Harbinger", "Frostval Barbarian", "Legion SwordMaster Assassin", "Northlands Monk", "Pirate", "Shadow Dragon Shinobi", "Pumpkin Lord", "Vampire Lord" });

        Core.Logger("=== Seasonal Classes - Completed! ===");
    }

    private void VariousClasses(bool rankUpClass)
    {
        Core.Logger("=== Doing Various Classes ===");

        Adv.GearStore();
        CheckAndExecute("Abyssal Angel Shadow", () => AAS.GetAbyssal(rankUpClass));
        CheckAndExecute("Archfiend", () => AF.GetArchfiend(rankUpClass));
        CheckAndExecute("Blood Sorceress", () => BS.GetBSorc(rankUpClass));
        CheckAndExecute("Doom Metal Necro", () => DMN.GetClass(rankUpClass));
        CheckAndExecute("Dragonslayer", () => DS.GetDragonslayer(rankUpClass));
        CheckAndExecute("Dragonslayer General", () => DSG.GetDSGeneral(rankUpClass));
        CheckAndExecute("DragonSoul Shinobi", () => DSS.GetDSS(rankUpClass));
        CheckAndExecute("Enforcer", () => Enf.GetClass(rankUpClass));
        CheckAndExecute("Frost SpititReaver", () => FSR.GetFSR(rankUpClass));
        CheckAndExecute("HighSeas Commander", () => HSC.GetHSC(rankUpClass));
        CheckAndExecute("Infinite Legion Dark Caster", () => ILDC.GetILDC(rankUpClass));
        CheckAndExecute("MechaJouster", () => MJ.GetMJ(rankUpClass));
        CheckAndExecute("Necromancer", () => Necro.GetNecromancer(rankUpClass));
        CheckAndExecute("Neo Metal Necro", () => NMN.GetClass(rankUpClass));
        CheckAndExecute("ProtoSartorium", () => PS.GetPS(rankUpClass));
        CheckAndExecute("Rustbucket", () => RB.GetRustbucket(rankUpClass));
        CheckAndExecute("Scarlet Sorceress", () => SS.GetSSorc(rankUpClass));
        CheckAndExecute("SwordMaster", () => SM.GetSwordMaster(rankUpClass));
        Adv.GearStore(true);
        Core.ToBank(new[] { "Abyssal Angel Shadow", "Archfiend", "Blood Sorceress", "Doom Metal Necro", "Dragonslayer", "Dragonslayer General", "DragonSoul Shinobi", "Enforcer", "Frost SpititReaver", "HighSeas Commander", "Infinite Legion Dark Caster", "MechaJouster", "Necromancer", "Neo Metal Necro", "ProtoSartorium", "Rustbucket", "Scarlet Sorceress", "SwordMaster" });

        Core.Logger("=== Various Classes - Completed! ===");
    }

    private void EndGameClasses(bool rankUpClass)
    {
        Core.Logger("=== Doing End Game Classes ===");

        Adv.GearStore();
        CheckAndExecute("ArchPaladin", () => AP.GetAP(rankUpClass));
        CheckAndExecute("Dragon of Time", () => DOT.GetDoT(rankUpClass, doExtra: false));
        CheckAndExecute("Void Highlord", () => VHL.GetVHL(rankUpClass));
        CheckAndExecute("Yami no Ronin", () => YNR.GetYnR(rankUpClass));
        Adv.GearStore(true);
        Core.ToBank(new[] { "ArchPaladin", "Dragon of Time", "Void Highlord", "Yami no Ronin" });

        Core.Logger("=== End Game Classes - Completed! ===");
    }

    private void ACorToHardtoGetClasses(bool rankUpClass)
    {
        Core.Logger("=== AC / Special Requirement / Army Classes ===");

        Adv.GearStore();
        // Why do you own these classes?
        CheckAndExecute("Grim Necromancer", () => GN.GetGN(rankUpClass)); // 600k ac purchased

        // Classes that require a certain time played:
        CheckAndExecute("SkyCharged Grenadier", () => SCG.GetSCG(rankUpClass)); // 9 years membership
        CheckAndExecute("Sentinel", () => Sentinel.GetSentinel(rankUpClass)); // 16 years played

        // Classes that Cost ACs / AC badges:
        CheckAndExecute("LightCaster", () => LC.GetLC(rankUpClass)); // LC gets LM at the same time
        CheckAndExecute("Legion Revenant", () => LR.GetLR(rankUpClass));
        CheckAndExecute("Exalted Soul Cleaver", () => ESC.GetClass(rankUpClass));

        // Classes that require an army or are just to damn hard to solo,
        // these scripts will more then likely just return when they cant farm an item:
        CheckAndExecute("Chaos Avenger", () => CAV.GetClass(rankUpClass));
        CheckAndExecute("Archmage", () => AM.GetAM(rankUpClass));
        CheckAndExecute("Verus DoomKnight", () => VDK.GetClass(rankUpClass));

        // Classes that take to long to farm for a bank class:
        CheckAndExecute("Arcana Invoker", () => AI.GetAI(rankUpClass));
        CheckAndExecute("ShadowScythe General", Daily.ShadowScytheClass);
        CheckAndExecute("Sovereign of Storms", () => SOS.GetSOS(rankUpClass));
        Adv.GearStore(true);
        Core.ToBank(new[] { "Grim Necromancer", "SkyCharged Grenadier", "Sentinel", "LightCaster", "Legion Revenant", "Exalted Soul Cleaver", "Chaos Avenger", "Archmage", "Verus DoomKnight", "Arcana Invoker", "ShadowScythe General", "Sovereign of Storms" });

        Core.Logger("=== AC / Special Requirement / Army Classes - Completed! ===");
    }


    bool IsitRank10(ItemBase item) => item != null && item.Quantity == 302500;

    void CheckAndExecute(string className, Action action)
    {
        // Find the item in both inventory and bank.
        ItemBase? Class = Bot.Inventory.Items.Concat(Bot.Bank.Items)
            .FirstOrDefault(x => x.Name.ToLower() == className.ToLower());

        bool ItemOwned = Bot.Inventory.Items.Concat(Bot.Bank.Items).Contains(Class);

        // Check if the item is found and if it meets the quantity requirement.
        if (!ItemOwned || (Class != null && !IsitRank10(Class)))
        {
            action();
        }
    }


}
