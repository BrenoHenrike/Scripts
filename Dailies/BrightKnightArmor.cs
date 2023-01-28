/*
name:  Bright Knight Armor Daily
description:  Bright Knight Armor
tags: daily, bright knight armor
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class BrightKnightArmor
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.BrightKnightArmor();

        Core.SetOptions(false);
    }
}
