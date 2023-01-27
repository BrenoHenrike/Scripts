/*
name:  Treasure Chest Keys Daily
description:  reasure Chest Keys
tags: monthly, treasure chest keys, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class MonthlyTreasureChestKeys
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.MonthlyTreasureChestKeys();

        Core.SetOptions(false);
    }
}
