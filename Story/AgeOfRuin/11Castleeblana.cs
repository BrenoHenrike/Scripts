/*
name: Castle eblana
description: This script completes the storyline in /castleeblana.
tags: age, of, ruin, saga, story, quest, castleeblana, castle, eblana, castle eblana
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
using Skua.Core.Interfaces;

public class Castleeblana
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAOR AOR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AOR.Castleeblana();

        Core.SetOptions(false);
    }
}
