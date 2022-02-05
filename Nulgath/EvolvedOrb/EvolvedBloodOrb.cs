//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class EvolvedBloodOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        GetEvolvedBloodOrb();
        Core.SetOptions(false);
    }
    
    public void GetEvolvedBloodOrb()
    {
        if(Core.CheckInventory("Evolved Blood Orb"))
            return;
        Nulgath.ApprovalAndFavor(200, 0);
        Nulgath.FarmUni13(3);
        Nulgath.FarmVoucher(false);
        Nulgath.FarmTotemofNulgath(10);
        Nulgath.FarmDiamondofNulgath(30);
        Core.BuyItem("archportal", 1211, "Evolved Blood Orb");
        Core.Logger($"Done, you have Blood ball");
    }

}