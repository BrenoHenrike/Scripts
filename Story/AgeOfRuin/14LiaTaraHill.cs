/*
name: The Hill of Lia Tara
description: This script completes the st oryline in /liatarahill.
tags: age, of, ruin, saga, story, quest, liatarahill, lia tara, liatara, hill, queen victoria alteon
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
using Skua.Core.Interfaces;

public class LiaTaraHill
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAOR AOR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AOR.LiaTaraHill();

        Core.SetOptions(false);
    }
}
