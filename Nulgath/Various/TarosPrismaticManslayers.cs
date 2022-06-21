//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nulgath/Various/TarosManslayer.cs
using RBot;
public class TarosPrismaticManslayers
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();
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
            Nulgath.SwindleBulk(200);
            Nulgath.FarmDarkCrystalShard(125);
            Nulgath.FarmDiamondofNulgath(300);
            Nulgath.FarmGemofNulgath(75);
            Nulgath.FarmBloodGem(35);
            Core.EnsureCompleteChoose(8496, Rewards);
            Bot.Sleep(Core.ActionDelay);
        }
    }
}