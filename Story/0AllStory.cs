//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs

//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs

//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/Doomwood/Necrodungeon.cs
//cs_include Scripts/Story/Doomwood/Necrotower.cs

//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs

//cs_include Scripts/Story/Laguna/00CompleteAllLaguna.cs
//cs_include Scripts/Story/Laguna/01DualPlane.cs
//cs_include Scripts/Story/Laguna/02ChaosAmulet.cs
//cs_include Scripts/Story/Laguna/03LagunaBeach.cs
//cs_include Scripts/Story/Laguna/04Laguna.cs

//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/DageChallengeStory.cs
//cs_include Scripts/Story/Legion/DarkAlliance.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
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
//cs_include Scripts/Story/Nation/Originul.cs

//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs

//cs_include Scripts/Story/SepulchureSaga/00CompleteSepulchureSaga.cs
//cs_include Scripts/Story/SepulchureSaga/01SepulchurePrequelAlden.cs
//cs_include Scripts/Story/SepulchureSaga/02SepulchurePrequelLynaria.cs
//cs_include Scripts/Story/SepulchureSaga/03SepulchuresRise.cs
//cs_include Scripts/Story/SepulchureSaga/04ShadowfallRise.cs

//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs

//cs_include Scripts/Story/Akriloth.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Story/Banished.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/BloodMoon.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ChaosQueenBeleen.cs
//cs_include Scripts/Story/Collection.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Story/DoomVault.cs
//cs_include Scripts/Story/DoomVaultB.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/DreadForest.cs
//cs_include Scripts/Story/EtherstormWastes.cs
//cs_include Scripts/Story/ExaltiaTower.cs
//cs_include Scripts/Story/FiendPast.cs
//cs_include Scripts/Story/FireHouse[SeasonalOrMem].cs
//cs_include Scripts/Story/GameHaven.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/Oddities.cs
//cs_include Scripts/Story/OrbHunt.cs
//cs_include Scripts/Story/Phoenixrise.cs
//cs_include Scripts/Story/Ruinedcrown.cs
//cs_include Scripts/Story/Shinkansen.cs
//cs_include Scripts/Story/SkyPirate.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/Table.cs
//cs_include Scripts/Story/TitanAttack.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/Tutorial.cs
//cs_include Scripts/Story/UltraTyndariusPrereqs.cs
//cs_include Scripts/Story/UnderGroundLab.cs
//cs_include Scripts/Story/VasalkarLairWar.cs
//cs_include Scripts/Story/WarfuryTraining.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Story/Yokai.cs
using RBot;

