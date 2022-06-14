//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using RBot;

public class DarknessShard
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDailies Dailies = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetShard();

        Core.SetOptions(false);
    }

    public void GetShard(int quant = 3)
    {
        if (Core.CheckInventory("Darkness Shard", quant))
            return;

        if (!Core.CheckInventory("Crypto Token", 3))
        {
            if (!Dailies.CheckDaily(6187))
            {
                Core.EnsureAccept(6187);
                Core.HuntMonster("boxes", "Sneevil", "Metal Ore");
                Core.EnsureComplete(6187);
            }
        }
        Core.BuyItem("curio", 1539, "Darkness Shard");
    }
}
