/*
name: All Classes
description: This script will get all of the classes that are currently farmable.
tags: all classes,class,farm,complete,all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Dailies\Cryomancer.cs
//cs_include Scripts/Other\Classes\Daily-Classes\BlazeBinder.cs
//cs_include Scripts/Dailies\LordOfOrder.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/RavenlossSaga.cs
//cs_include Scripts/Other\Classes\REP-based\Arachnomancer.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Other\Classes\REP-based\Bard.cs
//cs_include Scripts/Other\Classes\REP-based\ChaosSlayer.cs
//cs_include Scripts/Other\Classes\REP-based\DarkbloodStormKing.cs
//cs_include Scripts/Other\Classes\REP-based\DeathKnight.cs
//cs_include Scripts/Other\Classes\REP-based\ElementalDracomancer.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Other\Classes\REP-based\EternalInversionist.cs
//cs_include Scripts/Other\Classes\REP-based\EvolvedShaman.cs
//cs_include Scripts/Other\Classes\REP-based\GlacialBerserker.cs
//cs_include Scripts/Other\Classes\REP-based\HorcEvader.cs
//cs_include Scripts/Other\Classes\REP-based\ImperialChunin.cs
//cs_include Scripts/Other\Classes\REP-based\Lycan.cs
//cs_include Scripts/Other\Classes\REP-based\MasterRanger.cs
//cs_include Scripts/Good\Paladin.cs
//cs_include Scripts/Other\Classes\REP-based\RoyalBattleMage.cs
//cs_include Scripts/Other\Classes\REP-based\Shaman.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Other\Classes\REP-based\StoneCrusher.cs
//cs_include Scripts/Other\Classes\REP-based\ThiefOfHours.cs
//cs_include Scripts/Other\Classes\REP-based\TrollSpellsmith.cs
//cs_include Scripts/Other\Classes\REP-based\BeastMaster[Mem].cs
//cs_include Scripts/Other\Classes\REP-based\UndeadSlayer[Mem].cs
//cs_include Scripts/Evil\DoomKnight[Mem].cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Other\Classes\LegionDoomKnight[Mem].cs
//cs_include Scripts/Other\Classes\Curio-Classes\LegendaryElementalWarrior[mem].cs
//cs_include Scripts/Other\Classes\Members-CLasses\Acolyte[Mem].cs
//cs_include Scripts/Other\Classes\Members-CLasses\AlphaOmega[Mem].cs
//cs_include Scripts/Story/Safiria[Member].cs
//cs_include Scripts/Other\MergeShops\BloodAncientMerge.cs
//cs_include Scripts/Other\Classes\Members-CLasses\BloodAncient[Mem].cs
//cs_include Scripts/Other\Classes\Members-CLasses\BloodTitan[Mem].cs
//cs_include Scripts/Other\Classes\Members-CLasses\ChronoAssassin[Mem].cs
//cs_include Scripts/Other\MergeShops\DeathPitArenaRepMerge.cs
//cs_include Scripts/Other\Classes\Members-CLasses\DrakelWarlord[Mem].cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
//cs_include Scripts/Seasonal\Frostvale\FrostvalBarbarian.cs
//cs_include Scripts/Seasonal\Frostvale\NorthlandsMonk.cs
//cs_include Scripts/Seasonal\LuckyDay\LuckyDayShamrockFairMerge.cs
//cs_include Scripts/Seasonal\LuckyDay\EvolvedLeprechaun.cs
//cs_include Scripts/Seasonal\MayThe4th\DarkLord.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonMerge.cs
//cs_include Scripts/Seasonal\Mogloween\VampireLord(Class).cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs
//cs_include Scripts/Seasonal\StaffBirthdays\DageTheEvil\LegionSwordMasterAssassin.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/BlazeBeardStory.cs
//cs_include Scripts/Seasonal\TalkLikeaPirateDay\MergeShops\BlazeBeardMerge.cs
//cs_include Scripts/Seasonal\TalkLikeaPirateDay\AlphaPirate.cs
//cs_include Scripts/Seasonal\TalkLikeaPirateDay\PirateClass.cs

//cs_include Scripts/Evil/VordredsArmor.cs
//cs_include Scripts/Other/Concerts/BattleConcert2023.cs
//cs_include Scripts/Other/Concerts/NeoMetalNecro.cs
//cs_include Scripts/Other\Concerts\DoomMetalNecro.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Legion\Various\UndeadLegionMerge.cs
//cs_include Scripts/Legion\Various\ExaltedSoulCleaver.cs
//cs_include Scripts/Nation\Various\Archfiend.cs
//cs_include Scripts/Other\Classes\BloodSorceress.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Other\Classes\DragonShinobi.cs
//cs_include Scripts/Story\Lair.cs
//cs_include Scripts/Other\Classes\Dragonslayer.cs
//cs_include Scripts/Other\Classes\DragonslayerGeneral.cs
//cs_include Scripts/Other\Classes\Enforcer.cs
//cs_include Scripts/Other\Classes\FrostSpiritReaver.cs
//cs_include Scripts/Other\Classes\HighSeasCommander[10y].cs
//cs_include Scripts/Other\Classes\LightMage.cs
//cs_include Scripts/Other\Classes\Necromancer.cs
//cs_include Scripts/Other\Classes\ScarletSorceress.cs
//cs_include Scripts/Other\Classes\Curio-Classes\AbyssalAngelsShadow.cs
//cs_include Scripts/Chaos\ChaosAvengerPreReqs.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Good\ArchPaladin.cs
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
//cs_include Scripts/Seasonal\TalkLikeaPirateDay\DoomPirateStory.cs
//cs_include Scripts/Seasonal\TalkLikeaPirateDay\MergeShops\DoomPirateHaulMerge.cs
//cs_include Scripts/Other\Classes\VerusDoomKnight.cs
//cs_include Scripts/Other/Weapons/AvatarOfDeathsScythe.cs
//cs_include Scripts/Other/Weapons/GuardianOfSpiritsBlade.cs
//cs_include Scripts/Other/Weapons/LanceOfTime.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAbezeth.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Other/MergeShops/CelestialChampMerge.cs
//cs_include Scripts/Other\Classes\LightCaster.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Other\Classes\DragonOfTime.cs
using Skua.Core.Interfaces;

public class AllClasses
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    #region Dailies
    private CoreDailies Daily = new();
    private BlazeBinder BB = new();
    private Cryomancer Cryo = new();
    private LordOfOrder LOO = new();
    #endregion

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
    #endregion

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
    private UndeadSlayer US = new();
    #endregion

    #region Seasonal
    private AlphaPirate APir = new();
    private DarkLord DL = new();
    private EvolvedLeprechaun EL = new();
    private FrostvalBarbarian FB = new();
    private LegionSwordMasterAssassin LSMA = new();
    private NorthlandsMonk NM = new();
    private PirateClass Pirate = new();
    private VampireLord VL = new();
    #endregion

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
    private HighSeasCommander HSC = new();
    private InfiniteLegionDC ILDC = new();
    private LightMage LM = new();
    private Necromancer Necro = new();
    private NeoMetalNecro NMN = new();
    private ScarletSorceress SS = new();
    private SwordMaster SM = new();
    #endregion

    #region End game
    private CoreArchMage AM = new();
    private ArchPaladin AP = new();
    private ChaosAvengerClass CAV = new();
    private DragonOfTime DOT = new();
    private LightCaster LC = new();
    private CoreLR LR = new();
    private VerusDoomKnightClass VDK = new();
    private CoreVHL VHL = new();
    private CoreYnR YNR = new();
    #endregion

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetAllClasses();
        Core.SetOptions(false);
    }

    public void GetAllClasses()
    {
        DailyClasses();
        RepClasses();
        MemClasses();
        SeasonalClasses();
        VariousClasses();
        EndGameClasses();
    }

    public void DailyClasses()
    {
        Core.Logger("=== Doing Daily Classes ===");

        BB.GetClass();
        Daily.CollectorClass();
        Cryo.DoCryomancer();
        Daily.DeathKnightLord();
        LOO.GetLoO();
        Daily.Pyromancer();
        Daily.ShadowScytheClass();

        Core.Logger("=== Daily Classes - Completed! ===");
    }

    public void RepClasses()
    {
        Core.Logger("=== Doing Reputation Classes ===");

        Arach.GetArach();
        CS.GetCS();
        DBSK.GetDSK();
        ED.GetClass();
        EI.GetEI();
        ES.GetES();
        GB.GetGB();
        HE.GetHE();
        IC.GetIC();
        Lycan.GetLycan();
        MR.GetMR();
        Pal.GetPaladin();
        RBM.GetRBM();
        Shaman.GetShaman();
        SC.GetSC();
        TOH.GetToH();
        TS.GetTS();

        Core.Logger("=== Reputation Classes - Completed! ===");
    }

    private void MemClasses()
    {
        if (!Core.IsMember)
            return;

        Core.Logger("=== Doing Member Classes ===");

        AO.GetAlphaOmega();
        Acolyte.GetAcolyte();
        Bard.GetBard();
        BM.GetBM();
        BA.GetBAnc();
        BT.Getclass();
        CA.GetChronoAss();
        DK.GetDK();
        DoomK.GetDoomKnight();
        DW.GetClass();
        LDK.GetLDK();
        LEW.GetLEW();
        US.GetUS();

        Core.Logger("=== Member Classes - Completed! ===");
    }

    private void SeasonalClasses()
    {
        Core.Logger("=== Doing Seasonal Classes ===");

        APir.GetAlphaPirate();
        DL.GetDL();
        EL.GetClass();
        FB.GetFB();
        LSMA.GetClass();
        NM.GetNlMonk();
        VL.GetClass();
        Pirate.GetPirate();

        Core.Logger("=== Seasonal Classes - Completed! ===");
    }

    private void VariousClasses()
    {
        Core.Logger("=== Doing Various Classes ===");

        AAS.GetAbyssal();
        AF.GetArchfiend();
        BS.GetBSorc();
        DMN.GetClass();
        DS.GetDragonslayer();
        DSG.GetDSGeneral();
        DSS.GetDSS();
        Enf.GetClass();
        ESC.GetClass();
        FSR.GetFSR();
        HSC.GetHSC();
        ILDC.GetILDC();
        LM.GetLM();
        Necro.GetNecromancer();
        NMN.GetClass();
        SS.GetSSorc();
        SM.GetSwordMaster();

        Core.Logger("=== Various Classes - Completed! ===");
    }

    private void EndGameClasses()
    {
        Core.Logger("=== Doing End Game Classes ===");

        AM.GetAM();
        AP.GetAP();
        CAV.GetClass();
        DOT.GetDoT();
        LC.GetLC();
        LR.GetLR(true);
        VDK.GetClass();
        VHL.GetVHL();
        YNR.GetYnR();

        Core.Logger("=== End Game Classes - Completed! ===");
    }
}
