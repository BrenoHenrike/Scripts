/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
using Skua.Core.Interfaces;

public class YokaiSwordScroll
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreYnR YNR = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        YNR.YokaiSwordScroll();

        Core.SetOptions(false);
    }
}
