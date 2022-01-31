//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/VHL/VoidHighlordsChallenge.cs
//cs_include Scripts/Nulgath/VHL/VoidCrystals.cs
//cs_include Scripts/Farm/IcestormArenaXP[1-100].cs 
using RBot;
public class Void_Highlord_AIO
{
    public string[] Rebank = { };


    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public VoidHighlordsChallenge VHL = new VoidHighlordsChallenge();
    public VoidCrystals ShinyRocks = new VoidCrystals();
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    public CoreNulgath Nulgath = new CoreNulgath();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Core.CheckInventory("Void Highlord"))
            return;

        Core.Logger("Level Check");
        Farm.Experience(80);

        Core.Logger("Roentgenium [Start]");
        Roentgenium();

        Core.Logger("Crystals [Start]");
        Crystals();

        Core.Logger("Purchase VHL [Start]");
        BuyVHL();

        //    Bot.Player.EquipItem("Void Highlord");
        //    Farm.IcestormArena(rankUpClass: true);    // When auto-enhance gets released/mde un // these 2 lines

        Core.Logger("All Finished");
    }


    public void Roentgenium(int quant = 15)
    {

        if (Core.CheckInventory("Roentgenium of Nulgath", quant))
            return;
            
        Core.AddDrop("Roentgenium of Nulgath");

        while (!Core.CheckInventory("Roentgenium of Nulgath", quant))
        {
            VHL.Challenge();
        }
    }

    public void Crystals()
    {
        ShinyRocks.VHLCrystals();
        
        Core.AddDrop("Void Crystal A");
        Core.AddDrop("Void Crystal B");

        Core.BuyItem("tercessuinotlim", 1355, "Void Crystal A");
        Core.BuyItem("tercessuinotlim", 1355, "Void Crystal B");
    }

    public void BuyVHL()
    {

        if (Core.CheckInventory("Void Highlord"))
            return;
            
        Core.AddDrop("Void Highlord");

        Core.JoinTercessuinotlim();
        Core.BuyItem("tercessuinotlim", 1355, "Void Highlord");
    }
}