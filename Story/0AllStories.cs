//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_Include Scripts/Farm/BuyScrolls.cs

//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs

//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/Doomwood/Necrodungeon.cs
//cs_include Scripts/Story/Doomwood/Necrotower.cs

//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs

//cs_include Scripts/Story/FireIsland/CoreFireIsland.cs

//cs_include Scripts/Story/IsleOfFotia/CoreIsleOfFotia.cs

//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/DageTheEvilIsland/CoreDageTheEvilIsland.cs
//cs_include Scripts/Story/Legion/DageChallengeStory.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/Legion/WorldSoul.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs

//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/LordsofChaos/ChoasFinaleBonus[Mem]/DeadlyDungeon[Mem].cs
//cs_include Scripts/Story/LordsofChaos/ChoasFinaleBonus[Mem]/KillerCatacombs[Mem].cs
//cs_include Scripts/Story/LordsofChaos/ChoasFinaleBonus[Mem]/PyramidofPain[Mem].cs

//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Story/Nation/FiendPast.cs
//cs_include Scripts/Story/Nation/Originul.cs
//cs_include Scripts/Story/Nation/Tercessuinotlim.cs

//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialPast.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/GoldenArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/OrbHunt.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/QueenBattle.cs

//cs_include Scripts/Story/SepulchureSaga/00CompleteSepulchureSaga.cs
//cs_include Scripts/Story/SepulchureSaga/01SepulchurePrequelAlden.cs
//cs_include Scripts/Story/SepulchureSaga/02SepulchurePrequelLynaria.cs
//cs_include Scripts/Story/SepulchureSaga/03SepulchuresRise.cs
//cs_include Scripts/Story/SepulchureSaga/04ShadowfallRise.cs

//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs

//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs

//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs

//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs


//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Story/ArtixWedding.cs

//cs_include Scripts/Story/Banished.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/BloodMoon.cs
//cs_include Scripts/Story/Borgars.cs

//cs_include Scripts/Story/CastleTunnels.cs
//cs_include Scripts/Story/CruxShip.cs

//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Story/DjinnGuard.cs
//cs_include Scripts/Story/DoomVault.cs
//cs_include Scripts/Story/DoomVaultB.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/DreadForest.cs
//cs_include Scripts/Story/DreamPalace.cs

//cs_include Scripts/Story/Eden.cs
//cs_include Scripts/Story/EtherstormWastes.cs
//cs_include Scripts/Story/ExaltiaTower.cs

//cs_include Scripts/Story/FireHouse[SeasonalOrMem].cs
//cs_include Scripts/Story/FrozenNorthlands.cs

//cs_include Scripts/Story/GameHaven.cs
//cs_include Scripts/Story/Glacera.cs

//cs_include Scripts/Story/J6Saga.cs

//cs_include Scripts/Story/LightoviaCave.cs

//cs_include Scripts/Story/Manor.cs
//cs_include Scripts/Story/MustyCave.cs

//cs_include Scripts/Story/NytheraSaga.cs

//cs_include Scripts/Story/Oddities.cs

//cs_include Scripts/Story/PoisonForest.cs

//cs_include Scripts/Story/RavenlossSaga.cs

//cs_include Scripts/Story/ShadowSlayerK.cs
//cs_include Scripts/Story/ShadowVault.cs
//cs_include Scripts/Story/ShadowVoid.cs
//cs_include Scripts/Story/Shinkansen.cs
//cs_include Scripts/Story/SkyGuardSaga.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/SuperDeath.cs

//cs_include Scripts/Story/Table.cs
//cs_include Scripts/Story/ThirdSpell.cs
//cs_include Scripts/Story/TitanAttack.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/Trygve.cs
//cs_include Scripts/Story/Tutorial.cs

//cs_include Scripts/Story/UnderGroundLab.cs

//cs_include Scripts/Story/VasalkarLairWar.cs

//cs_include Scripts/Story/XansLair.cs

//cs_include Scripts/Story/Yokai.cs

//cs_include Scripts/Story/MemetsRealm/CoreMemet.cs


using Skua.Core.Interfaces;

