/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class BeastMaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBM();

        Core.SetOptions(false);
    }

    public void GetBM(bool rankUpClass = true)
    {
        if (Core.CheckInventory("BeastMaster"))
            return;

        Adv.BuyItem("northpointe", 976, "BeastMaster", shopItemID: 16031);

        if (rankUpClass)
            Adv.rankUpClass("BeastMaster");
    }
}
