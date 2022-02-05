//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/JoinLegion[UndeadWarrior].cs
using RBot;

public class LegionFealty3
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public CoreFarms Farm = new CoreFarms();
    public JoinLegion JoinLegion = new JoinLegion();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        ExaltedCrown();

        Core.SetOptions(false);
    }

    public void ExaltedCrown(int quant = 10)
    {
        if (Core.CheckInventory("Exalted Crown", quant))
            return;

        JoinLegion.JoinLegionQuests();

        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF3);

        int i = 1;
        Core.Logger($"Farming {quant} Exalted Crown");
        while (!Core.CheckInventory("Exalted Crown", quant))
        {
            Core.EnsureAccept(6899);

            Farm.Gold(500000);
            Core.BuyItem("underworld", 216, "Hooded Legion Cowl");

            Legion.FarmLegionToken(4000);

            Legion.ApprovalAndFavor(0, 300);

            Legion.EmblemofDage(1);

            Legion.DiamondTokenofDage(30);

            Legion.DarkToken(100);

            Core.EnsureComplete(6899);
            Bot.Player.Pickup("Exalted Crown");
            Core.Logger($"Completed x{i++}");
        }
    }
}