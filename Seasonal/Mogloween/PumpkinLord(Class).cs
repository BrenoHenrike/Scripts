/*
name: (Class) Pumpkin Lord
description: This script will get Pumpkin Lord class.
tags: class, mogloween, seasonal, pumpkin, pumpkin lord, pumpkinlord
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class PumpkinLord
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetClass(true);

        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = false)
    {
        if (!Core.isSeasonalMapActive("mogloween") || Core.CheckInventory("Pumpkin Lord", toInv: false))
            return;

        Core.EquipClass(ClassType.Solo);

        Core.HuntMonster("mogloween", "Great Pumpkin King", "Pumpkin Lord", isTemp: false);

        if (rankUpClass)
            Adv.RankUpClass("Pumpkin Lord");
    }
}
