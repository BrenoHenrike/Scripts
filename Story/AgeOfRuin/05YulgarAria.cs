/*
name: Yulgar + Aria
description: This script completes the Yulgar's storyline in /sunlightzone.
tags: age, of, ruin, saga, story, quest, yulgar, aria, father, day, venomous, rose
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
using Skua.Core.Interfaces;

public class YulgarAria
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAOR AOR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AOR.YulgarAria();
        Core.SetOptions(false);
    }
}
