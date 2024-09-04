/*
name: Trygve
description: This script will complete the storyline in /trygve.
tags: hollowborn, saga, trygve, neofortress, lae
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs
using Skua.Core.Interfaces;

public class Trygve
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreHollowbornStory HB = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HB.Trygve();

        Core.SetOptions(false);
    }
}
