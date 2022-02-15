//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/06aScarletta(ShatterGlassMaze).cs
//cs_include Scripts/Story/ThroneofDarkness/06bScarletta(TowerofMirrors).cs
//cs_include Scripts/Other/Classes/BloodSorceress.cs
using RBot;

public class ScarletSorceress
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm => new CoreFarms();
    public TowerofMirrors TOM = new TowerofMirrors();
    public BloodSorceress BS = new BloodSorceress();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetSSorc();

        Core.SetOptions(false);
    }

    public void GetSSorc(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Scarlet Sorceress"))
            return;

        Core.AddDrop("Scarlet Sorceress");

        TOM.TowerofMirrorsSaga();
        BS.GetBSorc(false);
        Farm.Experience(50);

        Core.ChainComplete(6236);
        Bot.Wait.ForPickup("Scarlet Sorceress");

        if (rankUpClass)
            Farm.rankUpClass("Scarlet Sorceress");
    }
}
