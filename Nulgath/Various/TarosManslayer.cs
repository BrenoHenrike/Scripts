//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/Various/PurifiedClaymoreOfDestiny.cs
using RBot;
public class TarosManslayer
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();
    public PurifiedClaymoreOfDestiny PCoD = new PurifiedClaymoreOfDestiny();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GuardianTaro();

        Core.SetOptions(false);
    }

    private string[] Rewards = {"Taro's Manslayer", "Taro Blademaster Guardian"};

    public void GuardianTaro()
    {
        if (Core.CheckInventory(Rewards))
            return;

        Core.AddDrop(Rewards);

        Farm.GoodREP();
        PCoD.GetPCoD();

        while (!Core.CheckInventory(Rewards))
        {
            Core.EnsureAccept(1111);
            Nulgath.FarmGemofNulgath(10);
            Core.HuntMonster("tercessuinotlim", "Dark Makai", "Dark Makai Rune");
            Core.EnsureCompleteChoose(1111, Rewards);
            Bot.Sleep(Core.ActionDelay);
        }
    }
}