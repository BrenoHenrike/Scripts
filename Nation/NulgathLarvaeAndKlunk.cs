/*
name: NulgathLarvaeAndKlunk
description: will either do "Nulgath (Klunk) [2568]" or "Nulgath (Larvae) [2566]" depending on what is currently available.
tags: nulgath, larvae, klunk, nation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class NulgathLarvaeAndKlunk
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        Nation.NulgathLarvae();

        Core.SetOptions(false);
    }
}
