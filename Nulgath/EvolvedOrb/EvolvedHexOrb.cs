//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class EvolvedHexOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        GetEvolvedHexOrb();
        Core.SetOptions(false);
    }
    
    public void GetEvolvedHexOrb()
    {
        if(Core.CheckInventory("Evolved Hex Orb"))
            return;
        Nulgath.ApprovalAndFavor(200, 0);
        Nulgath.FarmUni13(3);
        Nulgath.FarmVoucher(false);
        Nulgath.FarmTotemofNulgath(10);
        Nulgath.FarmDarkCrystalShard(30);
        Nulgath.Supplies("Tainted Gem", 30);
        Core.BuyItem("archportal", 1211, "Evolved Hex Orb");
        Core.Logger($"Done, you have Hex ball");
    }

}