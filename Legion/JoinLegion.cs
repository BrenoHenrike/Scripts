/*
name: Join the Legion
description: This script will join Dage's undead. It will also buy (with permission) the Undead Warrior. Option available to have the Undead Warrior be sold after joining the leigon.
tags: dage, legion, tokens, undead, warrior, bludrut, brawl, fail, king
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;

public class JointheLegion
{
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreLegion Legion = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Legion.JoinLegion();

        Core.SetOptions(false);
    }
}
