//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class ShadowLegacyofNulgath
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetSLoN();

        Core.SetOptions(false);
    }

    public void GetSLoN()
    {
        if (Core.CheckInventory("Shadow Legacy of Nulgath"))
            return;

        Core.AddDrop("Shadow Legacy of Nulgath", "Letter from Asuka and Tendou");

        Nulgath.ApprovalAndFavor(100, 0);
        Nulgath.Supplies("Voucher of Nulgath (non-mem)");
        Nulgath.EssenceofNulgath(100);
        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Hadean Onyx of Nulgath", 1, false);
        Core.HuntMonster("Citadel", "Burning Witch", "Letter from Asuka and Tendou");
        Farm.Gold(3000000);
        Core.BuyItem("archportal", 1211, "Shadow Legacy of Nulgath");
        Bot.Wait.ForPickup("Shadow Legacy of Nulgath");
    }
}