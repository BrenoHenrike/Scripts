/*
name: ThiefOfHours
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class ThiefOfHours
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetToH();

        Core.SetOptions(false);
    }

    public void GetToH(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Thief of Hours"))
            return;

        Farm.ChronoSpanREP();

        Core.BuyItem("thespan", 439, "Thief of Hours");

        if (rankUpClass)
            Adv.RankUpClass("Thief of Hours");
    }
}
