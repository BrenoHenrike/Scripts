//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Good/BloD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Story/Tutorial.cs
//cs_include Scripts/Evil/NecroticSwordOfDoom.cs
//cs_include Scripts/Story/Battleunder.cs
//cs_include Scripts/Story/TowerofDoom.cs 
//cs_include Scripts/Other/BloodSorceress.cs
//cs_include Scripts/Story/HedgeMaze.cs
//cs_include Scripts/Other/Necromancer.cs
//cs_include Scripts/VHL_Combo.cs
//cs_include Scripts/Nulgath/VHL/VoidCrystals.cs
//cs_include Scripts/Nulgath/VHL/VoidHighlordsChallenge.cs

using RBot;

public class BecauseyNot //By Tato
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    public CoreNulgath Nulgath = new CoreNulgath();
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreSDKA SDKA = new CoreSDKA();
    public NSOD NSOD = new NSOD();
    public Tutorial Tutorial = new Tutorial();
    public HedgeMaze Maze = new HedgeMaze();
    public BloodSorceress Blood = new BloodSorceress();
    public Void_Highlord_AIO VHL_AIO = new Void_Highlord_AIO();
    public BattleUnder BattleUnder = new BattleUnder();
    public TowerOfDoom TOD = new TowerOfDoom();

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Bot.Player.Level < 10)
        { Tutorial.Badges(); }
        Farm.IcestormArena(level: 50, rankUpClass: true);
        Maze.HedgeMaze_Questline();
        Blood.GetBSorc();
        BattleUnder.BattleUnderAll();
        Farm.IcestormArena(80);
        BLOD.DoAll();
        TOD.TowerProgress();
        Farm.IcestormArena(100);
        SDKA.DoAll();
        VHL_AIO.DoAll();
    }


}