/*
name: (Pax) Death Pit PvP
description: This will finish the Death Pit PvP story.
tags: death, pit, pvp, farm, story, pax, throne, darkness
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;

public class DeathPitPvP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TOD.DeathPitPVP();

        Core.SetOptions(false);
    }
}
