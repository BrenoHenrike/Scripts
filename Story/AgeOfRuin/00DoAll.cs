/*
name: Age Of Ruin (All)
description: This script completes the full Age Of Ruin saga.
tags: age, ruin, saga, story, quest, termina, temple, ashray, village, sunlight, twilight, zone, yulgar, aria, midnight, abyssal, deep_water, trench_observation, balemorale, castle_eblana, loughshine, naoise grave, naoisegrave, lia tara, liatara, hill, hill of lia tara, castle gaheris, castlegaheris, castle, gaheris
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
using Skua.Core.Interfaces;

public class AgeOfRuinAll
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAOR AOR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AOR.DoAll();
        Core.SetOptions(false);
    }
}
