//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoidWarlock
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();

    public readonly string[] Rewards =
    {
        "Void Warlock",
        "Void Warlock Scythe",
        "Void Warlock Staff",
        "Void Warlock Overfiend Blade",
        "Void Warlock Overfiend Blade Pet",
        "Void Warlock Crown"
    };
    public readonly string[] Rewards2 =
    {
        "Void Warlock Helm",
        "Void Warlock Hood",
        "Void Warlock Cape",
        "Void Warlock Tendrils Cape",
        "Void Warlock Tendrils",
        "Void Warlock Horns",
    };
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop(Rewards);
        Core.AddDrop(Rewards2);
        Core.AddDrop("Brittney's Winter Diamond");

        int i = 1;
        Core.Logger("Starting [Tools for the Job] Quest");
        while(!Core.CheckInventory(Rewards, toInv: false))
        {
            Nulgath.FarmUni13(2);
            Nulgath.FarmVoucher(false);
            Nulgath.FarmBloodGem(90);
            Nulgath.SwindleBulk(100);
            Core.HuntMonster("northlands", "Aisha's Drake", "Brittney's Winter Diamond", 1, false);

            Core.EnsureAccept(6683);
            Core.EnsureCompleteChoose(6683);
            Core.Logger($"Completed x{i++}");
        }
        Core.Logger("All drops acquired from [Tools for the Job] Quest");
        i = 1;
        Core.Logger("Starting [Corrupted Touch] Quest");
        while(!Core.CheckInventory(Rewards2, toInv: false))
        {
            Nulgath.FarmUni13();
            Nulgath.FarmVoucher(true);
            Nulgath.FarmDiamondofNulgath(75);
            Nulgath.FarmGemofNulgath(100);
            Nulgath.SwindleBulk(75);
            Nulgath.ApprovalAndFavor(1000, 0);
            Core.HuntMonster("northlands", "Aisha's Drake", "Brittney's Winter Diamond", 1, false);

            Core.EnsureAccept(6684);
            Core.EnsureCompleteChoose(6684);
            Core.Logger($"Completed x{i++}");
        }
        Core.Logger("All drops acquired from [Corrupted Touch] Quest");
        Core.SetOptions(false);
    }
}