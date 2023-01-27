/*
name:  HardCore Metals Daily
description:  HardCore Metals
tags: daily, hardcore metals, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class HardCoreMetals
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public List<IOption> Options = new()
    {
        new Option<HardCoreMetalsEnum>("metals", "Which Metal", "Select the metal you wish to get here")
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.HardCoreMetals(new[] { Bot.Config!.Get<HardCoreMetalsEnum>("metals").ToString() }, 10, false);

        Core.SetOptions(false);
    }
}
