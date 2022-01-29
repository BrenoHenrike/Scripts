//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class test2
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

        GetAllOrb();

		Core.SetOptions(false);
	}

    public void GetAllOrb()
    {
        EvolvedBloodOrb();
        EvolvedHexOrb();
        EvolvedShadowOrb();
        Core.Logger($"Done, you have the balls");
    }
    
    public void EvolvedBloodOrb()
    {
        if(Core.CheckInventory("Evolved Blood Orb"))
            return;
        Nulgath.ApprovalAndFavor(200, 0);
        Nulgath.FarmUni13(3);
        Nulgath.FarmVoucher(false);
        Nulgath.FarmTotemofNulgath(10);
        Nulgath.FarmDiamondofNulgath(30);
        Core.BuyItem("archportal", 1211, "Evolved Blood Orb");
        Core.Logger($"Buy Evolved Blood Orb");
    }
    public void EvolvedHexOrb()
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
        Core.Logger($"Buy Evolved Hex Orb");
    }
    public void EvolvedShadowOrb()
    {
        if(Core.IsMember == false)
            return;
        else if(Core.CheckInventory("Evolved Shadow Orb"))
            return;
        Nulgath.ApprovalAndFavor(200, 0);
        Nulgath.FarmUni13(3);
        Nulgath.FarmVoucher(false);
        Nulgath.FarmVoucher(true);
        Nulgath.FarmTotemofNulgath(10);
        Nulgath.FarmGemofNulgath(20);
        Core.BuyItem("archportal", 1211, "Evolved Shadow Orb");
        Core.Logger($"Buy Evolved Shadow Orb");
    }
}