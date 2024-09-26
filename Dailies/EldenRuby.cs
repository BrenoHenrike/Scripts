/*
name: Elden Ruby
description: This script will complete the daily quest for Elden Ruby.
tags: daily, eldenruby,ruby, shadowrealm, blood-dyed stones, shadow realm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class EldenRuby
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.EldenRuby();

        Core.SetOptions(false);
    }
}
