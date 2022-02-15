//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class BloodSorceress
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm => new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBSorc();

        Core.SetOptions(false);
    }

    public void GetBSorc(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Blood Sorceress"))
            return;

        Core.HuntMonster("towerofmirrors", "Scarletta", "Blood Sorceress", isTemp: false);
        if (rankUpClass)
            Farm.rankUpClass("Blood Sorceress");
    }
}