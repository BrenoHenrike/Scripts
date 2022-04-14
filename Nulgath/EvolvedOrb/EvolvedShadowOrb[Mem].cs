//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class EvolvedShadowOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        GetEvolvedShadowOrb();
        Core.SetOptions(false);
    }

    public void GetEvolvedShadowOrb()
    {
        if (!Core.IsMember)
            return;
        else if (Core.CheckInventory("Evolved Shadow Orb"))
            return;
        Nulgath.ApprovalAndFavor(200, 0);
        Nulgath.FarmUni13(3);
        Nulgath.FarmVoucher(false);
        Nulgath.FarmVoucher(true);
        Nulgath.FarmTotemofNulgath(10);
        Nulgath.FarmGemofNulgath(20);
        Core.BuyItem("archportal", 1211, "Evolved Shadow Orb");
        Core.Logger($"Done, you have Shadow ball");
    }

}