public class AllStories
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();

    #region Folder based
    // 7 Deadly Dragons
    public Core7DD DD = new();
    public HatchTheEgg Egg = new();
    public GetSDD SDD = new();

    // Doomwood
    public AQWZombies AQWZombies = new();
    public DoomwoodPart3 DoomwoodPart3 = new();
    public NecroDungeon NecroDungeon = new();
    public NecroTowerStory NecroTower = new();

    // Elergy of Madness - Darkon
    public CoreAstravia CoreAstravia = new();

    //Fire Island
    public CoreFireIsland FI = new();

    // Isle Of Fotia
    public CoreIsleOfFotia CoreIsleOfFotia = new();

    // Legion
    public CoreDageTheEvilIsland DageIsland = new();
    public DageChallengeStory DageChallengeStory = new();
    public DarkWarLegionandNation DarkWar = new();
    public SeraphicWar_Story SeraphicWar_Story = new();
    public SevenCircles SevenCircles = new();
    public WorldSoul WorldSoul = new();

    // 13 Lord of Chaos
    public Core13LoC LOC => new();
    public DeadlyDungeon DeadlyDungeon = new();
    public KillerCatacombs KillerCatacombs = new();
    public PyramidofPain PyramidofPain = new();

    // Nation
    public Bamboozle Bamboozle = new();
    public CitadelRuins CitadelRuins = new();
    public FiendPast FiendPast = new();
    public Fiendshard_Story Fiendshard_Story = new();
    public Originul_Story Originul_Story = new();

    public Tercessuinotlim Tercessuinotlim = new();

    // Queen of Monsters
    public CoreQOM QOM => new();
    public BrightOak BrightOak = new();
    public CelestialArenaQuests CelestialArena = new();
    public CelestialPast CelestialPast = new();
    public GoldenArena GoldenArena = new();
    public LivingDungeon LivingDungeon = new();
    public OrbHunt OrbHunt = new();
    public QueenBattle QueenBattle = new();

    // Sepulchure Saga
    public CompleteSepulchureSaga SeppySaga = new();

    // Shadows of Chaos
    public CoreSoC CoreSoC = new();

    // Shadow of War
    public CoreSoW SOW = new();

    //Summer 2015 AdventureMap
    public CoreSummer CoreSummer = new();

    // Throne of Darkness
    public CoreToD TOD = new();

    //MemetsRealm
    public MemetsRealm MemetsRealm = new();

    #endregion

    #region Standalone (sorted alphabetically)
    public Artixpointe Artixpointe = new();
    public ArtixWedding ArtixWedding = new();

    public Banished Banished = new();
    public BattleUnder BattleUnder = new();
    public BloodMoon BloodMoon = new();
    public Borgars Borgars = new();

    public CastleTunnels CastleTunnels = new();
    public CruxShip CruxShip = new();

    public DjinnGateStory DjinnGateStory = new();
    public DjinnGuard DjinnGuard = new();
    public DoomVaultA DoomVaultA = new();
    public DoomVaultB DoomVaultB = new();
    public DragonFableOrigins DragonFableOrigins = new();
    public DreadForest DreadForest = new();
    public DreamPalace DreamPalace = new();

    public Eden Eden = new();
    public EtherStormWastes EtherStormWastes = new();
    public ExaltiaTower ExaltiaTower = new();

    public Gamehaven Gamehaven = new();

    public FireHouse FireHouse = new();
    public FrozenNorthlands FrozenNorthlands = new();

    public GlaceraStory GlaceraStory = new();

    public LightoviaCave LightoviaCave = new();

    public Manor Manor = new();
    public MustyCave MustyCave = new();

    public NytheraSaga NytheraSaga = new();

    public J6Saga J6Saga = new();

    public Oddities Oddities = new();

    public PoisonForest PoisonForest = new();

    public RavenlossSaga RavenlossSaga = new();

    public ShadowSlayerK ShadowSlayerK = new();
    public ShadowVault ShadowVault = new();
    public ShadowVoid ShadowVoid = new();
    public Shinkansen Shinkansen = new();
    public SkyGuardSaga SkyGuardSaga = new();
    public StarSinc StarSinc = new();
    public SuperDeath SuperDeath = new();

    public Table Table = new();
    public ThirdSpell ThirdSpell = new();
    public TitanAttackStory TitanAttackStory = new();
    public TowerOfDoom TowerOfDoom = new();
    public Trygve Trygve = new();
    public Tutorial Tutorial = new();

    public UnderGroundLab UnderGroundLab = new();

    public LairWar LairWar = new(); //VasalkarLairWar.cs

    public XansLair Xans = new();

    public YokaiQuests Yokai = new();

    #endregion

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CompleteAll();

        Core.SetOptions(false);
    }

    public void CompleteAll()
    {
        Tutorial.Badges();
        Core.Logger($"Story: Tutorial - Complete");

        #region In folders
        #region 13LOC
        Adv.GearStore();
        LOC.Complete13LOC(true);
        Adv.GearStore(true);
        Core.Logger($"Saga: The 13 Lords of Chaos - Complete");
        if (Core.IsMember)
        {
            DeadlyDungeon.DeadlyDungeonQuest_Line();
            Core.Logger($"Story: Deadly Dungeon - Complete");

            KillerCatacombs.KillerCatacombs_Line();
            Core.Logger($"Story: Killer Catacombs - Complete");

            PyramidofPain.PyramidofPain_Line();
            Core.Logger($"Story: Pyramid of Pain - Complete");
        }
        Core.Logger($"Saga: The 13 Lords of Chaos (Extra) - Complete");
        #endregion

        #region 7DD
        DD.Complete7DD();
        Core.Logger($"Saga: 7 Deadly Dragons - Complete");
        Egg.Hatch();
        Core.Logger($"Saga: 7 Deadly Dragons (Extra) - Complete");
        #endregion

        #region Doomwood
        AQWZombies.Storyline();
        Core.Logger($"Story: AQW Zombies - Complete");

        NecroDungeon.NecrodungeonStoryLine();
        Core.Logger($"Story: Necro Dungeon - Complete");

        NecroTower.DoAll();
        Core.Logger($"Story: Necro Tower - Complete");

        DoomwoodPart3.StoryLine();
        Core.Logger($"Story: Doomwood Part 3 - Complete");
        #endregion

        #region Elergy
        CoreAstravia.CompleteCoreAstravia();
        Core.Logger($"Saga: Elergy of Madness - Complete");
        #endregion

        #region CoreFireIsland
        FI.CompleteFireIsland();
        Core.Logger($"Saga: Fireisland Maps - Complete");
        #endregion

        #region IsleOfFotia
        CoreIsleOfFotia.CompleteALL();
        Core.Logger($"Saga: Isle of Fotia - Complete");
        #endregion

        #region Legion
        #region CoreDageTheEvilIsland
        DageIsland.CompleteDageTheEvilIslandStory();
        Core.Logger($"Saga: DageTheEvil island Maps - Complete");
        #endregion

        DageChallengeStory.DageChallengeQuests();
        Core.Logger($"Story: Dage Challenge - Complete");

        DarkWar.DarkWarLegion();
        DarkWar.DarkWarNation();
        Core.Logger($"Story: Dark War - Complete");

        SeraphicWar_Story.SeraphicWar_Questline();
        Core.Logger($"Story: Seraphic War - Complete");

        SevenCircles.CirclesWar();
        Core.Logger($"Story: Seven Circles - Complete");

        WorldSoul.WorldSoulQuests();
        Core.Logger($"Story: World Soul - Complete");
        #endregion

        #region Nation
        Bamboozle.BamboozleQuest();
        Core.Logger($"Story: Bamboozle - Complete");

        CitadelRuins.DoAll();
        Core.Logger($"Story: Citadel Ruins - Complete");

        Fiendshard_Story.Fiendshard_Questline();
        Core.Logger($"Story: Fiendshard - Complete");

        FiendPast.DoAll();
        Core.Logger($"Story: Fiend Past - Complete");

        Tercessuinotlim.JadziaQuests();
        Core.Logger($"Story: Tercessuinotlim - Complete");
        #endregion

        #region QoM
        QOM.CompleteEverything();
        Core.Logger($"Saga: Queen of Monsters - Complete");


        BrightOak.doall();
        Core.Logger($"Story: BrightOak - Complete");

        CelestialArena.Arena1to10();
        CelestialArena.Arena11to20();
        CelestialArena.Arena21to29();
        Core.Logger($"Story: Celestial Arena - Complete");

        CelestialPast.CompleteCeletialPast();
        Core.Logger($"Story: Celestial Past - Complete");

        GoldenArena.StoryLine();
        Core.Logger($"Story: GoldenArena - Complete");

        LivingDungeon.LivingDungeonStory();
        Core.Logger($"Story: LivingDungeon - Complete");

        OrbHunt.SagaName();
        Core.Logger($"Story: Orb Hunt - Complete");

        QueenBattle.StoryLine();
        Core.Logger($"Story: QueenBattle - Complete");
        #endregion

        #region Seppy
        SeppySaga.CompleteALL();
        Core.Logger($"Saga: Sepulchure - Complete");
        #endregion

        #region Shadows Of Chaos
        CoreSoC.CompleteCoreSoC();
        Core.Logger($"Saga: Shadows of Chaos - Complete");
        #endregion

        #region Shadow Of War
        SOW.CompleteCoreSoW();
        Core.Logger($"Saga: Shadow of War [Part1&2] - Complete");
        #endregion

        #region Summer 2015 AdventureMap
        CoreSummer.DoAll();
        Core.Logger($"Saga: Summer 2015 AdventureMap - Complete");
        #endregion

        #region ToD
        TOD.CompleteToD();
        Core.Logger($"Saga: Throne of Darkness - Complete");
        #endregion

        #endregion

        #region Standalone

        Artixpointe.OmniArtifact();
        Core.Logger($"Story: Artixpointe - Complete");

        ArtixWedding.ArtixWeddingComplete();
        Core.Logger($"Story: ArtixWedding - Complete");


        Banished.doall();
        Core.Logger($"Story: Banished - Complete");

        BattleUnder.BattleUnderAll();
        Core.Logger($"Story: BattleUnder - Complete");

        BloodMoon.BloodMoonSaga();
        Core.Logger($"Story: Blood Moon - Complete");

        Borgars.BorgarQuests();
        Core.Logger($"Story: Borgars - Complete");


        CastleTunnels.StoryLine();
        Core.Logger($"Story: CastleTunnels - Complete");

        CelestialPast.CompleteCeletialPast();
        Core.Logger($"Story: CelestialPast - Complete");

        CruxShip.StoryLine();
        Core.Logger($"Story: CruxShip - Complete");


        DjinnGateStory.DjinnGate();
        Core.Logger($"Story: Djinn Gate - Complete");

        DjinnGuard.CompleteDjinnGuard();
        Core.Logger($"Story: Djinn Guard - Complete");

        DoomVaultA.StoryLine();
        Core.Logger($"Story: Doom Vault B - Complete");
        DoomVaultB.StoryLine();
        Core.Logger($"Story: Doom Vault A - Complete");

        DragonFableOrigins.DragonFableOriginsAll();
        Core.Logger($"Saga: Dragon Fable Origins - Complete");

        DreadForest.Storyline();
        Core.Logger($"Story: Dread Forest - Complete");

        DreamPalace.CompleteDreamPalace();
        Core.Logger($"Story: Dream Palace - Complete");


        Eden.EdenStoryline();
        Core.Logger($"Story: Eden - Complete");

        EtherStormWastes.StoryLine();
        Core.Logger($"Story: Ether Storm Wastes - Complete");

        ExaltiaTower.StoryLine();
        Core.Logger($"Story: Exaltia Tower - Complete");


        FireHouse.Storyline();
        Core.Logger($"Story: Fire House - {(Core.isCompletedBefore(1564) ? "Complete" : "Skipped")}");

        FrozenNorthlands.Storyline();
        Core.Logger($"Story: Frozen Northlands - Complete");


        Gamehaven.Storyline();
        Core.Logger($"Story: Game Haven - Complete");

        GlaceraStory.DoAll();
        Core.Logger($"Story: Glacera - Complete");

        J6Saga.J6();
        Core.Logger($"Sage: J6 - Complete");


        LightoviaCave.LightoviaCaveQuests();
        Core.Logger($"Story: LightoviaCave - Complete");


        Manor.StoryLine();
        Core.Logger($"Story: Manor - Complete");

        MustyCave.Storyline();
        Core.Logger($"Story: MustyCave - Complete");


        NytheraSaga.DoAll();
        Core.Logger($"Saga: Nythera - Complete");


        Oddities.StoryLine();
        Core.Logger($"Story: Oddities - {(Core.isCompletedBefore(8667) ? "Complete" : "Member Only")}");


        PoisonForest.StoryLine();
        Core.Logger($"Story: PoisonForest - Complete");


        RavenlossSaga.DoAll();
        Core.Logger($"Saga: RavenLoss - Complete");


        ShadowSlayerK.Storyline();
        Core.Logger($"Story: Shadow Slayer K - Complete");

        ShadowVault.StoryLine();
        Core.Logger($"Story: ShadowVault - Complete");

        ShadowVoid.ShadowVoidQuests();
        Core.Logger($"Story: ShadowVoid - Complete");

        Shinkansen.Storyline();
        Core.Logger($"Story: Shinkansen - Complete");

        SkyGuardSaga.DoAll();
        Core.Logger($"Saga: SkyGuard Saga - Complete");

        StarSinc.StarSincQuests();
        Core.Logger($"Story: Star Sinc - Complete");

        SuperDeath.StoryLine();
        Core.Logger($"Story: SuperDeath - Complete");


        Table.DoAll();
        Core.Logger($"Story: Table - Complete");

        ThirdSpell.StoryLine();
        Core.Logger($"Story: Third Spell - Complete");

        TitanAttackStory.DoAll();
        Core.Logger($"Story: Titan Attack - Complete");

        TowerOfDoom.TowerProgress();
        Core.Logger($"Story: Tower Of Doom - Complete");

        Trygve.Storyline();
        Core.Logger($"Story: Trygve - Complete");



        UnderGroundLab.partofundergroundlabb();
        Core.Logger($"Story: Underground Lab - Complete?");


        LairWar.doAll();
        Core.Logger($"Story: Vasalkar Lair War - Complete");


        Xans.DoAll();
        Core.Logger($"Story: Xans Lair - Complete");


        Yokai.Quests();
        Core.Logger($"Story: Yokai - Complete");

        #endregion
    }
}