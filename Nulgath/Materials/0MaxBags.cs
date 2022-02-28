//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class MaxItems
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        MaxBags();

        Core.SetOptions(false);
    }

    public void MaxBags()
    {
        Core.AddDrop(Nulgath.bagDrops);

        Nulgath.FarmBloodGem();
        Nulgath.FarmDarkCrystalShard();
        Nulgath.FarmDiamondofNulgath();
        Nulgath.FarmGemofNulgath();
        Nulgath.FarmTotemofNulgath();
        Nulgath.FarmUni10();
        Nulgath.FarmUni13(13);
        Nulgath.FarmVoucher(true);
        Nulgath.FarmVoucher(false);
    }
}