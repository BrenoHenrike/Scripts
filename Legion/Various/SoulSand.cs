//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
using RBot;

public class AnotherOneBitesTheDust
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public CoreAdvanced Adv = new CoreAdvanced();
    public SeraphicWar_Story SeraphicWar = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SoulSand();

        Core.SetOptions(false);
    }

    public void SoulSand(int quant = 50)
    {
        if (Core.CheckInventory("Soul Sand", quant))
            return;

        Core.AddDrop("Soul Sand");
        int i = 1;

        Farm.Experience(65);

        Core.Logger($"Farming {quant} Soul Sand");
        while (!Core.CheckInventory("Soul Sand", quant))
        {
            Core.EnsureAccept(7991);
            Farm.BattleUnderB("Bone Dust", 333);
            Legion.ApprovalAndFavor(0, 400);
            SeraphicWar.SeraphicWar_Questline();
            Legion.DarkToken(80);
            Core.EnsureComplete(7991);
            Core.Logger($"Completed x{i++}");
        }
    }
}