/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class DarknessShard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDailies Dailies = new();

    public void ScriptMain(IScriptInterface bot)
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
            Dailies.CryptoToken();
        }
        Core.BuyItem("curio", 1539, "Darkness Shard");
    }
}
