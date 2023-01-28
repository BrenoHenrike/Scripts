/*
name:  Ballyhoo Ad Rewards Daily
description:  realy just gets you 200-500 gold.. not realy usefull
tags: ballyhoo, rewards, useless
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class BallyhooAdRewards
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.BallyhooAdRewards();

        Core.SetOptions(false);
    }
}
