/*
path: Other/Classes/REP-based/Arachnomancer.cs
fileName: Arachnomancer.cs
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class Arachnomancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArach();

        Core.SetOptions(false);
    }

    public void GetArach(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Arachnomancer"))
            return;

        Farm.RavenlossREP();

        Core.BuyItem("ravenloss", 850, "Arachnomancer", shopItemID: 23292);

        if (rankUpClass)
            Adv.rankUpClass("Arachnomancer");
    }
}
