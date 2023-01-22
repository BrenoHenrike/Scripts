/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class HorcEvader
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetHE();

        Core.SetOptions(false);
    }

    public void GetHE(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Horc Evader"))
            return;

        Adv.BuyItem("bloodtusk", 308, "Horc Evader");

        if (rankUpClass)
            Adv.rankUpClass("Horc Evader");
    }
}
