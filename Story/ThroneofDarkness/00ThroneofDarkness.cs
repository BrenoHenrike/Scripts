//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Story/ThroneofDarkness/01Vaden(CastleofBones).cs
//cs_include Scripts/Story/ThroneofDarkness/02aXeven(ParadoxPortal).cs
//cs_include Scripts/Story/ThroneofDarkness/03aZiri(BaconCatFortress).cs
//cs_include Scripts/Story/ThroneofDarkness/04aPax(DeathPit).cs
//cs_include Scripts/Story/ThroneofDarkness/05aSekt(ShiftingPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/06aScarletta(ShatterGlassMaze).cs
//cs_include Scripts/Story/ThroneofDarkness/06bScarletta(TowerofMirrors.cs
using RBot;

public class CompleteThroneOfDarknessSaga
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    public CastleofBones s01 = new CastleofBones();
    public ParadoxPortal s02 = new ParadoxPortal();
    public FlyingBaconCatFortress s03 = new FlyingBaconCatFortress();
    public DeathPitArena s04 = new DeathPitArena();
    public ShiftingPyramid s05 = new ShiftingPyramid();
    public FourthDimensionalPyramid s06 = new FourthDimensionalPyramid();
    public HedgeMaze s07 = new HedgeMaze();
    public TowerofMirrors s08 = new TowerofMirrors();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteToD();

        Core.SetOptions(false);
    }

    public void CompleteToD()
    {
        Core.Logger("Castle of Bones");
        s01.CastleofBonesSaga();
        Core.Logger("Paradox Portal");
        s02.ParadoxPortalSaga();
        Core.Logger("Flying BaconCat Fortress");
        s03.FlyingBaconCatFortressSaga();
        Core.Logger("Death Pi tArena");
        s04.DeathPitArenaSaga();
        Core.Logger("Shifting Pyramid");
        s05.ShiftingPyramidSaga();
        Core.Logger("Fourth Dimensional Pyramid");
        s06.FourthDimensionalPyramidSaga();
        Core.Logger("HedgeMaze");
        s07.HedgeMaze_Questline();
        Core.Logger("Tower of Mirrors");
        s08.TowerofMirrorsSaga();
    }
}
