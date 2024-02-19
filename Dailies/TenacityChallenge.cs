/*
name: Tenacity Challenge
description: Farms the quest Tenacity Challenge to get Tainted Gems, Dark Crystal Shards and Blood Gem of the Archfiend (requires Nulgath Challenge Pet)
tags: nation, tainted gem, dark crystal shard, blood gem of the archfiend, nulgath challenge pet, daily
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class TenacityChallenge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.TenacityChallenge();

        Core.SetOptions(false);
    }
}
