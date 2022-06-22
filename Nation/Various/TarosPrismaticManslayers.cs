//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
using RBot;
public class TarosPrismaticManslayers
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();
    public TarosManslayer Taro = new TarosManslayer();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        TemptationTest();

        Core.SetOptions(false);
    }

    private string[] Rewards = { "Taro's Prismatic Manslayer", "Taro's Dual Prismatic Manslayers", "Taro's BattleBlade" };

    public void TemptationTest()
    {
        if (Core.CheckInventory(Rewards))
            return;

        Core.AddDrop(Rewards);

        Farm.Experience(80);
        Farm.GoodREP();
        Taro.GuardianTaro();

        while (!Bot.ShouldExit() && !Core.CheckInventory(Rewards))
        {
            Core.EnsureAccept(8496);
            Nation.SwindleBulk(200);
            Nation.FarmDarkCrystalShard(125);
            Nation.FarmDiamondofNulgath(300);
            Nation.FarmGemofNulgath(75);
            Nation.FarmBloodGem(35);
            Core.EnsureCompleteChoose(8496, Rewards);
            Bot.Sleep(Core.ActionDelay);
        }
    }
}