public class AllStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();

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
    // Laguna
    public CompleteLaguna Laguna = new();
    // Legion
    public DageChallengeStory DageChallengeStory = new();
    public DarkAlliance_Story DarkAlliance_Story = new();
    public DarkWarLegionandNation DarkWar = new();
    public DarkAlly_Story DarkAlly_Story = new();
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
    public Fiendshard_Story Fiendshard_Story = new();
    public Originul_Story Originul_Story = new();
    // Queen of Monsters
    public CoreQOM QOM => new();
    public BrightOak BrightOak = new();
    public CelestialArenaQuests CelestialArena = new();
    public LivingDungeon LivingDungeon = new();
    // Sepulchure Saga
    public CompleteSepulchureSaga SeppySaga = new();
    // Throne of Darkness
    public CoreToD TOD = new();
    // Standalone
    public Akriloth Akriloth = new();
    public Artixpointe Artixpointe = new();
    public Banished Banished = new();
    public BattleUnder BattleUnder = new();
    public BloodMoon BloodMoon = new();
    public Borgars Borgars = new();
    public ChaosQueenBeleen ChaosQueenBeleen = new();
    public Collection Collection = new();
    public DjinnGateStory DjinnGateStory = new();
    public DoomVaultA DoomVaultA = new();
    public DoomVaultB DoomVaultB = new();
    public DragonFableOrigins DragonFableOrigins = new();
    public DreadForest DreadForest = new();
    public EtherStormWastes EtherStormWastes = new();
    public ExaltiaTower ExaltiaTower = new();
    public Gamehaven Gamehaven = new();
    public FireHouse FireHouse = new();
    public FiendPast FiendPast = new();
    public GlaceraStory GlaceraStory = new();
    public J6Saga J6Saga = new();
    public Oddities Oddities = new();
    public OrbHunt OrbHunt = new();
    public PhoenixriseStory PhoenixriseStory = new();
    public RuinedCrown RuinedCrown = new();
    public Shinkansen Shinkansen = new();
    public SkyPirateQuests SkyPirateQuests = new();
    public StarSinc StarSinc = new();
    public Table Table = new();
    public TitanAttackStory TitanAttackStory = new();
    public TowerOfDoom TowerOfDoom = new();
    public Tutorial Tutorial = new();
    public Tyndarius Tyndarius = new();
    public UnderGroundLab UnderGroundLab = new();
    public LairWar LairWar = new();
    public WarTraining WarTraining = new();
    public XansLair Xans = new();
    public YokaiQuests YokaiQuests = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteAll();

        Core.SetOptions(false);
    }

    public void CompleteAll()
    {
        Tutorial.Badges();
        Core.Logger($"Story: Tutorial - Complete");

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

        #region Laguna
        Laguna.CompleteALL();
        Core.Logger($"Story: Laguna - Complete");
        #endregion

        #region Legion
        DageChallengeStory.DageChallengeQuests();
        Core.Logger($"Story: Dage Challenge - Complete");

        DarkAlliance_Story.DarkAlliance_Questline();
        Core.Logger($"Story: Dark Alliance - Complete");

        DarkAlly_Story.DarkAlly_Questline();
        Core.Logger($"Story: Dark Ally - Complete");

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
        #endregion

        #region QoM
        QOM.CompleteEverything();
        Core.Logger($"Saga: Queen of Monsters - Complete");

        BrightOak.doall();
        Core.Logger($"BrightOak Complete");

        CelestialArena.Arena1to10();
        CelestialArena.Arena11to20();
        CelestialArena.Arena21to29();
        Core.Logger($"Story: Celestial Arena - Complete");

        LivingDungeon.LivingDungeonStory();
        Core.Logger($"LivingDungeon Complete");
        #endregion

        #region Seppy
        SeppySaga.CompleteALL();
        Core.Logger($"Story: Sepulchure Saga - Complete");
        #endregion

        #region ToD
        TOD.CompleteToD();
        Core.Logger($"Saga: Throne of Darkness - Complete");
        #endregion

        #region Standalone
        Akriloth.Storyline();
        Core.Logger($"Story: Akriloth - Complete");

        Artixpointe.OmniArtifact();
        Core.Logger($"Story: Artixpointe - Complete");

        Banished.doall();
        Core.Logger($"Story: Banished - Complete");

        BattleUnder.BattleUnderAll();
        Core.Logger($"Story: BattleUnder - Complete");

        BloodMoon.BloodMoonSaga();
        Core.Logger($"Story: Blood Moon - Complete");

        Borgars.BorgarQuests();
        Core.Logger($"Story: Borgars - Complete");

        Collection.CollectionStory();
        Core.Logger($"Story: Collection - Complete");

        DjinnGateStory.DjinnGate();
        Core.Logger($"Story: Djinn Gate - Complete");

        DoomVaultA.StoryLine();
        Core.Logger($"Story: Doom Vault B - Complete");
        DoomVaultB.StoryLine();
        Core.Logger($"Story: Doom Vault A - Complete");

        DragonFableOrigins.DragonFableOriginsAll();
        Core.Logger($"Saga: Dragon Fable Origins - Complete");

        DreadForest.Storyline();
        Core.Logger($"Story: Dread Forest - Complete");

        EtherStormWastes.StoryLine();
        Core.Logger($"Story: Ether Storm Wastes - Complete");

        ExaltiaTower.StoryLine();
        Core.Logger($"Story: Exaltia Tower - Complete");

        FiendPast.DoAll();
        Core.Logger($"Story: Fiend Past - Complete");

        FireHouse.Storyline();
        Core.Logger($"Story: Fire House - {(Core.isCompletedBefore(1564) ? "Complete" : "Skipped")}");

        Gamehaven.Storyline();
        Core.Logger($"Story: Game Haven - Complete");

        GlaceraStory.DoAll();
        Core.Logger($"Story: Glacera - Complete");

        J6Saga.J6();
        Core.Logger($"Sage: J6 - Complete");

        Oddities.StoryLine();
        Core.Logger($"Story: Oddities - {(Core.isCompletedBefore(8667) ? "Complete" : "Member Only")}");

        OrbHunt.SagaName();
        Core.Logger($"Story: Orb Hunt - Complete");

        PhoenixriseStory.PhoenixAll();
        Core.Logger($"Story: Phoenixrise - Complete");

        RuinedCrown.DoAll();
        Core.Logger($"Story: Ruined Crown - Complete");

        Shinkansen.Storyline();
        Core.Logger($"Story: Shinkansen - Complete");

        SkyPirateQuests.Storyline();
        Core.Logger($"Story: Sky Pirate - Complete");

        StarSinc.StarSincQuests();
        Core.Logger($"Story: Star Sinc - Complete");

        Table.DoAll();
        Core.Logger($"Story: Table - Complete");

        TitanAttackStory.DoAll();
        Core.Logger($"Story: Titan Attack - Complete");

        TowerOfDoom.TowerProgress();
        Core.Logger($"Story: Tower Of Doom - Complete");

        Tyndarius.DoALl();
        Core.Logger($"Story: Tyndarius - Complete");

        UnderGroundLab.partofundergroundlabb();
        Core.Logger($"Story: Underground Lab - Complete?");

        LairWar.doAll();
        Core.Logger($"Story: Vasalkar Lair War - Complete");

        WarTraining.StoryLine();
        Core.Logger($"Story: WarTraining - Complete");

        Xans.DoAll();
        Core.Logger($"Story: Xans Lair - Complete");

        YokaiQuests.Quests();
        Core.Logger($"Story: Yokai - Complete");
        #endregion
    }
}