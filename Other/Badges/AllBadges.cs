/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Asylum.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Story/ArtixWedding.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/CruxShip.cs
//cs_include Scripts/Story/EtherstormWastes.cs
//cs_include Scripts/Story/RavenlossSaga.cs
//cs_include Scripts/Story/ShadowVault.cs
//cs_include Scripts/Story/SkyGuardSaga.cs
//cs_include Scripts/Story/UnderGroundLab.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/GoldenArena.cs
//cs_include Scripts/Story/Cornelis[mem].cs
//cs_include Scripts/Other/Badges/6thBirthdaySavior.cs
//cs_include Scripts/Other/Badges/BattleBabysitter.cs
//cs_include Scripts/Other/Badges/BattleConVIP.cs
//cs_include Scripts/Other/Badges/CelestialChampion.cs
//cs_include Scripts/Other/Badges/ChaosPuppetMaster.cs
//cs_include Scripts/Other/Badges/Committed.cs
//cs_include Scripts/Other/Badges/ConZombieSlayer.cs
//cs_include Scripts/Other/Badges/CornelisReborn.cs
//cs_include Scripts/Other/Badges/CtrlAltDelMemberBadge.cs
//cs_include Scripts/Other/Badges/DerpMoosefishBadge.cs
//cs_include Scripts/Other/Badges/DesolichFreed.cs
//cs_include Scripts/Other/Badges/GoldenLaurel.cs
//cs_include Scripts/Other/Badges/HordeZombieSLAYER.cs
//cs_include Scripts/Other/Badges/LordOfTheWeddingRing.cs
//cs_include Scripts/Other/Badges/MoglinPunter.cs
//cs_include Scripts/Other/Badges/MummySlayerAndCruxShadowsDefender.cs
//cs_include Scripts/Other/Badges/RavenlossWarAndChampion.cs
//cs_include Scripts/Other/Badges/ShadowVaultChampion.cs
//cs_include Scripts/Other/Badges/SkyPirateSlayerBadge.cs
//cs_include Scripts/Other/Badges/StoneCold.cs
//cs_include Scripts/Other/Badges/TableFlipper.cs
//cs_include Scripts/Other/Badges/YouMadBroBadge.cs
//cs_include Scripts/Other/Badges/VoidHighlordBadge.cs
//cs_include Scripts/Other/Badges/StoryARC.cs
//cs_include Scripts/Other/Badges/JusticeSquad.cs
//cs_include Scripts/Other/Badges/ThiefofChaos.cs
//cs_include Scripts/Other/Badges/Goal.cs
//cs_include Scripts/Other/Badges/UltraCarnax.cs
//cs_include Scripts/Story/MagicThief.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
//cs_include Scripts/Seasonal/Frostvale/FrostvaleBadges.cs
using Skua.Core.Interfaces;

public class AllBadges
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public CornelisRebornbadge CRB = new();
    public DerpMoosefishBadge DMF = new();
    public SkyPirateBadge SPB = new();
    public YouMadBroBadge YMBB = new();
    public MoglinPunter MPB = new();
    public CtrlAltDelMemberBadge CAD = new();
    public BirthdaySavior BS = new();
    public BattleBabysitter BB = new();
    public BattleConVIP BCV = new();
    public CelestialArenaChampion CAC = new();
    public ChaosPuppetMaster CPM = new();
    public Committed C = new();
    public ConZombieSlayer CZS = new();
    public DesolichFreed DF = new();
    public GoldenLaurel GL = new();
    public HordeZombieSLAYER HZS = new();
    public LordOfTheWeddingRing LOTWR = new();
    public MummySlayerAndCruxShadowsDefender MSACSD = new();
    public RavenlossWarAndChampion RWAC = new();
    public ShadowVaultChampion SVC = new();
    public StoneCold SC = new();
    public TableFlipper TF = new();
    public VoidHighlordBadge VHL = new();
    public StoryArcBadge SA = new();
    public JusticeSquadBadge JS = new();
    public ThiefofChaosBadge ToC = new();
    public UltraCarnaxBadge UC = new();
    public GoalBadge G = new();
    public FrostvaleBadges FV = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }
    
    public void DoAll()
    {
        CRB.Badge();
        SPB.Badge();
        MPB.Badge();
        CAD.Badge();
        BS.Badge();
        BB.Badge();
        BCV.Badge();
        CAC.Badge();
        CPM.Badge();
        C.Badge();
        CZS.Badge();
        DF.Badge();
        GL.Badge();
        HZS.Badge();
        LOTWR.Badge();
        MSACSD.Badge();
        RWAC.Badge();
        SVC.Badge();
        SC.Badge();
        TF.Badge();
        DMF.Badge();
        YMBB.Badge();
        VHL.Badge();
        SA.Badge();
        JS.Badge();
        ToC.Badge();
        UC.Badge();
        G.Badge();
        FV.Badges();
        //add more as they are made.
    }
}
