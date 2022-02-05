//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/LordsofChaos/5Wolfwing(Darkovia).cs
using RBot;

public class Lycan
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public SagaDarkovia Darko = new SagaDarkovia();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetLycan();

        Core.SetOptions(false);
    }

    public void GetLycan(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Lycan"))
            return;

        Darko.CompleteSaga();
        Farm.LycanREP();

        Core.BuyItem("lycan", 161, "Lycan");

        if (rankUpClass)
            Farm.rankUpClass("Lycan");
    }
}