//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/01Vaden(CastleofBones).cs
//cs_include Scripts/Story/ThroneofDarkness/02aXeven(ParadoxPortal).cs
//cs_include Scripts/Story/ThroneofDarkness/03aZiri(BaconCatFortress).cs
//cs_include Scripts/Story/ThroneofDarkness/04aPax(DeathPit).cs
//cs_include Scripts/Story/ThroneofDarkness/05aSekt(ShiftingPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/06aScarletta(ShatterGlassMaze).cs
//cs_include Scripts/Story/ThroneofDarkness/06bScarletta(TowerofMirrors).cs
using RBot;

public class CompleteThroneOfDarknessSaga
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CastleofBones s01 = new CastleofBones();
    public ParadoxPortal s02a = new ParadoxPortal();
    public FlyingBaconCatFortress s03a = new FlyingBaconCatFortress();
    public DeathPitArena s04a = new DeathPitArena();
    public ShiftingPyramid s05a = new ShiftingPyramid();
    public FourthDimensionalPyramid s05b = new FourthDimensionalPyramid();
    public HedgeMaze s06a = new HedgeMaze();
    public TowerofMirrors s06b = new TowerofMirrors();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteToD();

        Core.SetOptions(false);
    }

    public void CompleteToD()
    {
        Core.EquipClass(ClassType.Solo);

        Core.Logger("Castle of Bones");
        s01.CastleofBonesSaga();
        Core.Logger("Paradox Portal");
        s02a.ParadoxPortalSaga();
        Core.Logger("Flying BaconCat Fortress");
        s03a.FlyingBaconCatFortressSaga();
        Core.Logger("Death Pi tArena");
        s04a.DeathPitArenaSaga();
        Core.Logger("Shifting Pyramid");
        s05a.ShiftingPyramidSaga();
        Core.Logger("Fourth Dimensional Pyramid");
        s05b.FourthDimensionalPyramidSaga();
        Core.Logger("HedgeMaze");
        s06a.HedgeMaze_Questline();
        Core.Logger("Tower of Mirrors");
        s06b.TowerofMirrorsSaga();
    }
}
